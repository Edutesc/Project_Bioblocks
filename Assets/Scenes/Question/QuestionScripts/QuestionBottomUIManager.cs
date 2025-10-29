using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuestionBottomUIManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button nextQuestionButton;
    [SerializeField] private Button previousButton; // ← ADICIONAR
    [SerializeField] private GameObject timePanel;
    [SerializeField] private TextMeshProUGUI timerText;

    public event UnityAction OnExitButtonClicked;
    public event UnityAction OnNextButtonClicked;
    public event UnityAction OnPreviousButtonClicked; // ← ADICIONAR

    public TextMeshProUGUI TimerText => timerText;

    private void Start()
    {
        ValidateComponents();
        InitializeUI();
    }

    private void ValidateComponents()
    {
        if (exitButton == null) Debug.LogError("ExitButton não atribuído");
        if (nextQuestionButton == null) Debug.LogError("NextQuestionButton não atribuído");
        if (previousButton == null) Debug.LogError("PreviousButton não atribuído"); // ← ADICIONAR
        if (timePanel == null) Debug.LogError("TimePanel não atribuído");
        if (timerText == null) Debug.LogError("TimerText não atribuído");
    }

    private void InitializeUI()
    {
        exitButton.gameObject.SetActive(true);
        nextQuestionButton.gameObject.SetActive(true);
        previousButton.gameObject.SetActive(true); // ← ADICIONAR
        
        exitButton.interactable = false;
        nextQuestionButton.interactable = false;
        previousButton.interactable = false; // ← ADICIONAR
        
        timePanel.SetActive(true);
    }

    // MODIFICAR este método para aceitar 3 parâmetros
    public void SetupNavigationButtons(UnityAction exitAction, UnityAction nextAction, UnityAction previousAction = null)
    {
        exitButton.onClick.RemoveAllListeners();
        nextQuestionButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners(); // ← ADICIONAR

        exitButton.onClick.AddListener(() =>
        {
            OnExitButtonClicked?.Invoke();
            var bottomBar = FindFirstObjectByType<NavigationBottomBarManager>();
            if (bottomBar != null)
            {
                bottomBar.ForceRefreshState();
            }
            exitAction?.Invoke();
        });

        nextQuestionButton.onClick.AddListener(() =>
        {
            OnNextButtonClicked?.Invoke();
            nextAction?.Invoke();
        });

        // ← ADICIONAR
        if (previousAction != null)
        {
            previousButton.onClick.AddListener(() =>
            {
                OnPreviousButtonClicked?.Invoke();
                previousAction?.Invoke();
            });
        }
    }

    public void EnableNavigationButtons()
    {
        exitButton.interactable = true;
        nextQuestionButton.interactable = true;
        previousButton.interactable = true; // ← ADICIONAR
    }

    public void DisableNavigationButtons()
    {
        exitButton.interactable = false;
        nextQuestionButton.interactable = false;
        previousButton.interactable = false; // ← ADICIONAR
    }
    
    // ← ADICIONAR (método opcional para controle granular)
    public void SetPreviousButtonState(bool interactable)
    {
        if (previousButton != null)
        {
            previousButton.interactable = interactable;
        }
    }
}