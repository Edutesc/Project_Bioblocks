using System.Collections.Generic;
using System.Linq;
using QuestionSystem;

public static class LevelCalculator
{
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

    private static int GetQuestionLevel(Question question)
    {
        if (question.questionLevel <= 0)
        {
            return 1;
        }
        return question.questionLevel;
    }
}

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
