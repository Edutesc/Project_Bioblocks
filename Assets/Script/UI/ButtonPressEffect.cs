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

    [Header("Scroll Protection")]
    [SerializeField] private float dragThreshold = 15f; // Distância em pixels para considerar drag
    [SerializeField] private float maxClickDuration = 0.5f; // Tempo máximo para considerar clique

    private Vector2 pointerDownPosition;
    private float pointerDownTime;
    private bool wasDragging = false;

    private void Start()
    {
        button = GetComponent<Button>();
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Guarda a posição inicial e o tempo
        pointerDownPosition = eventData.position;
        pointerDownTime = Time.time;
        wasDragging = false;

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

        // Calcula a distância do arrasto
        float dragDistance = Vector2.Distance(pointerDownPosition, eventData.position);
        float pressDuration = Time.time - pointerDownTime;

        // Verifica se foi um arrasto (scroll) ou um clique
        wasDragging = dragDistance > dragThreshold || pressDuration > maxClickDuration;

        if (wasDragging)
        {
            Debug.Log($"Scroll detectado! Distância: {dragDistance:F2}px - Não executando clique");
        }
        else
        {
            Debug.Log($"Clique válido! Distância: {dragDistance:F2}px");
        }

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

        // Só executa o clique se NÃO foi um arrasto
        if (!wasDragging && button != null && button.interactable)
        {
            Debug.Log("Executando onClick do botão");
            button.onClick.Invoke();
        }
    }
}