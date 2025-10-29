using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using QuestionSystem;

/// <summary>
/// Gerenciador de navegação entre questões (Próxima/Anterior)
/// ADAPTADO para integrar com QuestionBottomUIManager existente
/// </summary>
public class QuestionNavigationManager : MonoBehaviour
{
    [Header("Integration with Existing Bottom Bar")]
    [SerializeField] private QuestionBottomUIManager bottomBarManager;
    
    [Header("New Previous Button (to add to existing bar)")]
    [SerializeField] private Button previousButton;

    // Lista de questões da sessão atual
    private List<Question> sessionQuestions;
    private int currentIndex;
    private string currentUserId;

    // Histórico de navegação (para "voltar")
    private Stack<int> navigationHistory;

    // Eventos
    public event Action<int> OnNavigateToQuestion;      // Dispara ao navegar para índice
    public event Action<Question> OnQuestionChanged;    // Dispara quando questão muda
    public event Action OnNavigateHome;                 // Dispara ao voltar para home (exit)

    private void Start()
    {
        ValidateComponents();
        SetupButtonListeners();
        navigationHistory = new Stack<int>();
    }

    private void ValidateComponents()
    {
        if (bottomBarManager == null)
        {
            Debug.LogError("[QuestionNavigationManager] QuestionBottomUIManager não atribuído!");
            return;
        }
        
        if (previousButton == null)
        {
            Debug.LogError("[QuestionNavigationManager] PreviousButton não atribuído!");
        }
    }

    private void SetupButtonListeners()
    {
        // Configura botão ANTERIOR (novo)
        if (previousButton != null)
        {
            previousButton.onClick.AddListener(OnPreviousButtonClicked);
        }
        
        // Conecta aos eventos existentes do QuestionBottomUIManager
        if (bottomBarManager != null)
        {
            bottomBarManager.OnNextButtonClicked += OnNextButtonClicked;
            bottomBarManager.OnExitButtonClicked += OnExitButtonClicked;
        }
    }

    /// <summary>
    /// Inicializa o sistema de navegação com a lista de questões
    /// </summary>
    public void Initialize(List<Question> questions, int startIndex = 0)
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("[QuestionNavigationManager] Lista de questões vazia ou null");
            return;
        }

        sessionQuestions = new List<Question>(questions);
        currentIndex = Mathf.Clamp(startIndex, 0, sessionQuestions.Count - 1);
        currentUserId = UserDataStore.CurrentUserData?.UserId;

        navigationHistory.Clear();
        navigationHistory.Push(currentIndex);

        UpdateNavigationState();

        Debug.Log($"[QuestionNavigationManager] Inicializado com {sessionQuestions.Count} questões, índice inicial: {currentIndex}");
    }

    /// <summary>
    /// Navega para a questão anterior
    /// </summary>
    private void OnPreviousButtonClicked()
    {
        if (!CanNavigatePrevious())
        {
            Debug.LogWarning("[QuestionNavigationManager] Não é possível voltar (primeira questão)");
            return;
        }

        int newIndex = currentIndex - 1;
        NavigateToIndex(newIndex, isGoingBack: true);
    }

    /// <summary>
    /// Navega para a próxima questão
    /// </summary>
    private void OnNextButtonClicked()
    {
        if (!CanNavigateNext())
        {
            Debug.LogWarning("[QuestionNavigationManager] Não é possível avançar (última questão)");
            return;
        }

        int newIndex = currentIndex + 1;
        NavigateToIndex(newIndex, isGoingBack: false);
    }

    /// <summary>
    /// Retorna para a cena principal (PathwayScene)
    /// Conectado ao exitButton do QuestionBottomUIManager
    /// </summary>
    private void OnExitButtonClicked()
    {
        Debug.Log("[QuestionNavigationManager] Navegando para Home (Exit)");
        OnNavigateHome?.Invoke();
    }

    /// <summary>
    /// Navega para um índice específico
    /// </summary>
    public void NavigateToIndex(int index, bool isGoingBack = false)
    {
        if (index < 0 || index >= sessionQuestions.Count)
        {
            Debug.LogError($"[QuestionNavigationManager] Índice inválido: {index}");
            return;
        }

        // Salva no histórico se não for voltar
        if (!isGoingBack)
        {
            navigationHistory.Push(currentIndex);
        }

        currentIndex = index;
        UpdateNavigationState();

        // Dispara eventos
        OnNavigateToQuestion?.Invoke(currentIndex);
        OnQuestionChanged?.Invoke(GetCurrentQuestion());

        Debug.Log($"[QuestionNavigationManager] Navegou para índice {currentIndex} (Q#{GetCurrentQuestion().questionNumber})");
    }

    /// <summary>
    /// Navega para uma questão específica pelo número
    /// </summary>
    public bool NavigateToQuestionNumber(int questionNumber)
    {
        int index = sessionQuestions.FindIndex(q => q.questionNumber == questionNumber);
        
        if (index >= 0)
        {
            NavigateToIndex(index);
            return true;
        }

        Debug.LogWarning($"[QuestionNavigationManager] Questão #{questionNumber} não encontrada na sessão");
        return false;
    }

    /// <summary>
    /// Atualiza o estado dos botões de navegação
    /// </summary>
    private void UpdateNavigationState()
    {
        // Atualiza botão ANTERIOR
        if (previousButton != null)
        {
            bool canGoPrevious = CanNavigatePrevious();
            previousButton.interactable = canGoPrevious;
        }

        // Botões NEXT e EXIT são gerenciados pelo QuestionBottomUIManager existente
        // Não precisamos controlar aqui

        Debug.Log($"[QuestionNavigationManager] Estado atualizado - Índice: {currentIndex}/{sessionQuestions.Count - 1}");
    }

    /// <summary>
    /// Verifica se pode navegar para anterior
    /// </summary>
    public bool CanNavigatePrevious()
    {
        return currentIndex > 0;
    }

    /// <summary>
    /// Verifica se pode navegar para próxima
    /// </summary>
    public bool CanNavigateNext()
    {
        return currentIndex < sessionQuestions.Count - 1;
    }

    /// <summary>
    /// Retorna a questão atual
    /// </summary>
    public Question GetCurrentQuestion()
    {
        if (currentIndex < 0 || currentIndex >= sessionQuestions.Count)
            return null;

        return sessionQuestions[currentIndex];
    }

    /// <summary>
    /// Retorna o índice atual
    /// </summary>
    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    /// <summary>
    /// Retorna o total de questões na sessão
    /// </summary>
    public int GetTotalQuestions()
    {
        return sessionQuestions?.Count ?? 0;
    }

    /// <summary>
    /// Verifica se uma questão foi respondida corretamente
    /// </summary>
    public bool IsQuestionAnsweredCorrectly(int questionNumber)
    {
        if (string.IsNullOrEmpty(currentUserId))
            return false;

        var question = sessionQuestions.Find(q => q.questionNumber == questionNumber);
        if (question == null)
            return false;

        var retryData = QuestionRetryStore.GetRetryData(
            currentUserId,
            question.questionDatabankName,
            questionNumber
        );

        return retryData.WasAnsweredCorrectly;
    }

    /// <summary>
    /// Verifica se uma questão tem retry disponível
    /// </summary>
    public bool DoesQuestionHaveRetry(int questionNumber)
    {
        if (string.IsNullOrEmpty(currentUserId))
            return false;

        var question = sessionQuestions.Find(q => q.questionNumber == questionNumber);
        if (question == null)
            return false;

        return QuestionRetryStore.CanRetryQuestion(
            currentUserId,
            question.questionDatabankName,
            questionNumber
        );
    }

    /// <summary>
    /// Habilita ou desabilita todos os botões de navegação
    /// Integra com QuestionBottomUIManager existente
    /// </summary>
    public void SetNavigationEnabled(bool enabled)
    {
        // Controla botão ANTERIOR (novo)
        if (previousButton != null)
        {
            previousButton.interactable = enabled && CanNavigatePrevious();
        }
        
        // Delega controle dos botões NEXT e EXIT para o QuestionBottomUIManager
        if (bottomBarManager != null)
        {
            if (enabled)
            {
                bottomBarManager.EnableNavigationButtons();
            }
            else
            {
                bottomBarManager.DisableNavigationButtons();
            }
        }

        Debug.Log($"[QuestionNavigationManager] Navegação {(enabled ? "habilitada" : "desabilitada")}");
    }

    /// <summary>
    /// Reseta o sistema de navegação
    /// </summary>
    public void Reset()
    {
        sessionQuestions?.Clear();
        navigationHistory?.Clear();
        currentIndex = 0;
        currentUserId = null;

        Debug.Log("[QuestionNavigationManager] Reset executado");
    }

    /// <summary>
    /// Retorna informações de debug sobre navegação
    /// </summary>
    public string GetNavigationDebugInfo()
    {
        if (sessionQuestions == null || sessionQuestions.Count == 0)
            return "Nenhuma sessão ativa";

        var current = GetCurrentQuestion();
        return $"Navegação: {currentIndex + 1}/{sessionQuestions.Count} - " +
               $"Q#{current?.questionNumber} - " +
               $"Pode voltar: {CanNavigatePrevious()} - " +
               $"Pode avançar: {CanNavigateNext()}";
    }

    private void OnDestroy()
    {
        // Remove listeners do botão ANTERIOR
        if (previousButton != null)
        {
            previousButton.onClick.RemoveListener(OnPreviousButtonClicked);
        }
        
        // Remove listeners do QuestionBottomUIManager
        if (bottomBarManager != null)
        {
            bottomBarManager.OnNextButtonClicked -= OnNextButtonClicked;
            bottomBarManager.OnExitButtonClicked -= OnExitButtonClicked;
        }
    }
}