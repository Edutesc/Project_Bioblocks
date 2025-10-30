using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Componente genérico para animar barras de progresso
/// Pode ser usado em qualquer lugar da UI com qualquer dimensão
/// </summary>
public class ProgressBarManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("A Image que será preenchida (pode ter qualquer cor/tamanho)")]
    [SerializeField] private Image fillImage;
    
    [Tooltip("Texto opcional 1 - Geralmente usado para mostrar valores (ex: '359 de 492', '75%')")]
    [SerializeField] private TextMeshProUGUI progressText;

    [Tooltip("Texto opcional 2 - Geralmente usado para mostrar labels/títulos (ex: 'Nível Básico', 'Level 6')")]
    [SerializeField] private TextMeshProUGUI labelText;

    [Header("Animation Settings")]
    [Tooltip("Duração da animação em segundos")]
    [SerializeField] private float animationDuration = 0.8f;
    
    [Tooltip("Curva de animação (suave, linear, etc)")]
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Tooltip("Animar automaticamente quando UpdateProgress for chamado")]
    [SerializeField] private bool animateOnUpdate = true;

    private float currentFillAmount = 0f;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        if (fillImage == null)
        {
            Debug.LogError($"[ProgressBarManager] {gameObject.name}: fillImage não está atribuído!");
        }

        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
    }

    public void UpdateProgress(int current, int total, string customProgressText = null, string customLabelText = null)
    {
        if (fillImage == null)
        {
            Debug.LogWarning($"[ProgressBarManager] {gameObject.name}: fillImage não está configurado!");
            return;
        }

        float targetProgress = total > 0 ? (float)current / total : 0f;

        if (progressText != null && !string.IsNullOrEmpty(customProgressText))
        {
            progressText.text = customProgressText;
        }

        if (labelText != null && !string.IsNullOrEmpty(customLabelText))
        {
            labelText.text = customLabelText;
        }

        if (animateOnUpdate && gameObject.activeInHierarchy)
        {
            AnimateToProgress(targetProgress);
        }
        else
        {
            fillImage.fillAmount = targetProgress;
            currentFillAmount = targetProgress;
        }

        Debug.Log($"[ProgressBarManager] {gameObject.name} atualizado: {current}/{total} ({targetProgress:P1}) - ProgressText: '{customProgressText}', LabelText: '{customLabelText}'");
    }


    public void UpdateProgressPercentage(float percentage, string customProgressText = null, string customLabelText = null)
    {
        float targetProgress = Mathf.Clamp01(percentage / 100f);

        if (progressText != null && !string.IsNullOrEmpty(customProgressText))
        {
            progressText.text = customProgressText;
        }

        if (labelText != null && !string.IsNullOrEmpty(customLabelText))
        {
            labelText.text = customLabelText;
        }

        if (animateOnUpdate && gameObject.activeInHierarchy)
        {
            AnimateToProgress(targetProgress);
        }
        else
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = targetProgress;
            }
            currentFillAmount = targetProgress;
        }

        Debug.Log($"[ProgressBarManager] {gameObject.name} atualizado: {percentage}% - ProgressText: '{customProgressText}', LabelText: '{customLabelText}'");
    }

    public void UpdateProgressNormalized(float normalizedValue, string customProgressText = null, string customLabelText = null)
    {
        float targetProgress = Mathf.Clamp01(normalizedValue);

        if (progressText != null && !string.IsNullOrEmpty(customProgressText))
        {
            progressText.text = customProgressText;
        }

        if (labelText != null && !string.IsNullOrEmpty(customLabelText))
        {
            labelText.text = customLabelText;
        }

        if (animateOnUpdate && gameObject.activeInHierarchy)
        {
            AnimateToProgress(targetProgress);
        }
        else
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = targetProgress;
            }
            currentFillAmount = targetProgress;
        }

        Debug.Log($"[ProgressBarManager] {gameObject.name} atualizado: {normalizedValue:F2} - ProgressText: '{customProgressText}', LabelText: '{customLabelText}'");
    }

    public void UpdateProgressTextOnly(string text)
    {
        if (progressText != null)
        {
            progressText.text = text;
        }
    }

    public void UpdateLabelTextOnly(string text)
    {
        if (labelText != null)
        {
            labelText.text = text;
        }
    }

    public void UpdateBothTexts(string progressTextValue, string labelTextValue)
    {
        if (progressText != null)
        {
            progressText.text = progressTextValue;
        }

        if (labelText != null)
        {
            labelText.text = labelTextValue;
        }
    }

    private void AnimateToProgress(float targetProgress)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(AnimateProgressCoroutine(targetProgress));
    }

    private IEnumerator AnimateProgressCoroutine(float targetProgress)
    {
        float startProgress = currentFillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / animationDuration);
            float curveValue = animationCurve.Evaluate(normalizedTime);
            currentFillAmount = Mathf.Lerp(startProgress, targetProgress, curveValue);
            
            if (fillImage != null)
            {
                fillImage.fillAmount = currentFillAmount;
            }

            yield return null;
        }

        if (fillImage != null)
        {
            fillImage.fillAmount = targetProgress;
        }
        currentFillAmount = targetProgress;

        animationCoroutine = null;
    }

    public void SetProgressImmediate(int current, int total, string customProgressText = null, string customLabelText = null)
    {
        bool previousAnimateState = animateOnUpdate;
        animateOnUpdate = false;

        UpdateProgress(current, total, customProgressText, customLabelText);

        animateOnUpdate = previousAnimateState;
    }

    public void SetProgressImmediatePercentage(float percentage, string customProgressText = null, string customLabelText = null)
    {
        bool previousAnimateState = animateOnUpdate;
        animateOnUpdate = false;

        UpdateProgressPercentage(percentage, customProgressText, customLabelText);

        animateOnUpdate = previousAnimateState;
    }

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
            progressText.text = "";
        }

        if (labelText != null)
        {
            labelText.text = "";
        }

        Debug.Log($"[ProgressBarManager] {gameObject.name} resetado");
    }

    public float GetCurrentProgress()
    {
        return currentFillAmount;
    }

    public float GetCurrentProgressPercentage()
    {
        return currentFillAmount * 100f;
    }

    public void SetAnimationEnabled(bool enabled)
    {
        animateOnUpdate = enabled;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (animationDuration < 0.1f)
        {
            animationDuration = 0.1f;
        }
    }
#endif
}
