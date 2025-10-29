using UnityEngine;
using System.Threading.Tasks;
using QuestionSystem;

/// <summary>
/// INTEGRAÇÃO: Sistema de Retry + Navegação
/// 
/// VERSÃO LIMPA - Usa apenas FeedbackUIElements (sem QuestionStatusUIManager)
/// </summary>
public class QuestionManagerRetryIntegration : MonoBehaviour
{
    [Header("Required Managers")]
    [SerializeField] private QuestionRetryManager retryManager;
    [SerializeField] private QuestionNavigationManager navigationManager;
    
    [Header("Existing Managers")]
    [SerializeField] private QuestionTimerManager timerManager;
    [SerializeField] private QuestionScoreManager scoreManager;
    [SerializeField] private QuestionAnswerManager answerManager;
    [SerializeField] private FeedbackUIElements feedbackUI;
    [SerializeField] private QuestionBottomUIManager bottomBarManager;

    [Header("Configuration")]
    [SerializeField] private bool enableRetrySystem = true;
    [SerializeField] private bool enableNavigationSystem = true;
    [SerializeField] private float delayBeforeEnableNavigation = 1.5f;

    private MonoBehaviour questionManager;
    private Question currentQuestion;
    private bool isProcessingAnswer;
    private string currentUserId;

    public void Initialize(MonoBehaviour mainQuestionManager)
    {
        questionManager = mainQuestionManager;
        currentUserId = UserDataStore.CurrentUserData?.UserId;
        ValidateComponents();
        SetupEventListeners();
        Debug.Log("[RetryIntegration] Sistema inicializado");
    }

    private void ValidateComponents()
    {
        if (retryManager == null) Debug.LogError("[RetryIntegration] QuestionRetryManager não atribuído!");
        if (navigationManager == null) Debug.LogError("[RetryIntegration] QuestionNavigationManager não atribuído!");
        if (timerManager == null) Debug.LogError("[RetryIntegration] QuestionTimerManager não atribuído!");
        if (scoreManager == null) Debug.LogError("[RetryIntegration] QuestionScoreManager não atribuído!");
        if (bottomBarManager == null) Debug.LogError("[RetryIntegration] QuestionBottomUIManager não atribuído!");
        if (feedbackUI == null) Debug.LogError("[RetryIntegration] FeedbackUIElements não atribuído!");
    }

    private void SetupEventListeners()
    {
        if (navigationManager != null)
        {
            navigationManager.OnQuestionChanged += OnNavigatedToQuestion;
            navigationManager.OnNavigateHome += OnNavigateHome;
        }

        if (retryManager != null)
        {
            retryManager.OnRetryAvailable += OnRetryBecameAvailable;
            retryManager.OnNoMoreRetries += OnNoMoreRetries;
        }
    }

    public void InitializeQuestion(Question question)
    {
        if (question == null)
        {
            Debug.LogError("[RetryIntegration] Question é null");
            return;
        }

        currentQuestion = question;

        if (retryManager != null && enableRetrySystem)
        {
            retryManager.InitializeForQuestion(question);
        }

        CheckIfReturningToQuestion(question);
        Debug.Log($"[RetryIntegration] Questão inicializada: Q#{question.questionNumber}");
    }

    private void CheckIfReturningToQuestion(Question question)
    {
        if (string.IsNullOrEmpty(currentUserId)) return;

        var retryData = QuestionRetryStore.GetRetryData(
            currentUserId,
            question.questionDatabankName,
            question.questionNumber
        );

        // Questão nunca foi tentada
        if (retryData.AttemptsUsed == 0)
        {
            return; // Nenhum feedback necessário
        }

        // Questão foi respondida corretamente
        if (retryData.WasAnsweredCorrectly)
        {
            HandleReturningToCorrectQuestion(retryData);
            return;
        }

        // Questão foi respondida errada mas tem retry
        if (retryData.CanRetry())
        {
            HandleReturningToQuestionWithRetry(retryData);
            return;
        }

        // Questão foi respondida errada sem retry disponível
        HandleReturningToIncorrectQuestion(retryData);
    }

    private void HandleReturningToCorrectQuestion(QuestionRetryData retryData)
    {
        Debug.Log("[RetryIntegration] Retornando para questão CORRETA - Modo visualização");

        // Mostra feedback
        if (feedbackUI != null)
        {
            bool isSecondAttempt = retryData.AttemptsUsed > 1;
            if (isSecondAttempt)
            {
                feedbackUI.ShowCorrectAnswerSecondAttempt();
            }
            else
            {
                feedbackUI.ShowCorrectAnswer(false);
            }
        }

        // Bloqueia botões
        if (answerManager != null) answerManager.DisableAllButtons();
        if (timerManager != null) timerManager.StopTimer();

        // Ativa navegação
        if (bottomBarManager != null) bottomBarManager.EnableNavigationButtons();
        if (navigationManager != null) navigationManager.SetNavigationEnabled(true);
    }

    private void HandleReturningToQuestionWithRetry(QuestionRetryData retryData)
    {
        Debug.Log("[RetryIntegration] Retornando para questão com RETRY disponível");

        // Mostra feedback
        if (feedbackUI != null)
        {
            feedbackUI.ShowWrongAnswerWithRetry();
        }

        // Habilita resposta
        if (answerManager != null) answerManager.EnableAllButtons();
        if (timerManager != null) timerManager.StartTimer();

        // Desativa navegação até responder
        if (navigationManager != null) navigationManager.SetNavigationEnabled(false);
        if (bottomBarManager != null) bottomBarManager.DisableNavigationButtons();
    }

    private void HandleReturningToIncorrectQuestion(QuestionRetryData retryData)
    {
        Debug.Log("[RetryIntegration] Retornando para questão ERRADA sem retry - Modo visualização");

        // Mostra feedback
        if (feedbackUI != null)
        {
            feedbackUI.ShowWrongAnswerFinal();
        }

        // Bloqueia botões
        if (answerManager != null) answerManager.DisableAllButtons();
        if (timerManager != null) timerManager.StopTimer();

        // Ativa navegação
        if (bottomBarManager != null) bottomBarManager.EnableNavigationButtons();
        if (navigationManager != null) navigationManager.SetNavigationEnabled(true);
    }

    public async Task<bool> ProcessAnswer(int selectedIndex, Question question, bool isTimeout = false)
    {
        if (isProcessingAnswer)
        {
            Debug.LogWarning("[RetryIntegration] Já está processando uma resposta");
            return false;
        }

        isProcessingAnswer = true;

        try
        {
            bool isCorrect = selectedIndex == question.correctIndex;

            if (timerManager != null) timerManager.StopTimer();

            // Registra tentativa e obtém pontos
            int points = 0;
            if (retryManager != null && enableRetrySystem)
            {
                points = await retryManager.RegisterAttempt(isCorrect, isTimeout);
            }
            else
            {
                points = isCorrect ? 5 : (isTimeout ? -1 : -2);
            }

            // Mostra feedback
            bool hasRetry = retryManager != null && retryManager.CanRetryCurrentQuestion();
            bool isSecondAttempt = retryManager != null && !retryManager.IsFirstAttempt();
            await ShowAnswerFeedback(isCorrect, isTimeout, points, hasRetry, isSecondAttempt);

            // Atualiza score
            if (scoreManager != null)
            {
                await scoreManager.UpdateScore(points, isCorrect, question);
            }

            // Desabilita botões
            if (answerManager != null) answerManager.DisableAllButtons();

            // Aguarda antes de habilitar navegação
            await Task.Delay((int)(delayBeforeEnableNavigation * 1000));

            // ✅ SE TEM RETRY: Reabilita resposta e REINICIA timer
            if (hasRetry && !isCorrect)
            {
                Debug.Log("[RetryIntegration] Retry disponível - Habilitando resposta novamente");
                
                if (answerManager != null)
                {
                    answerManager.EnableAllButtons();
                }
                
                // REINICIA o timer para nova tentativa
                if (timerManager != null)
                {
                    timerManager.ResetTimer(); // Método que você precisa ter no TimerManager
                    timerManager.StartTimer();
                    Debug.Log("[RetryIntegration] Timer REINICIADO para retry");
                }
                
                // Mantém navegação DESABILITADA (usuário precisa responder)
                if (bottomBarManager != null)
                {
                    bottomBarManager.DisableNavigationButtons();
                }
                
                if (navigationManager != null)
                {
                    navigationManager.SetNavigationEnabled(false);
                }
            }
            else
            {
                // SEM RETRY ou ACERTOU: Habilita navegação
                if (bottomBarManager != null)
                {
                    bottomBarManager.EnableNavigationButtons();
                }
                
                if (navigationManager != null)
                {
                    navigationManager.SetNavigationEnabled(true);
                }
            }

            Debug.Log($"[RetryIntegration] Resposta processada - Correta: {isCorrect}, Pontos: {points}");
            return isCorrect;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[RetryIntegration] Erro: {ex.Message}");
            return false;
        }
        finally
        {
            isProcessingAnswer = false;
        }
    }

    private async Task ShowAnswerFeedback(bool isCorrect, bool isTimeout, int points, bool hasRetryAvailable, bool isSecondAttempt)
    {
        if (feedbackUI == null) return;

        bool hasBonus = scoreManager != null && scoreManager.HasBonusActive();

        if (isTimeout)
        {
            if (hasRetryAvailable)
                feedbackUI.ShowTimeoutWithRetry();
            else
                feedbackUI.ShowTimeout();
        }
        else if (isCorrect)
        {
            if (isSecondAttempt)
                feedbackUI.ShowCorrectAnswerSecondAttempt();
            else
                feedbackUI.ShowCorrectAnswer(hasBonus);
        }
        else
        {
            if (hasRetryAvailable)
                feedbackUI.ShowWrongAnswerWithRetry();
            else
                feedbackUI.ShowWrongAnswerFinal();
        }

        await Task.Delay((int)(feedbackUI.DisplayDuration * 1000));
    }

    private void OnNavigatedToQuestion(Question question)
    {
        Debug.Log($"[RetryIntegration] Navegado para Q#{question.questionNumber}");
    }

    private void OnNavigateHome()
    {
        Debug.Log("[RetryIntegration] Navegando para PathwayScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("PathwayScene");
    }

    private void OnRetryBecameAvailable()
    {
        Debug.Log("[RetryIntegration] Retry disponível");
        // Não precisa mostrar mensagem - o RetryText já está atualizado
    }

    private void OnNoMoreRetries()
    {
        Debug.Log("[RetryIntegration] Sem mais retries");
    }

    public bool CanRetryCurrentQuestion()
    {
        return retryManager != null && retryManager.CanRetryCurrentQuestion();
    }

    public void ResetRetrySystem()
    {
        if (retryManager != null) retryManager.ResetForNewSession();
        if (navigationManager != null) navigationManager.Reset();
    }

    private void OnDestroy()
    {
        if (navigationManager != null)
        {
            navigationManager.OnQuestionChanged -= OnNavigatedToQuestion;
            navigationManager.OnNavigateHome -= OnNavigateHome;
        }

        if (retryManager != null)
        {
            retryManager.OnRetryAvailable -= OnRetryBecameAvailable;
            retryManager.OnNoMoreRetries -= OnNoMoreRetries;
        }
    }
}