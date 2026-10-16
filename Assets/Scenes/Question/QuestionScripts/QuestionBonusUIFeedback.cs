using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionBonusUIFeedback : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI bonusMessageText;
    [SerializeField] private Image bonusPanel;

    private CanvasGroup canvasGroup;
    private QuestionTimerManager timerManager;
    private QuestionCanvasGroupManager groupManager;
    private bool modalOpen;

    private void Awake()
    {
        if (bonusMessageText == null)
        {
            bonusMessageText = transform.Find("FeedbackText")?.GetComponent<TextMeshProUGUI>();
            if (bonusMessageText == null)
                Debug.LogError("QuestionBonusUIFeedback: Não foi possível encontrar o TextMeshProUGUI 'FeedbackText'");
        }

        if (bonusPanel == null)
        {
            bonusPanel = GetComponent<Image>();
            if (bonusPanel == null)
                Debug.LogError("QuestionBonusUIFeedback: Não foi possível encontrar o componente Image");
        }
        // O controlador pode estar fora do objeto visual (como na QuestionScene).
        GameObject visualRoot = bonusPanel != null ? bonusPanel.gameObject : gameObject;
        canvasGroup = visualRoot.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = visualRoot.AddComponent<CanvasGroup>();

    }

    private void Start()
    {
        if (!modalOpen) HideFeedback();
    }

    // Vincule este método ao On Click do botão OK no Inspector.
    public void DismissBonusFeedback() => HideFeedback();

    private void OnDisable()
    {
        ReleaseModal();
    }

    private void ReleaseModal()
    {
        if (!modalOpen) return;
        modalOpen = false;
        if (timerManager != null) timerManager.SetBonusFeedbackPaused(false);
        if (groupManager != null) groupManager.SetBonusModalOpen(false);
    }

    public void ShowBonusActivatedFeedback()
    {
        Debug.Log("Mostrando feedback de bônus");
        gameObject.SetActive(true);
        canvasGroup.gameObject.SetActive(true);
        if (timerManager == null) timerManager = FindFirstObjectByType<QuestionTimerManager>();
        if (groupManager == null) groupManager = FindFirstObjectByType<QuestionCanvasGroupManager>();
        modalOpen = true;
        timerManager?.SetBonusFeedbackPaused(true);
        groupManager?.SetBonusModalOpen(true);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void HideFeedback()
    {
        Debug.Log("Escondendo feedback de bônus");
        ReleaseModal();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsVisible()
    {
        if (canvasGroup != null)
            return canvasGroup.alpha > 0f && canvasGroup.gameObject.activeInHierarchy;
        
        return gameObject.activeSelf;
    }

    public void ForceVisibility(bool visible)
    {
        if (visible)
            ShowBonusActivatedFeedback();
        else
            HideFeedback();
    }
}