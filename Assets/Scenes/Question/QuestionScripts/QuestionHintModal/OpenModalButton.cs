using UnityEngine;
using UnityEngine.UI;
 
/// <summary>
/// Coloque este script no mesmo GameObject que contém o componente Button
/// na MainScene. Ele registra o listener do botão sem usar lambdas.
/// </summary>
[RequireComponent(typeof(Button))]
public class OpenModalButton : MonoBehaviour
{
    private Button button;
 
    private void Awake()
    {
        button = GetComponent<Button>();
    }
 
    private void Start()
    {
        // Registra o método sem operador lambda
        button.onClick.AddListener(OnButtonClicked);
    }
 
    private void OnDestroy()
    {
        // Boa prática: remover o listener ao destruir o objeto
        button.onClick.RemoveListener(OnButtonClicked);
    }
 
    private void OnButtonClicked()
    {
        if (ModalManager.Instance == null)
        {
            Debug.LogWarning("ModalManager não encontrado na cena!");
            return;
        }
 
        ModalManager.Instance.OpenModal();
    }
}
 