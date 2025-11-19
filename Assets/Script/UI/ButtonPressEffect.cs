using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonPressEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private RectTransform rect;
    private Vector3 originalScale;
    private Coroutine currentAnimation;

    [Header("Animation Settings")]
    [SerializeField] private float pressedScale = 0.98f;
    [SerializeField] private float pressDuration = 0.15f;
    [SerializeField] private float releaseDuration = 0.15f;

    [Header("Scroll Protection")]
    [SerializeField] private float dragThreshold = 15f; 
    [SerializeField] private float maxClickDuration = 0.5f; 

    private Vector2 pointerDownPosition;
    private float pointerDownTime;
    private bool wasDragging = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        rect = GetComponent<RectTransform>();

        if (rect == null)
        {
            Debug.LogError("ButtonPressEffect precisa de um RectTransform!", this);
            enabled = false;
            return;
        }

        originalScale = rect.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button == null || !button.interactable)
        {
            return;
        }

        pointerDownPosition = eventData.position;
        pointerDownTime = Time.time;
        wasDragging = false;

        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(PressDown());
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (button == null || !button.interactable)
        {
            return;
        }

        float dragDistance = Vector2.Distance(pointerDownPosition, eventData.position);
        float pressDuration = Time.time - pointerDownTime;
        wasDragging = dragDistance > dragThreshold || pressDuration > maxClickDuration;

        if (wasDragging)
        {
            Debug.Log($"Scroll detectado! Dist�ncia: {dragDistance:F2}px - N�o executando clique");
        }
        else
        {
            Debug.Log($"Clique v�lido! Dist�ncia: {dragDistance:F2}px");
        }

        if (currentAnimation != null)
            StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(PressUp());
    }
    
    public void ForceStopAnimation()
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;
        }

        if (rect != null)
        {
            rect.localScale = originalScale;
        }
    }

    private IEnumerator PressDown()
    {
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

        if (!wasDragging && button != null && button.interactable)
        {
            Debug.Log("Executando onClick do bot�o");
            button.onClick.Invoke();
        }
    }
}