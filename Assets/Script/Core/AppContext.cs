using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using System;

/// <summary>
/// AppContext é o ponto central de composição do app.
///
/// RESPONSABILIDADES:
///   - Inicializar todos os serviços Firebase na ordem correta
///   - Expor os serviços como interfaces (IFirestoreRepository, etc.)
///   - Sobreviver entre cenas via DontDestroyOnLoad
///
/// SETUP NO UNITY:
///   1. Crie um GameObject vazio chamado "App" na InitializationScene
///   2. Adicione este script ao GameObject "App"
///   3. Adicione também FirestoreRepository, AuthenticationRepository,
///      StorageRepository e UserSyncService como componentes do mesmo GameObject "App"
///   4. O AppContext encontra todos eles via GetComponent no Awake
/// </summary>
public class AppContext : MonoBehaviour
{
    private static AppContext _instance;
    public static event Action OnReady;

    // ─── Serviços expostos como interfaces ───────────────
    public static IFirestoreRepository      Firestore         { get; private set; }
    public static IAuthRepository           Auth              { get; private set; }
    public static IStorageRepository        Storage           { get; private set; }
    public static IStatisticsProvider       Statistics        { get; private set; }
    public static INavigationService        Navigation        { get; private set; }
    public static ISceneDataService         SceneData         { get; private set; }
    public static IDatabaseManager          LocalDatabase     { get; private set; }  // SQLite — remover após migração LiteDB
    public static ILiteDBService            LocalDB           { get; private set; }  // LiteDB — novo
    public static IUserSyncService          UserSync          { get; private set; }  // novo
    public static IImageCacheService        ImageCache        { get; private set; }
    public static IImageUploadService       ImageUpload       { get; private set; }
    public static IAnsweredQuestionsManager AnsweredQuestions { get; private set; }
    public static IPlayerLevelService       PlayerLevel       { get; private set; }

    public static bool IsReady { get; private set; }

    private async void Awake()
    {
        Debug.Log("[AppContext] Awake() chamado");
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        await InitializeServices();
    }

    private async Task InitializeServices()
    {
        IsReady = false;

        try
        {
            // 1. Firebase
            Debug.Log("[AppContext] Verificando dependências do Firebase...");
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus != DependencyStatus.Available)
                throw new Exception($"[AppContext] Firebase dependencies unavailable: {dependencyStatus}");
            Debug.Log("[AppContext] Firebase disponível.");

            // 2. GetComponents
            var authRepo             = GetComponent<AuthenticationRepository>();
            var firestoreRepo        = GetComponent<FirestoreRepository>();
            var storageRepo          = GetComponent<StorageRepository>();
            var statsManager         = GetComponent<DatabaseStatisticsManager>();
            var navigationMgr        = GetComponent<NavigationManager>();
            var sceneDataMgr         = GetComponent<SceneDataManager>();
            var databaseMgr          = GetComponent<DatabaseManager>();         // SQLite — remover após migração
            var imageCacheSvc        = GetComponent<ImageCacheService>();
            var imageUploadSvc       = GetComponent<ImageUploadService>();
            var answeredQuestionsMgr = GetComponent<AnsweredQuestionsManager>();
            var playerLevelMgr       = GetComponent<PlayerLevelService>();
            var userSyncSvc          = GetComponent<UserSyncService>();

            // 3. Validações
            if (authRepo == null)             throw new Exception("[AppContext] AuthenticationRepository não encontrado.");
            if (firestoreRepo == null)        throw new Exception("[AppContext] FirestoreRepository não encontrado.");
            if (databaseMgr == null)          throw new Exception("[AppContext] DatabaseManager não encontrado.");
            if (imageCacheSvc == null)        throw new Exception("[AppContext] ImageCacheService não encontrado.");
            if (navigationMgr == null)        throw new Exception("[AppContext] NavigationManager não encontrado.");
            if (sceneDataMgr == null)         throw new Exception("[AppContext] SceneDataManager não encontrado.");
            if (statsManager == null)         throw new Exception("[AppContext] DatabaseStatisticsManager não encontrado.");
            if (storageRepo == null)          throw new Exception("[AppContext] StorageRepository não encontrado.");
            if (imageUploadSvc == null)       throw new Exception("[AppContext] ImageUploadService não encontrado.");
            if (answeredQuestionsMgr == null) throw new Exception("[AppContext] AnsweredQuestionsManager não encontrado.");
            if (playerLevelMgr == null)       throw new Exception("[AppContext] PlayerLevelService não encontrado.");
            if (userSyncSvc == null)          throw new Exception("[AppContext] UserSyncService não encontrado. Adicione o componente ao GameObject App.");

            // 4. Inicializa Firebase
            await authRepo.InitializeAsync();
            firestoreRepo.Initialize();
            storageRepo.Initialize();

            // 5. Injeção de dependências cruzadas
            authRepo.InjectDependencies(firestoreRepo);
            storageRepo.InjectDependencies(authRepo);
            imageUploadSvc.InjectDependencies(storageRepo);
            navigationMgr.InjectDependencies(sceneDataMgr);

            // 6. LiteDB — instanciado diretamente (não é MonoBehaviour)
            var liteDB = new LiteDBService();

            // 7. UserSyncService recebe dependências via injeção
            userSyncSvc.Initialize(liteDB, firestoreRepo);

            // 8. Estatísticas
            await statsManager.Initialize();

            // 9. Expõe interfaces
            Auth              = authRepo;
            Firestore         = firestoreRepo;
            Storage           = storageRepo;
            Statistics        = statsManager;
            Navigation        = navigationMgr;
            SceneData         = sceneDataMgr;
            LocalDatabase     = databaseMgr;    // SQLite — remover após migração
            LocalDB           = liteDB;
            UserSync          = userSyncSvc;
            ImageCache        = imageCacheSvc;
            ImageUpload       = imageUploadSvc;
            AnsweredQuestions = answeredQuestionsMgr;
            PlayerLevel       = playerLevelMgr;

            IsReady = true;
            OnReady?.Invoke();
            Debug.Log("[AppContext] Todos os serviços prontos.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AppContext] Falha na inicialização: {e.Message}\n{e.StackTrace}");
            if (e.InnerException != null)
                Debug.LogError($"[AppContext] InnerException: {e.InnerException.Message}");
            IsReady = false;
            throw;
        }
    }

    public static void OverrideForTests(
        IFirestoreRepository      firestore         = null,
        IAuthRepository           auth              = null,
        IStorageRepository        storage           = null,
        IStatisticsProvider       statistics        = null,
        INavigationService        navigation        = null,
        ISceneDataService         sceneData         = null,
        IDatabaseManager          localDatabase     = null,
        ILiteDBService            localDB           = null,
        IUserSyncService          userSync          = null,
        IImageCacheService        imageCache        = null,
        IImageUploadService       imageUpload       = null,
        IAnsweredQuestionsManager answeredQuestions = null,
        IPlayerLevelService       playerLevel       = null)
    {
        if (firestore         != null) Firestore         = firestore;
        if (auth              != null) Auth              = auth;
        if (storage           != null) Storage           = storage;
        if (statistics        != null) Statistics        = statistics;
        if (navigation        != null) Navigation        = navigation;
        if (sceneData         != null) SceneData         = sceneData;
        if (localDatabase     != null) LocalDatabase     = localDatabase;
        if (localDB           != null) LocalDB           = localDB;
        if (userSync          != null) UserSync          = userSync;
        if (imageCache        != null) ImageCache        = imageCache;
        if (imageUpload       != null) ImageUpload       = imageUpload;
        if (answeredQuestions != null) AnsweredQuestions = answeredQuestions;
        if (playerLevel       != null) PlayerLevel       = playerLevel;
        IsReady = true;
    }

    private void OnDestroy()
    {
        if (LocalDB is IDisposable disposable)
            disposable.Dispose();
    }
}