using System.Collections;
using System.Collections.Generic;
using QuestionSystem;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHintButtonManager : MonoBehaviour
{
    [SerializeField] private Button hintButton;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform buttonRectTransform;
    [SerializeField] private string hintSceneName = "QuestionHintScene";
    [SerializeField] private float dropOffsetY = 80f;
    [SerializeField] private float animationDuration = 0.35f;

    private Question _currentQuestion;
    private Vector2 _visibleAnchoredPosition;
    private Coroutine _animationRoutine;
    private bool _hasCachedVisiblePosition;

    private void Awake()
    {
        if (hintButton == null)
            hintButton = GetComponent<Button>();

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if (buttonRectTransform == null)
            buttonRectTransform = transform as RectTransform;

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

        gameObject.SetActive(true);
        StopActiveAnimation();
        _animationRoutine = StartCoroutine(AnimateDropIn());
    }

    public void OpenHintScene()
    {
        if (!HasHint(_currentQuestion))
            return;

        var sceneData = new Dictionary<string, object>
        {
            { "question", _currentQuestion }
        };

        if (AppContext.Navigation != null)
            AppContext.Navigation.NavigateTo(hintSceneName, sceneData);
        else
            Debug.LogError("[QuestionHintButtonManager] Navigation não disponível no AppContext.");
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
