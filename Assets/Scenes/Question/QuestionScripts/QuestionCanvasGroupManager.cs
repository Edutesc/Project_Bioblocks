using QuestionSystem;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCanvasGroupManager : MonoBehaviour
{
    [Header("Question UI")]
    [SerializeField] private CanvasGroup questionTextContainer;
    [SerializeField] private CanvasGroup questionImageContainer;

    [Header("Answer UI")]
    [SerializeField] private CanvasGroup answerTextCanvasGroup;
    [SerializeField] private CanvasGroup answerImageCanvasGroup;
    [SerializeField] private CanvasGroup answerOpenCanvasGroup;

    [Header("Feedback UI")]
    [SerializeField] private CanvasGroup questionsCompletedFeedback;
    [SerializeField] private Image feedbackPanel;

    [Header("Bonus UI")]
    [SerializeField] private CanvasGroup questionBonusUIFeedback;

    [Header("Other UI")]
    [SerializeField] private CanvasGroup loadingCanvasGroup;
    [SerializeField] private CanvasGroup questionBottomBar;

    [Header("Level Configuration")]
    [SerializeField] private QuestionLevelConfig levelConfig;

    [Header("Background Images")]
    [SerializeField] private Image questionTextBackgroundImage;
    [SerializeField] private Image questionImageBackgroundImage;

    private int currentQuestionLevel = 1;

    private void Awake()
    {
        if (!AreAllCanvasGroupsAssigned())
        {
            Debug.LogError("Alguns CanvasGroups não estão atribuídos corretamente!");
            return;
        }

        InitializeCanvasGroups();
    }

    public void ShowLoading()
    {
        SetCanvasGroupState(loadingCanvasGroup,    true);
        SetCanvasGroupState(questionTextContainer, false);
        SetCanvasGroupState(questionImageContainer, false);
        SetCanvasGroupState(answerTextCanvasGroup, false);
        SetCanvasGroupState(answerImageCanvasGroup, false);
        SetCanvasGroupState(answerOpenCanvasGroup, false);
        SetCanvasGroupState(questionBottomBar,     false);
    }

    /// <summary>Mostra os canvas groups corretos de acordo com o tipo de questão/resposta (enum).</summary>
    public void ShowQuestion(QuestionType questionType, AnswerType answerType, int questionLevel)
    {
        currentQuestionLevel = questionLevel;
        SetCanvasGroupState(loadingCanvasGroup,     false);
        SetCanvasGroupState(questionTextContainer,  questionType == QuestionType.Text);
        SetCanvasGroupState(questionImageContainer, questionType == QuestionType.Image);
        SetCanvasGroupState(answerTextCanvasGroup,  answerType   == AnswerType.Text);
        SetCanvasGroupState(answerImageCanvasGroup, answerType   == AnswerType.Image);
        SetCanvasGroupState(answerOpenCanvasGroup,  answerType   == AnswerType.Open);
        SetCanvasGroupState(questionBottomBar,      true);
    }

    /// <summary>Overload legado — mantido para retrocompatibilidade.</summary>
    public void ShowQuestion(bool isImageQuestion, bool isImageAnswer, int questionLevel)
    {
        ShowQuestion(
            isImageQuestion ? QuestionType.Image : QuestionType.Text,
            isImageAnswer   ? AnswerType.Image   : AnswerType.Text,
            questionLevel);
    }

    public Color GetFeedbackColorForCurrentLevel(bool isCorrect)
    {
        if (levelConfig == null) return isCorrect ? Color.green : Color.red;

        var theme = levelConfig.GetThemeForLevel(currentQuestionLevel);
        return theme != null
            ? (isCorrect ? theme.feedbackCorrectColor : theme.feedbackIncorrectColor)
            : (isCorrect ? Color.green : Color.red);
    }

    public void ShowAnswerFeedback(bool isCorrect, Color correctColor, Color incorrectColor)
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.gameObject.SetActive(true);
            feedbackPanel.color = isCorrect ? correctColor : incorrectColor;
        }
    }

    public void HideAnswerFeedback()
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.gameObject.SetActive(false);
        }
    }

    public void ShowCompletionFeedback()
    {
        if (questionTextContainer  != null) questionTextContainer.gameObject.SetActive(false);
        if (questionImageContainer != null) questionImageContainer.gameObject.SetActive(false);
        if (answerTextCanvasGroup  != null) answerTextCanvasGroup.gameObject.SetActive(false);
        if (answerImageCanvasGroup != null) answerImageCanvasGroup.gameObject.SetActive(false);
        if (answerOpenCanvasGroup  != null) answerOpenCanvasGroup.gameObject.SetActive(false);

        if (feedbackPanel != null)
        {
            feedbackPanel.gameObject.SetActive(false);
        }

        foreach (var canvasGroup in GetAllCanvasGroups())
        {
            if (canvasGroup != null && canvasGroup != questionsCompletedFeedback && canvasGroup != questionBonusUIFeedback)
            {
                SetCanvasGroupState(canvasGroup, false);
            }
        }

        SetCanvasGroupState(questionBottomBar, false);

        if (questionsCompletedFeedback != null)
        {
            questionsCompletedFeedback.gameObject.SetActive(true);
            questionsCompletedFeedback.alpha = 1f;
            questionsCompletedFeedback.interactable = true;
            questionsCompletedFeedback.blocksRaycasts = true;
        }
        else
        {
            Debug.LogError("Erro crítico: questionsCompletedFeedback é nulo!");
        }
    }

    public void HideCompletionFeedback()
    {
        if (questionsCompletedFeedback != null)
        {
            questionsCompletedFeedback.gameObject.SetActive(false);
            SetCanvasGroupState(questionsCompletedFeedback, false);
        }
    }

    public void ShowBonusFeedback(bool show)
    {
        if (questionBonusUIFeedback != null)
        {
            questionBonusUIFeedback.gameObject.SetActive(show);
            questionBonusUIFeedback.alpha = show ? 1f : 0f;
            questionBonusUIFeedback.interactable = show;
            questionBonusUIFeedback.blocksRaycasts = show;
        }
        else
        {
            Debug.LogWarning("bonusFeedbackCanvasGroup não está atribuído! Não é possível mostrar o feedback de bônus.");
        }
    }

    public void DisableAnswers()
    {
        SetCanvasGroupInteractable(answerTextCanvasGroup,  false);
        SetCanvasGroupInteractable(answerImageCanvasGroup, false);
        SetCanvasGroupInteractable(answerOpenCanvasGroup,  false);
    }

    private void InitializeCanvasGroups()
    {
        TopBarManager topBar = FindFirstObjectByType<TopBarManager>();
        GameObject topBarObj = topBar?.gameObject;

        foreach (var canvasGroup in GetAllCanvasGroups())
        {
            if (canvasGroup != null)
            {
                if (canvasGroup != questionBonusUIFeedback &&
                    (topBarObj == null || !IsChildOrSelf(canvasGroup.gameObject, topBarObj)))
                {
                    SetCanvasGroupState(canvasGroup, false);
                }
                else if (canvasGroup == questionBonusUIFeedback)
                {
                    canvasGroup.gameObject.SetActive(false);
                    canvasGroup.alpha = 0f;
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }
            }
        }

        if (topBar != null)
        {
            if (!topBarObj.activeInHierarchy)
            {
                topBarObj.SetActive(true);
            }
        }
    }

    private bool IsChildOrSelf(GameObject child, GameObject parent)
    {
        if (child == parent)
            return true;

        Transform childTransform = child.transform;
        while (childTransform.parent != null)
        {
            if (childTransform.parent.gameObject == parent)
                return true;

            childTransform = childTransform.parent;
        }

        return false;
    }

    private void SetCanvasGroupState(CanvasGroup canvasGroup, bool active)
    {
        if (canvasGroup == null) return;

        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = active ? 1f : 0f;
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
    }

    private void SetCanvasGroupInteractable(CanvasGroup canvasGroup, bool interactable)
    {
        if (canvasGroup == null) return;

        if (!interactable)
        {
            StopAllButtonAnimations(canvasGroup);
        }

        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = interactable;
    }

    private void StopAllButtonAnimations(CanvasGroup canvasGroup)
    {
        if (canvasGroup == null) return;

        Button[] buttons = canvasGroup.GetComponentsInChildren<Button>();

        foreach (Button btn in buttons)
        {
            ButtonPressEffect pressEffect = btn.GetComponent<ButtonPressEffect>();

            if (pressEffect != null)
            {
                pressEffect.ForceStopAnimation();
            }
        }

        Debug.Log($"Animações paradas para {buttons.Length} botões no CanvasGroup: {canvasGroup.name}");
    }

    private CanvasGroup[] GetAllCanvasGroups()
    {
        return new CanvasGroup[]
        {
            loadingCanvasGroup,
            answerTextCanvasGroup,
            answerImageCanvasGroup,
            answerOpenCanvasGroup,
            questionsCompletedFeedback,
            questionBottomBar,
            questionTextContainer,
            questionImageContainer,
            questionBonusUIFeedback
        };
    }

    public CanvasGroup LoadingCanvasGroup     => loadingCanvasGroup;
    public CanvasGroup AnswerTextCanvasGroup  => answerTextCanvasGroup;
    public CanvasGroup AnswerImageCanvasGroup => answerImageCanvasGroup;
    public CanvasGroup AnswerOpenCanvasGroup  => answerOpenCanvasGroup;
    public CanvasGroup QuestionsCompletedFeedback => questionsCompletedFeedback;
    public CanvasGroup BottomBar              => questionBottomBar;
    public CanvasGroup QuestionTextContainer  => questionTextContainer;
    public CanvasGroup QuestionImageContainer => questionImageContainer;
    public CanvasGroup BonusFeedbackCanvasGroup => questionBonusUIFeedback;

    public bool AreAllCanvasGroupsAssigned()
    {
        bool allAssigned = true;

        if (answerImageCanvasGroup == null)
        {
            Debug.LogError("AnswerImageCanvasGroup não está atribuído no Inspector");
            allAssigned = false;
        }

        if (answerTextCanvasGroup == null)
        {
            Debug.LogError("AnswerTextCanvasGroup não está atribuído no Inspector");
            allAssigned = false;
        }

        if (questionImageContainer == null)
        {
            Debug.LogError("QuestionImageContainer não está atribuído no Inspector");
            allAssigned = false;
        }

        if (questionTextContainer == null)
        {
            Debug.LogError("QuestionTextContainer não está atribuído no Inspector");
            allAssigned = false;
        }

        return allAssigned;
    }
}