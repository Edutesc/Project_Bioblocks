using System;
using System.Threading.Tasks;
using UnityEngine;
public class UserSyncService : MonoBehaviour
{
    private static UserSyncService _instance;

    public static UserSyncService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<UserSyncService>();

                if (_instance == null)
                {
                    Debug.LogError("UserSyncService não foi encontrado na cena! " +
                                   "Arraste o script para um GameObject.");
                }
            }
            return _instance;
        }
    }
    private LiteDBService _liteDB;
    private FirestoreRepository _firestore;
    private bool _isSyncing = false;
    private bool _isInitialized = false;
    public event Action OnSyncStarted;
    public event Action<bool> OnSyncCompleted;
    private void Start() => Initialize();
    private bool Initialize()
    {
        _liteDB = LiteDBService.Instance;
        _firestore = FirestoreRepository.Instance;
        ConnectivityMonitor.Instance.OnConnectivityChanged += OnConnectivityChanged;
        _isInitialized = true;
        return true;
    }
    public async Task LoadUserAsync(string userId)
    {
        if (ConnectivityMonitor.Instance.IsOnline)
            await SyncFromFirestoreAsync(userId);
        else
            LoadFromLocal(userId);
    }
    public async Task SaveUserAsync(UserDataDB user)
    {
        _liteDB.SaveUser(user);
        UserDataStore.CurrentUserData = UserDataDBMapper.ToUserData(user);
        if (ConnectivityMonitor.Instance.IsOnline)
            await UploadToFirestoreAsync(user);
    }
    private async void OnConnectivityChanged(bool isOnline)
    {
        if (isOnline) await SyncPendingUsersAsync();
    }
    public async Task SyncPendingUsersAsync()
    {
        var pending = _liteDB.GetAllDirtyUsers();
        foreach (var user in pending)
            await UploadToFirestoreAsync(user);
    }


    private async Task SyncFromFirestoreAsync(string userId)
    {
        Debug.Log($"[SYNC] Buscando {userId} no Firestore...");
        await Task.Yield();
    }

    private void LoadFromLocal(string userId)
    {
        var localUser = _liteDB.GetUser(userId);
        if (localUser != null)
        {
            UserDataStore.CurrentUserData = UserDataDBMapper.ToUserData(localUser);
            Debug.Log("[SYNC] Usuário carregado do banco local.");
        }
    }

    private async Task UploadToFirestoreAsync(UserDataDB user)
    {
        Debug.Log($"[SYNC] Enviando {user.NickName} para a nuvem...");
        await Task.Yield();
    }
    
}
