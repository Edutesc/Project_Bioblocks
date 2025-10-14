using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DeleteAccountPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        HidePanel();
    }

    public void ShowPanel()
    {
        if (canvasGroup == null)
        {
            Debug.LogError($"CanvasGroup é null em {gameObject.name}");
            return;
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Debug.Log($"Painel mostrado - Alpha: {canvasGroup.alpha}");
    }

    public void HidePanel()
    {
        if (canvasGroup == null)
        {
            Debug.LogError($"CanvasGroup é null em {gameObject.name}");
            return;
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        // Garantir que fique oculto mesmo quando reativado
        if (canvasGroup != null)
        {
            HidePanel();
        }
    }
}
