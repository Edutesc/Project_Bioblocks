using System;
using System.Collections.Generic;
using Firebase.Firestore;

/// <summary>
/// Modelo de dados para armazenar informações de retry no Firebase
/// Integrado ao sistema existente de AnsweredQuestions
/// </summary>
[Serializable]
[FirestoreData]
public class QuestionRetryData
{
    [FirestoreProperty]
    public int QuestionNumber { get; set; }

    [FirestoreProperty]
    public int AttemptsUsed { get; set; } // 0, 1, ou 2

    [FirestoreProperty]
    public bool WasAnsweredCorrectly { get; set; }

    [FirestoreProperty]
    public int PointsEarned { get; set; }

    [FirestoreProperty]
    public Timestamp LastAttemptTime { get; set; }

    public QuestionRetryData()
    {
        AttemptsUsed = 0;
        WasAnsweredCorrectly = false;
        PointsEarned = 0;
        LastAttemptTime = Timestamp.FromDateTime(DateTime.UtcNow);
    }

    public bool CanRetry()
    {
        return AttemptsUsed < 2 && !WasAnsweredCorrectly;
    }

    public bool IsFirstAttempt()
    {
        return AttemptsUsed == 0;
    }

    public bool IsLastAttempt()
    {
        return AttemptsUsed >= 1; // 2ª tentativa é a última
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "QuestionNumber", QuestionNumber },
            { "AttemptsUsed", AttemptsUsed },
            { "WasAnsweredCorrectly", WasAnsweredCorrectly },
            { "PointsEarned", PointsEarned },
            { "LastAttemptTime", LastAttemptTime }
        };
    }
}

/// <summary>
/// Store local para gerenciar estados de retry durante a sessão
/// Sincroniza com Firebase quando necessário
/// </summary>
public static class QuestionRetryStore
{
    // Estrutura: userId -> databankName -> questionNumber -> RetryData
    private static Dictionary<string, Dictionary<string, Dictionary<int, QuestionRetryData>>> _retryData =
        new Dictionary<string, Dictionary<string, Dictionary<int, QuestionRetryData>>>();

    /// <summary>
    /// Obtém os dados de retry de uma questão específica
    /// </summary>
    public static QuestionRetryData GetRetryData(string userId, string databankName, int questionNumber)
    {
        if (_retryData.TryGetValue(userId, out var userRetries))
        {
            if (userRetries.TryGetValue(databankName, out var databankRetries))
            {
                if (databankRetries.TryGetValue(questionNumber, out var retryData))
                {
                    return retryData;
                }
            }
        }

        // Retorna novo se não existir
        return new QuestionRetryData { QuestionNumber = questionNumber };
    }

    /// <summary>
    /// Atualiza os dados de retry de uma questão
    /// </summary>
    public static void UpdateRetryData(string userId, string databankName, int questionNumber, QuestionRetryData retryData)
    {
        if (!_retryData.ContainsKey(userId))
        {
            _retryData[userId] = new Dictionary<string, Dictionary<int, QuestionRetryData>>();
        }

        if (!_retryData[userId].ContainsKey(databankName))
        {
            _retryData[userId][databankName] = new Dictionary<int, QuestionRetryData>();
        }

        _retryData[userId][databankName][questionNumber] = retryData;
        
        UnityEngine.Debug.Log($"[RetryStore] Atualizado - User: {userId}, DB: {databankName}, Q#{questionNumber}, Attempts: {retryData.AttemptsUsed}");
    }

    /// <summary>
    /// Registra uma tentativa (incrementa contador)
    /// </summary>
    public static void RegisterAttempt(string userId, string databankName, int questionNumber, bool wasCorrect, int pointsEarned)
    {
        var retryData = GetRetryData(userId, databankName, questionNumber);
        
        retryData.AttemptsUsed++;
        retryData.WasAnsweredCorrectly = wasCorrect;
        retryData.PointsEarned = pointsEarned;
        retryData.LastAttemptTime = Timestamp.FromDateTime(DateTime.UtcNow);

        UpdateRetryData(userId, databankName, questionNumber, retryData);
    }

    /// <summary>
    /// Verifica se uma questão pode ter retry
    /// </summary>
    public static bool CanRetryQuestion(string userId, string databankName, int questionNumber)
    {
        var retryData = GetRetryData(userId, databankName, questionNumber);
        return retryData.CanRetry();
    }

    /// <summary>
    /// Verifica se é a primeira tentativa
    /// </summary>
    public static bool IsFirstAttempt(string userId, string databankName, int questionNumber)
    {
        var retryData = GetRetryData(userId, databankName, questionNumber);
        return retryData.IsFirstAttempt();
    }

    /// <summary>
    /// Verifica se é a última tentativa permitida
    /// </summary>
    public static bool IsLastAttempt(string userId, string databankName, int questionNumber)
    {
        var retryData = GetRetryData(userId, databankName, questionNumber);
        return retryData.IsLastAttempt();
    }

    /// <summary>
    /// Obtém o número de tentativas usadas
    /// </summary>
    public static int GetAttemptsUsed(string userId, string databankName, int questionNumber)
    {
        var retryData = GetRetryData(userId, databankName, questionNumber);
        return retryData.AttemptsUsed;
    }

    /// <summary>
    /// Limpa os dados de retry de um usuário
    /// </summary>
    public static void ClearUserRetryData(string userId)
    {
        if (_retryData.ContainsKey(userId))
        {
            _retryData.Remove(userId);
            UnityEngine.Debug.Log($"[RetryStore] Dados de retry limpos para user: {userId}");
        }
    }

    /// <summary>
    /// Limpa os dados de retry de um banco específico
    /// </summary>
    public static void ClearDatabankRetryData(string userId, string databankName)
    {
        if (_retryData.TryGetValue(userId, out var userRetries))
        {
            if (userRetries.ContainsKey(databankName))
            {
                userRetries.Remove(databankName);
                UnityEngine.Debug.Log($"[RetryStore] Dados de retry limpos para DB: {databankName}");
            }
        }
    }

    /// <summary>
    /// Limpa todos os dados (útil no logout)
    /// </summary>
    public static void ClearAll()
    {
        _retryData.Clear();
        UnityEngine.Debug.Log("[RetryStore] Todos os dados de retry foram limpos");
    }

    /// <summary>
    /// Obtém todas as questões com retry disponível em um banco
    /// </summary>
    public static List<int> GetQuestionsWithRetryAvailable(string userId, string databankName)
    {
        var questionsWithRetry = new List<int>();

        if (_retryData.TryGetValue(userId, out var userRetries))
        {
            if (userRetries.TryGetValue(databankName, out var databankRetries))
            {
                foreach (var kvp in databankRetries)
                {
                    if (kvp.Value.CanRetry())
                    {
                        questionsWithRetry.Add(kvp.Key);
                    }
                }
            }
        }

        return questionsWithRetry;
    }

    /// <summary>
    /// Debug: Mostra estatísticas de retry
    /// </summary>
    public static void LogRetryStatistics(string userId, string databankName)
    {
        if (!_retryData.TryGetValue(userId, out var userRetries)) return;
        if (!userRetries.TryGetValue(databankName, out var databankRetries)) return;

        int totalQuestions = databankRetries.Count;
        int questionsWithRetry = 0;
        int questionsAnsweredCorrectly = 0;

        foreach (var kvp in databankRetries)
        {
            if (kvp.Value.CanRetry()) questionsWithRetry++;
            if (kvp.Value.WasAnsweredCorrectly) questionsAnsweredCorrectly++;
        }

        UnityEngine.Debug.Log($"[RetryStore] Estatísticas - DB: {databankName}");
        UnityEngine.Debug.Log($"  Total de questões tentadas: {totalQuestions}");
        UnityEngine.Debug.Log($"  Questões com retry disponível: {questionsWithRetry}");
        UnityEngine.Debug.Log($"  Questões respondidas corretamente: {questionsAnsweredCorrectly}");
    }
}