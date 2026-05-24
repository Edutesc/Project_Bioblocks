using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QuestionSystem;
using UnityEngine;

/// <summary>
/// Orquestra a sincronização de imagens entre Firebase Storage e cache local em disco.
///
/// Estratégia offline-first por tema:
///   • Primeiro acesso a uma imagem: tenta cache local; depois Firebase Storage.
///   • Cache válido: usa cache local.
///   • Sem internet + cache presente: usa cache local.
///   • Sem internet + sem cache: retorna null.
///
/// Prewarm:
///   PrewarmAsync agrupa as Question por topic e baixa os temas em ordem do enum
///   QuestionSet, com paralelismo limitado dentro de cada tema.
///
/// Observação importante:
///   Downloads podem ocorrer em paralelo, mas o ImageLocalRepository serializa
///   o acesso ao manifesto local do cache de imagens.
/// </summary>
public class ImageSyncService : MonoBehaviour, IImageSyncService
{
    [Tooltip("Limite de downloads paralelos dentro de um topic.")]
    [SerializeField] private int maxParallelDownloads = 4;

    [Tooltip("Quantidade máxima de imagens iniciadas por lote dentro de um topic.")]
    [SerializeField] private int prewarmBatchSize = 10;

    [Tooltip("Pausa entre lotes do prewarm, em milissegundos.")]
    [SerializeField] private int delayBetweenPrewarmBatchesMs = 750;

    private IFirebaseStorageImageRepository _storage;
    private IImageLocalRepository _local;
    private IAuthGate _authGate;

    private CancellationTokenSource _lifetimeCts;
    private CancellationTokenSource _prewarmCts;

    private readonly HashSet<string> _readyTopics = new HashSet<string>();

    // Cache negativo — keys que já falharam download nesta sessão.
    // Evita bater repetidamente no Storage para imagens ausentes.
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, byte> _knownMissingKeys
        = new System.Collections.Concurrent.ConcurrentDictionary<string, byte>();

    public bool IsSyncing { get; private set; }
    public bool IsCacheReady { get; private set; }
    public string LastError { get; private set; }

    // ── Injeção de dependências ────────────────────────────────────────────────

    public void InjectDependencies(
        IFirebaseStorageImageRepository storage,
        IImageLocalRepository local,
        IAuthGate authGate = null)
    {
        _storage = storage;
        _local = local;
        _authGate = authGate;

        ResetLifetimeCancellation();

        IsCacheReady = false;

        // Não bloqueia a inicialização.
        // Apenas tenta descobrir se já há cache local.
        _ = RefreshCacheReadyStateAsync();

        Debug.Log("[ImageSyncService] Dependências injetadas.");
    }

    private void OnDisable()
    {
        CancelPrewarm("serviço desabilitado");
    }

    private void OnDestroy()
    {
        CancelPrewarm("serviço destruído");
        DisposeCancellationSources();
    }

    private void OnApplicationQuit()
    {
        CancelPrewarm("aplicação encerrando");
    }

    private async Task RefreshCacheReadyStateAsync()
    {
        try
        {
            if (_local == null)
                return;

            DateTime? latestCache = await _local.GetLatestCacheTimestampAsync();

            IsCacheReady = latestCache != null;

            Debug.Log($"[ImageSyncService] Inicializado. IsCacheReady={IsCacheReady}");
        }
        catch (Exception e)
        {
            IsCacheReady = false;
            Debug.LogWarning($"[ImageSyncService] Não foi possível verificar cache inicial: {e.Message}");
        }
    }

    public bool IsTopicReady(string topic)
    {
        return !string.IsNullOrEmpty(topic) && _readyTopics.Contains(topic);
    }

    // ── Obtenção de uma imagem ─────────────────────────────────────────────────

    public async Task<Texture2D> GetImageAsync(
        string storageKey,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(storageKey))
        {
            LastError = "storageKey vazio.";
            return null;
        }

        try
        {
            ct.ThrowIfCancellationRequested();

            // 1. Cache local.
            // Importante: GetCachedTextureAsync cria Texture2D internamente.
            // Portanto, este método deve ser chamado a partir da main thread.
            Texture2D cachedTexture = await _local.GetCachedTextureAsync(storageKey, ct);

            if (cachedTexture != null)
            {
                LastError = null;
                IsCacheReady = true;
                return cachedTexture;
            }

            // 2. Cache negativo — já tentamos baixar nesta sessão e falhou.
            if (_knownMissingKeys.ContainsKey(storageKey))
                return null;

            // 3. Sem internet → desiste.
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                LastError = $"Sem internet e '{storageKey}' não está em cache.";
                Debug.LogWarning($"[ImageSyncService] {LastError}");
                return null;
            }

            // 4. Baixa do Firebase Storage.
            // Sem ConfigureAwait(false), porque depois tentamos carregar Texture2D,
            // que deve ocorrer na main thread.
            string topic = ExtractTopicFromKey(storageKey);

            byte[] bytes = await _storage.DownloadImageAsync(storageKey);

            ct.ThrowIfCancellationRequested();

            if (bytes == null)
            {
                _knownMissingKeys.TryAdd(storageKey, 0);
                LastError = $"Falha ao baixar '{storageKey}'.";
                return null;
            }

            // 5. Salva bytes no cache local.
            // A serialização do manifesto ocorre dentro do ImageLocalRepository.
            await _local.SaveAsync(storageKey, bytes, topic, ct);

            // 6. Carrega a textura recém-cacheada.
            Texture2D fetchedTexture = await _local.GetCachedTextureAsync(storageKey, ct);

            if (fetchedTexture != null)
            {
                LastError = null;
                IsCacheReady = true;
                return fetchedTexture;
            }

            LastError = $"Imagem '{storageKey}' foi baixada, mas não pôde ser carregada do cache.";
            return null;
        }
        catch (OperationCanceledException)
        {
            LastError = $"Operação cancelada para '{storageKey}'.";
            Debug.LogWarning($"[ImageSyncService] {LastError}");
            return null;
        }
        catch (Exception e)
        {
            LastError = e.Message;
            Debug.LogError($"[ImageSyncService] Erro ao obter '{storageKey}': {e}");
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

        if (questions == null)
            return;

        IsSyncing = true;
        LastError = null;

        using (var linkedCts = CreatePrewarmCancellation(ct))
        {
            CancellationToken prewarmToken = linkedCts.Token;

            try
            {
                if (_authGate != null)
                    await _authGate.WaitForAuthenticatedAsync(prewarmToken);

                prewarmToken.ThrowIfCancellationRequested();

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
                    prewarmToken.ThrowIfCancellationRequested();

                    string topic = topicGroup.Key;

                    var allKeys = topicGroup
                        .Select(j => j.key)
                        .Distinct()
                        .ToList();

                    var keysToDownload = new List<string>();

                    foreach (string key in allKeys)
                    {
                        prewarmToken.ThrowIfCancellationRequested();

                        if (_knownMissingKeys.ContainsKey(key))
                            continue;

                        bool hasLocal = await _local.HasAsync(key, prewarmToken);

                        if (!hasLocal)
                            keysToDownload.Add(key);
                    }

                    int alreadyCachedOrSkipped = allKeys.Count - keysToDownload.Count;

                    processed += alreadyCachedOrSkipped;
                    progress?.Report((float)processed / totalKeys);

                    if (keysToDownload.Count == 0)
                    {
                        _readyTopics.Add(topic);
                        IsCacheReady = true;
                        onTopicReady?.Invoke(topic);

                        Debug.Log($"[ImageSyncService] Topic '{topic}' já estava pronto.");
                        continue;
                    }

                    Debug.Log(
                        $"[ImageSyncService] Prewarm topic='{topic}': {keysToDownload.Count} imagens " +
                        $"(já em cache ou ignoradas: {alreadyCachedOrSkipped})."
                    );

                    int batchSize = Mathf.Max(1, prewarmBatchSize);
                    int totalBatches = Mathf.CeilToInt((float)keysToDownload.Count / batchSize);

                    for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
                    {
                        prewarmToken.ThrowIfCancellationRequested();

                        var batch = keysToDownload
                            .Skip(batchIndex * batchSize)
                            .Take(batchSize)
                            .ToList();

                        Debug.Log(
                            $"[ImageSyncService] Topic '{topic}' lote {batchIndex + 1}/{totalBatches}: " +
                            $"{batch.Count} imagens."
                        );

                        await DownloadAndCacheBatchAsync(batch, topic, prewarmToken);

                        processed += batch.Count;
                        progress?.Report((float)processed / totalKeys);

                        if (batchIndex < totalBatches - 1)
                            await DelayBetweenBatchesAsync(prewarmToken);
                    }

                    // Cleanup apenas ao final de cada topic.
                    // Evita rodar FindAll/Delete depois de cada imagem baixada.
                    await _local.CleanupOldCacheIfNeededAsync(prewarmToken);

                    _readyTopics.Add(topic);
                    IsCacheReady = true;
                    onTopicReady?.Invoke(topic);

                    Debug.Log(
                        $"[ImageSyncService] Topic '{topic}' pronto " +
                        $"({_readyTopics.Count}/{jobsByTopic.Count})."
                    );
                }

                progress?.Report(1f);
                LastError = null;
            }
            catch (OperationCanceledException)
            {
                LastError = "Prewarm cancelado.";
                Debug.LogWarning("[ImageSyncService] Prewarm cancelado.");
            }
            catch (Exception e)
            {
                LastError = e.Message;
                Debug.LogError($"[ImageSyncService] Erro durante prewarm: {e}");
            }
            finally
            {
                IsSyncing = false;
                ClearPrewarmCancellation(linkedCts);
            }
        }
    }

    // ── Helper de download/cache ───────────────────────────────────────────────

    private async Task DownloadAndCacheOneAsync(
        string key,
        string topic,
        SemaphoreSlim sem,
        CancellationToken ct)
    {
        await sem.WaitAsync(ct);

        try
        {
            ct.ThrowIfCancellationRequested();

            byte[] bytes = await _storage.DownloadImageAsync(key);

            ct.ThrowIfCancellationRequested();

            if (bytes == null)
            {
                _knownMissingKeys.TryAdd(key, 0);
                return;
            }

            await _local.SaveAsync(key, bytes, topic, ct);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            LastError = e.Message;

            Debug.LogError(
                $"[ImageSyncService] Erro ao baixar/cachear imagem '{key}' " +
                $"do topic '{topic}': {e.Message}"
            );
        }
        finally
        {
            sem.Release();
        }
    }

    private async Task DownloadAndCacheBatchAsync(
        List<string> keys,
        string topic,
        CancellationToken ct)
    {
        if (keys == null || keys.Count == 0)
            return;

        int parallelLimit = Mathf.Max(1, maxParallelDownloads);

        using (var sem = new SemaphoreSlim(parallelLimit))
        {
            var tasks = keys
                .Select(key => DownloadAndCacheOneAsync(key, topic, sem, ct))
                .ToList();

            await Task.WhenAll(tasks);
        }
    }

    private async Task DelayBetweenBatchesAsync(CancellationToken ct)
    {
        int delayMs = Mathf.Max(0, delayBetweenPrewarmBatchesMs);

        if (delayMs <= 0)
            return;

        await Task.Delay(delayMs, ct);
    }

    // ── Utilitários ────────────────────────────────────────────────────────────

    /// <summary>
    /// Recupera o topic a partir de uma storage key no formato "<topic>/<filename>".
    /// Retorna null se a key não tiver "/".
    /// </summary>
    private static string ExtractTopicFromKey(string storageKey)
    {
        if (string.IsNullOrEmpty(storageKey))
            return null;

        int slash = storageKey.IndexOf('/');

        return slash > 0
            ? storageKey.Substring(0, slash)
            : null;
    }

    /// <summary>
    /// Ordem do prewarm = ordem do enum QuestionSet.
    /// Topics não mapeados vão para o fim.
    /// </summary>
    private static int TopicOrder(string topic)
    {
        if (Enum.TryParse(topic, ignoreCase: false, out QuestionSet set))
            return (int)set;

        return int.MaxValue;
    }

    private CancellationTokenSource CreatePrewarmCancellation(CancellationToken callerToken)
    {
        CancelPrewarm("novo prewarm iniciado");
        ResetLifetimeCancellation();

        _prewarmCts = CancellationTokenSource.CreateLinkedTokenSource(
            _lifetimeCts.Token,
            callerToken
        );

        return _prewarmCts;
    }

    private void CancelPrewarm(string reason)
    {
        try
        {
            if (_prewarmCts != null && !_prewarmCts.IsCancellationRequested)
            {
                _prewarmCts.Cancel();
                Debug.Log($"[ImageSyncService] Prewarm cancelado: {reason}.");
            }

            if (_lifetimeCts != null && !_lifetimeCts.IsCancellationRequested)
                _lifetimeCts.Cancel();
        }
        catch (ObjectDisposedException)
        {
        }
    }

    private void ClearPrewarmCancellation(CancellationTokenSource completedCts)
    {
        if (ReferenceEquals(_prewarmCts, completedCts))
            _prewarmCts = null;
    }

    private void ResetLifetimeCancellation()
    {
        if (_lifetimeCts != null && !_lifetimeCts.IsCancellationRequested)
            return;

        _lifetimeCts?.Dispose();
        _lifetimeCts = new CancellationTokenSource();
    }

    private void DisposeCancellationSources()
    {
        if (!IsSyncing)
        {
            _prewarmCts?.Dispose();
            _prewarmCts = null;
        }

        if (!IsSyncing)
        {
            _lifetimeCts?.Dispose();
            _lifetimeCts = null;
        }
    }
}
