using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using QuestionSystem;

/// <summary>
/// Serviço que orquestra a sincronização de questões entre o Firestore e o cache LiteDB.
///
/// Estratégia offline-first:
///   - Primeira abertura (sem cache) → baixa tudo do Firestore → salva no LiteDB
///   - Cache válido (< cacheDaysThreshold)  → usa LiteDB diretamente, sem rede
///   - Cache expirado                       → baixa do Firestore em background e atualiza LiteDB
///   - Sem internet + cache presente        → usa LiteDB (modo offline transparente)
///   - Sem internet + sem cache             → IsCacheReady = false (app deve tratar este estado)
///
/// Após popular o cache de questões, dispara o prewarm das imagens via
/// IImageSyncService (eager, ordenado por topic). O prewarm é não-bloqueante:
/// InitializeAsync retorna assim que as questões estão prontas e as imagens
/// continuam baixando em background.
/// </summary>
public class QuestionSyncService : MonoBehaviour, IQuestionSyncService
{
    [Tooltip("Número de dias antes de considerar o cache de questões desatualizado.")]
    [SerializeField] private float cacheDaysThreshold = 7f;

    private IFirestoreQuestionRepository _firestore;
    private IQuestionLocalRepository     _local;
    private IImageSyncService            _imageSync;

    private bool _authListenerRegistered;

    public bool IsSyncing    { get; private set; }
    public bool IsCacheReady { get; private set; }

    // ── Injeção de dependências ────────────────────────────────────────────────

    public void InjectDependencies(
        IFirestoreQuestionRepository firestore,
        IQuestionLocalRepository     local,
        IImageSyncService            imageSync = null)
    {
        _firestore = firestore;
        _local     = local;
        _imageSync = imageSync;
    }

    public void RegisterAuthListener()
    {
        if (_authListenerRegistered)
            return;

        if (_firestore == null || _local == null)
        {
            Debug.LogWarning("[QuestionSyncService] Dependências ainda não injetadas. Listener não registrado.");
            return;
        }

        Firebase.Auth.FirebaseAuth.DefaultInstance.StateChanged += OnAuthStateChanged;
        _authListenerRegistered = true;

        Debug.Log("[QuestionSyncService] Auth listener registrado após injeção de dependências.");
    }

    private async void OnAuthStateChanged(object sender, EventArgs e)
    {
        var auth = sender as Firebase.Auth.FirebaseAuth ?? Firebase.Auth.FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser == null) return;                                      // logout → nada a fazer
        if (IsSyncing)                return;                                      // já está sincronizando
        if (IsCacheReady && _local.HasAnyQuestions() && !IsCacheStale()) return;   // já temos cache válido

        Debug.Log("[QuestionSyncService] Auth state mudou para autenticado — disparando re-sync.");
        await InitializeAsync();
    }

    private void OnDestroy()
    {
        if (_authListenerRegistered)
        {
            try { Firebase.Auth.FirebaseAuth.DefaultInstance.StateChanged -= OnAuthStateChanged; }
            catch { /* SDK pode já estar desligado */ }
        }
    }

    // ── Inicialização ──────────────────────────────────────────────────────────

    public async Task<bool> InitializeAsync()
    {
        if (_firestore == null || _local == null)
        {
            Debug.LogWarning("[QuestionSyncService] InitializeAsync chamado antes da injeção de dependências.");
            IsCacheReady = false;
            return false;
        }

        if (IsSyncing) return IsCacheReady;
        IsSyncing = true;

        try
        {
            bool hasCache = _local.HasAnyQuestions();

            if (!hasCache)
            {
                // ── Primeira abertura: sem cache local ────────────────────────
                Debug.Log("[QuestionSyncService] Sem cache local — baixando questões do Firestore...");
                long remoteVersion = await FetchRemoteVersionSafe();
                bool success = await DownloadAndCacheAll(remoteVersion);
                IsCacheReady = success;
                return IsCacheReady;
            }

            // ── Cache existe: checar versão remota como invalidação primária ──
            long remote = await FetchRemoteVersionSafe();

            if (remote != -1L && remote != _local.GetCachedVersion())
            {
                // Versão mudou (novo upload de questões) → refresh em background
                Debug.Log($"[QuestionSyncService] Nova versão remota ({remote}) — atualizando cache em background...");
                IsCacheReady = true;   // usa o cache antigo enquanto atualiza
                _ = RefreshCacheInBackground(remote);
            }
            else if (remote == -1L && IsCacheStale())
            {
                // Sem internet e cache TTL expirado → refresh assim que tiver conexão;
                // por ora, usa o cache antigo (melhor do que nada).
                Debug.Log("[QuestionSyncService] Sem acesso ao Firestore e cache expirado — usando cache antigo como fallback.");
                IsCacheReady = true;
            }
            else
            {
                Debug.Log("[QuestionSyncService] Cache válido e atualizado — usando LiteDB diretamente.");
                IsCacheReady = true;

                // Mesmo com cache de questões válido, dispara o prewarm de imagens.
                // É barato: o ImageSyncService pula imagens que já estão em cache.
                _ = PrewarmImagesAsync(_local.GetAllQuestions());
            }

            return IsCacheReady;
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionSyncService] Erro na inicialização: {e.Message}");

            IsCacheReady = _local.HasAnyQuestions();
            if (IsCacheReady)
                Debug.LogWarning("[QuestionSyncService] Usando cache antigo como fallback.");

            return IsCacheReady;
        }
        finally
        {
            IsSyncing = false;
        }
    }

    /// <summary>
    /// Busca a versão remota sem lançar exceção — retorna -1 em qualquer falha.
    /// Isso garante que erros de rede não interrompam o fluxo de inicialização.
    /// </summary>
    private async Task<long> FetchRemoteVersionSafe()
    {
        try
        {
            long remoteVersion = await _firestore.GetRemoteVersion();
            return remoteVersion;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[QuestionSyncService] Não foi possível buscar versão remota: {e.Message}");
            return -1L;
        }
    }

    // ── Leitura (síncrona, chamada pelos IQuestionDatabase) ────────────────────

    public List<Question> GetQuestionsForDatabankName(string databankName)
    {
        if (!IsCacheReady)
        {
            Debug.LogError("[QuestionSyncService] Cache não está pronto. InitializeAsync() deve ser concluído primeiro.");
            return new List<Question>();
        }

        var questions = _local.GetQuestionsByDatabankName(databankName);
        Debug.Log($"[QuestionSyncService] {questions.Count} questões carregadas do LiteDB para '{databankName}'.");
        return questions;
    }

    // ── Sincronização ──────────────────────────────────────────────────────────

    /// <summary>
    /// Baixa todas as questões do Firestore, salva no LiteDB, persiste a versão
    /// e dispara prewarm de imagens.
    /// </summary>
    private async Task<bool> DownloadAndCacheAll(long remoteVersion = -1L)
    {
        try
        {
            List<Question> questions = await _firestore.GetAllQuestions();

            if (questions == null || questions.Count == 0)
            {
                Debug.LogWarning("[QuestionSyncService] Firestore retornou lista vazia de questões.");
                return false;
            }

            _local.SaveQuestions(questions);

            try
            {
                if (remoteVersion != -1L)
                {
                    _local.SaveCachedVersion(remoteVersion);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[QuestionSyncService] Questões salvas, mas falha ao salvar versão do cache: {e.Message}");
            }
            
            Debug.Log($"[QuestionSyncService] {questions.Count} questões cacheadas no LiteDB (versão {remoteVersion}).");

            _ = PrewarmImagesAsync(questions);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionSyncService] Falha ao baixar questões do Firestore: {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Atualização em background das questões: limpa cache antigo, salva novas,
    /// persiste a nova versão e dispara prewarm das imagens.
    /// </summary>
    private async Task RefreshCacheInBackground(long newVersion = -1L)
    {
        IsSyncing = true;

        try
        {
            List<Question> questions = await _firestore.GetAllQuestions();

            if (questions == null || questions.Count == 0)
            {
                Debug.LogWarning("[QuestionSyncService] Refresh em background retornou lista vazia — cache antigo mantido.");
                return;
            }

            _local.ClearAll();
            _local.SaveQuestions(questions);

            if (newVersion != -1L)
            {
                _local.SaveCachedVersion(newVersion);
            }

            Debug.Log($"[QuestionSyncService] Cache atualizado em background com {questions.Count} questões (versão {newVersion}).");

            _ = PrewarmImagesAsync(questions);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[QuestionSyncService] Refresh em background falhou (usando cache antigo): {e.Message}");
        }
        finally
        {
            IsSyncing = false;
        }
    }

    private async Task PrewarmImagesAsync(IEnumerable<Question> questions)
    {
        if (_imageSync == null)
        {
            Debug.Log("[QuestionSyncService] ImageSyncService não injetado — pulando prewarm de imagens.");
            return;
        }

        try
        {
            await _imageSync.PrewarmAsync(
                questions,
                progress: null,
                onTopicReady: topic => Debug.Log($"[QuestionSyncService] Topic '{topic}' pronto para jogo offline."),
                ct: default);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[QuestionSyncService] Prewarm de imagens falhou: {e.Message}");
        }
    }

    // ── Utilitários ────────────────────────────────────────────────────────────

    private bool IsCacheStale()
    {
        DateTime latestCache = _local.GetLatestCacheTimestamp();
        if (latestCache == DateTime.MinValue) return true;

        double daysSinceCache = (DateTime.Now - latestCache).TotalDays;
        return daysSinceCache > cacheDaysThreshold;
    }
}
