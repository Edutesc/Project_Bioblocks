using UnityEngine;

[CreateAssetMenu(fileName = "QuestionLevelConfig", menuName = "Question System/Level Config")]
public class QuestionLevelConfig : ScriptableObject
{
    [System.Serializable]
    public class LevelTheme
    {
        [Header("Identity")]
        public int level;
        public string levelName;

        [Header("Sprites")]
        public Sprite questionBackground;
        public Sprite answerButtonBackground;

        [Header("Text Colors")]
        public Color letterTextColor = Color.white;
        public Color answerTextColor = Color.black;

        [Header("Feedback Colors")]
        public Color feedbackCorrectColor = Color.green;
        public Color feedbackIncorrectColor = Color.red;
    }

    public LevelTheme[] levelThemes = new LevelTheme[3];

    public LevelTheme GetThemeForLevel(int level)
    {
        int index = Mathf.Clamp(level, 1, 3) - 1;

        if (levelThemes != null && index < levelThemes.Length)
        {
            return levelThemes[index];
        }

        Debug.LogError($"Theme not found for level {level}!");
        return null;
    }
}