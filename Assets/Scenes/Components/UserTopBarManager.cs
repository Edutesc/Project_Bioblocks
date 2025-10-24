using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UserTopBarManager : BarsManager
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

    private static UserTopBarManager _instance;
    private float lastVerificationTime = 0f;
    private string pendingAvatarUrl = null;

    protected override string BarName => "PersistentUserTopBar";
    protected override string BarChildName => "TopBar";

    public static UserTopBarManager Instance => _instance;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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

    protected override void OnStart()
    {
        UserDataStore.OnUserDataChanged += OnUserDataChanged;
        UpdateFromCurrentUserData();
    }

    protected override void OnCleanup()
    {
        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete -= OnNavigationComplete;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;

        if (_instance == this)
        {
            _instance = null;
        }
    }

    protected override void RegisterWithNavigationManager()
    {
        base.RegisterWithNavigationManager();

        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete += OnNavigationComplete;
        }
    }

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

    protected override void OnEnable()
    {
        base.OnEnable();

        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete += OnNavigationComplete;
        }

        RefreshPendingAvatar();
    }

    private void OnDisable()
    {
        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.OnNavigationComplete -= OnNavigationComplete;
        }
    }

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
            int streak = 5;
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
}