using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Gerencia estatísticas dos bancos de questões, incluindo contagem por nível
/// </summary>
public static class QuestionBankStatistics
{
    // Dicionário que armazena o número total de questões por banco de dados
    private static Dictionary<string, int> totalQuestionsPerDatabase = new Dictionary<string, int>();

    // Dicionário que armazena o número de questões por nível em cada banco
    private static Dictionary<string, Dictionary<int, int>> questionsPerLevelPerDatabase =
        new Dictionary<string, Dictionary<int, int>>();

    /// <summary>
    /// Define o número total de questões para um banco de dados específico
    /// </summary>
    public static void SetTotalQuestions(string databaseName, int count)
    {
        if (string.IsNullOrEmpty(databaseName))
        {
            Debug.LogError("Nome do banco de dados inválido");
            return;
        }

        totalQuestionsPerDatabase[databaseName] = count;
        Debug.Log($"Banco de dados '{databaseName}' atualizado com {count} questões no total");
    }

    /// <summary>
    /// Obtém o número total de questões de um banco de dados
    /// </summary>
    public static int GetTotalQuestions(string databaseName)
    {
        if (string.IsNullOrEmpty(databaseName))
        {
            Debug.LogError("Nome do banco de dados inválido");
            return 0;
        }

        if (totalQuestionsPerDatabase.TryGetValue(databaseName, out int count))
        {
            return count;
        }

        Debug.LogWarning($"Número total de questões para '{databaseName}' não encontrado. Retornando 0.");
        return 0;
    }

    /// <summary>
    /// Define o número de questões por nível para um banco específico
    /// </summary>
    public static void SetQuestionsPerLevel(string databaseName, Dictionary<int, int> levelCounts)
    {
        if (string.IsNullOrEmpty(databaseName))
        {
            Debug.LogError("Nome do banco de dados inválido");
            return;
        }

        questionsPerLevelPerDatabase[databaseName] = new Dictionary<int, int>(levelCounts);

        Debug.Log($"Banco '{databaseName}' - Questões por nível: " +
                  $"Nível 1: {levelCounts.GetValueOrDefault(1, 0)}, " +
                  $"Nível 2: {levelCounts.GetValueOrDefault(2, 0)}, " +
                  $"Nível 3: {levelCounts.GetValueOrDefault(3, 0)}");
    }

    /// <summary>
    /// Obtém o número de questões de um nível específico em um banco
    /// </summary>
    public static int GetQuestionsForLevel(string databaseName, int level)
    {
        if (string.IsNullOrEmpty(databaseName))
        {
            Debug.LogError("Nome do banco de dados inválido");
            return 0;
        }

        if (level < 1 || level > 3)
        {
            Debug.LogError($"Nível inválido: {level}. Deve ser 1, 2 ou 3.");
            return 0;
        }

        if (questionsPerLevelPerDatabase.TryGetValue(databaseName, out Dictionary<int, int> levelCounts))
        {
            return levelCounts.GetValueOrDefault(level, 0);
        }

        Debug.LogWarning($"Estatísticas de nível não encontradas para '{databaseName}'");
        return 0;
    }

    /// <summary>
    /// Verifica se todas as questões de um nível foram respondidas
    /// </summary>
    public static bool AreAllLevelQuestionsAnswered(string databaseName, int level, int answeredCount)
    {
        int totalQuestionsInLevel = GetQuestionsForLevel(databaseName, level);
        return totalQuestionsInLevel > 0 && answeredCount >= totalQuestionsInLevel;
    }

    /// <summary>
    /// Verifica se todas as questões de um banco de dados foram respondidas
    /// </summary>
    public static bool AreAllQuestionsAnswered(string databaseName, int answeredCount)
    {
        int totalQuestions = GetTotalQuestions(databaseName);
        return totalQuestions > 0 && answeredCount >= totalQuestions;
    }

    /// <summary>
    /// Obtém um resumo completo das estatísticas de um banco
    /// </summary>
    public static string GetDatabaseSummary(string databaseName)
    {
        int total = GetTotalQuestions(databaseName);
        int level1 = GetQuestionsForLevel(databaseName, 1);
        int level2 = GetQuestionsForLevel(databaseName, 2);
        int level3 = GetQuestionsForLevel(databaseName, 3);

        return $"Banco: {databaseName}\n" +
               $"Total: {total} questões\n" +
               $"Nível 1 (Básico): {level1} questões\n" +
               $"Nível 2 (Intermediário): {level2} questões\n" +
               $"Nível 3 (Difícil): {level3} questões";
    }

    /// <summary>
    /// Limpa todas as estatísticas armazenadas
    /// </summary>
    public static void ClearAllStatistics()
    {
        totalQuestionsPerDatabase.Clear();
        questionsPerLevelPerDatabase.Clear();
        Debug.Log("Todas as estatísticas foram limpas");
    }

    /// <summary>
    /// Verifica se um banco tem estatísticas registradas
    /// </summary>
    public static bool HasStatistics(string databaseName)
    {
        return totalQuestionsPerDatabase.ContainsKey(databaseName);
    }

    /// <summary>
    /// Obtém todos os bancos com estatísticas registradas
    /// </summary>
    public static List<string> GetAllRegisteredDatabases()
    {
        return new List<string>(totalQuestionsPerDatabase.Keys);
    }
}