using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingSpinnerComponent : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject spinnerContainer;
    [SerializeField] private Image spinnerBackground;
    [SerializeField] private Image spinnerBorder;

    [Header("Configuration")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private bool rotateBackground = false;

    private static LoadingSpinnerComponent _instance;

    private CanvasGroup canvasGroup;
    private GraphicRaycaster graphicRaycaster;

    private bool waitForSceneLoad = false;
    private string sceneToWaitFor = string.Empty;
    private Coroutine hideCoroutine;

    public static LoadingSpinnerComponent Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<LoadingSpinnerComponent>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        graphicRaycaster = GetComponent<GraphicRaycaster>();

        if (spinnerContainer == null)
        {
            Debug.LogError("[LoadingSpinner] spinnerContainer não foi vinculado no prefab.");
        }

        if (spinnerBackground == null)
        {
            Debug.LogWarning("[LoadingSpinner] spinnerBackground não foi vinculado no prefab.");
        }

        if (spinnerBorder == null)
        {
            Debug.LogWarning("[LoadingSpinner] spinnerBorder não foi vinculado no prefab.");
        }

        HideSpinner();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Update()
    {
        if (spinnerContainer == null || !spinnerContainer.activeSelf)
            return;

        if (spinnerBorder != null)
        {
            spinnerBorder.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        if (rotateBackground && spinnerBackground != null)
        {
            spinnerBackground.transform.Rotate(0, 0, rotationSpeed * 0.2f * Time.deltaTime);
        }
    }

    public void ShowSpinnerUntilSceneLoaded(string sceneName)
    {
        waitForSceneLoad = true;
        sceneToWaitFor = sceneName;

        ShowSpinner();
    }

    public void ShowSpinner()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        if (spinnerContainer != null)
        {
            spinnerContainer.SetActive(true);
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        if (graphicRaycaster != null)
        {
            graphicRaycaster.enabled = true;
        }

        SetRaycastTargets(true);

        Debug.Log("[LoadingSpinner] Spinner mostrado.");
    }

    public void HideSpinner()
    {
        waitForSceneLoad = false;
        sceneToWaitFor = string.Empty;

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (graphicRaycaster != null)
        {
            graphicRaycaster.enabled = false;
        }

        SetRaycastTargets(false);

        if (spinnerContainer != null)
        {
            spinnerContainer.SetActive(false);
        }

        Debug.Log("[LoadingSpinner] Spinner escondido e raycasts desativados.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (waitForSceneLoad && scene.name == sceneToWaitFor)
        {
            hideCoroutine = StartCoroutine(HideSpinnerDelayed(0.2f));
        }
    }

    private IEnumerator HideSpinnerDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideSpinner();
    }

    private void SetRaycastTargets(bool enabled)
    {
        if (spinnerContainer == null)
            return;

        Graphic[] graphics = spinnerContainer.GetComponentsInChildren<Graphic>(true);

        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = enabled;
        }
    }
}