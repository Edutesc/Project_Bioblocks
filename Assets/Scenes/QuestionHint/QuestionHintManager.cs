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

    [Header("Hint Containers")]
    [SerializeField] private GameObject hintTextContainer;
    [SerializeField] private GameObject hintImageContainer;
    [SerializeField] private GameObject hintVideoContainer;
    [SerializeField] private GameObject hintLinkContainer;

    [Header("Optional Link UI")]
    [SerializeField] private GameObject linkContainer;
    [SerializeField] private Button linkButton;
    [SerializeField] private TextMeshProUGUI linkText;

    [Header("Modal")]
    [SerializeField] private RectTransform modalRoot;
    [SerializeField] private CanvasGroup modalCanvasGroup;
    [SerializeField] private Canvas modalCanvas;
    [SerializeField] private RectTransform modalContent;
    [SerializeField] private RectTransform modalBackground;
    [SerializeField] private RectTransform modalHeader;
    [SerializeField] private RectTransform contentScrollView;
    [SerializeField] private float modalAnimationDuration = 0.35f;
    [SerializeField] private float modalHiddenOffsetY = 2300f;
    [SerializeField] private float modalMinHeight = 520f;
    [SerializeField] private float modalMaxHeight = 1800f;
    [SerializeField] private float modalHeightMultiplier = 1.2f;
    [SerializeField] private float modalTopPadding = 50f;
    [SerializeField] private float modalHeaderContentSpacing = 20f;
    [SerializeField] private float modalBottomPadding = 60f;
    [SerializeField] private int modalSortingOrder = 500;

    [Header("Content Layout")]
    [SerializeField] private float contentTopPadding = 20f;
    [SerializeField] private float contentSpacing = 32f;
    [SerializeField] private float contentBottomPadding = 20f;
    [SerializeField] private float textContainerMinHeight = 210f;
    [SerializeField] private float textContainerMaxHeight = 900f;
    [SerializeField] private float textContainerTopPadding = 26f;
    [SerializeField] private float textContainerBottomPadding = 64f;
    [SerializeField] private float textInset = 20f;

    [Header("Video")]
    [SerializeField] private bool playVideoOnLoad;

    private CancellationTokenSource _imageLoadCts;
    private Sprite _loadedImageSprite;
    private bool _loadedImageOwnsTexture;
    private string _currentLink;
    private Vector2 _modalVisiblePosition;
    private Coroutine _modalAnimationRoutine;

    private void Awake()
    {
        ResolveModalReferences();
        ResolveContainerReferences();
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
        RefreshModalSize();
    }

    private void PlayOpenAnimation()
    {
        ResolveModalReferences();
        ResolveContainerReferences();
        RefreshModalSize();

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
        bool hasText = !string.IsNullOrWhiteSpace(text);
        SetContainerVisible(hintTextContainer, hasText);

        if (!hasText)
        {
            if (hintText != null)
                hintText.gameObject.SetActive(false);
            return;
        }

        if (hintText == null)
            return;

        hintText.text = ChemicalFormatter.Format(text);
        hintText.gameObject.SetActive(true);
        RefreshHintLayout();
    }

    private async Task ShowImageAsync(string imagePath, Question sourceQuestion)
    {
        CancelImageLoad();
        ClearLoadedImage();

        if (string.IsNullOrWhiteSpace(imagePath))
        {
            SetContainerVisible(hintImageContainer, false);
            if (hintImage != null)
                hintImage.gameObject.SetActive(false);
            return;
        }

        SetContainerVisible(hintImageContainer, true);
        RefreshModalSize();

        if (hintImage == null)
            return;

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
                SetContainerVisible(hintImageContainer, false);
                RefreshModalSize();
                return;
            }

            _loadedImageSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f,
                0,
                SpriteMeshType.FullRect);

            hintImage.sprite = _loadedImageSprite;
            hintImage.preserveAspect = true;
            hintImage.gameObject.SetActive(true);
            RefreshHintLayout();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[QuestionHintManager] Erro ao carregar imagem de hint '{resolvedImagePath}': {e.Message}");
            hintImage.gameObject.SetActive(false);
            SetContainerVisible(hintImageContainer, false);
            RefreshModalSize();
        }
    }

    private void ShowVideo(string videoUrl)
    {
        bool hasVideo = !string.IsNullOrWhiteSpace(videoUrl);
        SetContainerVisible(hintVideoContainer, hasVideo);

        if (hintVideo == null)
            return;

        if (!hasVideo)
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
        bool hasLink = !string.IsNullOrEmpty(_currentLink);

        SetContainerVisible(hintLinkContainer, hasLink);

        if (linkContainer != null && linkContainer != hintLinkContainer)
            linkContainer.SetActive(hasLink);

        if (linkButton != null)
            linkButton.gameObject.SetActive(hasLink);

        if (linkText != null)
        {
            linkText.text = _currentLink ?? "";
            linkText.gameObject.SetActive(hasLink);
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
            modalRoot = FindRectTransformInScene("ModalPanel");

        if (modalRoot == null)
            modalRoot = FindRectTransformInScene("MainBackground");

        if (modalContent == null)
            modalContent = FindRectTransformInScene("Content");

        if (modalBackground == null)
            modalBackground = FindRectTransformInScene("MainBackground");

        if (modalHeader == null)
            modalHeader = FindRectTransformInScene("Header");

        if (contentScrollView == null)
            contentScrollView = FindRectTransformInScene("ContentScrollView");

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

    private void ResolveContainerReferences()
    {
        if (hintTextContainer == null)
            hintTextContainer = FindGameObjectInScene("HintTextContainer");

        if (hintImageContainer == null)
            hintImageContainer = FindGameObjectInScene("HintImageContainer");

        if (hintVideoContainer == null)
            hintVideoContainer = FindGameObjectInScene("HintVideoContainer");

        if (hintLinkContainer == null)
            hintLinkContainer = linkContainer;
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
        RefreshModalSize();

        if (modalRoot != null)
            modalRoot.anchoredPosition = GetModalVisiblePosition() + Vector2.down * modalHiddenOffsetY;

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
        RefreshModalSize();

        if (modalRoot == null || modalCanvasGroup == null)
        {
            UnloadThisSceneIfAdditive();
            yield break;
        }

        yield return AnimateModal(
            modalRoot.anchoredPosition,
            GetModalVisiblePosition() + Vector2.down * modalHiddenOffsetY,
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

    private void RefreshModalSize()
    {
        if (modalRoot == null)
            return;

        RefreshHintLayout();

        if (modalContent != null)
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(modalContent);
        }

        float contentHeight = GetContentPreferredHeight();
        float headerHeight = modalHeader != null
            ? Mathf.Max(modalHeader.rect.height, modalHeader.sizeDelta.y)
            : 0f;
        float measuredHeight = modalTopPadding + headerHeight + modalHeaderContentSpacing + contentHeight + modalBottomPadding;
        float targetHeight = Mathf.Clamp(
            measuredHeight * modalHeightMultiplier,
            modalMinHeight,
            modalMaxHeight);
        float scrollViewHeight = Mathf.Max(
            0f,
            targetHeight - modalTopPadding - headerHeight - modalHeaderContentSpacing - modalBottomPadding);

        modalRoot.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
        RefreshModalFrameLayout(targetHeight, headerHeight, scrollViewHeight);
        Canvas.ForceUpdateCanvases();

        _modalVisiblePosition = GetModalVisiblePosition();
    }

    private void RefreshModalFrameLayout(float modalHeight, float headerHeight, float scrollViewHeight)
    {
        if (modalBackground != null)
        {
            modalBackground.anchorMin = new Vector2(0.5f, 0.5f);
            modalBackground.anchorMax = new Vector2(0.5f, 0.5f);
            modalBackground.pivot = new Vector2(0.5f, 0.5f);
            modalBackground.anchoredPosition = Vector2.zero;
            modalBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, modalHeight);
        }

        if (modalHeader != null)
        {
            modalHeader.anchorMin = new Vector2(0.5f, 1f);
            modalHeader.anchorMax = new Vector2(0.5f, 1f);
            modalHeader.pivot = new Vector2(0.5f, 0.5f);
            modalHeader.anchoredPosition = new Vector2(0f, -(modalTopPadding + headerHeight * 0.5f));
        }

        if (contentScrollView == null)
            return;

        float scrollViewWidth = Mathf.Max(contentScrollView.rect.width, contentScrollView.sizeDelta.x);
        contentScrollView.anchorMin = new Vector2(0.5f, 1f);
        contentScrollView.anchorMax = new Vector2(0.5f, 1f);
        contentScrollView.pivot = new Vector2(0.5f, 1f);
        contentScrollView.anchoredPosition = new Vector2(
            0f,
            -(modalTopPadding + headerHeight + modalHeaderContentSpacing));
        contentScrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollViewWidth);
        contentScrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollViewHeight);
    }

    private void RefreshHintLayout()
    {
        if (modalContent == null)
            modalContent = FindRectTransformInScene("Content");

        float cursorY = -contentTopPadding;
        float widestChild = modalContent != null ? modalContent.rect.width : 0f;

        LayoutTextHint();
        PositionVisibleHintContainer(hintTextContainer, ref cursorY, ref widestChild);
        PositionVisibleHintContainer(hintImageContainer, ref cursorY, ref widestChild);
        PositionVisibleHintContainer(hintVideoContainer, ref cursorY, ref widestChild);
        PositionVisibleHintContainer(hintLinkContainer, ref cursorY, ref widestChild);

        if (modalContent == null)
            return;

        float contentHeight = Mathf.Max(0f, -cursorY - contentSpacing + contentBottomPadding);
        modalContent.anchorMin = new Vector2(0f, 1f);
        modalContent.anchorMax = new Vector2(1f, 1f);
        modalContent.pivot = new Vector2(0f, 1f);
        modalContent.anchoredPosition = Vector2.zero;
        modalContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);
    }

    private void LayoutTextHint()
    {
        if (hintTextContainer == null || hintText == null || !hintTextContainer.activeSelf)
            return;

        RectTransform containerRect = hintTextContainer.transform as RectTransform;
        RectTransform textRect = hintText.rectTransform;
        if (containerRect == null || textRect == null)
            return;

        float containerWidth = Mathf.Max(containerRect.rect.width, containerRect.sizeDelta.x);
        float textWidth = Mathf.Max(1f, containerWidth - textInset * 2f);
        float preferredTextHeight = hintText.GetPreferredValues(hintText.text, textWidth, 0f).y;
        float targetHeight = Mathf.Clamp(
            preferredTextHeight + textContainerTopPadding + textContainerBottomPadding,
            textContainerMinHeight,
            textContainerMaxHeight);

        containerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);

        textRect.anchorMin = new Vector2(0f, 0f);
        textRect.anchorMax = new Vector2(1f, 1f);
        textRect.pivot = new Vector2(0.5f, 0.5f);
        textRect.anchoredPosition = Vector2.zero;
        textRect.offsetMin = new Vector2(textInset, textContainerBottomPadding);
        textRect.offsetMax = new Vector2(-textInset, -textContainerTopPadding);
    }

    private void PositionVisibleHintContainer(GameObject container, ref float cursorY, ref float widestChild)
    {
        if (container == null || !container.activeSelf)
            return;

        RectTransform rectTransform = container.transform as RectTransform;
        if (rectTransform == null)
            return;

        float height = Mathf.Max(0f, rectTransform.rect.height);
        float width = Mathf.Max(rectTransform.rect.width, rectTransform.sizeDelta.x);

        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = new Vector2(0f, cursorY);

        cursorY -= height + contentSpacing;
        widestChild = Mathf.Max(widestChild, width);
    }

    private float GetContentPreferredHeight()
    {
        if (modalContent == null)
            return 0f;

        float preferredHeight = LayoutUtility.GetPreferredHeight(modalContent);
        if (preferredHeight > 0f)
            return preferredHeight;

        float fallbackHeight = 0f;
        VerticalLayoutGroup layoutGroup = modalContent.GetComponent<VerticalLayoutGroup>();
        float spacing = layoutGroup != null ? layoutGroup.spacing : 0f;
        int visibleChildren = 0;

        for (int i = 0; i < modalContent.childCount; i++)
        {
            RectTransform child = modalContent.GetChild(i) as RectTransform;
            if (child == null || !child.gameObject.activeSelf)
                continue;

            fallbackHeight += child.rect.height;
            visibleChildren++;
        }

        if (visibleChildren > 1)
            fallbackHeight += spacing * (visibleChildren - 1);

        if (layoutGroup != null)
            fallbackHeight += layoutGroup.padding.top + layoutGroup.padding.bottom;

        return fallbackHeight;
    }

    private Vector2 GetModalVisiblePosition()
    {
        if (modalRoot == null)
            return Vector2.zero;

        return new Vector2(modalRoot.anchoredPosition.x, 0f);
    }

    private void SetContainerVisible(GameObject container, bool visible)
    {
        if (container != null)
            container.SetActive(visible);
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

    private GameObject FindGameObjectInScene(string objectName)
    {
        Scene currentScene = gameObject.scene;

        foreach (GameObject root in currentScene.GetRootGameObjects())
        {
            Transform[] transforms = root.GetComponentsInChildren<Transform>(true);
            foreach (Transform candidate in transforms)
            {
                if (candidate.name == objectName)
                    return candidate.gameObject;
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
        ResolveContainerReferences();

        SetContainerVisible(hintTextContainer, false);
        SetContainerVisible(hintImageContainer, false);
        SetContainerVisible(hintVideoContainer, false);
        SetContainerVisible(hintLinkContainer, false);

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
