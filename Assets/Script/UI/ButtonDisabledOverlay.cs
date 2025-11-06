using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabledOverlay : MonoBehaviour
{
    [SerializeField] private Image overlayImage;
    
    [Header("Overlay Settings")]
    [SerializeField] private Color overlayColor = new Color(0.5f, 0.5f, 0.5f, 0.7f);
    
    private Button targetButton;
    
    private void Awake()
    {
        // Busca o botão automaticamente no mesmo GameObject
        targetButton = GetComponent<Button>();
        
        if (targetButton == null)
        {
            Debug.LogError($"Button component não encontrado em {gameObject.name}");
            return;
        }
        
        SetupOverlay();
    }
    
    private void OnEnable()
    {
        UpdateOverlay();
    }
    
    private void Update()
    {
        UpdateOverlay();
    }
    
    private void SetupOverlay()
    {
        if (overlayImage != null)
        {
            overlayImage.raycastTarget = false;
            overlayImage.color = overlayColor;
        }
    }
    
    private void UpdateOverlay()
    {
        if (overlayImage != null && targetButton != null)
        {
            overlayImage.enabled = !targetButton.interactable;
        }
    }
}