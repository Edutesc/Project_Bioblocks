using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Gerencia a sincronização da coleção Users entre LiteDB (local) e Firestore (nuvem).
///
/// RESPONSABILIDADES:
///   - Carregar usuário: online → Firestore → salva LiteDB; offline → LiteDB
///   - Salvar usuário: sempre salva LiteDB; se online, sobe para Firestore
///   - Sincronizar pendentes quando a conexão volta
///
/// SETUP NO UNITY:
///   Adicione este script ao mesmo GameObject "App" onde está o AppContext.
///   Ele será registrado no AppContext como IUserSyncService.
/// </summary>
public class UserSyncService : MonoBehaviour, IUserSyncService
{
    private ILiteDBService      _localDB;
    private IFirestoreRepository _firestore;
    private bool                _isSyncing = false;

    public event Action         OnSyncStarted;
    public event Action<bool>   OnSyncCompleted;

    // ─────────────────────────────────────────────────────
    // Inicialização — chamada pelo AppContext após os serviços estarem prontos
    // ─────────────────────────────────────────────────────

    public void Initialize(ILiteDBService localDB, IFirestoreRepository firestore)
    {
        _localDB   = localDB;
        _firestore = firestore;

        ConnectivityMonitor.Instance.OnConnectivityChanged += OnConnectivityChanged;
        Debug.Log("[UserSyncService] Inicializado.");
    }

    // ─────────────────────────────────────────────────────
    // API pública
    // ─────────────────────────────────────────────────────

    /// <summary>
    /// Carrega o usuário. Online: busca no Firestore e atualiza LiteDB.
    /// Offline: lê do LiteDB.
    /// </summary>
    public async Task LoadUserAsync(string userId)
    {
        if (ConnectivityMonitor.Instance.IsOnline)
            await SyncFromFirestoreAsync(userId);
        else
            LoadFromLocal(userId);
    }

    /// <summary>
    /// Salva alterações do usuário. Sempre persiste no LiteDB.
    /// Se online, sobe para o Firestore imediatamente.
    /// </summary>
    public async Task SaveUserAsync(UserDataDB user)
    {
        _localDB.SaveUser(user);
        UserDataStore.CurrentUserData = UserDataDBMapper.ToUserData(user);

        if (ConnectivityMonitor.Instance.IsOnline)
            await UploadToFirestoreAsync(user);
    }

    /// <summary>
    /// Sincroniza todos os registros pendentes (IsDirty = true) com o Firestore.
    /// </summary>
    public async Task SyncPendingUsersAsync()
    {
        if (_isSyncing) return;

        _isSyncing = true;
        OnSyncStarted?.Invoke();
        bool success = true;

        try
        {
            var pending = _localDB.GetAllDirtyUsers();
            Debug.Log($"[UserSyncService] {pending.Count} usuário(s) pendente(s) para sync.");

            foreach (var user in pending)
            {
                await UploadToFirestoreAsync(user);
            }
        }
        catch (Exception e)
        {
            success = false;
            Debug.LogError($"[UserSyncService] Erro no sync: {e.Message}");
        }
        finally
        {
            _isSyncing = false;
            OnSyncCompleted?.Invoke(success);
        }
    }

    // ─────────────────────────────────────────────────────
    // Privados
    // ─────────────────────────────────────────────────────

    private async Task SyncFromFirestoreAsync(string userId)
    {
        try
        {
            Debug.Log($"[UserSyncService] Buscando {userId} no Firestore...");
            UserData userData = await _firestore.GetUserData(userId);

            if (userData == null)
            {
                Debug.LogWarning("[UserSyncService] Usuário não encontrado no Firestore. Tentando LiteDB...");
                LoadFromLocal(userId);
                return;
            }

            // Salva localmente com IsDirty = false (veio do servidor, já está sincronizado)
            var userDB = UserDataDBMapper.FromUserData(userData);
            userDB.IsDirty    = false;
            userDB.SyncStatus = SyncStatus.Synced;
            _localDB.SaveUser(userDB);
            // Corrige o flag após o SaveUser que força IsDirty = true
            _localDB.MarkUserAsSynced(userId);

            UserDataStore.CurrentUserData = userData;
            Debug.Log($"[UserSyncService] Usuário {userId} sincronizado do Firestore.");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[UserSyncService] Falha ao buscar do Firestore: {e.Message}. Usando LiteDB.");
            LoadFromLocal(userId);
        }
    }

    private void LoadFromLocal(string userId)
    {
        var localUser = _localDB.GetUser(userId);
        if (localUser != null)
        {
            UserDataStore.CurrentUserData = UserDataDBMapper.ToUserData(localUser);
            Debug.Log("[UserSyncService] Usuário carregado do LiteDB.");
        }
        else
        {
            Debug.LogWarning("[UserSyncService] Usuário não encontrado nem localmente.");
        }
    }

    private async Task UploadToFirestoreAsync(UserDataDB user)
    {
        try
        {
            UserData userData = UserDataDBMapper.ToUserData(user);
            await _firestore.UpdateUserData(userData);
            _localDB.MarkUserAsSynced(user.UserId);
            Debug.Log($"[UserSyncService] {user.NickName} enviado para o Firestore.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserSyncService] Falha ao enviar {user.NickName}: {e.Message}");
        }
    }

    private async void OnConnectivityChanged(bool isOnline)
    {
        if (isOnline)
        {
            Debug.Log("[UserSyncService] Conexão restaurada — sincronizando pendentes...");
            await SyncPendingUsersAsync();
        }
    }

    private void OnDestroy()
    {
        ConnectivityMonitor.Instance.OnConnectivityChanged -= OnConnectivityChanged;
    }
}