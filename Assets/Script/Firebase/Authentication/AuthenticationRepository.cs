using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;

public class AuthenticationRepository : MonoBehaviour, IAuthRepository
{
    private FirebaseAuth _auth;

    private IFirestoreUserRepository     _usersRemote;
    private INicknameRepository          _nicknames;
    private IUserRealtimeListenerService _userRealtimeListeners;

    private bool isInitialized;

    public bool IsInitialized => isInitialized;

    public string CurrentUserId => _auth?.CurrentUser?.UserId;

    // -------------------------------------------------------
    // Inicialização / Injeção
    // -------------------------------------------------------

    public void InjectDependencies(
        IFirestoreUserRepository usersRemote,
        INicknameRepository nicknames,
        IUserRealtimeListenerService userRealtimeListeners = null)
    {
        _usersRemote = usersRemote ?? throw new ArgumentNullException(nameof(usersRemote));
        _nicknames = nicknames ?? throw new ArgumentNullException(nameof(nicknames));
        _userRealtimeListeners = userRealtimeListeners;
    }

    public async Task InitializeAsync()
    {
        if (isInitialized)
            return;

        try
        {
            var app = Firebase.FirebaseApp.DefaultInstance;

            if (app == null)
            {
                Debug.Log("[AuthRepository] Creating Firebase App");
                app = Firebase.FirebaseApp.Create();
            }

            _auth = FirebaseAuth.DefaultInstance;
            isInitialized = true;

            Debug.Log("[AuthRepository] Firebase Authentication initialized successfully");
            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Firebase initialization failed: {e.Message}");
            throw;
        }
    }

    // -------------------------------------------------------
    // IAuthRepository
    // -------------------------------------------------------

    public bool IsUserLoggedIn()
    {
        var user = _auth?.CurrentUser;
        return user != null && !user.IsAnonymous;
    }

    public bool HasLocalSession()
    {
        return _auth?.CurrentUser != null;
    }

    public async Task<UserData> SignInWithEmailAsync(string email, string password)
    {
        EnsureInitialized();
        EnsureRepositoriesInjected();

        try
        {
            var result = await _auth.SignInWithEmailAndPasswordAsync(email, password);

            if (result?.User == null)
                throw new Exception("Login falhou: resultado ou usuário nulo.");

            string uid = result.User.UserId;

            UserData userData = await _usersRemote.GetUserData(uid);

            if (userData == null)
                throw new Exception("Dados do usuário não encontrados.");

            UserDataStore.CurrentUserData = userData;
            return userData;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Exception during login:\n {e}");
            throw;
        }
    }

    public async Task<UserData> RegisterUserAsync(
        string name,
        string nickName,
        string email,
        string password)
    {
        EnsureInitialized();
        EnsureRepositoriesInjected();

        try
        {
            bool nicknameTaken = await _nicknames.AreNicknameTaken(nickName);

            if (nicknameTaken)
                throw new Exception("Este nickname já está em uso. Por favor, escolha outro.");

            var result = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

            if (result?.User == null)
                throw new Exception("Registro falhou: resultado ou usuário nulo.");

            string token = await result.User.TokenAsync(forceRefresh: true);

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Token vazio após criação do usuário.");

            var user = new UserData
            {
                UserId = result.User.UserId,
                NickName = nickName,
                Name = name,
                Email = email,
                ProfileImageUrl = "",
                Score = 0,
                WeekScore = 0,
                QuestionTypeProgress = 0,
                CreatedTime = DateTime.UtcNow,
                IsUserRegistered = true,
                AnsweredQuestions = new Dictionary<string, List<int>>()
            };

            await _usersRemote.CreateUserDocument(user);

            // Mantém o comportamento antigo do FirestoreRepository.CreateUserDocument:
            // além de criar Users/{userId}, também reserva Nicknames/{nickName}.
            await _nicknames.ReserveNickname(nickName, user.UserId);

            UserDataStore.CurrentUserData = user;
            return user;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Registration failed: {e.Message}");
            throw;
        }
    }

    public async Task LogoutAsync()
    {
        EnsureInitialized();

        try
        {
            _userRealtimeListeners?.StopListening();
            AppContext.UserRealtimeListeners?.StopListening();

            _auth.SignOut();
            UserDataStore.CurrentUserData = null;

            await Task.CompletedTask;

            Debug.Log("[AuthRepository] Usuário deslogado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro ao fazer logout: {e.Message}");
            throw;
        }
    }

    public async Task ReloadCurrentUserAsync()
    {
        var user = _auth?.CurrentUser;

        if (user == null)
            throw new Exception("Nenhum usuário logado.");

        await user.ReloadAsync();
    }

    public async Task CheckAuthenticationStatus()
    {
        try
        {
            EnsureInitialized();

            var user = _auth.CurrentUser;

            if (user == null)
                throw new Exception("Usuário não está autenticado.");

            await user.ReloadAsync();

            string token = await user.TokenAsync(true);

            if (string.IsNullOrEmpty(token))
                throw new ReauthenticationRequiredException("Token inválido.");

            Debug.Log("[AuthRepository] Token atualizado com sucesso");
        }
        catch (Firebase.FirebaseException e) when (IsRecentAuthenticationRequired(e))
        {
            Debug.LogError("[AuthRepository] É necessário reautenticar");
            throw new ReauthenticationRequiredException("É necessário reautenticar para prosseguir");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro ao verificar autenticação: {e.Message}");
            throw;
        }
    }

    public async Task DeleteUser(string userId)
    {
        try
        {
            EnsureInitialized();

            var user = _auth.CurrentUser;

            if (user == null)
                throw new Exception("Usuário não está autenticado.");

            await user.ReloadAsync();

            string token = await user.TokenAsync(true);

            if (string.IsNullOrEmpty(token))
                throw new ReauthenticationRequiredException("Token inválido.");

            Debug.Log("[AuthRepository] Token atualizado, tentando deletar usuário...");

            await user.DeleteAsync();

            Debug.Log("[AuthRepository] Usuário deletado com sucesso do Authentication");
        }
        catch (Firebase.FirebaseException e) when (IsRecentAuthenticationRequired(e))
        {
            Debug.LogError("[AuthRepository] É necessário reautenticar para deletar o usuário");
            throw new ReauthenticationRequiredException("É necessário reautenticar para deletar a conta");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro ao deletar usuário: {e.Message}");
            throw;
        }
    }

    public async Task ReauthenticateUser(string email, string password)
    {
        try
        {
            EnsureInitialized();

            var user = _auth.CurrentUser;

            if (user == null)
                throw new Exception("Usuário não está autenticado.");

            Credential credential = EmailAuthProvider.GetCredential(email, password);

            await user.ReauthenticateAsync(credential);

            Debug.Log("[AuthRepository] Usuário reautenticado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AuthRepository] Erro na reautenticação: {e.Message}");
            throw;
        }
    }

    // -------------------------------------------------------
    // Helpers
    // -------------------------------------------------------

    private void EnsureInitialized()
    {
        if (!isInitialized)
            throw new Exception("Firebase não inicializado.");

        if (_auth == null)
            throw new Exception("FirebaseAuth não inicializado.");
    }

    private static bool IsRecentAuthenticationRequired(Firebase.FirebaseException exception)
    {
        string message = exception.Message?.ToLowerInvariant() ?? string.Empty;
        return message.Contains("recent authentication")
            || message.Contains("requires-recent-login")
            || message.Contains("requires recent login")
            || (message.Contains("recent") && (message.Contains("auth") || message.Contains("login")));
    }

    private void EnsureRepositoriesInjected()
    {
        if (_usersRemote == null)
            throw new Exception("IFirestoreUserRepository não injetado.");

        if (_nicknames == null)
            throw new Exception("INicknameRepository não injetado.");
    }
}
