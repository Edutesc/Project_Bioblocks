using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Sistema de feedback REFATORADO para usar imagens estáticas
/// Mantém compatibilidade com código existente
/// </summary>
public class FeedbackUIElements : MonoBehaviour
{
    [Header("Feedback Simples (Respostas) - NOVO SISTEMA COM SPRITES")]
    [SerializeField] private Image feedbackPanel;
    
    [Header("Sprites de Feedback")]
    [SerializeField] private Sprite feedbackCorrect5Points;
    [Tooltip("Imagem: 'Resposta correta! +5 pontos'")]
    
    [SerializeField] private Sprite feedbackCorrect10PointsBonus;
    [Tooltip("Imagem: 'Resposta correta! +10 pontos (Bônus ativo!)'")]
    
    [SerializeField] private Sprite feedbackWrong;
    [Tooltip("Imagem: 'Resposta errada! -2 pontos'")]
    
    [SerializeField] private Sprite feedbackTimeout;
    [Tooltip("Imagem: 'Tempo Esgotado! -1 ponto'")]

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
    [SerializeField] private bool useAnimation = true;

    // OBSOLETO: Mantido por compatibilidade, mas não usado mais
    [Header("OBSOLETO - Será removido")]
    [SerializeField] private TextMeshProUGUI feedbackText;

    // Propriedades públicas (mantém compatibilidade)
    public Image FeedbackPanel => feedbackPanel;
    public TextMeshProUGUI QuestionsCompletedFeedbackText => questionsCompletedFeedbackText;
    public float FadeDuration => fadeDuration;
    public float DisplayDuration => displayDuration;

    // Propriedades de conclusão de nível
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

        // Esconde feedback simples no início
        if (feedbackPanel != null)
        {
            feedbackPanel.gameObject.SetActive(false);
        }

        // Esconde o texto antigo (obsoleto)
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    // ========================================
    // NOVOS MÉTODOS - SISTEMA COM SPRITES
    // ========================================

    /// <summary>
    /// Mostra feedback de resposta correta
    /// </summary>
    /// <param name="bonusActive">Se true, mostra imagem de +10 pontos (bônus). Se false, mostra +5 pontos</param>
    public void ShowCorrectAnswer(bool bonusActive)
    {
        Sprite sprite = bonusActive ? feedbackCorrect10PointsBonus : feedbackCorrect5Points;
        ShowFeedbackSprite(sprite);
    }

    /// <summary>
    /// Mostra feedback de resposta errada
    /// </summary>
    public void ShowWrongAnswer()
    {
        ShowFeedbackSprite(feedbackWrong);
    }

    /// <summary>
    /// Mostra feedback de tempo esgotado
    /// </summary>
    public void ShowTimeout()
    {
        ShowFeedbackSprite(feedbackTimeout);
    }

    /// <summary>
    /// Método interno para exibir um sprite de feedback
    /// </summary>
    private void ShowFeedbackSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogError("[FeedbackUIElements] Sprite de feedback não atribuído!");
            return;
        }

        if (feedbackPanel == null)
        {
            Debug.LogError("[FeedbackUIElements] FeedbackPanel não atribuído!");
            return;
        }

        // Para a coroutine anterior se existir
        if (currentFeedbackCoroutine != null)
        {
            StopCoroutine(currentFeedbackCoroutine);
        }

        // Define o sprite
        feedbackPanel.sprite = sprite;
        feedbackPanel.color = Color.white; // Remove qualquer tint de cor

        // Mostra o feedback
        feedbackPanel.gameObject.SetActive(true);

        // Inicia a animação se estiver habilitada
        if (useAnimation)
        {
            currentFeedbackCoroutine = StartCoroutine(AnimateFeedbackSprite());
        }
    }

    /// <summary>
    /// Animação de fade para o feedback com sprite
    /// </summary>
    private IEnumerator AnimateFeedbackSprite()
    {
        // Fade in
        Color color = feedbackPanel.color;
        color.a = 0f;
        feedbackPanel.color = color;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            feedbackPanel.color = color;
            yield return null;
        }

        color.a = 1f;
        feedbackPanel.color = color;

        // Aguarda tempo de exibição
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            feedbackPanel.color = color;
            yield return null;
        }

        // Esconde
        HideSimpleFeedback();

        currentFeedbackCoroutine = null;
    }

    // ========================================
    // MÉTODOS ANTIGOS - MANTIDOS POR COMPATIBILIDADE
    // ========================================

    /// <summary>
    /// [OBSOLETO] Mostra feedback simples com texto
    /// NOTA: Este método ainda funciona mas não é mais usado com o novo sistema de sprites
    /// Mantido apenas para compatibilidade com código antigo
    /// </summary>
    [System.Obsolete("Use ShowCorrectAnswer(), ShowWrongAnswer() ou ShowTimeout() em vez disso")]
    public void ShowSimpleFeedback(string message, bool isCorrect)
    {
        Debug.LogWarning("[FeedbackUIElements] ShowSimpleFeedback está obsoleto. Use ShowCorrectAnswer/ShowWrongAnswer/ShowTimeout");
        
        // Fallback: Se ainda for chamado, mostra o sprite apropriado
        if (isCorrect)
        {
            ShowCorrectAnswer(bonusActive: false);
        }
        else
        {
            ShowWrongAnswer();
        }
    }

    /// <summary>
    /// Esconde feedback simples
    /// </summary>
    public void HideSimpleFeedback()
    {
        if (currentFeedbackCoroutine != null)
        {
            StopCoroutine(currentFeedbackCoroutine);
            currentFeedbackCoroutine = null;
        }

        if (feedbackPanel != null)
        {
            feedbackPanel.gameObject.SetActive(false);
        }
    }

    // ========================================
    // MÉTODOS DE CONCLUSÃO DE NÍVEL (SEM ALTERAÇÕES)
    // ========================================

    /// <summary>
    /// Mostra feedback de conclusão de nível com título e texto
    /// </summary>
    public void ShowLevelCompletionFeedback(string title, string bodyText, bool isSuccess = true)
    {
        if (levelCompletionFeedbackGroup == null)
        {
            Debug.LogWarning("LevelCompletionFeedbackGroup não está atribuído.");
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

    // ========================================
    // VALIDAÇÃO
    // ========================================

    public void ValidateComponents()
    {
        bool hasErrors = false;

        // Componentes essenciais
        if (feedbackPanel == null)
        {
            Debug.LogError("[FeedbackUIElements] FeedbackPanel não atribuído!");
            hasErrors = true;
        }

        // Sprites de feedback
        if (feedbackCorrect5Points == null)
        {
            Debug.LogError("[FeedbackUIElements] Sprite 'feedbackCorrect5Points' não atribuído!");
            hasErrors = true;
        }

        if (feedbackCorrect10PointsBonus == null)
        {
            Debug.LogError("[FeedbackUIElements] Sprite 'feedbackCorrect10PointsBonus' não atribuído!");
            hasErrors = true;
        }

        if (feedbackWrong == null)
        {
            Debug.LogError("[FeedbackUIElements] Sprite 'feedbackWrong' não atribuído!");
            hasErrors = true;
        }

        if (feedbackTimeout == null)
        {
            Debug.LogError("[FeedbackUIElements] Sprite 'feedbackTimeout' não atribuído!");
            hasErrors = true;
        }

        // Componentes opcionais
        if (questionsCompletedFeedbackText == null)
            Debug.LogWarning("[FeedbackUIElements] QuestionsCompletedFeedbackText não atribuído");

        if (levelCompletionFeedbackGroup == null)
            Debug.LogWarning("[FeedbackUIElements] LevelCompletionFeedbackGroup não atribuído (feedback de nível)");

        if (hasErrors)
        {
            Debug.LogError("[FeedbackUIElements] Configure os sprites de feedback no Inspector!");
        }
    }

    // ========================================
    // UTILITÁRIOS
    // ========================================

    private Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    /// <summary>
    /// Configura o tempo de exibição do feedback
    /// </summary>
    public void SetDisplayDuration(float duration)
    {
        displayDuration = duration;
    }

    /// <summary>
    /// Configura o tempo de fade in/out
    /// </summary>
    public void SetFadeDuration(float duration)
    {
        fadeDuration = duration;
    }

    /// <summary>
    /// Ativa ou desativa as animações
    /// </summary>
    public void SetAnimationEnabled(bool enabled)
    {
        useAnimation = enabled;
    }
}