using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QuestionSystem;
using UnityEngine;

/// <summary>
/// Orquestra a sincronização de imagens entre Firebase Storage e cache local LiteDB.
///
/// Estratégia offline-first por tema:
///   • Primeiro acesso a uma imagem: tenta cache local; depois Firebase Storage.
///   • Cache válido (&lt; CACHE_EXPIRY_DAYS): usa LiteDB direto.
///   • Sem internet + cache presente: usa LiteDB.
///   • Sem internet + sem cache: retorna null (UI deve tratar — não há fallback Resources).
///
/// Prewarm:
///   PrewarmAsync agrupa as Question por topic e baixa os temas em ordem do enum
///   QuestionSet, com paralelismo limitado dentro de cada tema. Assim que um tema
///   termina, ele fica pronto para jogo offline mesmo enquanto os demais baixam.
/// </summary>
public class ImageSyncService : MonoBehaviour, IImageSyncService
{
    [Tooltip("Limite de downloads paralelos dentro de um topic.")]
    [SerializeField] private int maxParallelDownloads = 4;

    private IFirebaseStorageImageRepository _storage;
    private IImageLocalRepository _local;
    private IAuthGate _authGate;

    private readonly HashSet<string> _readyTopics = new HashSet<string>();

    // Cache negativo — keys que já falharam download nesta sessão.
    // Evita ficar batendo no Storage de novo (com retry de 8s) toda vez que
    // a UI re-pede uma imagem cujo arquivo não existe no bucket.
    // Thread-safe (escrito do prewarm em paralelo).
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, byte> _knownMissingKeys
        = new System.Collections.Concurrent.ConcurrentDictionary<string, byte>();

    public bool   IsSyncing    { get; private set; }
    public bool   IsCacheReady { get; private set; }
    public string LastError    { get; private set; }

    // ── Injeção de dependências ────────────────────────────────────────────────

    public void InjectDependencies(
        IFirebaseStorageImageRepository storage,
        IImageLocalRepository local,
        IAuthGate authGate = null)
    {
        _storage  = storage;
        _local    = local;
        _authGate = authGate;

        // Estado inicial: se já há QUALQUER imagem no cache, marcamos como ready
        // (modo offline pode aproveitar, e o prewarm só vai completar o que faltar).
        IsCacheReady = _local.GetLatestCacheTimestamp() != null;
        Debug.Log($"[ImageSyncService] Inicializado. IsCacheReady={IsCacheReady}");
    }

    public bool IsTopicReady(string topic)
    {
        return !string.IsNullOrEmpty(topic) && _readyTopics.Contains(topic);
    }

    // ── Obtenção de uma imagem ─────────────────────────────────────────────────

    public async Task<Texture2D> GetImageAsync(string storageKey, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(storageKey))
        {
            LastError = "storageKey vazio.";
            return null;
        }

        try
        {
            // 1. Cache local
            if (_local.TryGetCachedTexture(storageKey, out var cachedTexture))
                return cachedTexture;

            // 2. Cache negativo — já tentamos e falhou nessa sessão
            if (_knownMissingKeys.ContainsKey(storageKey))
                return null;

            // 3. Sem internet → desiste
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                LastError = $"Sem internet e '{storageKey}' não está em cache.";
                Debug.LogWarning($"[ImageSyncService] {LastError}");
                return null;
            }

            // 4. Baixa do Firebase Storage (on-demand). Sem ConfigureAwait(false)
            //    para preservar o SynchronizationContext da main thread —
            //    TryGetCachedTexture mais abaixo cria Texture2D, que só funciona
            //    da main thread.
            string topic = ExtractTopicFromKey(storageKey);
            byte[] bytes = await _storage.DownloadImageAsync(storageKey);
            if (bytes == null)
            {
                _knownMissingKeys.TryAdd(storageKey, 0);  // não tenta de novo
                LastError = $"Falha ao baixar '{storageKey}'.";
                return null;
            }

            // 4. Salva no cache (thread-safe, só bytes) e devolve a textura recém-cacheada.
            _local.Save(storageKey, bytes, topic);

            if (_local.TryGetCachedTexture(storageKey, out var fetchedTexture))
            {
                LastError = null;
                IsCacheReady = true;
                return fetchedTexture;
            }

            return null;
        }
        catch (Exception e)
        {
            LastError = e.Message;
            Debug.LogError($"[ImageSyncService] Erro ao obter '{storageKey}': {e.Message}");
            return null;
        }
    }

    // ── Prewarm ordenado por topic ─────────────────────────────────────────────
    public async Task PrewarmAsync(
    IEnumerable<Question> questions,
    IProgress<float> progress = null,
    Action<string> onTopicReady = null,
    CancellationToken ct = default)
    {
        if (IsSyncing)
        {
            Debug.LogWarning("[ImageSyncService] Prewarm já em andamento — ignorando nova chamada.");
            return;
        }

        if (questions == null) return;

        IsSyncing = true;
        LastError = null;

        try
        {
            if (_authGate != null)
                await _authGate.WaitForAuthenticatedAsync(ct);

            ct.ThrowIfCancellationRequested();

            var jobsByTopic = questions
                .Where(q => q != null)
                .SelectMany(q => QuestionStorageKeys.AllForQuestion(q)
                    .Select(k => (key: k, topic: q.topic)))
                .Where(j => !string.IsNullOrEmpty(j.key) &&
                            !string.IsNullOrEmpty(j.topic))
                .GroupBy(j => j.topic)
                .OrderBy(g => TopicOrder(g.Key))
                .ToList();

            int totalKeys = jobsByTopic.Sum(g => g.Select(j => j.key).Distinct().Count());

            if (totalKeys == 0)
            {
                progress?.Report(1f);
                IsCacheReady = true;
                return;
            }

            int processed = 0;

            foreach (var topicGroup in jobsByTopic)
            {
                ct.ThrowIfCancellationRequested();

                string topic = topicGroup.Key;

                var allKeys = topicGroup
                    .Select(j => j.key)
                    .Distinct()
                    .ToList();

                var keysToDownload = allKeys
                    .Where(k => !_local.Has(k))
                    .Where(k => !_knownMissingKeys.ContainsKey(k))
                    .ToList();

                int alreadyCached = allKeys.Count - keysToDownload.Count;
                processed += alreadyCached;
                progress?.Report((float)processed / totalKeys);

                if (keysToDownload.Count == 0)
                {
                    _readyTopics.Add(topic);
                    IsCacheReady = true;
                    onTopicReady?.Invoke(topic);
                    continue;
                }

                Debug.Log(
                    $"[ImageSyncService] Prewarm topic='{topic}': {keysToDownload.Count} imagens " +
                    $"(já em cache: {alreadyCached})."
                );

                using (var sem = new SemaphoreSlim(maxParallelDownloads))
                {
                    var tasks = keysToDownload
                        .Select(key => DownloadAndCacheOneAsync(key, topic, sem, ct))
                        .ToList();

                    foreach (var task in tasks)
                    {
                        await task;

                        processed++;
                        progress?.Report((float)processed / totalKeys);
                    }
                }

                _readyTopics.Add(topic);
                IsCacheReady = true;
                onTopicReady?.Invoke(topic);

                Debug.Log(
                    $"[ImageSyncService] Topic '{topic}' pronto " +
                    $"({_readyTopics.Count}/{jobsByTopic.Count})."
                );
            }
        }
        catch (OperationCanceledException)
        {
            Debug.LogWarning("[ImageSyncService] Prewarm cancelado.");
        }
        catch (Exception e)
        {
            LastError = e.Message;
            Debug.LogError($"[ImageSyncService] Erro durante prewarm: {e.Message}");
        }
        finally
        {
            IsSyncing = false;
        }
    }

    // -- Helper -------------------------------
    private async Task DownloadAndCacheOneAsync(
    string key,
    string topic,
    SemaphoreSlim sem,
    CancellationToken ct)
    {
        await sem.WaitAsync(ct).ConfigureAwait(false);

        try
        {
            ct.ThrowIfCancellationRequested();
            byte[] bytes = await _storage.DownloadImageAsync(key).ConfigureAwait(false);

            if (bytes != null)
            {
                _local.Save(key, bytes, topic);
            }
            else
            {
                _knownMissingKeys.TryAdd(key, 0);
            }
        }
        finally
        {
            sem.Release();
        }
    }

    // ── Utilitários ────────────────────────────────────────────────────────────

    /// <summary>
    /// Recupera o topic a partir de uma storage key no formato "&lt;topic&gt;/&lt;filename&gt;".
    /// Retorna null se a key não tiver "/".
    /// </summary>
    private static string ExtractTopicFromKey(string storageKey)
    {
        if (string.IsNullOrEmpty(storageKey)) return null;
        int slash = storageKey.IndexOf('/');
        return slash > 0 ? storageKey.Substring(0, slash) : null;
    }

    /// <summary>
    /// Ordem do prewarm = ordem do enum QuestionSet. Topics não mapeados vão pro fim.
    /// </summary>
    private static int TopicOrder(string topic)
    {
        if (Enum.TryParse(topic, ignoreCase: false, out QuestionSet set))
            return (int)set;
        return int.MaxValue;
    }
}
