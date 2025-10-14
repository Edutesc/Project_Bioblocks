using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonPressEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private RectTransform rect;
    private Vector3 originalScale;

    private float pressedScale = 0.98f;
    private float pressDuration = 0.15f;
    private float releaseDuration = 0.15f;
    private Coroutine currentAnimation;

    private void Start()
    {
        button = GetComponent<Button>();
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Botão pressionado!");

        // Para qualquer animação em andamento
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        // Inicia animação de afundamento
        currentAnimation = StartCoroutine(PressDown());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Botão solto!");

        // Para a animação de afundamento
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        // Inicia animação de retorno
        currentAnimation = StartCoroutine(PressUp());
    }

    private IEnumerator PressDown()
    {
        // Afundar
        float elapsed = 0f;
        Vector3 targetScale = new Vector3(pressedScale, pressedScale, pressedScale);

        while (elapsed < pressDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pressDuration;
            rect.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        rect.localScale = targetScale;
    }

    private IEnumerator PressUp()
    {
        // Voltar
        float elapsed = 0f;
        Vector3 targetScale = new Vector3(pressedScale, pressedScale, pressedScale);

        while (elapsed < releaseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / releaseDuration;
            rect.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        rect.localScale = originalScale;

        // Agora executa o clique do botão
        button.onClick.Invoke();
    }
}