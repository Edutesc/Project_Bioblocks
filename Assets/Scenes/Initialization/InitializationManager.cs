using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Firebase;

public class InitializationManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject retryPanel;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text errorText;

    [Header("Configuration")]
    [SerializeField] private float minimumLoadingTime = 2.0f;

    [Header("Global Loading Spinner")]
    [SerializeField] private GameObject globalSpinnerPrefab;

    private LoadingSpinnerComponent globalSpinner;

    private void Awake()
    {
        InitializeGlobalSpinner();
    }

    private void InitializeGlobalSpinner()
    {
        try
        {
            // If we already have a spinner instance, use that one
            globalSpinner = LoadingSpinnerComponent.Instance;
            
            // If we're supposed to use a prefab instead of the singleton instance
            if (globalSpinner == null && globalSpinnerPrefab != null)
            {
                Canvas mainCanvas = FindObjectOfType<Canvas>();
                GameObject spinnerObject;
                
                if (mainCanvas != null)
                {
                    spinnerObject = Instantiate(globalSpinnerPrefab, mainCanvas.transform);
                }
                else
                {
                    spinnerObject = Instantiate(globalSpinnerPrefab);
                }
                
                spinnerObject.name = "GlobalLoadingSpinner";
                DontDestroyOnLoad(spinnerObject);
                
                globalSpinner = spinnerObject.GetComponent<LoadingSpinnerComponent>();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error initializing spinner: {e.Message}");
        }
    }

    private void Start()
    {
        SetupUI();
        StartInitialization();
    }

    private void SetupUI()
    {
        if (retryPanel != null)
            retryPanel.SetActive(false);
            
        if (progressBar != null)
            progressBar.fillAmount = 0f;
    }

    private async void StartInitialization()
    {
        float startTime = Time.time;

        try
        {
            try
            {
                if (globalSpinner != null)
                {
                    globalSpinner.ShowSpinner();
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error showing spinner: {e.Message}");
            }

            UpdateStatus("Inicializando Firebase...");
            await InitializeFirebaseServices();
            UpdateProgress(0.3f);

            UpdateStatus("Verificando autenticação...");
            bool isAuthenticated = await CheckAuthentication();
            UpdateProgress(0.5f);
            bool userDataLoaded = false;

            if (isAuthenticated)
            {
                UpdateStatus("Carregando dados do usuário...");
                userDataLoaded = await LoadUserData();
                UpdateProgress(0.7f);

                if (userDataLoaded)
                {
                    UpdateStatus("Carregando bancos de questões...");
                    await DatabaseStatisticsManager.Instance.Initialize();
                    UpdateProgress(0.85f);

                    UpdateStatus("Configurando sistema de níveis...");
                    InitializePlayerLevelManager();
                    UpdateProgress(0.9f);
                }
            }

            float elapsed = Time.time - startTime;
            if (elapsed < minimumLoadingTime)
            {
                await Task.Delay(Mathf.RoundToInt((minimumLoadingTime - elapsed) * 1000));
            }

            try
            {
                if (isAuthenticated && userDataLoaded)
                {
                    if (globalSpinner != null)
                    {
                        globalSpinner.ShowSpinnerUntilSceneLoaded("PathwayScene");
                    }
                    SceneManager.LoadScene("PathwayScene");
                }
                else
                {
                    if (globalSpinner != null)
                    {
                        globalSpinner.ShowSpinnerUntilSceneLoaded("LoginView");
                    }
                    SceneManager.LoadScene("LoginView");
                }
            }
            catch (Exception)
            {
                if (isAuthenticated && userDataLoaded)
                {
                    SceneManager.LoadScene("PathwayScene");
                }
                else
                {
                    SceneManager.LoadScene("LoginView");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[InitializationManager] INITIALIZATION FAILED: {ex.GetType().Name}: {ex.Message}");
            Debug.LogError($"[InitializationManager] StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Debug.LogError($"[InitializationManager] InnerException: {ex.InnerException.Message}");
            }

            try
            {
                if (globalSpinner != null)
                {
                    globalSpinner.HideSpinner();
                }
            }
            catch { }

            ShowError($"Falha na inicialização: {ex.Message}");
        }  
    }

    private async Task InitializeFirebaseServices()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus != DependencyStatus.Available)
        {
            throw new System.Exception($"Could not resolve all Firebase dependencies: {dependencyStatus}");
        }

        await AuthenticationRepository.Instance.InitializeAsync();
        FirestoreRepository.Instance.Initialize();
        StorageRepository.Instance.Initialize();
    }

    //Modifiquei essa função

    private async Task<bool> LoadUserData()
{
    try
    {
        await Task.Yield();
        // No futuro, aqui entrará o ID do Firebase. 
            // Por enquanto, usamos o ID que você definiu no seu Mock/Banco Local.
            string meuIdDeTeste = "mock-user-123"; 

        // Chamamos o método padrão do seu LiteDBService (sem alterá-lo)
        var dbData = LiteDBService.Instance.GetUser(meuIdDeTeste);

        if (dbData == null)
        {
            Debug.LogError($"[LiteDB] ERRO: Usuário '{meuIdDeTeste}' não encontrado. Verifique se o banco local foi populado.");
            return false;
        }

        // --- TRADUTOR (DTO) ---
        // Convertemos o dado do Banco (UserDataDB) para o dado do Jogo (UserData)
        UserData convertedData = new UserData();
        convertedData.UserId = dbData.UserId;
        convertedData.Score = dbData.Score;
        convertedData.PlayerLevel = dbData.PlayerLevel;

        // Salva no Store para a Top Bar ler
        UserDataStore.CurrentUserData = convertedData;

        Debug.Log($"[LITE DB] Sucesso! Usuário '{dbData.NickName}' carregado. Score: {convertedData.Score}");

        // Notifica outros sistemas (como o de Nível)
        if (PlayerLevelManager.Instance != null)
        {
            PlayerLevelManager.Instance.OnUserDataLoaded(convertedData);
        }

        return true;
    }
    catch (System.Exception e)
    {
        Debug.LogError($"[InitializationManager] Falha crítica no carregamento: {e.Message}");
        return false;
    }
} 





    private async Task<bool> CheckAuthentication()
    {
        var user = AuthenticationRepository.Instance.Auth.CurrentUser;
        if (user != null)
        {
            try
            {
                await user.ReloadAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        return false;
    }

    private void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }

    private void UpdateProgress(float progress)
    {
        if (progressBar != null)
        {
            progressBar.fillAmount = progress;
        }
    }

    private void ShowError(string message)
    {
        if (retryPanel != null)
        {
            retryPanel.SetActive(true);

            if (errorText != null)
            {
                errorText.text = message;
            }
        }
    }

    private void InitializePlayerLevelManager()
    {
        try
        {
            if (PlayerLevelManager.Instance == null)
            {
                Debug.LogError("[InitializationManager] PlayerLevelManager não encontrado na cena!");
                return;
            }
            
            Debug.Log("[InitializationManager] PlayerLevelManager encontrado. Aguardando verificação...");
            
            // Aguardar para garantir que tudo foi carregado
            StartCoroutine(WaitAndCheckPlayerLevel());
        }
        catch (Exception e)
        {
            Debug.LogError($"[InitializationManager] Erro ao inicializar PlayerLevelManager: {e.Message}");
        }
    }

    private System.Collections.IEnumerator WaitAndCheckPlayerLevel()
    {
        // Aguardar 2 segundos para garantir que TUDO foi inicializado
        yield return new WaitForSeconds(2f);
        
        Debug.Log("[InitializationManager] Verificando estado do PlayerLevelManager...");
        
        if (UserDataStore.CurrentUserData != null)
        {
            Debug.Log($"[InitializationManager] UserData disponível. UserId: {UserDataStore.CurrentUserData.UserId}, Level: {UserDataStore.CurrentUserData.PlayerLevel}");
            
            if (UserDataStore.CurrentUserData.PlayerLevel == 0)
            {
                Debug.LogWarning("[InitializationManager] Level ainda está em 0 após 2 segundos. Forçando recálculo...");
                
                if (PlayerLevelManager.Instance != null)
                {
                    _ = ForceRecalculatePlayerLevel();
                }
            }
            else
            {
                Debug.Log($"[InitializationManager] Level carregado corretamente: {UserDataStore.CurrentUserData.PlayerLevel}");
            }
        }
        else
        {
            Debug.LogError("[InitializationManager] UserData ainda é null após 2 segundos!");
        }
    }

    private async Task ForceRecalculatePlayerLevel()
    {
        try
        {
            if (PlayerLevelManager.Instance != null)
            {
                await PlayerLevelManager.Instance.RecalculateTotalAnswered();
                await PlayerLevelManager.Instance.CheckAndHandleLevelUp();
                Debug.Log("[InitializationManager] Recálculo de level forçado completado");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[InitializationManager] Erro ao forçar recálculo: {e.Message}");
        }
    }
}