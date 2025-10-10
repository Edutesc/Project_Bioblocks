using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Sistema melhorado de feedback com título e texto
/// </summary>
public class FeedbackUIElements : MonoBehaviour
{
    [Header("Feedback Simples (Respostas)")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Image feedbackPanel;

    [Header("Feedback Completo (Conclusão de Nível)")]
    [SerializeField] private CanvasGroup levelCompletionFeedbackGroup;
    [SerializeField] private TextMeshProUGUI levelCompletionTitle;
    [SerializeField] private TextMeshProUGUI levelCompletionText;
    [SerializeField] private Image levelCompletionPanel;

    [Header("Feedback de Questões Completas")]
    [SerializeField] private TextMeshProUGUI questionsCompletedFeedbackText;

    [Header("Configurações")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayDuration = 3f;

    // Propriedades públicas (mantém compatibilidade)
    public TextMeshProUGUI FeedbackText => feedbackText;
    public Image FeedbackPanel => feedbackPanel;
    public TextMeshProUGUI QuestionsCompletedFeedbackText => questionsCompletedFeedbackText;
    public float FadeDuration => fadeDuration;
    public float DisplayDuration => displayDuration;

    // Novas propriedades
    public CanvasGroup LevelCompletionFeedbackGroup => levelCompletionFeedbackGroup;
    public TextMeshProUGUI LevelCompletionTitle => levelCompletionTitle;
    public TextMeshProUGUI LevelCompletionText => levelCompletionText;
    public Image LevelCompletionPanel => levelCompletionPanel;

    private Coroutine currentFeedbackCoroutine;

    private void Awake()
    {
        ValidateComponents();

        // Inicializar feedback de conclusão de nível como invisível
        if (levelCompletionFeedbackGroup != null)
        {
            levelCompletionFeedbackGroup.alpha = 0f;
            levelCompletionFeedbackGroup.interactable = false;
            levelCompletionFeedbackGroup.blocksRaycasts = false;
            levelCompletionFeedbackGroup.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Mostra feedback simples (para respostas corretas/incorretas)
    /// </summary>
    public void ShowSimpleFeedback(string message, bool isCorrect)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
        }

        if (feedbackPanel != null)
        {
            feedbackPanel.gameObject.SetActive(true);
            feedbackPanel.color = isCorrect ? HexToColor("#D4EDDA") : HexToColor("#F8D7DA");
        }
    }

    /// <summary>
    /// Mostra feedback de conclusão de nível com título e texto
    /// </summary>
    public void ShowLevelCompletionFeedback(string title, string bodyText, bool isSuccess = true)
    {
        if (levelCompletionFeedbackGroup == null)
        {
            Debug.LogWarning("LevelCompletionFeedbackGroup não está atribuído. Usando feedback simples.");
            ShowSimpleFeedback($"{title}\n{bodyText}", isSuccess);
            return;
        }

        // Define título e texto
        if (levelCompletionTitle != null)
        {
            levelCompletionTitle.text = title;
        }

        if (levelCompletionText != null)
        {
            levelCompletionText.text = bodyText;
        }

        // NÃO altera a cor do painel - mantém a cor configurada no Inspector

        // Para animação anterior se houver
        if (currentFeedbackCoroutine != null)
        {
            StopCoroutine(currentFeedbackCoroutine);
        }

        // Mostra com animação
        currentFeedbackCoroutine = StartCoroutine(ShowLevelFeedbackCoroutine());
    }

    /// <summary>
    /// Animação de entrada do feedback de nível
    /// </summary>
    private IEnumerator ShowLevelFeedbackCoroutine()
    {
        // Ativa o grupo
        levelCompletionFeedbackGroup.gameObject.SetActive(true);
        levelCompletionFeedbackGroup.interactable = true;
        levelCompletionFeedbackGroup.blocksRaycasts = true;

        // Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            levelCompletionFeedbackGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }

        levelCompletionFeedbackGroup.alpha = 1f;

        // Aguarda tempo de exibição
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            levelCompletionFeedbackGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        // Desativa
        levelCompletionFeedbackGroup.alpha = 0f;
        levelCompletionFeedbackGroup.interactable = false;
        levelCompletionFeedbackGroup.blocksRaycasts = false;
        levelCompletionFeedbackGroup.gameObject.SetActive(false);

        currentFeedbackCoroutine = null;
    }

    /// <summary>
    /// Esconde o feedback de nível imediatamente
    /// </summary>
    public void HideLevelCompletionFeedback()
    {
        if (currentFeedbackCoroutine != null)
        {
            StopCoroutine(currentFeedbackCoroutine);
            currentFeedbackCoroutine = null;
        }

        if (levelCompletionFeedbackGroup != null)
        {
            levelCompletionFeedbackGroup.alpha = 0f;
            levelCompletionFeedbackGroup.interactable = false;
            levelCompletionFeedbackGroup.blocksRaycasts = false;
            levelCompletionFeedbackGroup.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Esconde feedback simples
    /// </summary>
    public void HideSimpleFeedback()
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.gameObject.SetActive(false);
        }
    }

    public void ValidateComponents()
    {
        if (feedbackText == null)
            Debug.LogWarning("FeedbackText não atribuído (feedback simples)");

        if (feedbackPanel == null)
            Debug.LogWarning("FeedbackPanel não atribuído (feedback simples)");

        if (questionsCompletedFeedbackText == null)
            Debug.LogWarning("QuestionsCompletedFeedbackText não atribuído");

        // Novos componentes (opcional)
        if (levelCompletionFeedbackGroup == null)
            Debug.LogWarning("LevelCompletionFeedbackGroup não atribuído (feedback de nível)");
    }

    private Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}