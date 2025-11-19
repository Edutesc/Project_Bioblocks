using UnityEngine;
using UnityEngine.UI;

public class QuestionBackgroundThemeManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private QuestionLevelConfig levelConfig;

    [Header("Question Backgrounds")]
    [SerializeField] private Image questionTextBackground;
    [SerializeField] private Image questionImageBackground;

    public void ApplyTheme(int questionLevel, bool isImageQuestion)
    {
        if (levelConfig == null)
        {
            Debug.LogError("QuestionLevelConfig não está atribuído!");
            return;
        }

        var theme = levelConfig.GetThemeForLevel(questionLevel);
        if (theme == null)
        {
            Debug.LogError($"Theme não encontrado para o level: {questionLevel}");
            return;
        }

        if (isImageQuestion)
        {
            if (questionImageBackground != null)
            {
                questionImageBackground.sprite = theme.questionImageBackground;
            }
            else
            {
                Debug.LogWarning("questionImageBackground não está atribuído!");
            }
        }
        else
        {
            if (questionTextBackground != null)
            {
                questionTextBackground.sprite = theme.questionBackground;
            }
            else
            {
                Debug.LogWarning("questionTextBackground não está atribuído!");
            }
        }
    }
}