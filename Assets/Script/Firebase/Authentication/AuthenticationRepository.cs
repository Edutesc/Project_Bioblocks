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
    private IAuthGate _authGate;

    private bool isInitialized;

    public bool IsInitialized => isInitialized;

    public string CurrentUserId
    {
        get
        {
            string firebaseUserId = _auth?.CurrentUser?.UserId;
            if (!string.IsNullOrEmpty(firebaseUserId))
                return firebaseUserId;

            string cachedUserId = UserDataStore.CurrentUserData?.UserId;
            if (!string.IsNullOrEmpty(cachedUserId))
                return cachedUserId;

            return PlayerPrefs.GetString("UserId", string.Empty);
        }
    }

    // -------------------------------------------------------
    // Inicialização / Injeção
    // -------------------------------------------------------

    public void InjectDependencies(
        IFirestoreUserRepository usersRemote,
        INicknameRepository nicknames,
        IUserRealtimeListenerService userRealtimeListeners = null,
        IAuthGate authGate = null)
    {
        _usersRemote = usersRemote ?? throw new ArgumentNullException(nameof(usersRemote));
        _nicknames = nicknames ?? throw new ArgumentNullException(nameof(nicknames));
        _userRealtimeListeners = userRealtimeListeners;
        _authGate = authGate;
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
        if (user != null && !user.IsAnonymous)
            return true;

        return HasCachedUserSession();
    }

    public bool HasLocalSession()
    {
        return _auth?.CurrentUser != null || HasCachedUserSession();
    }

    private static bool HasCachedUserSession()
    {
        string currentUserId = UserDataStore.CurrentUserData?.UserId;
        return !string.IsNullOrEmpty(currentUserId);
    }

    public async Task<UserData> SignInWithEmailAsync(string email, string password)
    {
        EnsureInitialized();
        EnsureRepositoriesInjected();

        try
        {
            var result = await SignInWithEnvironmentRecoveryAsync(email, password);

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

    private async Task<AuthResult> SignInWithEnvironmentRecoveryAsync(
        string email,
        string password)
    {
        var result = await _auth.SignInWithEmailAndPasswordAsync(email, password);

        if (result?.User == null || _authGate == null)
            return result;

        try
        {
            await _authGate.WaitForAuthenticatedAsync();
            return result;
        }
        catch (FirebaseEnvironmentMismatchException e)
        {
            Debug.LogWarning(
                $"[AuthRepository] Sessão de outro ambiente detectada durante login. " +
                $"Limpando e repetindo autenticação uma vez: {e.Message}"
            );

            ClearIncompatibleIdentity();
            result = await _auth.SignInWithEmailAndPasswordAsync(email, password);

            if (result?.User != null)
                await _authGate.WaitForAuthenticatedAsync();

            return result;
        }
    }

    private void ClearIncompatibleIdentity()
    {
        _auth.SignOut();
        UserDataStore.CurrentUserData = null;
        LocalSessionState.MarkSignedOut();
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

            if (_authGate != null)
            {
                try
                {
                    await _authGate.WaitForAuthenticatedAsync();
                }
                catch (FirebaseEnvironmentMismatchException)
                {
                    // A conta pode já ter sido criada antes de detectarmos o
                    // token legado. Não repetimos CreateUser; limpamos a sessão
                    // incompatível e autenticamos a conta recém-criada.
                    ClearIncompatibleIdentity();
                    result = await _auth.SignInWithEmailAndPasswordAsync(email, password);
                    await _authGate.WaitForAuthenticatedAsync();
                }
            }

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

            try
            {
                await _usersRemote.CreateUserDocument(user);
            }
            catch
            {
                // Não deixa uma conta Auth sem Users/{uid}. Como a conta acabou
                // de ser criada, DeleteAsync atende ao requisito de login recente.
                try
                {
                    if (_auth.CurrentUser?.UserId == user.UserId)
                        await _auth.CurrentUser.DeleteAsync();

                    Debug.LogWarning(
                        $"[AuthRepository] Registro revertido: conta Auth {user.UserId} " +
                        "removida após falha ao criar Users/{uid}."
                    );
                }
                catch (Exception rollbackError)
                {
                    Debug.LogError(
                        $"[AuthRepository] Falha ao reverter conta Auth órfã " +
                        $"{user.UserId}: {rollbackError.Message}"
                    );
                }

                throw;
            }

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
            string userId = CurrentUserId;

            _userRealtimeListeners?.StopListening();
            AppContext.UserRealtimeListeners?.StopListening();

            _auth.SignOut();
            UserDataStore.CurrentUserData = null;
            LocalSessionState.MarkSignedOut();

            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    AppContext.UserDataLocal?.DeleteUser(userId);
                }
                catch (Exception cacheError)
                {
                    // A identidade já foi invalidada; uma falha de limpeza do
                    // cache não pode transformar um logout concluído em erro.
                    Debug.LogWarning($"[AuthRepository] Sessão encerrada, mas o cache do usuário não pôde ser removido: {cacheError.Message}");
                }
            }

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

            if (_authGate != null)
            {
                await _authGate.WaitForAuthenticatedAsync();
            }
            else
            {
                string token = await user.TokenAsync(false);
                if (string.IsNullOrEmpty(token))
                    throw new ReauthenticationRequiredException("Token inválido.");
            }

            Debug.Log("[AuthRepository] Sessão Firebase validada com sucesso");
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
