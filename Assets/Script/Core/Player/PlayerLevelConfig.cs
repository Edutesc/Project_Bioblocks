using UnityEngine;

public static class PlayerLevelConfig
{
    public static readonly LevelThreshold[] LEVEL_THRESHOLDS = new LevelThreshold[]
    {
        new LevelThreshold(1, 0.00f, 0.10f, 1000),
        new LevelThreshold(2, 0.10f, 0.20f, 2000),
        new LevelThreshold(3, 0.20f, 0.30f, 3000),
        new LevelThreshold(4, 0.30f, 0.40f, 4000),
        new LevelThreshold(5, 0.40f, 0.50f, 5000),
        new LevelThreshold(6, 0.50f, 0.60f, 6000),
        new LevelThreshold(7, 0.60f, 0.70f, 7000),
        new LevelThreshold(8, 0.70f, 0.80f, 8000),
        new LevelThreshold(9, 0.80f, 0.90f, 9000),
        new LevelThreshold(10, 0.90f, 1.00f, 10000)
    };

    [System.Serializable]
    public struct LevelThreshold
    {
        public int Level;
        public float MinPercentage;
        public float MaxPercentage;
        public int BonusPoints;

        public LevelThreshold(int level, float min, float max, int bonus)
        {
            Level = level;
            MinPercentage = min;
            MaxPercentage = max;
            BonusPoints = bonus;
        }

        public int GetRequiredQuestions(int totalQuestions)
        {
            return Mathf.CeilToInt(totalQuestions * MaxPercentage);
        }

        public int GetMinRequiredQuestions(int totalQuestions)
        {
            return Mathf.CeilToInt(totalQuestions * MinPercentage);
        }
    }

    public static int CalculateLevel(int questionsAnswered, int totalQuestions)
    {
        if (totalQuestions <= 0) return 1;

        float percentage = (float)questionsAnswered / totalQuestions;

        foreach (var threshold in LEVEL_THRESHOLDS)
        {
            if (percentage >= threshold.MinPercentage && percentage < threshold.MaxPercentage)
            {
                return threshold.Level;
            }
        }

        return 10;
    }

    public static int GetBonusForLevel(int level)
    {
        foreach (var threshold in LEVEL_THRESHOLDS)
        {
            if (threshold.Level == level)
            {
                return threshold.BonusPoints;
            }
        }
        return 0;
    }

    public static LevelThreshold GetThresholdForLevel(int level)
    {
        foreach (var threshold in LEVEL_THRESHOLDS)
        {
            if (threshold.Level == level)
            {
                return threshold;
            }
        }
        return LEVEL_THRESHOLDS[0];
    }
}