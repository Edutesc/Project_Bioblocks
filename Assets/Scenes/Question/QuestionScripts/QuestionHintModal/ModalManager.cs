using UnityEngine;
using UnityEngine.SceneManagement;
 
/// <summary>
/// Gerencia a abertura e o fechamento do modal via carregamento aditivo de cena.
/// Coloque este script em um GameObject persistente na MainScene.
/// </summary>
public class ModalManager : MonoBehaviour
{
    // Nome da cena do modal registrada no Build Settings
    [SerializeField] private string modalSceneName = "ModalScene";
 
    // Flag para evitar abrir o modal duas vezes
    private bool isModalOpen = false;
 
    // Instância Singleton simples (sem lambda, sem ?.)
    public static ModalManager Instance;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    /// <summary>
    /// Chamado pelo botão na MainScene para abrir o modal.
    /// </summary>
    public void OpenModal()
    {
        if (isModalOpen)
        {
            return;
        }
 
        isModalOpen = true;
 
        // Carrega a ModalScene por cima da cena atual (sem descarregar a MainScene)
        SceneManager.LoadScene(modalSceneName, LoadSceneMode.Additive);
    }
 
    /// <summary>
    /// Chamado pelo botão de fechar dentro da ModalScene.
    /// </summary>
    public void CloseModal()
    {
        if (!isModalOpen)
        {
            return;
        }
 
        Scene modal = SceneManager.GetSceneByName(modalSceneName);
 
        if (modal.IsValid())
        {
            SceneManager.UnloadSceneAsync(modal);
        }
 
        isModalOpen = false;
    }
}