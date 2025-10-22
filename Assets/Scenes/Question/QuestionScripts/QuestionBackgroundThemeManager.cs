using UnityEngine;
using UnityEngine.UI;

public class QuestionBackgroundThemeManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private QuestionLevelConfig levelConfig;

    [Header("Question Text Background (apenas para questões de texto)")]
    [SerializeField] private Image questionTextBackground;

    // NÃO precisa de questionImageBackground porque a imagem já vem com background

    public void ApplyTheme(int questionLevel)
    {
        var theme = levelConfig.GetThemeForLevel(questionLevel);

        if (theme == null)
        {
            Debug.LogError("Theme não encontrado para o level: " + questionLevel);
            return;
        }

        // Só aplica no container de TEXTO
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
