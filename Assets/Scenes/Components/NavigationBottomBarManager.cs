using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NavigationBottomBarManager : BarsManager
{
    [System.Serializable]
    public class NavButton
    {
        public string buttonName;
        public string targetScene;
        public Button button;
        public Image normalIcon;
        public Image filledIcon;
    }

    [Header("Botões de Navegação")]
    [SerializeField] private NavButton homeButton;
    [SerializeField] private NavButton rankingButton;
    [SerializeField] private NavButton favoritesButton;
    [SerializeField] private NavButton medalsButton;
    [SerializeField] private NavButton profileButton;

    [Header("Persistência")]
    [SerializeField]
    private List<string> scenesWithoutBottomBar = new List<string>()
    {
        "LoginView",
        "RegisterView",
        "QuestionScene",
        "ResetDatabaseView"
    };

    private List<NavButton> allButtons = new List<NavButton>();
    private static NavigationBottomBarManager _instance;
    protected override string BarName => "PersistentBottomBar";
    protected override string BarChildName => "BottomBar";

    public static NavigationBottomBarManager Instance
    {
        get { return _instance; }
    }

    protected override void ConfigureSingleton()
    {

        if (_instance != null && _instance != this)
        {
            isDuplicate = true;
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    protected override void OnAwake()
    {
        base.scenesWithoutBar.Clear();
        foreach (var scene in scenesWithoutBottomBar)
        {
            base.scenesWithoutBar.Add(scene);
        }

        // ⭐ Remove listener anterior (caso exista) para evitar duplicatas
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // ⭐ Registra o listener
        SceneManager.sceneLoaded += OnSceneLoaded;

        InitializeButtons();
    }

    private void InitializeButtons()
    {
        allButtons.Clear();
        if (homeButton.button != null) allButtons.Add(homeButton);
        if (rankingButton.button != null) allButtons.Add(rankingButton);
        if (favoritesButton.button != null) allButtons.Add(favoritesButton);
        if (medalsButton.button != null) allButtons.Add(medalsButton);
        if (profileButton.button != null) allButtons.Add(profileButton);

        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        foreach (var buttonInfo in allButtons)
        {
            if (buttonInfo.button != null)
            {
                buttonInfo.button.onClick.RemoveAllListeners();
                string targetButtonName = buttonInfo.buttonName;
                string targetSceneName = buttonInfo.targetScene;

                buttonInfo.button.onClick.AddListener(() =>
                {
                    if (NavigationManager.Instance != null)
                    {
                        NavigationManager.Instance.NavigateTo(targetSceneName);
                    }
                    else
                    {
                        StartCoroutine(RetryNavigation(targetSceneName, targetButtonName));
                    }
                });
            }
        }
    }

    private System.Collections.IEnumerator RetryNavigation(string sceneName, string buttonName)
    {
        float timeout = 2f;
        float elapsed = 0f;

        while (NavigationManager.Instance == null && elapsed < timeout)
        {
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        if (NavigationManager.Instance != null)
        {
            NavigationManager.Instance.NavigateTo(sceneName);
        }
    }

    protected override void AdjustVisibilityForCurrentScene()
    {
        bool shouldShowBar = !scenesWithoutBar.Contains(currentScene);

        Transform barChild = transform.Find(BarChildName);

        if (!shouldShowBar)
        {
            if (barChild != null)
            {
                barChild.gameObject.SetActive(false);
            }

            UpdateCanvasElements(false);
        }
        else
        {
            if (barChild != null)
            {
                barChild.gameObject.SetActive(true);
            }

            UpdateCanvasElements(true);
        }
    }

    protected override void UpdateButtonVisibility(string sceneName)
    {
        UpdateButtonDisplay(sceneName);
    }

    public void UpdateButtonDisplay(string sceneName)
    {
        foreach (var button in allButtons)
        {
            if (button != null && button.normalIcon != null && button.filledIcon != null)
            {
                bool isActiveButton = (button.targetScene == sceneName);
                button.normalIcon.gameObject.SetActive(!isActiveButton);
                button.filledIcon.gameObject.SetActive(isActiveButton);
            }
        }
    }

    public void AddSceneWithoutBottomBar(string sceneName)
    {
        AddSceneWithoutBar(sceneName);
    }

    public void RemoveSceneWithoutBottomBar(string sceneName)
    {
        RemoveSceneWithoutBar(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        bool shouldShow = !scenesWithoutBar.Contains(sceneName);
        SetBarVisibility(shouldShow);

        if (shouldShow && gameObject.activeSelf)
        {
            if (sceneName == "PathwayScene")
            {
                StartCoroutine(ReinitializeForMobile());
            }
            else
            {
                UpdateButtonDisplay(sceneName);
            }
        }
    }

    private System.Collections.IEnumerator ReinitializeForMobile()
    {
        yield return null;
        yield return null;
        yield return null;

        float timeout = 3f;
        float elapsed = 0f;

        while (NavigationManager.Instance == null && elapsed < timeout)
        {
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        if (NavigationManager.Instance != null)
        {
            SetupButtonListeners();

            foreach (var btn in allButtons)
            {
                if (btn.button != null)
                {
                    btn.button.interactable = true;
                }
            }

            UpdateButtonDisplay("PathwayScene");
        }
     }

    protected override void OnCleanup()
    {
        if (!isDuplicate && _instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        base.OnCleanup();
    }
}