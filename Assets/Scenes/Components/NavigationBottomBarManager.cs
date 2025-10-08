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
        Debug.Log("[ANDROID-BOTTOMBAR] ConfigureSingleton CHAMADO"); // ⭐
        Debug.Log($"[ANDROID-BOTTOMBAR] _instance atual: {(_instance != null ? "EXISTE" : "NULL")}"); // ⭐
        Debug.Log($"[ANDROID-BOTTOMBAR] Este objeto é a instância? {_instance == this}"); // ⭐

        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("[ANDROID-BOTTOMBAR] ⚠️ DUPLICATA DETECTADA! Destruindo este objeto..."); // ⭐
            isDuplicate = true;
            Destroy(gameObject);
            return;
        }

        _instance = this;
        Debug.Log("[ANDROID-BOTTOMBAR] ✓ Instância configurada como singleton"); // ⭐
    }

    protected override void OnAwake()
    {
        Debug.Log("[ANDROID-BOTTOMBAR] ========== OnAwake INICIADO =========="); // ⭐
        Debug.Log($"[ANDROID-BOTTOMBAR] Cena atual: {SceneManager.GetActiveScene().name}"); // ⭐

        base.scenesWithoutBar.Clear();
        foreach (var scene in scenesWithoutBottomBar)
        {
            base.scenesWithoutBar.Add(scene);
        }

        // ⭐ Remove listener anterior (caso exista) para evitar duplicatas
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("[ANDROID-BOTTOMBAR] Listener anterior removido (se existia)"); // ⭐

        // ⭐ Registra o listener
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("[ANDROID-BOTTOMBAR] Listener de SceneLoaded REGISTRADO"); // ⭐

        InitializeButtons();
        Debug.Log("[ANDROID-BOTTOMBAR] ========== OnAwake CONCLUÍDO =========="); // ⭐
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
        Debug.Log($"[ANDROID-BOTTOMBAR] ========== SetupButtonListeners INICIADO ==========");
        Debug.Log($"[ANDROID-BOTTOMBAR] Quantidade de botões: {allButtons.Count}");
        Debug.Log($"[ANDROID-BOTTOMBAR] NavigationManager existe? {NavigationManager.Instance != null}");

        foreach (var buttonInfo in allButtons)
        {
            if (buttonInfo.button != null)
            {
                // ⭐ DIAGNÓSTICO CRÍTICO - mostra estado do botão
                Debug.Log($"[ANDROID-BOTTOMBAR] === Configurando: {buttonInfo.buttonName} ===");
                Debug.Log($"  GameObject: {buttonInfo.button.gameObject.name}");
                Debug.Log($"  Ativo na hierarquia? {buttonInfo.button.gameObject.activeInHierarchy}");
                Debug.Log($"  Enabled? {buttonInfo.button.enabled}");
                Debug.Log($"  Interactable? {buttonInfo.button.interactable}");

                // ⭐ Verifica se o RectTransform está visível
                RectTransform rect = buttonInfo.button.GetComponent<RectTransform>();
                if (rect != null)
                {
                    Debug.Log($"  Posição: {rect.position}");
                    Debug.Log($"  Tamanho: {rect.sizeDelta}");
                }

                buttonInfo.button.onClick.RemoveAllListeners();
                string targetButtonName = buttonInfo.buttonName;
                string targetSceneName = buttonInfo.targetScene;

                buttonInfo.button.onClick.AddListener(() =>
                {
                    Debug.Log($"[ANDROID-BOTTOMBAR] !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Debug.Log($"[ANDROID-BOTTOMBAR] CLIQUE DETECTADO: {targetButtonName}");
                    Debug.Log($"[ANDROID-BOTTOMBAR] !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                    if (NavigationManager.Instance != null)
                    {
                        Debug.Log($"[ANDROID-BOTTOMBAR] Navegando para {targetSceneName}");
                        NavigationManager.Instance.NavigateTo(targetSceneName);
                    }
                    else
                    {
                        Debug.LogError("[ANDROID-BOTTOMBAR] NavigationManager NULL!");
                        StartCoroutine(RetryNavigation(targetSceneName, targetButtonName));
                    }
                });

                Debug.Log($"  Listener CONFIGURADO ✓");
            }
            else
            {
                Debug.LogError($"[ANDROID-BOTTOMBAR] Botão NULL: {buttonInfo.buttonName}");
            }
        }

        Debug.Log($"[ANDROID-BOTTOMBAR] ========== SetupButtonListeners CONCLUÍDO ==========");
    }

    private System.Collections.IEnumerator RetryNavigation(string sceneName, string buttonName)
    {
        if (debugLogs) Debug.Log($"Aguardando NavigationManager para navegar para {sceneName}...");

        float timeout = 2f;
        float elapsed = 0f;

        while (NavigationManager.Instance == null && elapsed < timeout)
        {
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        if (NavigationManager.Instance != null)
        {
            if (debugLogs) Debug.Log($"NavigationManager recuperado - navegando para {sceneName}");
            NavigationManager.Instance.NavigateTo(sceneName);
        }
        else
        {
            Debug.LogError($"Timeout: Não foi possível navegar para {sceneName} após {timeout}s - NavigationManager não disponível");
        }
    }

    protected override void AdjustVisibilityForCurrentScene()
    {
        bool shouldShowBar = !scenesWithoutBar.Contains(currentScene);

        if (debugLogs)
        {
            Debug.Log($"[BottomBar Debug] Cena atual: '{currentScene}'");
            Debug.Log($"[BottomBar Debug] Cenas sem barra: {string.Join(", ", scenesWithoutBar)}");
            Debug.Log($"[BottomBar Debug] A cena atual está na lista? {scenesWithoutBar.Contains(currentScene)}");
            Debug.Log($"[BottomBar Debug] shouldShowBar = {shouldShowBar}");
        }

        Transform barChild = transform.Find(BarChildName);

        if (!shouldShowBar)
        {
            if (barChild != null)
            {
                barChild.gameObject.SetActive(false);
                if (debugLogs) Debug.Log($"[BottomBar Debug] Desativando {BarChildName} na cena {currentScene}");
            }
            else
            {
                Debug.LogWarning($"[BottomBar Debug] Filho '{BarChildName}' não encontrado!");
            }

            UpdateCanvasElements(false);

            if (debugLogs) Debug.Log($"[BottomBar Debug] Forçando desativação da BottomBar na cena {currentScene}");
        }
        else
        {
            if (barChild != null)
            {
                barChild.gameObject.SetActive(true);
                if (debugLogs) Debug.Log($"[BottomBar Debug] Ativando {BarChildName} na cena {currentScene}");
            }
            else
            {
                Debug.LogWarning($"[BottomBar Debug] Filho '{BarChildName}' não encontrado!");
            }

            UpdateCanvasElements(true);
        }

        if (debugLogs) Debug.Log($"BottomBar visibilidade na cena {currentScene}: {shouldShowBar}");
    }

    protected override void UpdateButtonVisibility(string sceneName)
    {
        UpdateButtonDisplay(sceneName);
    }

    public void UpdateButtonDisplay(string sceneName)
    {
        if (debugLogs) Debug.Log($"Atualizando BottomBar para cena: {sceneName}");

        foreach (var button in allButtons)
        {
            if (button != null && button.normalIcon != null && button.filledIcon != null)
            {
                bool isActiveButton = (button.targetScene == sceneName);
                button.normalIcon.gameObject.SetActive(!isActiveButton);
                button.filledIcon.gameObject.SetActive(isActiveButton);

                if (debugLogs && isActiveButton)
                {
                    Debug.Log($"Ativado botão: {button.buttonName} para cena {sceneName}");
                }
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
        Debug.Log($"[ANDROID-BOTTOMBAR] OnSceneLoaded CHAMADO! Cena: {scene.name}"); // ⭐ ADICIONE

        string sceneName = scene.name;
        bool shouldShow = !scenesWithoutBar.Contains(sceneName);
        SetBarVisibility(shouldShow);

        if (shouldShow && gameObject.activeSelf)
        {
            if (sceneName == "PathwayScene")
            {
                Debug.Log("[ANDROID-BOTTOMBAR] É PathwayScene! Chamando ReinitializeForMobile"); // ⭐ ADICIONE
                StartCoroutine(ReinitializeForMobile());
            }
            else
            {
                UpdateButtonDisplay(sceneName);
            }
        }

        Debug.Log($"{BarName}: Visibilidade após carregamento de cena: {gameObject.activeSelf}");
    }

    private System.Collections.IEnumerator ReinitializeForMobile()
    {
        Debug.Log("[ANDROID-BOTTOMBAR] ReinitializeForMobile INICIADO"); // ⭐ ADICIONE

        yield return null;
        yield return null;
        yield return null;

        Debug.Log("[ANDROID-BOTTOMBAR] Aguardando NavigationManager..."); // ⭐ ADICIONE

        float timeout = 3f;
        float elapsed = 0f;

        while (NavigationManager.Instance == null && elapsed < timeout)
        {
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        Debug.Log($"[ANDROID-BOTTOMBAR] NavigationManager encontrado? {NavigationManager.Instance != null}"); // ⭐ ADICIONE

        if (NavigationManager.Instance != null)
        {
            Debug.Log("[ANDROID-BOTTOMBAR] CHAMANDO SetupButtonListeners AGORA!"); // ⭐ ADICIONE
            SetupButtonListeners();

            foreach (var btn in allButtons)
            {
                if (btn.button != null)
                {
                    btn.button.interactable = true;
                    Debug.Log($"[BOTTOMBAR] {btn.buttonName} -> interactable = TRUE");
                }
            }

            UpdateButtonDisplay("PathwayScene");
            Debug.Log("[ANDROID-BOTTOMBAR] BottomBar listeners reconectados!"); // ⭐ ADICIONE
        }
        else
        {
            Debug.LogError("[ANDROID-BOTTOMBAR] NavigationManager NÃO ENCONTRADO após timeout!");
        }
    }

    protected override void OnCleanup()
    {
        Debug.Log("[ANDROID-BOTTOMBAR] ========== OnCleanup CHAMADO =========="); // ⭐
        Debug.Log("[ANDROID-BOTTOMBAR] Removendo listener de SceneLoaded"); // ⭐

        // ⭐ SÓ remove listener se NÃO for duplicata E for a instância ativa
        if (!isDuplicate && _instance == this)
        {
            Debug.Log("[ANDROID-BOTTOMBAR] Esta é a instância ativa - Removendo listener de SceneLoaded");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        else
        {
            Debug.Log("[ANDROID-BOTTOMBAR] Duplicata ou instância inativa - NÃO removendo listener");
        }

        base.OnCleanup();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    
        Debug.Log("[ANDROID-BOTTOMBAR] ========== OnCleanup CONCLUÍDO =========="); // ⭐
    }

    // ⭐ Adicione este método público
    public void DiagnoseButtons()
    {
        Debug.Log("[BOTTOMBAR-DIAG] ========== DIAGNÓSTICO SIMPLES ==========");
        Debug.Log($"[BOTTOMBAR-DIAG] Total botões: {allButtons.Count}");

        for (int i = 0; i < allButtons.Count; i++)
        {
            var btn = allButtons[i];
            if (btn.button != null)
            {
                Debug.Log($"[BOTTOMBAR-DIAG] [{i}] {btn.buttonName}");
                Debug.Log($"[BOTTOMBAR-DIAG]     Ativo? {btn.button.gameObject.activeInHierarchy}");
                Debug.Log($"[BOTTOMBAR-DIAG]     Interactable? {btn.button.interactable}");
                Debug.Log($"[BOTTOMBAR-DIAG]     Listeners? {btn.button.onClick.GetPersistentEventCount()}");
            }
        }

        Debug.Log("[BOTTOMBAR-DIAG] ========== FIM DIAGNÓSTICO ==========");
    }
}