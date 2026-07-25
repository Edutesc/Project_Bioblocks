using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using TMPro;

public class RegisterManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button backButton;
    [SerializeField] private FeedbackManager feedbackManager;
    [SerializeField] private LoadingSpinnerComponent loadingSpinner;

    private IAuthRepository              _auth;
    private INicknameRepository          _nicknames;
    private IFirestoreUserRepository     _usersRemote;
    private IUserDataLocalRepository     _userDataLocal;
    private INavigationService           _navigation;

    private bool isProcessing = false;
    private static readonly System.Random _avatarRng = new System.Random();

    private void Start()
    {
        _auth          = AppContext.Auth;
        _nicknames     = AppContext.Nicknames;
        _usersRemote   = AppContext.FirestoreUsers;
        _userDataLocal = AppContext.UserDataLocal;
        _navigation    = AppContext.Navigation;

        if (_auth == null)
            Debug.LogError("[RegisterManager] AppContext.Auth está nulo.");

        if (_nicknames == null)
            Debug.LogError("[RegisterManager] AppContext.Nicknames está nulo.");

        if (_usersRemote == null)
            Debug.LogError("[RegisterManager] AppContext.FirestoreUsers está nulo.");

        if (_navigation == null)
            Debug.LogError("[RegisterManager] AppContext.Navigation está nulo.");

        nickNameInput.contentType    = TMP_InputField.ContentType.Standard;
        nickNameInput.characterLimit = 15;
        nickNameInput.onValueChanged.AddListener(ValidateNickname);

        registerButton.onClick.AddListener(HandleRegistration);

    }

    // -------------------------------------------------------
    // Registro
    // -------------------------------------------------------

    public async void HandleRegistration()
    {
        if (isProcessing)
            return;

        string name     = nameInput.text?.Trim();
        string nickName = nickNameInput.text?.Trim();
        string email    = emailInput.text?.Trim().ToLower();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(nickName) ||
            string.IsNullOrEmpty(name)     ||
            string.IsNullOrEmpty(email)    ||
            string.IsNullOrEmpty(password))
        {
            feedbackManager.ShowFeedback("Todos os campos são obrigatórios.", true);
            return;
        }

        if (nickName.Length < 3)
        {
            feedbackManager.ShowFeedback("Nickname deve possuir mais de 3 caracteres.", true);
            return;
        }

        if (_auth == null || _nicknames == null || _usersRemote == null || _navigation == null)
        {
            feedbackManager.ShowFeedback("Serviços ainda não inicializados. Tente novamente.", true);
            return;
        }

        isProcessing = true;
        SetAllButtonsInteractable(false);
        feedbackManager.HideFeedback();
        loadingSpinner?.ShowSpinner();

        bool success = false;

        try
        {
            bool nicknameExists = await _nicknames.AreNicknameTaken(nickName);

            if (nicknameExists)
                throw new Exception("Este nickname já está em uso. Por favor, escolha outro.");

            Debug.Log("=== LIMPEZA ANTES DE REGISTRAR NOVO USUÁRIO ===");

            UserDataStore.CurrentUserData = null;
            AppContext.AnsweredQuestions?.ResetManager();
            AnsweredQuestionsListStore.ClearAll();

            Debug.Log("=== LIMPEZA CONCLUÍDA, INICIANDO REGISTRO ===");

            // AuthenticationRepository agora cria o usuário no Auth,
            // cria o documento em Users e reserva o nickname em Nicknames.
            UserData userData = await _auth.RegisterUserAsync(name, nickName, email, password);

            if (userData == null)
                throw new Exception("Erro ao carregar dados do usuário recém-criado.");

            if (string.IsNullOrEmpty(userData.UserId))
                throw new Exception("Erro: usuário criado mas ID não encontrado.");

            UserDataStore.CurrentUserData = userData;
            CacheUserDataLocally(userData);

            Debug.Log("[RegisterManager] UserData definido. Iniciando ForceUpdate...");
            await AppContext.AnsweredQuestions.ForceUpdate();
            Debug.Log("[RegisterManager] ForceUpdate concluído.");

            _ = AssignRandomDefaultAvatarAsync(userData);

            success = true;
            _navigation.NavigateTo("PathwayScene");
        }
        catch (FirebaseException e)
        {
            string errorMessage = GetFirebaseAuthErrorMessage(e);
            Debug.LogWarning($"[RegisterManager] {errorMessage}");
            feedbackManager.ShowFeedback(errorMessage, true);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[RegisterManager] {e.Message}");
            feedbackManager.ShowFeedback(e.Message, true);
        }
        finally
        {
            if (!success)
            {
                loadingSpinner?.HideSpinner();
                SetAllButtonsInteractable(true);
                isProcessing = false;
            }
        }
    }

    private void CacheUserDataLocally(UserData userData)
    {
        if (userData == null || string.IsNullOrEmpty(userData.UserId) || _userDataLocal == null)
            return;

        try
        {
            _userDataLocal.UpdateUser(userData);
            _userDataLocal.MarkAsSynced(userData.UserId);
            PlayerPrefs.SetString("UserId", userData.UserId);
            PlayerPrefs.SetString("UserEmail", userData.Email ?? string.Empty);
            PlayerPrefs.SetString("UserNickname", userData.NickName ?? string.Empty);
            PlayerPrefs.Save();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[RegisterManager] Falha ao salvar UserData no cache local: {e.Message}");
        }
    }

    // -------------------------------------------------------
    // Avatar padrão aleatório
    // -------------------------------------------------------

    private async Task AssignRandomDefaultAvatarAsync(UserData userData)
    {
        try
        {
            var defaults = AvatarCatalog.Defaults;

            if (defaults.Count == 0)
            {
                Debug.LogWarning("[RegisterManager] AvatarCatalog.Defaults vazio — avatar padrão não atribuído.");
                return;
            }

            var chosen = defaults[_avatarRng.Next(defaults.Count)];
            string presetUrl = $"preset:{chosen.Id}";

            Debug.Log($"[RegisterManager] Avatar padrão sorteado: {chosen.Id} ({chosen.DisplayName})");

            try
            {
                await _usersRemote.UpdateUserProfileImageUrl(userData.UserId, presetUrl);
                Debug.Log("[RegisterManager] FirestoreUsers atualizado com preset:id");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[RegisterManager] Firestore update falhou: {e.Message}");
            }

            userData.ProfileImageUrl = presetUrl;
            UserDataStore.CurrentUserData = userData;

            _userDataLocal?.UpdateUser(userData);

            UserAvatarSyncHelper.NotifyAvatarChanged(presetUrl);

            Debug.Log("[RegisterManager] Avatar padrão aplicado com sucesso.");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[RegisterManager] Erro ao atribuir avatar padrão: {e.Message}");
        }
    }

    // -------------------------------------------------------
    // Navegação
    // -------------------------------------------------------

    public void SceneLoader()
    {
        if (isProcessing)
            return;

        isProcessing = true;
        SetAllButtonsInteractable(false);

        _navigation.NavigateTo("LoginView");
    }

    // -------------------------------------------------------
    // Validação
    // -------------------------------------------------------

    private void ValidateNickname(string value)
    {
        if (value.Length < 3)
        {
            MainThreadDispatcher.Enqueue(() =>
                feedbackManager.ShowFeedback("Nickname deve possuir mais de 3 caracteres.", true)
            );
        }
        else
        {
            MainThreadDispatcher.Enqueue(() =>
                feedbackManager.HideFeedback()
            );
        }
    }

    // -------------------------------------------------------
    // UI helpers
    // -------------------------------------------------------

    private void SetAllButtonsInteractable(bool interactable)
    {
        registerButton.interactable = interactable;

        if (backButton != null)
            backButton.interactable = interactable;

        nameInput.interactable     = interactable;
        nickNameInput.interactable = interactable;
        emailInput.interactable    = interactable;
        passwordInput.interactable = interactable;
    }

    // -------------------------------------------------------
    // Tradução de erros Firebase
    // -------------------------------------------------------

    private string GetFirebaseAuthErrorMessage(FirebaseException e)
    {
        var errorCode = (int)e.ErrorCode;

        return errorCode switch
        {
            (int)AuthError.EmailAlreadyInUse => "Email já registrado.",
            (int)AuthError.WeakPassword      => "Senha muito fraca.",
            (int)AuthError.InvalidEmail      => "Email inválido.",
            _                                => e.Message
        };
    }
}
