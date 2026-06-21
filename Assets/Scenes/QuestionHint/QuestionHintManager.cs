using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using QuestionSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuestionHintManager : MonoBehaviour
{
    private const string QUESTION_KEY = "question";
    private const string HINT_KEY = "questionHint";

    [Header("Hint UI")]
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private Image hintImage;
    [SerializeField] private VideoPlayer hintVideo;

    [Header("Optional Link UI")]
    [SerializeField] private GameObject linkContainer;
    [SerializeField] private Button linkButton;
    [SerializeField] private TextMeshProUGUI linkText;

    [Header("Modal")]
    [SerializeField] private RectTransform modalRoot;
    [SerializeField] private CanvasGroup modalCanvasGroup;
    [SerializeField] private Canvas modalCanvas;
    [SerializeField] private float modalAnimationDuration = 0.35f;
    [SerializeField] private float modalHiddenOffsetY = 2300f;
    [SerializeField] private int modalSortingOrder = 500;

    [Header("Video")]
    [SerializeField] private bool playVideoOnLoad;

    private CancellationTokenSource _imageLoadCts;
    private Sprite _loadedImageSprite;
    private bool _loadedImageOwnsTexture;
    private string _currentLink;
    private Vector2 _modalVisiblePosition;
    private Coroutine _modalAnimationRoutine;
    private bool _hasModalVisiblePosition;

    private void Awake()
    {
        ResolveModalReferences();
        ConfigureAsAdditiveModal();
        PrepareModalHidden();
        HideAllHintViews();
        ConfigureLinkButton();
    }

    private void Start()
    {
        PopulateFromSceneData();
        PlayOpenAnimation();
    }

    public void ShowAsModal(Question question)
    {
        Populate(question);
        PlayOpenAnimation();
    }

    public void CloseModal()
    {
        if (_modalAnimationRoutine != null)
            StopCoroutine(_modalAnimationRoutine);

        _modalAnimationRoutine = StartCoroutine(CloseModalRoutine());
    }

    public void Populate(Question question)
    {
        if (question == null)
        {
            Debug.LogWarning("[QuestionHintManager] Question recebida é null.");
            HideAllHintViews();
            return;
        }

        Populate(question.questionHint, question);
    }

    public void Populate(QuestionHint hint)
    {
        Populate(hint, null);
    }

    private void Populate(QuestionHint hint, Question sourceQuestion)
    {
        HideAllHintViews();

        if (hint == null || !hint.HasAnyHint)
        {
            Debug.LogWarning("[QuestionHintManager] Nenhum hint disponível para apresentar.");
            return;
        }

        ShowText(hint.text);
        _ = ShowImageAsync(hint.imagePath, sourceQuestion);
        ShowVideo(hint.videoUrl);
        ShowLink(hint.link);
    }

    private void PlayOpenAnimation()
    {
        ResolveModalReferences();
        CacheModalVisiblePosition();

        if (modalRoot == null || modalCanvasGroup == null)
            return;

        if (_modalAnimationRoutine != null)
            StopCoroutine(_modalAnimationRoutine);

        gameObject.SetActive(true);
        modalRoot.gameObject.SetActive(true);
        _modalAnimationRoutine = StartCoroutine(AnimateModal(
            _modalVisiblePosition + Vector2.down * modalHiddenOffsetY,
            _modalVisiblePosition,
            0f,
            1f,
            unloadSceneWhenFinished: false));
    }

    private void PopulateFromSceneData()
    {
        ISceneDataService sceneData = AppContext.SceneData;
        Dictionary<string, object> data = sceneData?.GetData();

        if (data == null || data.Count == 0)
        {
            Debug.LogWarning("[QuestionHintManager] SceneData vazio. Passe a Question ou QuestionHint ao abrir a cena.");
            HideAllHintViews();
            return;
        }

        if (TryGetValue<Question>(data, QUESTION_KEY, out Question question))
        {
            Populate(question);
            return;
        }

        if (TryGetValue<QuestionHint>(data, HINT_KEY, out QuestionHint hint))
        {
            Populate(hint);
            return;
        }

        QuestionHint hintFromFields = BuildHintFromLooseFields(data);
        Populate(hintFromFields);
    }

    private void ShowText(string text)
    {
        if (hintText == null)
            return;

        if (string.IsNullOrWhiteSpace(text))
        {
            hintText.gameObject.SetActive(false);
            return;
        }

        hintText.text = ChemicalFormatter.Format(text);
        hintText.gameObject.SetActive(true);
    }

    private async Task ShowImageAsync(string imagePath, Question sourceQuestion)
    {
        if (hintImage == null)
            return;

        CancelImageLoad();
        ClearLoadedImage();

        if (string.IsNullOrWhiteSpace(imagePath))
        {
            hintImage.gameObject.SetActive(false);
            return;
        }

        _imageLoadCts = new CancellationTokenSource();
        CancellationToken ct = _imageLoadCts.Token;

        string resolvedImagePath = ResolveHintImagePath(imagePath, sourceQuestion);

        try
        {
            Texture2D texture = null;

            if (AppContext.ImageSync != null)
            {
                texture = await AppContext.ImageSync.GetImageAsync(resolvedImagePath, ct);
                _loadedImageOwnsTexture = texture != null;
            }
            else
            {
                texture = Resources.Load<Texture2D>(resolvedImagePath);
                if (texture == null && resolvedImagePath != imagePath)
                    texture = Resources.Load<Texture2D>(imagePath);
                _loadedImageOwnsTexture = false;
            }

            if (ct.IsCancellationRequested)
            {
                if (_loadedImageOwnsTexture && texture != null)
                    Destroy(texture);
                return;
            }

            if (texture == null)
            {
                Debug.LogWarning($"[QuestionHintManager] Imagem de hint não encontrada: '{resolvedImagePath}'.");
                hintImage.gameObject.SetActive(false);
                return;
            }

            _loadedImageSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f);

            hintImage.sprite = _loadedImageSprite;
            hintImage.preserveAspect = true;
            hintImage.gameObject.SetActive(true);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[QuestionHintManager] Erro ao carregar imagem de hint '{resolvedImagePath}': {e.Message}");
            hintImage.gameObject.SetActive(false);
        }
    }

    private void ShowVideo(string videoUrl)
    {
        if (hintVideo == null)
            return;

        if (string.IsNullOrWhiteSpace(videoUrl))
        {
            hintVideo.Stop();
            hintVideo.gameObject.SetActive(false);
            return;
        }

        hintVideo.Stop();
        hintVideo.source = VideoSource.Url;
        hintVideo.url = videoUrl;
        hintVideo.playOnAwake = playVideoOnLoad;
        hintVideo.gameObject.SetActive(true);

        if (playVideoOnLoad)
            hintVideo.Play();
    }

    private void ShowLink(string link)
    {
        _currentLink = string.IsNullOrWhiteSpace(link) ? null : link.Trim();

        if (linkContainer != null)
            linkContainer.SetActive(!string.IsNullOrEmpty(_currentLink));

        if (linkButton != null)
            linkButton.gameObject.SetActive(!string.IsNullOrEmpty(_currentLink));

        if (linkText != null)
        {
            linkText.text = _currentLink ?? "";
            linkText.gameObject.SetActive(!string.IsNullOrEmpty(_currentLink));
        }
    }

    public void OpenCurrentLink()
    {
        if (string.IsNullOrEmpty(_currentLink))
            return;

        Application.OpenURL(_currentLink);
    }

    private void ConfigureLinkButton()
    {
        if (linkButton == null)
            return;

        linkButton.onClick.RemoveListener(OpenCurrentLink);
        linkButton.onClick.AddListener(OpenCurrentLink);
    }

    private void ResolveModalReferences()
    {
        if (modalRoot == null)
            modalRoot = FindRectTransformInScene("MainBackground");

        if (modalRoot == null)
            modalRoot = FindObjectOfType<Canvas>()?.transform as RectTransform;

        if (modalCanvas == null)
            modalCanvas = modalRoot != null
                ? modalRoot.GetComponentInParent<Canvas>()
                : FindObjectOfType<Canvas>();

        if (modalCanvasGroup == null && modalRoot != null)
            modalCanvasGroup = modalRoot.GetComponent<CanvasGroup>();

        if (modalCanvasGroup == null && modalRoot != null)
            modalCanvasGroup = modalRoot.gameObject.AddComponent<CanvasGroup>();
    }

    private void ConfigureAsAdditiveModal()
    {
        if (modalCanvas != null)
        {
            modalCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            modalCanvas.overrideSorting = true;
            modalCanvas.sortingOrder = modalSortingOrder;
        }
    }

    private void PrepareModalHidden()
    {
        CacheModalVisiblePosition();

        if (modalRoot != null)
            modalRoot.anchoredPosition = _modalVisiblePosition + Vector2.down * modalHiddenOffsetY;

        if (modalCanvasGroup != null)
        {
            modalCanvasGroup.alpha = 0f;
            modalCanvasGroup.interactable = false;
            modalCanvasGroup.blocksRaycasts = false;
        }
    }

    private IEnumerator CloseModalRoutine()
    {
        ResolveModalReferences();
        CacheModalVisiblePosition();

        if (modalRoot == null || modalCanvasGroup == null)
        {
            UnloadThisSceneIfAdditive();
            yield break;
        }

        yield return AnimateModal(
            modalRoot.anchoredPosition,
            _modalVisiblePosition + Vector2.down * modalHiddenOffsetY,
            modalCanvasGroup.alpha,
            0f,
            unloadSceneWhenFinished: true);
    }

    private IEnumerator AnimateModal(
        Vector2 fromPosition,
        Vector2 toPosition,
        float fromAlpha,
        float toAlpha,
        bool unloadSceneWhenFinished)
    {
        float elapsed = 0f;
        float duration = Mathf.Max(0.01f, modalAnimationDuration);

        modalRoot.anchoredPosition = fromPosition;
        modalCanvasGroup.alpha = fromAlpha;
        modalCanvasGroup.interactable = false;
        modalCanvasGroup.blocksRaycasts = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float eased = Mathf.SmoothStep(0f, 1f, t);

            modalRoot.anchoredPosition = Vector2.LerpUnclamped(fromPosition, toPosition, eased);
            modalCanvasGroup.alpha = Mathf.LerpUnclamped(fromAlpha, toAlpha, eased);

            yield return null;
        }

        modalRoot.anchoredPosition = toPosition;
        modalCanvasGroup.alpha = toAlpha;
        modalCanvasGroup.interactable = !unloadSceneWhenFinished;
        modalCanvasGroup.blocksRaycasts = !unloadSceneWhenFinished;
        _modalAnimationRoutine = null;

        if (unloadSceneWhenFinished)
            UnloadThisSceneIfAdditive();
    }

    private void CacheModalVisiblePosition()
    {
        if (_hasModalVisiblePosition || modalRoot == null)
            return;

        _modalVisiblePosition = modalRoot.anchoredPosition;
        _hasModalVisiblePosition = true;
    }

    private RectTransform FindRectTransformInScene(string objectName)
    {
        Scene currentScene = gameObject.scene;

        foreach (GameObject root in currentScene.GetRootGameObjects())
        {
            Transform[] transforms = root.GetComponentsInChildren<Transform>(true);
            foreach (Transform candidate in transforms)
            {
                if (candidate.name == objectName && candidate is RectTransform rectTransform)
                    return rectTransform;
            }
        }

        return null;
    }

    private void UnloadThisSceneIfAdditive()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
            return;
        }

        if (modalRoot != null)
            modalRoot.gameObject.SetActive(false);
    }

    private void HideAllHintViews()
    {
        if (hintText != null)
            hintText.gameObject.SetActive(false);

        if (hintImage != null)
            hintImage.gameObject.SetActive(false);

        if (hintVideo != null)
        {
            hintVideo.Stop();
            hintVideo.gameObject.SetActive(false);
        }

        ShowLink(null);
    }

    private static string ResolveHintImagePath(string imagePath, Question sourceQuestion)
    {
        if (sourceQuestion == null)
            return imagePath;

        string resolved = QuestionStorageKeys.Resolve(imagePath, sourceQuestion.topic);
        return string.IsNullOrEmpty(resolved) ? imagePath : resolved;
    }

    private static QuestionHint BuildHintFromLooseFields(Dictionary<string, object> data)
    {
        return new QuestionHint
        {
            text = GetString(data, "text", "hintText"),
            imagePath = GetString(data, "imagePath", "hintImagePath"),
            videoUrl = GetString(data, "videoUrl", "hintVideoUrl"),
            link = GetString(data, "link", "hintLink")
        };
    }

    private static string GetString(Dictionary<string, object> data, params string[] keys)
    {
        foreach (string key in keys)
        {
            if (data.TryGetValue(key, out object value) && value != null)
                return value.ToString();
        }

        return null;
    }

    private static bool TryGetValue<T>(Dictionary<string, object> data, string key, out T value)
    {
        value = default;

        if (!data.TryGetValue(key, out object raw) || raw == null)
            return false;

        if (raw is T typedValue)
        {
            value = typedValue;
            return true;
        }

        return false;
    }

    private void CancelImageLoad()
    {
        if (_imageLoadCts == null)
            return;

        try
        {
            _imageLoadCts.Cancel();
        }
        catch
        {
            // No-op: cancellation is best-effort during scene teardown.
        }

        _imageLoadCts.Dispose();
        _imageLoadCts = null;
    }

    private void ClearLoadedImage()
    {
        if (hintImage != null)
            hintImage.sprite = null;

        if (_loadedImageSprite == null)
            return;

        Texture2D texture = _loadedImageSprite.texture;
        Destroy(_loadedImageSprite);
        if (_loadedImageOwnsTexture && texture != null)
            Destroy(texture);

        _loadedImageSprite = null;
        _loadedImageOwnsTexture = false;
    }

    private void OnDestroy()
    {
        if (_modalAnimationRoutine != null)
            StopCoroutine(_modalAnimationRoutine);

        CancelImageLoad();
        ClearLoadedImage();

        if (linkButton != null)
            linkButton.onClick.RemoveListener(OpenCurrentLink);
    }
}
