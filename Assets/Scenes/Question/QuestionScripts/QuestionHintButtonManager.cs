using System.Collections;
using System.Collections.Generic;
using QuestionSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionHintButtonManager : MonoBehaviour
{
    [SerializeField] private Button hintButton;
    [SerializeField] private Image hintButtonImage;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform buttonRectTransform;

    [Header("Sprites por nível")]
    [SerializeField] private Sprite level1ButtonSprite;
    [SerializeField] private Sprite level2ButtonSprite;
    [SerializeField] private Sprite level3ButtonSprite;

    [Header("Navigation")]
    [SerializeField] private string hintSceneName = "QuestionHintScene";

    [Header("Animation")]
    [SerializeField] private float dropOffsetY = 80f;
    [SerializeField] private float animationDuration = 0.35f;

    private Question _currentQuestion;
    private Vector2 _visibleAnchoredPosition;
    private Coroutine _animationRoutine;
    private bool _hasCachedVisiblePosition;
    private bool _isOpeningHintScene;

    private void Awake()
    {
        ResolveReferences();

        CacheVisiblePosition();
        ConfigureButton();
        HideInstant();
    }

    public void HideInstant()
    {
        StopActiveAnimation();
        CacheVisiblePosition();

        _currentQuestion = null;

        if (buttonRectTransform != null)
            buttonRectTransform.anchoredPosition = _visibleAnchoredPosition;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        gameObject.SetActive(false);
    }

    public void ShowForQuestion(Question question)
    {
        if (!HasHint(question))
        {
            HideInstant();
            return;
        }

        _currentQuestion = question;
        CacheVisiblePosition();
        ResolveButtonImageReference();
        ApplySpriteForQuestionLevel(question.questionLevel);

        gameObject.SetActive(true);
        StopActiveAnimation();
        _animationRoutine = StartCoroutine(AnimateDropIn());
    }

    public void OpenHintScene()
    {
        if (!HasHint(_currentQuestion))
            return;

        if (_isOpeningHintScene)
            return;

        StartCoroutine(OpenHintSceneRoutine());
    }

    private IEnumerator OpenHintSceneRoutine()
    {
        _isOpeningHintScene = true;

        var sceneData = new Dictionary<string, object>
        {
            { "question", _currentQuestion }
        };

        if (AppContext.SceneData != null)
            AppContext.SceneData.SetData(sceneData);
        else
            Debug.LogError("[QuestionHintButtonManager] SceneData não disponível no AppContext.");

        Scene loadedHintScene = SceneManager.GetSceneByName(hintSceneName);
        if (loadedHintScene.IsValid() && loadedHintScene.isLoaded)
        {
            RefreshOpenHintScene(loadedHintScene);
            _isOpeningHintScene = false;
            yield break;
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(hintSceneName, LoadSceneMode.Additive);
        if (loadOperation == null)
        {
            Debug.LogError($"[QuestionHintButtonManager] Não foi possível carregar a cena '{hintSceneName}'.");
            _isOpeningHintScene = false;
            yield break;
        }

        while (!loadOperation.isDone)
            yield return null;

        Scene hintScene = SceneManager.GetSceneByName(hintSceneName);
        if (hintScene.IsValid())
            RefreshOpenHintScene(hintScene);

        _isOpeningHintScene = false;
    }

    private IEnumerator AnimateDropIn()
    {
        if (buttonRectTransform == null || canvasGroup == null)
            yield break;

        Vector2 startPosition = _visibleAnchoredPosition + Vector2.up * dropOffsetY;
        Vector2 endPosition = _visibleAnchoredPosition;

        buttonRectTransform.anchoredPosition = startPosition;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float elapsed = 0f;
        float duration = Mathf.Max(0.01f, animationDuration);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float eased = Mathf.SmoothStep(0f, 1f, t);

            buttonRectTransform.anchoredPosition = Vector2.LerpUnclamped(startPosition, endPosition, eased);
            canvasGroup.alpha = eased;

            yield return null;
        }

        buttonRectTransform.anchoredPosition = endPosition;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        _animationRoutine = null;
    }

    private void ConfigureButton()
    {
        if (hintButton == null)
            return;

        hintButton.onClick.RemoveListener(OpenHintScene);
        hintButton.onClick.AddListener(OpenHintScene);
    }

    private void RefreshOpenHintScene(Scene hintScene)
    {
        foreach (GameObject root in hintScene.GetRootGameObjects())
        {
            QuestionHintManager[] managers = root.GetComponentsInChildren<QuestionHintManager>(true);
            foreach (QuestionHintManager manager in managers)
                manager.ShowAsModal(_currentQuestion);
        }
    }

    private void ResolveReferences()
    {
        if (hintButton == null)
            hintButton = GetComponent<Button>();

        ResolveButtonImageReference();

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if (buttonRectTransform == null)
            buttonRectTransform = transform as RectTransform;
    }

    private void ResolveButtonImageReference()
    {
        if (hintButtonImage != null)
            return;

        if (hintButton != null && hintButton.targetGraphic is Image targetImage)
        {
            hintButtonImage = targetImage;
            return;
        }

        if (hintButton != null)
            hintButtonImage = hintButton.GetComponent<Image>();

        if (hintButtonImage == null)
            hintButtonImage = GetComponent<Image>();
    }

    private void ApplySpriteForQuestionLevel(int questionLevel)
    {
        if (hintButtonImage == null)
            return;

        Sprite sprite = GetSpriteForQuestionLevel(questionLevel);
        if (sprite != null)
            hintButtonImage.sprite = sprite;
    }

    private Sprite GetSpriteForQuestionLevel(int questionLevel)
    {
        return questionLevel switch
        {
            1 => level1ButtonSprite,
            2 => level2ButtonSprite,
            3 => level3ButtonSprite,
            _ => level1ButtonSprite
        };
    }

    private void CacheVisiblePosition()
    {
        if (_hasCachedVisiblePosition || buttonRectTransform == null)
            return;

        _visibleAnchoredPosition = buttonRectTransform.anchoredPosition;
        _hasCachedVisiblePosition = true;
    }

    private void StopActiveAnimation()
    {
        if (_animationRoutine == null)
            return;

        StopCoroutine(_animationRoutine);
        _animationRoutine = null;
    }

    private static bool HasHint(Question question)
    {
        return question?.questionHint != null && question.questionHint.HasAnyHint;
    }

    private void OnDestroy()
    {
        StopActiveAnimation();

        if (hintButton != null)
            hintButton.onClick.RemoveListener(OpenHintScene);
    }
}
