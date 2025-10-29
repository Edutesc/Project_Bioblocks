using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System.Threading.Tasks;

public class UserHeaderManager : BarsManager
{
    [Header("Elementos da User TopBar")]
    [SerializeField] private RawImage avatarImage;
    [SerializeField] private Image avatarImageBackground;
    [SerializeField] private ProfileImageLoader avatarManager;
    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private Image starImage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image xpBar;
    [SerializeField] private Image xpBarFill;
    [SerializeField] private TMP_Text xpBarText;
    [SerializeField] private Image bonusBarImage;
    [SerializeField] private Image fireIcon;
    [SerializeField] private TMP_Text bonusText;
    [SerializeField] private Image levelBarImage;
    [SerializeField] private TMP_Text levelText;

    [Header("Player Level UI")]
    [SerializeField] private Image playerLevelContainer;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private Image playerLevelBackground;
    [SerializeField] private Image playerLevelProgressBar;
    [SerializeField] private TextMeshProUGUI playerLevelProgressText;

    [Header("Level Colors (opcional)")]
    [SerializeField] private Color[] levelColors = new Color[]
    {
        new Color(0.85f, 0.75f, 0.95f),  // Level 1 - Roxo claro (como na imagem)
        new Color(0.4f, 0.8f, 1.0f),     // Level 2 - Azul ciano vibrante
        new Color(0.2f, 0.9f, 0.95f),    // Level 3 - Azul turquesa brilhante
        new Color(0.3f, 1.0f, 0.4f),     // Level 4 - Verde limão
        new Color(1.0f, 0.95f, 0.3f),    // Level 5 - Amarelo brilhante
        new Color(1.0f, 0.75f, 0.2f),    // Level 6 - Laranja vibrante
        new Color(1.0f, 0.5f, 0.2f),     // Level 7 - Laranja forte
        new Color(1.0f, 0.3f, 0.3f),     // Level 8 - Vermelho coral
        new Color(1.0f, 0.2f, 0.6f),     // Level 9 - Rosa pink
        new Color(1.0f, 0.85f, 0.0f)     // Level 10 - Dourado brilhante
    };

    [Header("Elementos de Bônus Timer")]
    [SerializeField] private GameObject bonusTimerContainer;
    [SerializeField] private TextMeshProUGUI bonusTimerText;
    [SerializeField] private Image bonusTimerBackgroundImage;

    [Header("Configurações de Bônus")]
    [SerializeField] private float updateInterval = 1f;
    [SerializeField] private float firestoreSyncInterval = 30f;

    [Header("Persistência")]
    [SerializeField]
    private List<string> scenesWithoutUserTopBar = new List<string>()
    {
        "LoginView",
        "RegisterView",
        "ResetDatabaseView"
    };

    [Header("Cenas com User TopBar")]
    [SerializeField]
    private List<string> scenesWithUserTopBar = new List<string>()
    {
        "ProfileScene",
        "QuestionScene",
        "HomeScene"
    };

    // Bonus Management
    private UserBonusManager userBonusManager;
    private QuestionSceneBonusManager questionSceneBonusManager;
    private List<BonusInfo> activeBonuses = new List<BonusInfo>();
    private string userId;
    private bool isBonusSystemInitialized = false;
    private float lastFirestoreUpdateTime = 0f;
    private Coroutine timerCoroutine;

    // Bonus display names
    private Dictionary<string, string> bonusDisplayNames = new Dictionary<string, string>()
    {
        { "specialBonus", "Bônus XP Triplicada" },
        { "listCompletionBonus", "Bônus XP Triplicada" },
        { "persistenceBonus", "Bônus XP Triplicada" },
        { "correctAnswerBonus", "Bônus XP Dobrada" },
        { "specialBonusPro", "Bônus XP Triplicada" },
        { "listCompletionBonusPro", "Bônus XP Triplicada" },
        { "persistenceBonusPro", "Bônus XP Triplicada" }
    };

    // Singleton
    private static UserHeaderManager _instance;
    private float lastVerificationTime = 0f;
    private string pendingAvatarUrl = null;

    protected override string BarName => "PersistentUserTopBar";
    protected override string BarChildName => "TopBar";

    public static UserHeaderManager Instance => _instance;

    // Events
    public event Action<int> OnBonusMultiplierUpdated;

    #region Unity Lifecycle

    protected override void ConfigureSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    protected override void OnAwake()
    {
        base.scenesWithoutBar = new List<string>(scenesWithoutUserTopBar);

        foreach (var scene in scenesWithoutUserTopBar)
        {
            if (!base.scenesWithoutBar.Contains(scene))
            {
                base.scenesWithoutBar.Add(scene);
            }
        }

        InitializeAvatarManager();
        InitializeBonusSystem();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnStart()
    {
        UserDataStore.OnUserDataChanged += OnUserDataChanged;
        UpdateFromCurrentUserData();
        InitializeBonusManagement();

        if (PlayerLevelManager.Instance != null)
        {
            PlayerLevelManager.OnLevelChanged += OnPlayerLevelChanged;
            PlayerLevelManager.OnLevelProgressUpdated += OnPlayerLevelProgressUpdated;
            UpdatePlayerLevelUI();
        }
    }

    protected override void OnCleanup()
    {
        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete -= OnNavigationComplete;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;

        StopBonusTimer();
        SaveBonusStateToFirestore();

        if (PlayerLevelManager.Instance != null)
        {
            PlayerLevelManager.OnLevelChanged -= OnPlayerLevelChanged;
            PlayerLevelManager.OnLevelProgressUpdated -= OnPlayerLevelProgressUpdated;
        }
        
        if (_instance == this)
        {
            _instance = null;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete += OnNavigationComplete;
        }

        RefreshPendingAvatar();

        if (isBonusSystemInitialized)
        {
            RefreshActiveBonuses();
        }
    }

    private void OnDisable()
    {
        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete -= OnNavigationComplete;
        }

        SaveBonusStateToFirestore();
    }

    #endregion

    #region Bonus System Initialization

    private void InitializeBonusSystem()
    {
        userBonusManager = new UserBonusManager();
        questionSceneBonusManager = new QuestionSceneBonusManager();

        if (bonusTimerContainer != null)
        {
            bonusTimerContainer.SetActive(false);
        }

        isBonusSystemInitialized = true;
        Debug.Log("UserTopBarManager: Sistema de bônus inicializado");
    }

    private async void InitializeBonusManagement()
    {
        if (UserDataStore.CurrentUserData != null && !string.IsNullOrEmpty(UserDataStore.CurrentUserData.UserId))
        {
            userId = UserDataStore.CurrentUserData.UserId;
            await FetchAndDisplayAllActiveBonuses();

            if (activeBonuses.Count > 0)
            {
                StartBonusTimer();
            }
        }
        else
        {
            Debug.LogWarning("UserTopBarManager: Usuário não está logado");
        }
    }

    #endregion

    #region Bonus Fetching and Display

    public async void RefreshActiveBonuses()
    {
        if (isBonusSystemInitialized && !string.IsNullOrEmpty(userId))
        {
            await FetchAndDisplayAllActiveBonuses();
        }
    }

    private async Task FetchAndDisplayAllActiveBonuses()
    {
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("UserTopBarManager: UserId não definido");
            return;
        }

        try
        {
            activeBonuses.Clear();
            List<Dictionary<string, object>> activeSceneBonuses = await questionSceneBonusManager.GetActiveBonuses(userId);
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (var bonusDict in activeSceneBonuses)
            {
                if (bonusDict.ContainsKey("BonusType") &&
                    bonusDict.ContainsKey("ExpirationTimestamp") &&
                    bonusDict.ContainsKey("BonusMultiplier"))
                {
                    string bonusType = bonusDict["BonusType"].ToString();
                    long expirationTimestamp = Convert.ToInt64(bonusDict["ExpirationTimestamp"]);
                    int multiplier = Convert.ToInt32(bonusDict["BonusMultiplier"]);

                    if (expirationTimestamp > currentTimestamp)
                    {
                        float remainingTime = expirationTimestamp - currentTimestamp;
                        string displayName = bonusDisplayNames.ContainsKey(bonusType) ? bonusDisplayNames[bonusType] : bonusType;

                        activeBonuses.Add(new BonusInfo
                        {
                            bonusName = bonusType,
                            remainingTime = remainingTime,
                            multiplier = multiplier,
                            displayName = displayName
                        });
                    }
                }
            }

            UpdateBonusUI();
            OnBonusMultiplierUpdated?.Invoke(GetTotalMultiplier());

            if (activeBonuses.Count > 0 && timerCoroutine == null)
            {
                StartBonusTimer();
            }
            else if (activeBonuses.Count == 0 && timerCoroutine != null)
            {
                StopBonusTimer();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"UserTopBarManager: Erro ao buscar bônus ativos: {e.Message}");
        }
    }

    private void UpdateBonusUI()
    {
        if (activeBonuses.Count == 0)
        {
            if (bonusTimerContainer != null)
            {
                bonusTimerContainer.SetActive(false);
            }
            return;
        }

        if (bonusTimerContainer != null)
        {
            bonusTimerContainer.SetActive(true);
        }

        int totalMultiplier = GetTotalMultiplier();
        BonusInfo earliestExpiringBonus = activeBonuses.OrderBy(b => b.remainingTime).FirstOrDefault();

        if (earliestExpiringBonus != null && bonusTimerText != null)
        {
            int minutes = Mathf.FloorToInt(earliestExpiringBonus.remainingTime / 60);
            int seconds = Mathf.FloorToInt(earliestExpiringBonus.remainingTime % 60);

            if (activeBonuses.Count > 1)
            {
                bonusTimerText.text = $"{minutes:00}:{seconds:00}";
            }
            else
            {
                string bonusName = earliestExpiringBonus.displayName;
                if (bonusName.StartsWith("Bônus "))
                {
                    bonusName = bonusName.Substring(6);
                }

                bonusTimerText.text = $"{minutes:00}:{seconds:00}";
            }
        }
    }

    #endregion

    #region Bonus Timer Management

    private void StartBonusTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        timerCoroutine = StartCoroutine(UpdateTimersCoroutine());
        Debug.Log("UserTopBarManager: Timer de bônus iniciado");
    }

    private void StopBonusTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
            Debug.Log("UserTopBarManager: Timer de bônus parado");
        }
    }

    private IEnumerator UpdateTimersCoroutine()
    {
        while (true)
        {
            bool anyBonusActive = false;
            List<BonusInfo> expiredBonuses = new List<BonusInfo>();

            foreach (var bonus in activeBonuses)
            {
                bonus.remainingTime -= updateInterval;

                if (bonus.remainingTime <= 0)
                {
                    expiredBonuses.Add(bonus);
                }
                else
                {
                    anyBonusActive = true;
                }
            }

            foreach (var expiredBonus in expiredBonuses)
            {
                activeBonuses.Remove(expiredBonus);
                Debug.Log($"UserTopBarManager: Bônus {expiredBonus.bonusName} expirou");
            }

            UpdateBonusUI();

            if (expiredBonuses.Count > 0)
            {
                OnBonusMultiplierUpdated?.Invoke(GetTotalMultiplier());
            }

            if (!anyBonusActive && activeBonuses.Count == 0)
            {
                if (bonusTimerContainer != null)
                {
                    bonusTimerContainer.SetActive(false);
                }
                timerCoroutine = null;
                yield break;
            }

            if (Time.time - lastFirestoreUpdateTime > firestoreSyncInterval)
            {
                SaveBonusStateToFirestore();
                lastFirestoreUpdateTime = Time.time;
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }

    #endregion

    #region Firestore Synchronization

    private async void SaveBonusStateToFirestore()
    {
        if (string.IsNullOrEmpty(userId) || activeBonuses.Count == 0)
        {
            return;
        }

        try
        {
            List<BonusType> bonusList = await userBonusManager.GetUserBonuses(userId);
            bool hasUserBonusChanges = false;

            foreach (var bonus in activeBonuses)
            {
                BonusType bonusToUpdate = bonusList.FirstOrDefault(b => b.BonusName == bonus.bonusName && b.IsBonusActive);
                if (bonusToUpdate != null)
                {
                    bonusToUpdate.SetExpirationFromDuration(bonus.remainingTime);
                    hasUserBonusChanges = true;
                }
            }

            if (hasUserBonusChanges)
            {
                await userBonusManager.SaveBonusList(userId, bonusList);
            }

            List<string> questionSceneBonusTypes = activeBonuses
                .Where(b => b.bonusName == "correctAnswerBonus" || b.bonusName == "specialBonus")
                .Select(b => b.bonusName)
                .ToList();

            foreach (string bonusType in questionSceneBonusTypes)
            {
                BonusInfo bonusInfo = activeBonuses.FirstOrDefault(b => b.bonusName == bonusType);
                if (bonusInfo != null)
                {
                    await questionSceneBonusManager.UpdateExpirationTimestamp(userId, bonusInfo.remainingTime);
                }
            }

            Debug.Log("UserTopBarManager: Estado de bônus salvo no Firestore");
        }
        catch (Exception e)
        {
            Debug.LogError($"UserTopBarManager: Erro ao salvar estado no Firestore: {e.Message}");
        }
    }

    #endregion

    #region Public Bonus Methods

    public int GetTotalMultiplier()
    {
        if (activeBonuses.Count == 0)
        {
            return 1;
        }

        int totalMultiplier = 0;

        foreach (var bonus in activeBonuses)
        {
            totalMultiplier += bonus.multiplier;
        }

        return totalMultiplier;
    }

    public int ApplyTotalBonus(int baseValue)
    {
        return baseValue * GetTotalMultiplier();
    }

    public bool IsAnyBonusActive()
    {
        return activeBonuses.Count > 0;
    }

    public bool IsBonusActive(string bonusType)
    {
        return activeBonuses.Any(b => b.bonusName == bonusType);
    }

    public void AddActiveBonus(string bonusType, float durationInSeconds, int multiplier)
    {
        if (string.IsNullOrEmpty(bonusType) || durationInSeconds <= 0 || multiplier <= 0)
        {
            return;
        }

        string displayName = bonusDisplayNames.ContainsKey(bonusType) ? bonusDisplayNames[bonusType] : bonusType;

        activeBonuses.Add(new BonusInfo
        {
            bonusName = bonusType,
            remainingTime = durationInSeconds,
            multiplier = multiplier,
            displayName = displayName
        });

        UpdateBonusUI();
        OnBonusMultiplierUpdated?.Invoke(GetTotalMultiplier());

        if (timerCoroutine == null)
        {
            StartBonusTimer();
        }

        Debug.Log($"UserTopBarManager: Bônus {bonusType} adicionado (duração: {durationInSeconds}s, multiplicador: {multiplier}x)");
    }

    #endregion

    #region Avatar Management

    private void InitializeAvatarManager()
    {
        if (avatarManager == null)
        {
            avatarManager = GetComponentInChildren<ProfileImageLoader>();
            if (avatarManager == null)
            {
                Debug.LogWarning("ProfileImageLoader não encontrado! Procurando RawImage para criar...");

                if (avatarImage != null)
                {
                    var avatarParent = avatarImage.transform.parent;
                    if (avatarParent != null)
                    {
                        avatarManager = avatarParent.gameObject.AddComponent<ProfileImageLoader>();
                        avatarManager.SetImageContent(avatarImage);
                        Debug.Log("ProfileImageLoader criado e configurado automaticamente");
                    }
                }
            }
        }

        if (avatarManager != null && avatarImage != null)
        {
            avatarManager.SetImageContent(avatarImage);
        }
    }

    #endregion

    #region Scene Management

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneBeingLoaded = true;
        currentScene = scene.name;
        Invoke(nameof(HandleSceneLoadComplete), 0.1f);
    }

    private void HandleSceneLoadComplete()
    {
        UpdateBarState(currentScene);
        isSceneBeingLoaded = false;
        RefreshPendingAvatar();
    }

    protected override void OnSceneChangedSpecific(string sceneName)
    {
        if (scenesWithUserTopBar.Contains(sceneName))
        {
            EnsureUserTopBarVisibilityInScene(sceneName);
        }
    }

    private void OnNavigationComplete(string sceneName)
    {
        if (Time.time - lastVerificationTime < 0.5f) return;
        lastVerificationTime = Time.time;
        UpdateBarState(sceneName);
        RefreshPendingAvatar();
    }

    protected override void RegisterWithNavigationManager()
    {
        base.RegisterWithNavigationManager();

        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete += OnNavigationComplete;
        }
    }

    #endregion

    #region User Data Management

    private void OnUserDataChanged(UserData userData)
    {
        if (userData != null)
        {
            UpdateUserInfoDisplay(userData);
        }
    }

    private void UpdateUserInfoDisplay(UserData userData)
    {
        if (userNameText != null)
        {
            userNameText.text = userData.NickName;
        }

        if (scoreText != null)
        {
            scoreText.text = $"{userData.WeekScore} pontos";
        }

        if (avatarManager != null)
        {
            if (avatarManager.gameObject.activeInHierarchy)
            {
                avatarManager.LoadProfileImage(userData.ProfileImageUrl);
                pendingAvatarUrl = null;
            }
            else
            {
                Debug.Log($"[UserTopBarManager] AvatarManager inativo. Armazenando URL para carregar quando ativado: {userData.ProfileImageUrl}");
                pendingAvatarUrl = userData.ProfileImageUrl;
            }
        }
        else
        {
            Debug.LogWarning("ProfileImageLoader não está disponível para carregar a imagem");
        }

        if (xpBarText != null && xpBarFill != null)
        {
            int currentXP = userData.WeekScore;
            int maxXP = 100;
            int nextLevel = 2;

            xpBarText.text = $"{currentXP} de {maxXP} até o Nível {nextLevel}";

            float fillAmount = (float)currentXP / maxXP;
            xpBarFill.fillAmount = fillAmount;
        }

        if (bonusText != null)
        {
            int streak = 2;
            bonusText.text = $"x{streak}";
        }

        if (levelText != null)
        {
            int currentLevel = 1;
            levelText.text = currentLevel.ToString();
        }
    }

    private void RefreshPendingAvatar()
    {
        if (!string.IsNullOrEmpty(pendingAvatarUrl) && avatarManager != null)
        {
            if (avatarManager.gameObject.activeInHierarchy)
            {
                Debug.Log($"[UserTopBarManager] Carregando avatar pendente: {pendingAvatarUrl}");
                string urlToLoad = pendingAvatarUrl;
                pendingAvatarUrl = null;
                avatarManager.LoadProfileImage(urlToLoad);
            }
        }
    }

    protected override void UpdateBarState(string sceneName)
    {
        base.UpdateBarState(sceneName);
        RefreshPendingAvatar();
    }

    private void UpdateFromCurrentUserData()
    {
        UserData currentUserData = UserDataStore.CurrentUserData;
        if (currentUserData != null)
        {
            UpdateUserInfoDisplay(currentUserData);
        }
    }

    protected override void UpdateButtonVisibility(string sceneName)
    {
    }

    #endregion

    #region Public User Info Methods

    public void EnsureUserTopBarVisibilityInScene(string sceneName)
    {
        RemoveSceneWithoutBar(sceneName);

        if (currentScene == sceneName)
        {
            gameObject.SetActive(true);
            EnsureBarIntegrity();
            RefreshPendingAvatar();
        }
    }

    public void SetUserName(string name)
    {
        if (userNameText != null)
        {
            userNameText.text = name;
        }
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score} pontos";
        }
    }

    public void SetXPProgress(int current, int max, int nextLevel)
    {
        if (xpBarText != null)
        {
            xpBarText.text = $"{current} de {max} até o Nível {nextLevel}";
        }

        if (xpBarFill != null)
        {
            float fillAmount = (float)current / max;
            xpBarFill.fillAmount = fillAmount;
        }
    }

    public void SetStreak(int streak)
    {
        if (bonusText != null)
        {
            bonusText.text = $"x{streak}";
        }
    }

    public void SetLevel(int level)
    {
        if (levelText != null)
        {
            levelText.text = level.ToString();
        }
    }

    public void UpdateAvatarFromUrl(string imageUrl)
    {
        if (avatarManager != null)
        {
            if (avatarManager.gameObject.activeInHierarchy)
            {
                avatarManager.LoadProfileImage(imageUrl);
            }
            else
            {
                Debug.Log($"[UserTopBarManager] Armazenando URL para carregar quando ativado: {imageUrl}");
                pendingAvatarUrl = imageUrl;
            }
        }
        else
        {
            Debug.LogWarning("ProfileImageLoader não está disponível");
        }
    }

    public void RefreshUserAvatar()
    {
        if (avatarManager != null)
        {
            avatarManager.LoadFromCurrentUser();
        }
    }

    #endregion

    #region Scene Configuration Methods

    public void AddSceneWithUserTopBar(string sceneName)
    {
        if (!scenesWithUserTopBar.Contains(sceneName))
        {
            scenesWithUserTopBar.Add(sceneName);
        }
        RemoveSceneWithoutBar(sceneName);
    }

    public void RemoveSceneWithUserTopBar(string sceneName)
    {
        if (scenesWithUserTopBar.Contains(sceneName))
        {
            scenesWithUserTopBar.Remove(sceneName);
        }
        AddSceneWithoutBar(sceneName);
    }

    public void AddSceneWithoutUserTopBar(string sceneName)
    {
        AddSceneWithoutBar(sceneName);
    }

    public void RemoveSceneWithoutUserTopBar(string sceneName)
    {
        RemoveSceneWithoutBar(sceneName);
    }

    private void OnPlayerLevelChanged(int oldLevel, int newLevel)
    {
        Debug.Log($"[UserHeaderManager] Level mudou: {oldLevel} → {newLevel}");
        UpdatePlayerLevelUI();
    }

    private void OnPlayerLevelProgressUpdated(int questionsAnswered)
    {
        UpdatePlayerLevelUI();
    }

    private void UpdatePlayerLevelUI()
    {
        if (PlayerLevelManager.Instance == null) return;

        int currentLevel = PlayerLevelManager.Instance.GetCurrentLevel();
        float progress = PlayerLevelManager.Instance.GetProgressInCurrentLevel();
        int questionsAnswered = PlayerLevelManager.Instance.GetTotalValidAnswered();
        int questionsUntilNext = PlayerLevelManager.Instance.GetQuestionsUntilNextLevel();
        int totalQuestions = PlayerLevelManager.Instance.GetTotalQuestionsInAllDatabanks();

        if (playerLevelText != null)
        {
            playerLevelText.text = currentLevel.ToString();
        }

        if (playerLevelBackground != null && levelColors != null && levelColors.Length >= 10)
        {
            int colorIndex = Mathf.Clamp(currentLevel - 1, 0, 9);
            playerLevelBackground.color = levelColors[colorIndex];
        }

        if (playerLevelProgressBar != null)
        {
            playerLevelProgressBar.fillAmount = progress;
        }

        if (playerLevelProgressText != null)
        {
            if (currentLevel >= 10)
            {
                playerLevelProgressText.text = "MÁXIMO!";
            }
            else
            {
                int nextLevelTotal = questionsAnswered + questionsUntilNext;
                playerLevelProgressText.text = $"{questionsAnswered}/{nextLevelTotal}";
            }
        }
    }

    #endregion

    #region Nested Classes

    private class BonusInfo
    {
        public string bonusName;
        public float remainingTime;
        public int multiplier;
        public string displayName;
    }

    #endregion
}