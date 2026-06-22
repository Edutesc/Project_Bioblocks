using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class InitializationManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float minimumLoadingTime = 2.0f;

[   Header("Initialization Loading Spinner")]
    [SerializeField] private LoadingSpinnerComponent loadingSpinner;

    private IFirestoreRepository _firestore;
    private IAuthRepository _auth;
    private IUserDataSyncService _userDataSync;
    private IUserDataLocalRepository _userDataLocal;

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

            ResetSessionIfFirebaseEnvironmentChanged(envCfg);

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
                if (statsManager != null)
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
                await ForceLogoutAndGoToLoginAsync("Usuário autenticado, mas documento/UserData não encontrado.");
                return;
            }

            float elapsed = Time.time - startTime;
            if (elapsed < minimumLoadingTime)
                await Task.Delay(Mathf.RoundToInt((minimumLoadingTime - elapsed) * 1000));

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
        if (!_auth.HasLocalSession())
            return false;

        try
        {
            await WithTimeout(_auth.ReloadCurrentUserAsync(), 8000, "Validação da sessão Firebase excedeu o tempo limite.");
            Debug.Log("[InitializationManager] Sessão validada com o servidor.");
            return true;
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
                    Debug.LogWarning("[InitializationManager] Sessão não validada online, mas há UserData local. Permitindo modo offline.");
                    return true;
                }
            }

            await ForceLogoutAndGoToLoginAsync("Sessão Firebase inválida e sem UserData local.");
            return false;
        }
    }

    private async Task<bool> LoadUserData()
    {
        try
        {
            if (!_auth.HasLocalSession()) return false;

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
                    Debug.LogWarning("[InitializationManager] UserData carregado do cache de emergência.");
                    return true;
                }
            }

            return false;
        }
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

    private async Task ForceLogoutAndGoToLoginAsync(string reason)
    {
        Debug.LogWarning($"[InitializationManager] Limpando sessão e voltando para LoginView. Motivo: {reason}");

        try
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[InitializationManager] Erro ao fazer Firebase SignOut: {e.Message}");
        }

        try
        {
            UserDataStore.CurrentUserData = null;
        }
        catch { }

        try
        {
            PlayerPrefs.DeleteKey("UserId");
            PlayerPrefs.DeleteKey("UserEmail");
            PlayerPrefs.DeleteKey("UserNickname");
            PlayerPrefs.Save();

            loadingSpinner?.HideSpinner();
            await Task.Yield();
            NavigateAfterInit(false);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[InitializationManager] Erro ao limpar PlayerPrefs: {e.Message}");
        }

        try
        {
            loadingSpinner?.HideSpinner();
        }
        catch { }

        NavigateAfterInit(false);
    }

    private void ResetSessionIfFirebaseEnvironmentChanged(EnvironmentConfig envCfg)
    {
        if (envCfg == null) return;

        const string envKey = "FirebaseEnvironment";
        string currentEnv = envCfg.FirebaseEnvironment.ToString();
        string previousEnv = PlayerPrefs.GetString(envKey, "");

        if (!string.IsNullOrEmpty(previousEnv) && previousEnv != currentEnv)
        {
            Debug.LogWarning($"[InitManager] Ambiente Firebase mudou de {previousEnv} para {currentEnv}. Limpando sessão local.");

            try
            {
                FirebaseAuth.DefaultInstance.SignOut();
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[InitManager] Erro ao limpar sessão ao trocar ambiente: {e.Message}");
            }

            UserDataStore.CurrentUserData = null;
            PlayerPrefs.DeleteKey("UserId");
            PlayerPrefs.DeleteKey("UserEmail");
            PlayerPrefs.DeleteKey("UserNickname");
        }

        PlayerPrefs.SetString(envKey, currentEnv);
        PlayerPrefs.Save();
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
