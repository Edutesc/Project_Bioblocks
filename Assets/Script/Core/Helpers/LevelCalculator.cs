using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QuestionSystem;

/// <summary>
/// Calcula dinamicamente qual nível deve ser mostrado
/// baseado APENAS nas questões respondidas corretamente no Firebase
/// </summary>
public static class LevelCalculator
{
    /// <summary>
    /// Calcula qual nível deve ser mostrado ao usuário
    /// Lógica:
    /// - Se há questões nível 1 não respondidas → retorna 1
    /// - Se TODAS nível 1 respondidas, mas há nível 2 não respondidas → retorna 2
    /// - Se TODAS nível 1 e 2 respondidas, mas há nível 3 não respondidas → retorna 3
    /// - E assim por diante para níveis futuros
    /// </summary>
    public static int CalculateCurrentLevel(
        List<Question> allQuestions,
        List<string> answeredQuestionsFromFirebase)
    {
        if (allQuestions == null || allQuestions.Count == 0)
        {
            Debug.LogWarning("CalculateCurrentLevel: Nenhuma questão no banco");
            return 1;
        }

        // Converte para HashSet para busca O(1)
        HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase ?? new List<string>());

        // Descobre o maior nível que existe no banco
        int maxLevel = allQuestions.Max(q => GetQuestionLevel(q));

        Debug.Log($"📊 CALCULANDO NÍVEL ATUAL:");
        Debug.Log($"  Total de questões no banco: {allQuestions.Count}");
        Debug.Log($"  Questões no Firebase: {answeredSet.Count}");
        Debug.Log($"  Maior nível no banco: {maxLevel}");

        // Verifica cada nível em ordem
        for (int level = 1; level <= maxLevel; level++)
        {
            // Pega todas as questões deste nível
            var questionsInLevel = allQuestions
                .Where(q => GetQuestionLevel(q) == level)
                .ToList();

            if (questionsInLevel.Count == 0)
            {
                Debug.LogWarning($"  Nível {level}: Sem questões, pulando...");
                continue;
            }

            // Conta quantas deste nível estão no Firebase
            int answeredInLevel = questionsInLevel
                .Count(q => answeredSet.Contains(q.questionNumber.ToString()));

            Debug.Log($"  Nível {level}: {answeredInLevel}/{questionsInLevel.Count} respondidas");

            // Se há questões não respondidas neste nível, este é o nível atual
            if (answeredInLevel < questionsInLevel.Count)
            {
                Debug.Log($"✅ NÍVEL ATUAL: {level} (há questões não respondidas neste nível)");
                return level;
            }
        }

        // Se chegou aqui, todas as questões de todos os níveis foram respondidas
        // Retorna o último nível
        Debug.Log($"✅ TODOS OS NÍVEIS COMPLETOS! Retornando nível {maxLevel}");
        return maxLevel;
    }

    /// <summary>
    /// Verifica se um nível específico está completo
    /// (todas as questões do nível estão no Firebase)
    /// </summary>
    public static bool IsLevelComplete(
        List<Question> allQuestions,
        List<string> answeredQuestionsFromFirebase,
        int level)
    {
        var questionsInLevel = allQuestions
            .Where(q => GetQuestionLevel(q) == level)
            .ToList();

        if (questionsInLevel.Count == 0)
        {
            return true; // Se não há questões, considera completo
        }

        HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase ?? new List<string>());

        int answeredInLevel = questionsInLevel
            .Count(q => answeredSet.Contains(q.questionNumber.ToString()));

        return answeredInLevel >= questionsInLevel.Count;
    }

    /// <summary>
    /// Obtém estatísticas de progresso por nível
    /// </summary>
    public static Dictionary<int, LevelStats> GetLevelStats(
        List<Question> allQuestions,
        List<string> answeredQuestionsFromFirebase)
    {
        var stats = new Dictionary<int, LevelStats>();

        if (allQuestions == null || allQuestions.Count == 0)
        {
            return stats;
        }

        HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase ?? new List<string>());

        // Agrupa questões por nível
        var byLevel = allQuestions.GroupBy(q => GetQuestionLevel(q));

        foreach (var group in byLevel.OrderBy(g => g.Key))
        {
            int level = group.Key;
            var questionsInLevel = group.ToList();
            int total = questionsInLevel.Count;
            int answered = questionsInLevel.Count(q => answeredSet.Contains(q.questionNumber.ToString()));

            stats[level] = new LevelStats
            {
                Level = level,
                TotalQuestions = total,
                AnsweredQuestions = answered,
                IsComplete = answered >= total,
                ProgressPercentage = total > 0 ? (float)answered / total * 100f : 0f
            };
        }

        return stats;
    }

    /// <summary>
    /// Obtém o nível de uma questão (trata casos onde questionLevel não foi definido)
    /// </summary>
    private static int GetQuestionLevel(Question question)
    {
        if (question.questionLevel <= 0)
        {
            return 1; // Questões sem nível definido = nível 1
        }
        return question.questionLevel;
    }

    /// <summary>
    /// Obtém o maior nível existente no banco
    /// </summary>
    public static int GetMaxLevel(List<Question> allQuestions)
    {
        if (allQuestions == null || allQuestions.Count == 0)
        {
            return 1;
        }

        int maxLevel = allQuestions.Max(q => GetQuestionLevel(q));
        return maxLevel > 0 ? maxLevel : 1;
    }
}

/// <summary>
/// Estatísticas de um nível
/// </summary>
public class LevelStats
{
    public int Level;
    public int TotalQuestions;
    public int AnsweredQuestions;
    public bool IsComplete;
    public float ProgressPercentage;

    public override string ToString()
    {
        string status = IsComplete ? "✅ Completo" : "⚡ Em progresso";
        return $"Nível {Level}: {AnsweredQuestions}/{TotalQuestions} ({ProgressPercentage:F0}%) - {status}";
    }
}