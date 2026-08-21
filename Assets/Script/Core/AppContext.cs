using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Firestore;
using System;

public class AppContext : MonoBehaviour
{
    private static AppContext _instance;
    public static event Action OnReady;

    // ── Repositórios/serviços Firestore específicos ────────────────────────────
    public static IFirestoreUserRepository          FirestoreUsers          { get; private set; }
    public static INicknameRepository               Nicknames               { get; private set; }
    public static IFirestoreQuestionStatsRepository QuestionStatsRemote     { get; private set; }
    public static IUserBonusRepository              UserBonus               { get; private set; }
    public static IUserRealtimeListenerService      UserRealtimeListeners   { get; private set; }
    public static IFirestoreRankingRepository       RankingRemote           { get; private set; }
    public static IRankingLocalRepository           RankingLocal            { get; private set; }

    // ── Fachada legada Firestore ───────────────────────────────────────────────
    // Mantida temporariamente para compatibilidade com classes que ainda recebem
    // IFirestoreRepository, como AuthenticationRepository, UserDataSyncService e
    // AvatarSelectionService.
    public static IFirestoreRepository              Firestore               { get; private set; }

    // ── Serviços existentes ────────────────────────────────────────────────────
    public static IAuthRepository                   Auth                    { get; private set; }
    public static IStatisticsProvider               Statistics              { get; private set; }
    public static INavigationService                Navigation              { get; private set; }
    public static ISceneDataService                 SceneData               { get; private set; }
    public static ILiteDBManager                    LocalDatabase           { get; private set; }
    public static IImageCacheService                ImageCache              { get; private set; }
    public static IAnsweredQuestionsManager         AnsweredQuestions       { get; private set; }
    public static IPlayerLevelService               PlayerLevel             { get; private set; }
    public static IUserDataLocalRepository          UserDataLocal           { get; private set; }
    public static IUserDataSyncService              UserDataSync            { get; private set; }
    public static RankingSyncService                RankingSync             { get; private set; }
    public static ConnectivityMonitor               Connectivity            { get; private set; }
    public static IFirestoreQuestionRepository      QuestionFirestore       { get; private set; }
    public static IQuestionLocalRepository          QuestionLocal           { get; private set; }
    public static IQuestionSyncService              QuestionSync            { get; private set; }
    public static IQuestionSource                   QuestionSource          { get; private set; }
    public static IAvatarSelectionService           AvatarSelection         { get; private set; }
    public static IFirebaseStorageImageRepository   ImageStorage            { get; private set; }
    public static IImageLocalRepository             ImageLocal              { get; private set; }
    public static IImageSyncService                 ImageSync               { get; private set; }
    public static ITopicReviewManager               TopicReview             { get; private set; }

    public static bool IsReady { get; private set; }

    private async void Awake()
    {
        var _ = MainThreadDispatcher.Instance;

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

        // ── Preview Mode — bypassa TODA inicialização Firebase ────────────────
        var envCfg = EnvironmentConfig.Load();

        if (envCfg != null && envCfg.QuestionPreviewMode)
        {
            Debug.Log("[AppContext] questionPreviewMode=true — inicialização Firebase ignorada.");

            ClearImageServicesForPreviewMode();

            QuestionSource    = new HardcodedQuestionSource();
            AnsweredQuestions = new FakeAnsweredQuestionsManager();

            // Navegação não depende de Firebase — inicializa normalmente para
            // permitir retorno à PathwayScene a partir da QuestionScene.
            var navigationMgr = GetComponent<NavigationManager>();
            var sceneDataMgr  = GetComponent<SceneDataManager>();

            if (navigationMgr == null)
            {
                Debug.LogError("[AppContext] NavigationManager não encontrado no Preview Mode.");
            }
            else if (sceneDataMgr == null)
            {
                Debug.LogError("[AppContext] SceneDataManager não encontrado no Preview Mode.");
            }
            else
            {
                navigationMgr.InjectDependencies(sceneDataMgr);

                Navigation = navigationMgr;
                SceneData  = sceneDataMgr;

                Debug.Log("[AppContext] Preview mode — Navigation e SceneData inicializados.");
            }

            IsReady = true;
            OnReady?.Invoke();

            Debug.Log("[AppContext] Preview mode pronto.");
            return;
        }

        try
        {
            Debug.Log("[AppContext] Verificando dependências do Firebase...");

            var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
            var timeoutTask    = Task.Delay(10000);

            var completed = await Task.WhenAny(dependencyTask, timeoutTask);

            if (completed == timeoutTask)
                throw new Exception("Timeout ao verificar Firebase. Verifique sua conexão.");

            var dependencyStatus = await dependencyTask;

            if (dependencyStatus != DependencyStatus.Available)
                throw new Exception($"Firebase dependencies unavailable: {dependencyStatus}");

            Debug.Log("[AppContext] Firebase disponível.");

            FirebaseApp runtimeApp = FirebaseApp.DefaultInstance;
            Debug.Log(
                $"[AppContext] Firebase runtime: " +
                $"project={runtimeApp.Options.ProjectId}, appId={runtimeApp.Options.AppId}."
            );

            // Sessao e caches pertencem ao projeto Firebase que os criou.
            // A protecao roda antes da abertura do LiteDB e do uso de Auth.
            FirebaseEnvironmentGuard.Apply(envCfg.FirebaseEnvironment);

            // ── 0. App Check — deve ser o primeiro serviço inicializado ───────────
            Debug.Log("AppCheck IN");

#if UNITY_EDITOR
            // No Editor usamos um IAppCheckProviderFactory custom que lê o UUID
            // de debug de <projectRoot>/firebase_app_check_debug_token.txt e
            // chama exchangeDebugToken da App Check API diretamente — não
            // depende de env var, que não funciona no plugin nativo do Editor
            // macOS.
            var debugFactory = FileBackedDebugAppCheckProviderFactory.TryCreateFromFile();

            if (debugFactory != null)
            {
                Firebase.AppCheck.FirebaseAppCheck.SetAppCheckProviderFactory(debugFactory);
                Debug.Log("[AppContext] App Check Debug Provider (file-backed) configurado.");
            }
            else
            {
                Debug.LogWarning("[AppContext] firebase_app_check_debug_token.txt ausente. App Check NÃO ativo no Editor.");
            }
#elif UNITY_ANDROID
            Firebase.AppCheck.FirebaseAppCheck.SetAppCheckProviderFactory(
                Firebase.AppCheck.PlayIntegrityProviderFactory.Instance);
#elif UNITY_IOS
            Firebase.AppCheck.FirebaseAppCheck.SetAppCheckProviderFactory(
                Firebase.AppCheck.DeviceCheckProviderFactory.Instance);
#endif

            Debug.Log("[AppContext] AppCheck inicializado.");

            // ── Obtenção dos componentes ───────────────────────────────────────
            var authRepo              = GetComponent<AuthenticationRepository>();
            var firestoreRepo         = GetComponent<FirestoreRepository>();
            var statsManager          = GetComponent<DatabaseStatisticsManager>();
            var navigationMgr         = GetComponent<NavigationManager>();
            var sceneDataMgr          = GetComponent<SceneDataManager>();
            var liteDBMgr             = GetComponent<LiteDBManager>();
            var imageCacheSvc         = GetComponent<ImageCacheService>();
            var answeredQuestionsMgr  = GetComponent<AnsweredQuestionsManager>();
            var playerLevelMgr        = GetComponent<PlayerLevelService>();
            var userDataLocalRepo     = GetComponent<UserDataLocalRepository>();
            var userDataSyncSvc       = GetComponent<UserDataSyncService>();
            var rankingSyncSvc        = GetComponent<RankingSyncService>();
            var connectivityMonitor   = GetComponent<ConnectivityMonitor>();
            var questionFirestoreRepo = GetComponent<FirestoreQuestionRepository>();
            var questionLocalRepo     = GetComponent<QuestionLocalRepository>();
            var questionSyncSvc       = GetComponent<QuestionSyncService>();
            var avatarSelectionSvc    = GetComponent<AvatarSelectionService>();
            var firebaseStorageRepo   = GetComponent<FirebaseStorageImageRepository>();
            var imageLocalRepo        = GetComponent<ImageLocalRepository>();
            var imageSyncSvc          = GetComponent<ImageSyncService>();
            var topicReview           = GetComponent<TopicReviewManager>();
            var topicReviewRepository = GetComponent<TopicReviewRepository>();

            // ── Validações existentes ──────────────────────────────────────────
            if (authRepo == null) throw new Exception("[AppContext] AuthenticationRepository não encontrado.");
            if (firestoreRepo           == null) throw new Exception("[AppContext] FirestoreRepository não encontrado.");
            if (liteDBMgr               == null) throw new Exception("[AppContext] LiteDBManager não encontrado.");
            if (imageCacheSvc           == null) throw new Exception("[AppContext] ImageCacheService não encontrado.");
            if (navigationMgr           == null) throw new Exception("[AppContext] NavigationManager não encontrado.");
            if (sceneDataMgr            == null) throw new Exception("[AppContext] SceneDataManager não encontrado.");
            if (statsManager            == null) throw new Exception("[AppContext] DatabaseStatisticsManager não encontrado.");
            if (answeredQuestionsMgr    == null) throw new Exception("[AppContext] AnsweredQuestionsManager não encontrado.");
            if (playerLevelMgr          == null) throw new Exception("[AppContext] PlayerLevelService não encontrado.");
            if (userDataLocalRepo       == null) throw new Exception("[AppContext] UserDataLocalRepository não encontrado.");
            if (userDataSyncSvc         == null) throw new Exception("[AppContext] UserDataSyncService não encontrado.");
            if (rankingSyncSvc          == null) throw new Exception("[AppContext] RankingSyncService não encontrado.");
            if (connectivityMonitor     == null) throw new Exception("[AppContext] ConnectivityMonitor não encontrado.");
            if (questionFirestoreRepo   == null) throw new Exception("[AppContext] FirestoreQuestionRepository não encontrado.");
            if (questionLocalRepo       == null) throw new Exception("[AppContext] QuestionLocalRepository não encontrado.");
            if (questionSyncSvc         == null) throw new Exception("[AppContext] QuestionSyncService não encontrado.");
            if (avatarSelectionSvc      == null) throw new Exception("[AppContext] AvatarSelectionService não encontrado.");
            if (firebaseStorageRepo     == null) throw new Exception("[AppContext] FirebaseStorageImageRepository não encontrado.");
            if (imageLocalRepo          == null) throw new Exception("[AppContext] ImageLocalRepository não encontrado.");
            if (imageSyncSvc            == null) throw new Exception("[AppContext] ImageSyncService não encontrado.");
            if (topicReview             == null) throw new Exception("[AppContext] TopicReview não encontrado.");
            if (topicReviewRepository   == null) throw new Exception("[AppContext] TopicReviewRepository não encontrado.");

            // ── 1. LiteDB ──────────────────────────────────────────────────────
            liteDBMgr.Initialize();

            // ── 2. Firebase/Auth/Firestore ─────────────────────────────────────
            await authRepo.InitializeAsync();
            firestoreRepo.Initialize();
            topicReviewRepository.Initialize();

            FirebaseFirestore firestoreDb = FirebaseFirestore.DefaultInstance;
            var authGate = new FirebaseAuthGate();

            // ── 3. Repositórios Firestore específicos ──────────────────────────
            var firestoreUserRepository      = new FirestoreUserRepository(firestoreDb);
            var nicknameRepository           = new FirestoreNicknameRepository(firestoreDb);
            var questionStatsRepository      = new FirestoreQuestionStatsRepository(firestoreDb);
            var userBonusRepository          = new FirestoreUserBonusRepository(firestoreDb);
            var userRealtimeListenerService  = new UserRealtimeListenerService(firestoreDb);
            var rankingRemoteRepository      = new FirestoreRankingRepository(firestoreDb);
            var rankingLocalRepository       = new RankingLocalRepository(liteDBMgr);

            // ── 4. Dependências cruzadas Firebase ─────────────────────────────
            // Ainda usa a fachada IFirestoreRepository por compatibilidade.
            // Migração futura:
            //   - AuthenticationRepository pode receber IFirestoreUserRepository + INicknameRepository.
            //   - UserDataSyncService pode receber IFirestoreUserRepository.
            //   - AvatarSelectionService pode receber IFirestoreUserRepository.
            authRepo.InjectDependencies(
                firestoreUserRepository,
                nicknameRepository,
                userRealtimeListenerService,
                authGate
            );

            // ── 5. Dependências locais/usuário ─────────────────────────────────
            userDataLocalRepo.InjectDependencies(liteDBMgr);
            userDataSyncSvc.InjectDependencies(userDataLocalRepo, firestoreRepo, authGate);
            imageCacheSvc.InjectDependencies();
            avatarSelectionSvc.InjectDependencies(firestoreRepo, userDataLocalRepo);

            // ── 6. Ranking remoto + cache LiteDB ───────────────────────────────
            rankingSyncSvc.InjectDependencies(
                rankingRemoteRepository,
                rankingLocalRepository,
                connectivityMonitor
            );

            // ── 7. Pipeline de imagens (Storage + cache em disco) ──────────────
            // O AuthGate faz o prewarm aguardar request.auth != null antes de
            // bater no Storage (cuja rule exige usuário autenticado + App Check).
            firebaseStorageRepo.Initialize();
            imageLocalRepo.InjectDependencies(imageCacheSvc);
            imageSyncSvc.InjectDependencies(
                firebaseStorageRepo,
                imageLocalRepo,
                authGate
            );

            // ── 8. Dependências LiteDB/questões ────────────────────────────────
            questionFirestoreRepo.Initialize();
            questionLocalRepo.InjectDependencies(liteDBMgr);
            questionSyncSvc.InjectDependencies(
                questionFirestoreRepo,
                questionLocalRepo,
                imageSyncSvc,
                authGate
            );
            questionSyncSvc.RegisterAuthListener();

            // ── 9. Navegação ───────────────────────────────────────────────────
            navigationMgr.InjectDependencies(sceneDataMgr);

            // ── 10. Fonte de questões ──────────────────────────────────────────
            // Prod e Dev → FirestoreQuestionSource (Firestore + LiteDB).
            // A única diferença entre Prod e Dev é o projeto Firebase ao qual apontam.
            QuestionSync   = questionSyncSvc;
            QuestionSource = new FirestoreQuestionSource(questionSyncSvc);

            var firebaseEnv = EnvironmentConfig.Load()?.FirebaseEnvironment;

            Debug.Log($"[AppContext] {firebaseEnv} mode — QuestionSource: FirestoreQuestionSource.");

            bool hasLocalSession = authRepo.HasLocalSession();

            if (hasLocalSession)
            {
                Debug.Log("[AppContext] Usuário autenticado localmente — inicializando questões.");
            }
            else
            {
                Debug.Log("[AppContext] Sem sessão local — pulando sync de questões e estatísticas no bootstrap.");
            }

            // ── 11. Expõe serviços Firestore específicos ───────────────────────
            FirestoreUsers        = firestoreUserRepository;
            Nicknames             = nicknameRepository;
            QuestionStatsRemote   = questionStatsRepository;
            UserBonus             = userBonusRepository;
            UserRealtimeListeners = userRealtimeListenerService;
            RankingRemote         = rankingRemoteRepository;
            RankingLocal          = rankingLocalRepository;

            // ── 12. Expõe serviços existentes ──────────────────────────────────
            Auth              = authRepo;
            Firestore         = firestoreRepo;
            Statistics        = statsManager;
            Navigation        = navigationMgr;
            SceneData         = sceneDataMgr;
            LocalDatabase     = liteDBMgr;
            ImageCache        = imageCacheSvc;
            AnsweredQuestions = answeredQuestionsMgr;
            PlayerLevel       = playerLevelMgr;
            UserDataLocal     = userDataLocalRepo;
            UserDataSync      = userDataSyncSvc;
            RankingSync       = rankingSyncSvc;
            Connectivity      = connectivityMonitor;
            QuestionFirestore = questionFirestoreRepo;
            QuestionLocal     = questionLocalRepo;
            QuestionSync      = questionSyncSvc;
            AvatarSelection   = avatarSelectionSvc;
            ImageStorage      = firebaseStorageRepo;
            ImageLocal        = imageLocalRepo;
            ImageSync         = imageSyncSvc;
            TopicReview       = topicReview;

            IsReady = true;
            OnReady?.Invoke();

            Debug.Log("[AppContext] Todos os serviços prontos.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AppContext] Falha na inicialização: {e.Message}");
            IsReady = false;
            throw;
        }
    }

    private static void ClearImageServicesForPreviewMode()
    {
        ImageCache   = null;
        ImageStorage = null;
        ImageLocal   = null;
        ImageSync    = null;
    }

    private static void ValidateFirebaseEnvironment(FirebaseEnvironment env)
    {
        string expectedProjectId = env == FirebaseEnvironment.Prod
            ? "microlearning-33132"
            : "microlearning-dev-79c0c";

#if UNITY_EDITOR
        string configPath = System.IO.Path.Combine(Application.dataPath, "google-services.json");

        if (System.IO.File.Exists(configPath))
        {
            string content = System.IO.File.ReadAllText(configPath);

            if (!content.Contains(expectedProjectId))
            {
                throw new Exception(
                    $"Firebase {env} esperado ({expectedProjectId}) não encontrado em google-services.json."
                );
            }
        }
        else
        {
            throw new Exception("google-services.json não encontrado em Assets/");
        }
#endif

        Debug.Log($"[AppContext] Firebase {env} ({expectedProjectId}) validado.");
    }

    public static void OverrideForTests(
        IFirestoreRepository              firestore               = null,
        IFirestoreUserRepository          firestoreUsers          = null,
        INicknameRepository               nicknames               = null,
        IFirestoreQuestionStatsRepository questionStatsRemote     = null,
        IUserBonusRepository              userBonus               = null,
        IUserRealtimeListenerService      userRealtimeListeners   = null,
        IFirestoreRankingRepository       rankingRemote           = null,
        IRankingLocalRepository           rankingLocal            = null,
        RankingSyncService                rankingSync             = null,
        ConnectivityMonitor               connectivity            = null,
        IAuthRepository                   auth                    = null,
        IStatisticsProvider               statistics              = null,
        INavigationService                navigation              = null,
        ISceneDataService                 sceneData               = null,
        ILiteDBManager                    localDatabase           = null,
        IImageCacheService                imageCache              = null,
        IAnsweredQuestionsManager         answeredQuestions       = null,
        IPlayerLevelService               playerLevel             = null,
        IUserDataLocalRepository          userDataLocal           = null,
        IUserDataSyncService              userDataSync            = null,
        IFirestoreQuestionRepository      questionFirestore       = null,
        IQuestionLocalRepository          questionLocal           = null,
        IQuestionSyncService              questionSync            = null,
        IQuestionSource                   questionSource          = null,
        IAvatarSelectionService           avatarSelection         = null,
        IFirebaseStorageImageRepository   imageStorage            = null,
        IImageLocalRepository             imageLocal              = null,
        IImageSyncService                 imageSync               = null,
        ITopicReviewManager               topicReview             = null)
    {
        if (firestore             != null) Firestore             = firestore;
        if (firestoreUsers        != null) FirestoreUsers        = firestoreUsers;
        if (nicknames             != null) Nicknames             = nicknames;
        if (questionStatsRemote   != null) QuestionStatsRemote   = questionStatsRemote;
        if (userBonus             != null) UserBonus             = userBonus;
        if (userRealtimeListeners != null) UserRealtimeListeners = userRealtimeListeners;
        if (rankingRemote         != null) RankingRemote         = rankingRemote;
        if (rankingLocal          != null) RankingLocal          = rankingLocal;
        if (rankingSync           != null) RankingSync           = rankingSync;
        if (connectivity          != null) Connectivity          = connectivity;

        if (auth                  != null) Auth                  = auth;
        if (statistics            != null) Statistics            = statistics;
        if (navigation            != null) Navigation            = navigation;
        if (sceneData             != null) SceneData             = sceneData;
        if (localDatabase         != null) LocalDatabase         = localDatabase;
        if (imageCache            != null) ImageCache            = imageCache;
        if (answeredQuestions     != null) AnsweredQuestions     = answeredQuestions;
        if (playerLevel           != null) PlayerLevel           = playerLevel;
        if (userDataLocal         != null) UserDataLocal         = userDataLocal;
        if (userDataSync          != null) UserDataSync          = userDataSync;
        if (questionFirestore     != null) QuestionFirestore     = questionFirestore;
        if (questionLocal         != null) QuestionLocal         = questionLocal;
        if (questionSync          != null) QuestionSync          = questionSync;
        if (questionSource        != null) QuestionSource        = questionSource;
        if (avatarSelection       != null) AvatarSelection       = avatarSelection;
        if (imageStorage          != null) ImageStorage          = imageStorage;
        if (imageLocal            != null) ImageLocal            = imageLocal;
        if (imageSync             != null) ImageSync             = imageSync;
        if (topicReview           != null) TopicReview          = topicReview;

        IsReady = true;
    }
}
