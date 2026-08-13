using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using QuestionSystem;

/// <summary>
/// Serviço que orquestra a sincronização de questões entre o Firestore e o cache LiteDB.
///
/// Estratégia offline-first:
///   - Primeira abertura (sem cache)       → baixa tudo do Firestore → salva no LiteDB
///   - Cache válido                        → usa LiteDB diretamente
///   - Versão remota diferente             → usa o cache atual e atualiza em background
///   - Sem internet + cache presente       → usa LiteDB como fallback
///   - Sem internet + sem cache            → IsCacheReady = false
///
/// O LiteDB não é acessado diretamente aqui. Todo acesso passa por
/// IQuestionLocalRepository, que por sua vez deve passar por ILiteDBManager.
/// </summary>
public class QuestionSyncService : MonoBehaviour, IQuestionSyncService
{
    [Tooltip("Número de dias antes de considerar o cache de questões desatualizado.")]
    [SerializeField] private float cacheDaysThreshold = 7f;

    private IFirestoreQuestionRepository _firestore;
    private IQuestionLocalRepository     _local;
    private IImageSyncService            _imageSync;
    private IAuthGate                    _authGate;

    private bool _authListenerRegistered;

    // Evita que duas chamadas simultâneas de InitializeAsync baixem as mesmas
    // questões em paralelo ou retornem IsCacheReady=false enquanto a primeira
    // inicialização ainda está em andamento.
    private readonly object _initializeTaskGate = new object();
    private Task<bool> _initializeTask;

    // Evita refreshes em background duplicados.
    private readonly object _refreshTaskGate = new object();
    private Task _refreshTask;

    // Mantém IsSyncing correto mesmo quando InitializeAsync dispara um refresh
    // em background antes de terminar.
    private readonly object _syncStateGate = new object();
    private int _activeSyncOperations;

    public bool IsSyncing    { get; private set; }
    public bool IsCacheReady { get; private set; }

    // ── Injeção de dependências ────────────────────────────────────────────────

    public void InjectDependencies(
        IFirestoreQuestionRepository firestore,
        IQuestionLocalRepository     local,
        IImageSyncService            imageSync = null,
        IAuthGate                    authGate = null)
    {
        _firestore = firestore;
        _local     = local;
        _imageSync = imageSync;
        _authGate  = authGate;
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

        if (auth.CurrentUser == null)
            return;

        try
        {
            if (_authGate != null)
                await _authGate.WaitForAuthenticatedAsync();
        }
        catch (Exception authError)
        {
            Debug.LogWarning($"[QuestionSyncService] Auth ainda não está pronta; sync adiado: {authError.Message}");
            return;
        }

        // Se já existe cache válido, não precisamos disparar outra sincronização.
        // A chamada a _local passa pelo LiteDBManager e será serializada com as
        // demais operações do LiteDB.
        if (IsCacheReady && _local.HasAnyQuestions() && !IsCacheStale())
            return;

        Debug.Log("[QuestionSyncService] Auth state mudou para autenticado — verificando sincronização de questões.");
        await InitializeAsync();
    }

    private void OnDestroy()
    {
        if (_authListenerRegistered)
        {
            try
            {
                Firebase.Auth.FirebaseAuth.DefaultInstance.StateChanged -= OnAuthStateChanged;
            }
            catch
            {
                // O SDK pode já ter sido desligado durante o encerramento do app.
            }
        }
    }

    // ── Inicialização ──────────────────────────────────────────────────────────

    public Task<bool> InitializeAsync()
    {
        if (_firestore == null || _local == null)
        {
            Debug.LogWarning("[QuestionSyncService] InitializeAsync chamado antes da injeção de dependências.");
            IsCacheReady = false;
            return Task.FromResult(false);
        }

        lock (_initializeTaskGate)
        {
            if (_initializeTask != null && !_initializeTask.IsCompleted)
                return _initializeTask;

            _initializeTask = InitializeCoreAsync();
            return _initializeTask;
        }
    }

    private async Task<bool> InitializeCoreAsync()
    {
        BeginSyncOperation();

        try
        {
            bool hasCache = _local.HasAnyQuestions();

            if (_authGate != null)
            {
                try
                {
                    await _authGate.WaitForAuthenticatedAsync();
                }
                catch (Exception authError)
                {
                    IsCacheReady = hasCache;
                    Debug.LogWarning(
                        $"[QuestionSyncService] Sessão remota indisponível; " +
                        $"usando cache local={hasCache}: {authError.Message}"
                    );
                    return IsCacheReady;
                }
            }

            if (!hasCache)
            {
                // Primeira abertura: sem cache local.
                // Aqui não é background: o app precisa das questões antes de liberar
                // o fluxo normal.
                Debug.Log("[QuestionSyncService] Sem cache local — baixando questões do Firestore...");

                long remoteVersion = await FetchRemoteVersionSafe();
                bool success = await DownloadAndCacheAll(remoteVersion);

                IsCacheReady = success;
                return IsCacheReady;
            }

            // Cache existe: checar versão remota como invalidação primária.
            long remoteVersionFromServer = await FetchRemoteVersionSafe();
            long localVersion            = _local.GetCachedVersion();

            if (remoteVersionFromServer != -1L && remoteVersionFromServer != localVersion)
            {
                // Versão mudou: mantém cache antigo disponível e atualiza em background.
                Debug.Log($"[QuestionSyncService] Nova versão remota ({remoteVersionFromServer}) detectada; versão local atual: {localVersion}. Atualizando cache em background...");

                IsCacheReady = true;
                StartRefreshCacheInBackground(remoteVersionFromServer);
            }
            else if (remoteVersionFromServer == -1L && IsCacheStale())
            {
                // Sem acesso ao Firestore e TTL expirado: ainda assim é melhor usar
                // o cache existente do que bloquear o app.
                Debug.Log("[QuestionSyncService] Sem acesso ao Firestore e cache expirado — usando cache antigo como fallback.");

                IsCacheReady = true;
            }
            else
            {
                Debug.Log("[QuestionSyncService] Cache válido e atualizado — usando LiteDB diretamente.");

                IsCacheReady = true;

                // Mesmo com cache de questões válido, dispara o prewarm de imagens.
                // O ImageSyncService deve pular imagens já cacheadas.
                _ = PrewarmImagesAsync(_local.GetAllQuestions());
            }

            return IsCacheReady;
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionSyncService] Erro na inicialização: {e.Message}");

            IsCacheReady = SafeHasAnyQuestions();

            if (IsCacheReady)
                Debug.LogWarning("[QuestionSyncService] Usando cache antigo como fallback.");

            return IsCacheReady;
        }
        finally
        {
            EndSyncOperation();
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

    // ── Leitura síncrona, chamada pelos IQuestionDatabase ──────────────────────

    public List<Question> GetQuestionsForDatabankName(string databankName)
    {
        if (!IsCacheReady)
        {
            Debug.LogError("[QuestionSyncService] Cache não está pronto. InitializeAsync() deve ser concluído primeiro.");
            return new List<Question>();
        }

        List<Question> questions = _local.GetQuestionsByDatabankName(databankName);

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
            SaveVersionSafe(remoteVersion);

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

    private void StartRefreshCacheInBackground(long newVersion = -1L)
    {
        lock (_refreshTaskGate)
        {
            if (_refreshTask != null && !_refreshTask.IsCompleted)
            {
                Debug.Log("[QuestionSyncService] Refresh de questões já está em andamento — nova solicitação ignorada.");
                return;
            }

            _refreshTask = RefreshCacheInBackground(newVersion);
        }
    }

    /// <summary>
    /// Atualização em background das questões.
    /// 
    /// Preferencialmente usa ReplaceAllQuestions quando o repositório concreto
    /// expõe esse método, pois isso troca o cache em uma única transação. Se a
    /// interface ainda não tiver sido atualizada, mantém fallback compatível:
    /// ClearAll() + SaveQuestions().
    /// </summary>
    private async Task RefreshCacheInBackground(long newVersion = -1L)
    {
        BeginSyncOperation();

        try
        {
            List<Question> questions = await _firestore.GetAllQuestions();

            if (questions == null || questions.Count == 0)
            {
                Debug.LogWarning("[QuestionSyncService] Refresh em background retornou lista vazia — cache antigo mantido.");
                return;
            }

            ReplaceLocalQuestionCache(questions);
            SaveVersionSafe(newVersion);

            Debug.Log($"[QuestionSyncService] Cache atualizado em background com {questions.Count} questões (versão {newVersion}).");

            _ = PrewarmImagesAsync(questions);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[QuestionSyncService] Refresh em background falhou; usando cache antigo: {e.Message}");
        }
        finally
        {
            EndSyncOperation();
        }
    }

    private void ReplaceLocalQuestionCache(List<Question> questions)
    {
        _local.ReplaceAllQuestions(questions);
    }

    private void SaveVersionSafe(long version)
    {
        if (version == -1L)
            return;

        try
        {
            _local.SaveCachedVersion(version);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[QuestionSyncService] Questões salvas, mas falha ao salvar versão do cache: {e.Message}");
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

        if (latestCache == DateTime.MinValue)
            return true;

        DateTime latestUtc = latestCache.Kind == DateTimeKind.Utc
            ? latestCache
            : latestCache.ToUniversalTime();

        double daysSinceCache = (DateTime.UtcNow - latestUtc).TotalDays;
        return daysSinceCache > cacheDaysThreshold;
    }

    private bool SafeHasAnyQuestions()
    {
        try
        {
            return _local.HasAnyQuestions();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[QuestionSyncService] Falha ao verificar cache local: {e.Message}");
            return false;
        }
    }

    private void BeginSyncOperation()
    {
        lock (_syncStateGate)
        {
            _activeSyncOperations++;
            IsSyncing = true;
        }
    }

    private void EndSyncOperation()
    {
        lock (_syncStateGate)
        {
            _activeSyncOperations = Math.Max(0, _activeSyncOperations - 1);
            IsSyncing = _activeSyncOperations > 0;
        }
    }
}
