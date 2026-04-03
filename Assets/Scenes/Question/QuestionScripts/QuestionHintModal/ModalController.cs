using UnityEngine;
using UnityEngine.UI;

public class ModalController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Button closeButton;
 
    private void Start()
    {
        if (closeButton == null)
        {
            Debug.LogError("CloseButton não atribuído no ModalController!");
            return;
        }
 
        closeButton.onClick.AddListener(OnCloseClicked);
    }
 
    private void OnDestroy()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(OnCloseClicked);
        }
    }
 
    private void OnCloseClicked()
    {
        if (ModalManager.Instance == null)
        {
            Debug.LogWarning("ModalManager não encontrado. Não foi possível fechar o modal.");
            return;
        }
 
        ModalManager.Instance.CloseModal();
    }
}