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

        [Header("Question Sprites")]
        public Sprite questionBackground;
        public Sprite questionImageBackground;

        [Header("Answer Sprites")]
        public Sprite answerButtonBackground;
        public Sprite answerImageButtonBackground;

        [Header("Answer Feedback Sprites")]
        public Sprite correctAnswerBackground;
        public Sprite incorrectAnswerBackground;
        public Sprite correctAnswerImageBackground;
        public Sprite incorrectAnswerImageBackground;

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