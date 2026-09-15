using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class InitializationManager : MonoBehaviour
{
    // Em cold starts (principalmente depois de o SO suspender/encerrar o app),
    // o SDK nativo pode publicar CurrentUser alguns segundos depois de
    // FirebaseAuth.DefaultInstance estar disponível. Não devemos interpretar
    // esse intervalo como logout.
    private const int FirebaseSessionRestoreTimeoutMillis = 10000;

    [Header("Configuration")]
    [SerializeField] private float minimumLoadingTime = 2.0f;

[   Header("Initialization Loading Spinner")]
    [SerializeField] private LoadingSpinnerComponent loadingSpinner;

    private IFirestoreRepository _firestore;
    private IAuthRepository _auth;
    private IUserDataSyncService _userDataSync;
    private IUserDataLocalRepository _userDataLocal;
    private bool _usingLocalSessionFallback;

    private void Awake()
    {
        if (loadingSpinner == null)
        {
            Debug.LogError("[InitializationManager] LoadingSpinnerComponent não foi vinculado no Inspector.");
            return;
        }

        loadingSpinner.ShowSpinner();
        loadingSpinner.SetMessage("Inicializando...");
    }

    private void Start()
    {
        StartInitialization();
    }

    private async void StartInitialization()
    {
        float startTime = Time.time;
        Debug.Log("[InitManager] StartInitialization começou");

        // ── Preview Mode — bypassa auth e dados de usuário ────────────────────
        var envCfg = EnvironmentConfig.Load();
        if (envCfg != null && envCfg.QuestionPreviewMode)
        {
            Debug.Log("[InitManager] questionPreviewMode=true — pulando auth e dados de usuário.");
            UpdateStatus("Modo preview ativo...");
            await WaitForAppContext();

            float elapsedPreview = Time.time - startTime;
            if (elapsedPreview < minimumLoadingTime)
                await Task.Delay(Mathf.RoundToInt((minimumLoadingTime - elapsedPreview) * 1000));

            loadingSpinner?.HideSpinner();
            SceneManager.LoadScene("PathwayScene", LoadSceneMode.Single);
            return;
        }

        try
        {
            if (!AppContext.IsReady)
            {
                Debug.Log("[InitManager] Aguardando AppContext...");
                UpdateStatus("Inicializando Firebase...");
                await WaitForAppContext();
                Debug.Log("[InitManager] AppContext pronto");
            }
            else
            {
                Debug.Log("[InitManager] AppContext já estava pronto");
            }

            _firestore     = AppContext.Firestore;
            _auth          = AppContext.Auth;
            _userDataSync  = AppContext.UserDataSync;
            _userDataLocal = AppContext.UserDataLocal;

            if (_auth == null)
                throw new Exception("[InitManager] AppContext.Auth está null após AppContext.IsReady=true.");

            bool isAuthenticated = await CheckAuthentication();
            // ── Usuário não autenticado: ir imediatamente para LoginView ─────────
            if (!isAuthenticated)
            {
                UpdateStatus("Redirecionando para login...");
                Debug.Log("[InitManager] Usuário não autenticado. Navegando imediatamente para LoginView.");

                try
                {
                    loadingSpinner?.HideSpinner();
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"[InitManager] Erro ao esconder spinner antes da LoginView: {e.Message}");
                }

                NavigateAfterInit(false);
                return;
            }

            // ── A partir daqui, só continua se estiver autenticado ────────────────
            bool userDataLoaded = false;

            UpdateStatus("Carregando dados do usuário...");
            Debug.Log("[InitManager] Carregando dados...");

            userDataLoaded = await LoadUserData();

            if (userDataLoaded)
            {
                UpdateStatus("Carregando bancos de questões...");

                if (AppContext.QuestionSync != null)
                {
                    bool questionsReady = await AppContext.QuestionSync.InitializeAsync();

                    if (!questionsReady)
                    {
                        Debug.LogWarning("[InitManager] Questões indisponíveis após inicialização.");

                        // Aqui você decide a política:
                        // 1) permitir seguir mesmo assim, se houver cache parcial;
                        // 2) mostrar tela de erro/retry;
                        // 3) voltar para LoginView.
                    }
                }
                else
                {
                    Debug.LogError("[InitManager] AppContext.QuestionSync está null.");
                }

                var statsManager = AppContext.Statistics as DatabaseStatisticsManager;
                if (_usingLocalSessionFallback)
                {
                    // Não segura a entrada offline por uma leitura remota de stats.
                    // A PathwayScene inicializa esse serviço em coroutine e a
                    // implementação já possui fallback para o cache de questões.
                    Debug.Log("[InitManager] Sessão offline — estatísticas remotas adiadas.");
                }
                else if (statsManager != null)
                {
                    await statsManager.Initialize();
                }
                else
                {
                    Debug.LogWarning("[InitManager] AppContext.Statistics não é DatabaseStatisticsManager ou está null.");
                }

                UpdateStatus("Configurando sistema de níveis...");
                InitializePlayerLevelService();
            }
            else
            {
                Debug.LogWarning("[InitManager] Usuário autenticado, mas dados do usuário não foram carregados.");
                await GoToLoginPreservingLocalSessionAsync("Usuário autenticado, mas documento/UserData não encontrado.");
                return;
            }

            float elapsed = Time.time - startTime;
            if (elapsed < minimumLoadingTime)
                await Task.Delay(Mathf.RoundToInt((minimumLoadingTime - elapsed) * 1000));

            StartRemoteSessionRefreshInBackgroundIfNeeded();
            NavigateAfterInit(userDataLoaded);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[InitializationManager] Falha: {ex.GetType().Name}: {ex.Message}");
            Debug.LogError($"[InitializationManager] StackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
                Debug.LogError($"[InitializationManager] InnerException: {ex.InnerException.Message}");

            try
            {
                loadingSpinner?.HideSpinner();
            }
            catch { }

            ShowError($"Falha na inicialização: {ex.Message}");
        }
    }

    private async Task WaitForAppContext()
    {
        float timeout = 15f;
        float elapsed = 0f;

        while (!AppContext.IsReady && elapsed < timeout)
        {
            await Task.Delay(100);
            elapsed += 0.1f;

            if (Mathf.RoundToInt(elapsed * 10) % 30 == 0)
                Debug.Log($"[InitManager] Aguardando AppContext... {elapsed:F1}s");
        }

        if (!AppContext.IsReady)
            throw new Exception("Sem conexão. Verifique sua internet e tente novamente.");
    }

    private async Task<bool> CheckAuthentication()
    {
        // Offline-first: se há uma sessão local explicitamente ativa e seu
        // UserData está no LiteDB, libera o app imediatamente. A identidade
        // Firebase será restaurada e validada em background antes de sincronizar.
        if (TryLoadOfflineUserFromCache("Sessão local disponível no bootstrap."))
            return true;

        if (!HasFirebaseCurrentUser())
        {
            UpdateStatus("Restaurando sua sessão...");
            Debug.Log("[InitializationManager] Firebase ainda não publicou CurrentUser. Aguardando restauração da sessão persistida.");

            bool restored = await WaitForLocalSessionRestore(FirebaseSessionRestoreTimeoutMillis);
            if (!restored)
                return TryLoadOfflineUserFromCache("Sem sessão Firebase restaurada.");

            Debug.Log("[InitializationManager] Sessão Firebase persistida restaurada.");
        }

        try
        {
            await WithTimeout(_auth.CheckAuthenticationStatus(), 8000, "Validação da sessão Firebase excedeu o tempo limite.");
            Debug.Log("[InitializationManager] Sessão validada com o servidor.");
            return true;
        }
        catch (FirebaseEnvironmentMismatchException e)
        {
            Debug.LogWarning($"[InitializationManager] Sessão pertence a outro ambiente: {e.Message}");
            ClearInvalidSessionIdentity();
            return false;
        }
        catch (Firebase.FirebaseException e) when (IsDefinitivelyInvalidSession(e))
        {
            Debug.LogWarning($"[InitializationManager] Sessão Firebase inválida; novo login necessário: {e.Message}");
            ClearInvalidSessionIdentity();
            return false;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[InitializationManager] Falha ao validar sessão Firebase: {e.Message}");

            string userId = _auth.CurrentUserId;

            if (!string.IsNullOrEmpty(userId) && _userDataLocal != null)
            {
                var cached = _userDataLocal.GetUser(userId);

                if (cached != null)
                {
                    UserDataStore.CurrentUserData = cached;
                    _usingLocalSessionFallback = true;
                    Debug.LogWarning("[InitializationManager] Sessão não validada online, mas há UserData local. Permitindo modo offline.");
                    return true;
                }
            }

            if (TryLoadOfflineUserFromCache("Sessão Firebase não validada."))
                return true;

            Debug.LogWarning("[InitializationManager] Sessão não validada online e sem UserData local. Indo para LoginView sem limpar a sessão Firebase.");
            return false;
        }
    }

    private static bool IsDefinitivelyInvalidSession(Firebase.FirebaseException exception)
    {
        var authError = (AuthError)exception.ErrorCode;
        return authError == AuthError.InvalidUserToken
            || authError == AuthError.UserTokenExpired
            || authError == AuthError.UserDisabled
            || authError == AuthError.UserNotFound;
    }

    private static void ClearInvalidSessionIdentity()
    {
        try
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[InitializationManager] Falha ao encerrar sessão inválida: {e.Message}");
        }

        UserDataStore.CurrentUserData = null;
        LocalSessionState.MarkSignedOut();
    }

    private async Task<bool> WaitForLocalSessionRestore(int timeoutMillis)
    {
        if (HasFirebaseCurrentUser())
            return true;

        var auth = FirebaseAuth.DefaultInstance;
        var restored = new TaskCompletionSource<bool>(
            TaskCreationOptions.RunContinuationsAsynchronously
        );

        EventHandler handler = null;
        handler = (_, __) =>
        {
            if (HasFirebaseCurrentUser())
                restored.TrySetResult(true);
        };

        auth.StateChanged += handler;

        try
        {
            if (HasFirebaseCurrentUser())
                return true;

            Task completed = await Task.WhenAny(restored.Task, Task.Delay(timeoutMillis));
            return completed == restored.Task && HasFirebaseCurrentUser();
        }
        finally
        {
            auth.StateChanged -= handler;
        }
    }

    private bool TryLoadOfflineUserFromCache(string reason)
    {
        string lastUserId = PlayerPrefs.GetString("UserId", string.Empty);
        if (!LocalSessionState.CanRestore(lastUserId) || _userDataLocal == null)
        {
            Debug.LogWarning($"[InitializationManager] Sem sessão local ativa. Motivo: {reason}");
            return false;
        }

        var cached = _userDataLocal.GetUser(lastUserId);
        if (cached == null)
        {
            Debug.LogWarning($"[InitializationManager] UserData local não foi encontrado para {lastUserId}. Motivo: {reason}");
            return false;
        }

        UserDataStore.CurrentUserData = cached;
        _usingLocalSessionFallback = true;
        PersistCurrentUserIdentity(cached);
        Debug.LogWarning($"[InitializationManager] Usando sessão local LiteDB para {lastUserId}. Motivo: {reason}");
        return true;
    }

    private async Task<bool> LoadUserData()
    {
        try
        {
            if (_usingLocalSessionFallback && UserDataStore.CurrentUserData != null)
                return true;

            if (!HasFirebaseCurrentUser())
                return UserDataStore.CurrentUserData != null
                    || TryLoadOfflineUserFromCache("Carregamento sem sessão Firebase.");

            string userId = _auth.CurrentUserId;
            if (string.IsNullOrEmpty(userId)) return false;

            // TrySyncPendingData já resolve todos os cenários:
            //   - sem cache local    → busca Firestore → salva LiteDB
            //   - cache dirty        → envia ao Firestore → marca synced
            //   - cache stale        → busca Firestore → atualiza LiteDB
            //   - cache válido       → carrega LiteDB direto
            //   - Firestore offline  → usa LiteDB como fallback
            await WithTimeout(
                _userDataSync.TrySyncPendingData(userId),
                12000,
                "Carregamento/sincronização de UserData excedeu o tempo limite.");

            if (UserDataStore.CurrentUserData == null)
            {
                Debug.LogError("[InitializationManager] UserData nulo após sync.");
                return false;
            }

            PersistCurrentUserIdentity(UserDataStore.CurrentUserData);

            Debug.Log($"[InitializationManager] UserData pronto. " +
                      $"UserId: {UserDataStore.CurrentUserData.UserId}, " +
                      $"Level: {UserDataStore.CurrentUserData.PlayerLevel}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[InitializationManager] Erro ao carregar dados: {e.Message}");

            // Último recurso: tenta carregar direto do LiteDB
            string userId = _auth.CurrentUserId;
            if (!string.IsNullOrEmpty(userId))
            {
                var cached = _userDataLocal.GetUser(userId);
                if (cached != null)
                {
                    UserDataStore.CurrentUserData = cached;
                    _usingLocalSessionFallback = true;
                    Debug.LogWarning("[InitializationManager] UserData carregado do cache de emergência.");
                    return true;
                }
            }

            return false;
        }
    }

    private static bool HasFirebaseCurrentUser()
    {
        return FirebaseAuth.DefaultInstance?.CurrentUser != null;
    }

    private static void PersistCurrentUserIdentity(UserData userData)
    {
        if (userData == null || string.IsNullOrEmpty(userData.UserId))
            return;

        // Também migra sessões criadas por versões antigas que ainda não
        // gravavam o UID local. Assim, um próximo cold start pode seguir pelo
        // cache mesmo se a restauração nativa do Firebase estiver lenta/offline.
        LocalSessionState.MarkAuthenticated(
            userData.UserId,
            userData.Email,
            userData.NickName);
    }

    private void StartRemoteSessionRefreshInBackgroundIfNeeded()
    {
        if (!_usingLocalSessionFallback)
            return;

        string localUserId = UserDataStore.CurrentUserData?.UserId;
        if (string.IsNullOrEmpty(localUserId))
            return;

        _ = RefreshRemoteSessionWhenAvailableAsync(localUserId);
    }

    private async Task RefreshRemoteSessionWhenAvailableAsync(string localUserId)
    {
        try
        {
            Debug.Log("[InitializationManager] Sessão local ativa. Tentando restaurar Firebase em background.");

            bool restored = HasFirebaseCurrentUser() || await WaitForLocalSessionRestore(30000);
            if (!restored)
            {
                Debug.LogWarning("[InitializationManager] Firebase não restaurou sessão em background. Mantendo modo local.");
                return;
            }

            await WithTimeout(
                _auth.CheckAuthenticationStatus(),
                8000,
                "Renovação do token Firebase em background excedeu o tempo limite.");

            string remoteUserId = FirebaseAuth.DefaultInstance.CurrentUser?.UserId;
            if (remoteUserId != localUserId)
            {
                Debug.LogError($"[InitializationManager] Divergência de identidade: Firebase restaurou {remoteUserId}, mas a sessão local pertence a {localUserId}. Novo login será exigido.");
                InvalidateLocalSessionAndNavigateToLogin();
                return;
            }

            var sync = _userDataSync ?? AppContext.UserDataSync;
            if (sync != null)
                await sync.TrySyncPendingData(localUserId);

            Debug.Log("[InitializationManager] Sessão Firebase restaurada e UserData sincronizado em background.");
        }
        catch (FirebaseEnvironmentMismatchException e)
        {
            Debug.LogWarning($"[InitializationManager] Sessão restaurada pertence a outro ambiente: {e.Message}");
            InvalidateLocalSessionAndNavigateToLogin();
        }
        catch (Firebase.FirebaseException e) when (IsDefinitivelyInvalidSession(e))
        {
            Debug.LogWarning($"[InitializationManager] Firebase invalidou definitivamente a sessão local: {e.Message}");
            InvalidateLocalSessionAndNavigateToLogin();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[InitializationManager] Não foi possível renovar Firebase em background. Modo local preservado: {e.Message}");
        }
    }

    private void InvalidateLocalSessionAndNavigateToLogin()
    {
        ClearInvalidSessionIdentity();
        SceneManager.LoadScene("LoginView", LoadSceneMode.Single);
    }

    private void NavigateAfterInit(bool authenticated)
    {
        string targetScene = authenticated ? "PathwayScene" : "LoginView";

        Debug.Log($"[InitManager] NavigateAfterInit chamado. authenticated={authenticated}, targetScene={targetScene}");

        if (!Application.CanStreamedLevelBeLoaded(targetScene))
        {
            Debug.LogError($"[InitManager] Cena não está carregável pela build: {targetScene}. Verifique Build Settings.");
            loadingSpinner?.SetMessage($"Cena {targetScene} não encontrada na build.");
            return;
        }

        try
        {
            SceneManager.LoadScene(targetScene, LoadSceneMode.Single);
        }
        catch (Exception e)
        {
            Debug.LogError($"[InitManager] Erro ao carregar cena {targetScene}: {e.GetType().Name}: {e.Message}");
            Debug.LogError(e.StackTrace);
            loadingSpinner?.SetMessage($"Erro ao carregar {targetScene}.");
        }
    }

    private void InitializePlayerLevelService()
    {
        if (AppContext.PlayerLevel == null)
        {
            Debug.LogError("[InitializationManager] PlayerLevelService não encontrado no AppContext.");
            return;
        }
        Debug.Log("[InitializationManager] PlayerLevelService pronto.");
    }

    private void UpdateStatus(string message)
    {
        loadingSpinner?.SetMessage(message);
    }

    private async Task GoToLoginPreservingLocalSessionAsync(string reason)
    {
        Debug.LogWarning($"[InitializationManager] Voltando para LoginView sem limpar sessão/cache local. Motivo: {reason}");

        try
        {
            UserDataStore.CurrentUserData = null;
        }
        catch { }

        try
        {
            loadingSpinner?.HideSpinner();
            await Task.Yield();
            NavigateAfterInit(false);
            return;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[InitializationManager] Erro ao navegar para LoginView: {e.Message}");
        }

        try
        {
            loadingSpinner?.HideSpinner();
        }
        catch { }

        NavigateAfterInit(false);
    }

    private static async Task WithTimeout(Task task, int timeoutMillis, string timeoutMessage)
    {
        Task completed = await Task.WhenAny(task, Task.Delay(timeoutMillis));
        if (completed != task)
            throw new TimeoutException(timeoutMessage);

        await task;
    }

    private void ShowError(string message)
    {
        Debug.LogError($"[InitManager] {message}");

        try
        {
            loadingSpinner?.ShowSpinner();
            loadingSpinner?.SetMessage(message);
        }
        catch { }
    }
}
