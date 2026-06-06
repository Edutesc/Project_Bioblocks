using System.Collections.Generic;
using System.Linq;
using QuestionSystem;

public readonly struct DifficultyMix
{
    public readonly float Basic;
    public readonly float Intermediate;
    public readonly float Difficult;

    public DifficultyMix(float basic, float intermediate, float difficult)
    {
        Basic = basic;
        Intermediate = intermediate;
        Difficult = difficult;
    }

    public static DifficultyMix Default => new DifficultyMix(0.5f, 0.3f, 0.2f);

    public float GetWeightForLevel(int level)
    {
        return level switch
        {
            1 => Basic,
            2 => Intermediate,
            3 => Difficult,
            _ => 0f
        };
    }
}

public static class QuestionSessionSelector
{
    public const int DefaultSessionSize = 10;

    public static List<Question> SelectQuestionsForSession(
        List<Question> allQuestions,
        List<string> answeredQuestionIds,
        int sessionSize = DefaultSessionSize)
    {
        return SelectQuestionsForSession(
            allQuestions,
            answeredQuestionIds,
            sessionSize,
            DifficultyMix.Default);
    }

    public static List<Question> SelectQuestionsForSession(
        List<Question> allQuestions,
        List<string> answeredQuestionIds,
        int sessionSize,
        DifficultyMix mix)
    {
        if (allQuestions == null || allQuestions.Count == 0 || sessionSize <= 0)
            return new List<Question>();

        var answeredSet = new HashSet<string>(answeredQuestionIds ?? new List<string>());

        var unanswered = allQuestions
            .Where(q => q != null && !answeredSet.Contains(q.questionNumber.ToString()))
            .OrderBy(q => GetQuestionLevel(q))
            .ThenBy(q => q.questionNumber)
            .ToList();

        if (unanswered.Count <= sessionSize)
            return unanswered;

        var byLevel = unanswered
            .GroupBy(GetQuestionLevel)
            .ToDictionary(g => g.Key, g => g.ToList());

        var targetCounts = CalculateTargetCounts(sessionSize, mix, byLevel);
        var selected = new List<Question>();
        var selectedNumbers = new HashSet<int>();

        foreach (var level in targetCounts.Keys.OrderBy(level => level))
        {
            if (!byLevel.TryGetValue(level, out var questionsInLevel))
                continue;

            foreach (var question in questionsInLevel.Take(targetCounts[level]))
                AddIfNeeded(selected, selectedNumbers, question);
        }

        FillRemainingSlots(selected, selectedNumbers, unanswered, sessionSize);

        return selected
            .OrderBy(q => GetQuestionLevel(q))
            .ThenBy(q => q.questionNumber)
            .ToList();
    }

    private static Dictionary<int, int> CalculateTargetCounts(
        int sessionSize,
        DifficultyMix mix,
        Dictionary<int, List<Question>> byLevel)
    {
        var weightedLevels = new[] { 1, 2, 3 }
            .Where(level => byLevel.ContainsKey(level) && mix.GetWeightForLevel(level) > 0f)
            .ToList();

        if (weightedLevels.Count == 0)
            return new Dictionary<int, int>();

        float totalWeight = weightedLevels.Sum(mix.GetWeightForLevel);
        var targets = new Dictionary<int, int>();
        var remainders = new Dictionary<int, float>();
        int allocated = 0;

        foreach (int level in weightedLevels)
        {
            float exact = sessionSize * (mix.GetWeightForLevel(level) / totalWeight);
            int count = Clamp((int)exact, 0, byLevel[level].Count);

            targets[level] = count;
            remainders[level] = exact - count;
            allocated += count;
        }

        while (allocated < sessionSize)
        {
            int nextLevel = weightedLevels
                .Where(level => targets[level] < byLevel[level].Count)
                .OrderByDescending(level => remainders[level])
                .ThenBy(level => level)
                .FirstOrDefault();

            if (nextLevel == 0)
                break;

            targets[nextLevel]++;
            allocated++;
            remainders[nextLevel] = 0f;
        }

        return targets;
    }

    private static void FillRemainingSlots(
        List<Question> selected,
        HashSet<int> selectedNumbers,
        List<Question> unanswered,
        int sessionSize)
    {
        foreach (var question in unanswered)
        {
            if (selected.Count >= sessionSize)
                return;

            AddIfNeeded(selected, selectedNumbers, question);
        }
    }

    private static void AddIfNeeded(
        List<Question> selected,
        HashSet<int> selectedNumbers,
        Question question)
    {
        if (selectedNumbers.Add(question.questionNumber))
            selected.Add(question);
    }

    private static int GetQuestionLevel(Question question)
    {
        return question.questionLevel <= 0 ? 1 : question.questionLevel;
    }

    private static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}
