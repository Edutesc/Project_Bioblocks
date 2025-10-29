using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;

/// <summary>
/// Gerenciador de sistema de retry (nova tentativa)
/// ATUALIZADO: Apenas atualiza o RetryText (2/2, 1/2, 0/2)
/// </summary>
public class QuestionRetryManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject retryIndicatorPanel;
    [SerializeField] private TextMeshProUGUI retryText;

    [Header("Configuration")]
    [SerializeField] private int maxAttempts = 2;
    [SerializeField] private bool showRetryIndicator = true;

    // Pontuação baseada em tentativas
    private const int POINTS_FIRST_CORRECT = 5;
    private const int POINTS_SECOND_CORRECT = 3;
    private const int PENALTY_FIRST_WRONG = -2;
    private const int PENALTY_SECOND_WRONG = -1;
    private const int PENALTY_TIMEOUT = -1;

    // Cache de dados da questão atual
    private string currentUserId;
    private string currentDatabankName;
    private int currentQuestionNumber;
    private QuestionRetryData currentRetryData;

    // Eventos
    public event Action<int> OnAttemptsChanged;
    public event Action OnLastAttemptReached;
    public event Action OnRetryAvailable;
    public event Action OnNoMoreRetries;

    private void Start()
    {
        ValidateComponents();
        
        // Sempre mostra o painel (sempre visível)
        if (retryIndicatorPanel != null)
        {
            retryIndicatorPanel.SetActive(true);
        }
        
        // Inicializa com 2/2 (ou pode deixar vazio se preferir)
        UpdateRetryText(0); // 0 tentativas usadas = 2/2 disponíveis
    }

    private void ValidateComponents()
    {
        if (retryIndicatorPanel == null)
            Debug.LogWarning("[QuestionRetryManager] RetryIndicatorPanel não atribuído");
        
        if (retryText == null)
            Debug.LogWarning("[QuestionRetryManager] RetryText não atribuído");
    }

    /// <summary>
    /// Inicializa o retry manager para uma nova questão
    /// </summary>
    public void InitializeForQuestion(Question question)
    {
        if (question == null)
        {
            Debug.LogError("[QuestionRetryManager] Question é null");
            return;
        }

        currentUserId = UserDataStore.CurrentUserData?.UserId;
        currentDatabankName = question.questionDatabankName;
        currentQuestionNumber = question.questionNumber;

        if (string.IsNullOrEmpty(currentUserId))
        {
            Debug.LogError("[QuestionRetryManager] UserId não disponível");
            return;
        }

        // Carrega dados de retry do store
        currentRetryData = QuestionRetryStore.GetRetryData(
            currentUserId,
            currentDatabankName,
            currentQuestionNumber
        );

        // Atualiza UI baseado nas tentativas já usadas
        UpdateRetryText(currentRetryData.AttemptsUsed);

        Debug.Log($"[QuestionRetryManager] Inicializado - Q#{currentQuestionNumber}, Tentativas: {currentRetryData.AttemptsUsed}/{maxAttempts}");
    }

    /// <summary>
    /// Registra uma tentativa de resposta e calcula pontos
    /// </summary>
    public async Task<int> RegisterAttempt(bool wasCorrect, bool isTimeout)
    {
        if (currentRetryData == null)
        {
            Debug.LogError("[QuestionRetryManager] currentRetryData é null");
            return 0;
        }

        // Incrementa contador de tentativas
        currentRetryData.AttemptsUsed++;
        currentRetryData.WasAnsweredCorrectly = wasCorrect;
        currentRetryData.LastAttemptTime = Firebase.Firestore.Timestamp.FromDateTime(DateTime.UtcNow);

        int points = CalculatePoints(wasCorrect, isTimeout, currentRetryData.AttemptsUsed);
        currentRetryData.PointsEarned = points;

        // Atualiza UI imediatamente
        UpdateRetryText(currentRetryData.AttemptsUsed);

        // Salva no Firebase
        await SaveRetryDataToFirebase();

        // Dispara eventos
        OnAttemptsChanged?.Invoke(currentRetryData.AttemptsUsed);

        if (currentRetryData.IsLastAttempt())
        {
            OnLastAttemptReached?.Invoke();
        }

        if (currentRetryData.CanRetry())
        {
            OnRetryAvailable?.Invoke();
        }
        else if (!wasCorrect)
        {
            OnNoMoreRetries?.Invoke();
        }

        Debug.Log($"[QuestionRetryManager] Tentativa registrada - Correta: {wasCorrect}, Tentativas: {currentRetryData.AttemptsUsed}/{maxAttempts}, Pontos: {points}");

        return points;
    }

    /// <summary>
    /// Salva dados de retry no Firebase
    /// </summary>
    private async Task SaveRetryDataToFirebase()
    {
        try
        {
            if (string.IsNullOrEmpty(currentUserId) || string.IsNullOrEmpty(currentDatabankName))
                return;

            var db = Firebase.Firestore.FirebaseFirestore.DefaultInstance;
            var docRef = db.Collection("UserRetries")
                .Document(currentUserId)
                .Collection(currentDatabankName)
                .Document($"Q{currentQuestionNumber}");

            await docRef.SetAsync(currentRetryData);

            // Atualiza store local
            QuestionRetryStore.UpdateRetryData(
                currentUserId,
                currentDatabankName,
                currentQuestionNumber,
                currentRetryData
            );

            Debug.Log($"[QuestionRetryManager] Dados salvos no Firebase - Q#{currentQuestionNumber}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionRetryManager] Erro ao salvar no Firebase: {e.Message}");
        }
    }

    /// <summary>
    /// Calcula pontos baseado na tentativa e resultado
    /// </summary>
    private int CalculatePoints(bool wasCorrect, bool isTimeout, int attemptNumber)
    {
        if (isTimeout)
        {
            return PENALTY_TIMEOUT;
        }

        if (wasCorrect)
        {
            return attemptNumber == 1 ? POINTS_FIRST_CORRECT : POINTS_SECOND_CORRECT;
        }
        else
        {
            return attemptNumber == 1 ? PENALTY_FIRST_WRONG : PENALTY_SECOND_WRONG;
        }
    }

    /// <summary>
    /// Atualiza o texto do indicador de retry
    /// FORMATO: "2/2", "1/2", "0/2"
    /// </summary>
    private void UpdateRetryText(int attemptsUsed)
    {
        if (!showRetryIndicator || retryText == null)
            return;

        int remainingAttempts = maxAttempts - attemptsUsed;

        // Formato simples: X/2 onde X são as tentativas RESTANTES
        retryText.text = $"{remainingAttempts}/{maxAttempts}";

        Debug.Log($"[QuestionRetryManager] RetryText atualizado: {remainingAttempts}/{maxAttempts}");
    }

    /// <summary>
    /// Verifica se a questão atual pode ter retry
    /// </summary>
    public bool CanRetryCurrentQuestion()
    {
        return currentRetryData != null && currentRetryData.CanRetry();
    }

    /// <summary>
    /// Verifica se é a primeira tentativa da questão atual
    /// </summary>
    public bool IsFirstAttempt()
    {
        return currentRetryData == null || currentRetryData.IsFirstAttempt();
    }

    /// <summary>
    /// Verifica se é a última tentativa da questão atual
    /// </summary>
    public bool IsLastAttempt()
    {
        return currentRetryData != null && currentRetryData.IsLastAttempt();
    }

    /// <summary>
    /// Obtém o número de tentativas usadas na questão atual
    /// </summary>
    public int GetCurrentAttemptsUsed()
    {
        return currentRetryData?.AttemptsUsed ?? 0;
    }

    /// <summary>
    /// Obtém tentativas restantes
    /// </summary>
    public int GetRemainingAttempts()
    {
        int used = currentRetryData?.AttemptsUsed ?? 0;
        return Mathf.Max(0, maxAttempts - used);
    }

    /// <summary>
    /// Obtém os dados de retry da questão atual
    /// </summary>
    public QuestionRetryData GetCurrentRetryData()
    {
        return currentRetryData;
    }

    /// <summary>
    /// Reseta o sistema de retry para uma nova sessão
    /// </summary>
    public void ResetForNewSession()
    {
        currentRetryData = null;
        currentUserId = null;
        currentDatabankName = null;
        currentQuestionNumber = 0;

        // Reseta UI para 2/2
        UpdateRetryText(0);

        Debug.Log("[QuestionRetryManager] Sistema resetado para nova sessão");
    }

    /// <summary>
    /// Carrega estatísticas de retry do Firebase
    /// </summary>
    public async Task<Dictionary<int, QuestionRetryData>> LoadAllRetryStats(string userId, string databankName)
    {
        var stats = new Dictionary<int, QuestionRetryData>();

        try
        {
            var db = Firebase.Firestore.FirebaseFirestore.DefaultInstance;
            var collection = db.Collection("UserRetries")
                .Document(userId)
                .Collection(databankName);

            var snapshot = await collection.GetSnapshotAsync();

            foreach (var doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    var data = doc.ConvertTo<QuestionRetryData>();
                    stats[data.QuestionNumber] = data;
                }
            }

            Debug.Log($"[QuestionRetryManager] Carregadas {stats.Count} estatísticas de retry do Firebase");
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionRetryManager] Erro ao carregar estatísticas: {e.Message}");
        }

        return stats;
    }
}