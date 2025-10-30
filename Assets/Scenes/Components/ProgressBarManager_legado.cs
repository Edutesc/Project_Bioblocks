using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Controla a barra de progresso de respostas corretas
/// Substitui o texto "Respostas corretas: X de Y" por uma visualização gráfica
/// </summary>
public class ProgressBarManager_legado : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fillImage; // A imagem azul que vai preencher
    [SerializeField] private TextMeshProUGUI progressText; // "25 de 68"
    [SerializeField] private TextMeshProUGUI levelNameText; // "Nível Básico"

    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.8f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private bool animateOnUpdate = true;

    private float currentFillAmount = 0f;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        if (fillImage == null)
        {
            Debug.LogError("ProgressBarController: fillImage não está atribuído!");
        }

        // Inicializa com fill zerado
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
    }

    /// <summary>
    /// Atualiza a barra de progresso com os valores atuais
    /// </summary>
    /// <param name="answeredCount">Número de questões respondidas corretamente</param>
    /// <param name="totalCount">Total de questões no nível</param>
    /// <param name="levelName">Nome do nível (ex: "Nível Básico")</param>
    public void UpdateProgress(int answeredCount, int totalCount, string levelName)
    {
        if (fillImage == null)
        {
            Debug.LogWarning("ProgressBarController: fillImage não está configurado!");
            return;
        }

        // Calcula o progresso (0 a 1)
        float targetProgress = totalCount > 0 ? (float)answeredCount / totalCount : 0f;

        // Atualiza o texto de progresso
        if (progressText != null)
        {
            progressText.text = $"{answeredCount} de {totalCount}";
        }

        // Atualiza o texto do nível
        if (levelNameText != null)
        {
            levelNameText.text = levelName;
        }

        // Anima ou atualiza diretamente
        if (animateOnUpdate && gameObject.activeInHierarchy)
        {
            AnimateToProgress(targetProgress);
        }
        else
        {
            fillImage.fillAmount = targetProgress;
            currentFillAmount = targetProgress;
        }

        Debug.Log($"📊 ProgressBar atualizada: {answeredCount}/{totalCount} ({targetProgress:P0}) - {levelName}");
    }

    /// <summary>
    /// Anima a barra de progresso até o valor alvo
    /// </summary>
    private void AnimateToProgress(float targetProgress)
    {
        // Para animação anterior se existir
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(AnimateProgressCoroutine(targetProgress));
    }

    /// <summary>
    /// Corrotina para animar o progresso suavemente
    /// </summary>
    private IEnumerator AnimateProgressCoroutine(float targetProgress)
    {
        float startProgress = currentFillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / animationDuration);

            // Aplica a curva de animação
            float curveValue = animationCurve.Evaluate(normalizedTime);

            // Interpola entre o valor inicial e o alvo
            currentFillAmount = Mathf.Lerp(startProgress, targetProgress, curveValue);
            fillImage.fillAmount = currentFillAmount;

            yield return null;
        }

        // Garante que o valor final seja exato
        fillImage.fillAmount = targetProgress;
        currentFillAmount = targetProgress;

        animationCoroutine = null;
    }

    /// <summary>
    /// Define o progresso instantaneamente sem animação
    /// </summary>
    public void SetProgressImmediate(int answeredCount, int totalCount, string levelName)
    {
        bool previousAnimateState = animateOnUpdate;
        animateOnUpdate = false;

        UpdateProgress(answeredCount, totalCount, levelName);

        animateOnUpdate = previousAnimateState;
    }

    /// <summary>
    /// Reseta a barra para 0
    /// </summary>
    public void ResetProgress()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }

        currentFillAmount = 0f;

        if (progressText != null)
        {
            progressText.text = "0 de 0";
        }

        if (levelNameText != null)
        {
            levelNameText.text = "";
        }
    }

    /// <summary>
    /// Retorna o progresso atual (0 a 1)
    /// </summary>
    public float GetCurrentProgress()
    {
        return currentFillAmount;
    }

    /// <summary>
    /// Retorna o progresso atual em percentual (0 a 100)
    /// </summary>
    public float GetCurrentProgressPercentage()
    {
        return currentFillAmount * 100f;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Garante que a duração seja positiva
        if (animationDuration < 0.1f)
        {
            animationDuration = 0.1f;
        }
    }
#endif
}
