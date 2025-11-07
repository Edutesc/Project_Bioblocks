using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtonThemeManager : MonoBehaviour
{
    [System.Serializable]
    public class AnswerButton
    {
        public Image buttonBackground;
        public TextMeshProUGUI letterText;
        public TextMeshProUGUI answerText;
    }

    [System.Serializable]
    public class ImageAnswerButton
    {
        public Image buttonBackground;
    }

    [Header("Configuration")]
    [SerializeField] private QuestionLevelConfig levelConfig;

    [Header("Text Answer Buttons")]
    [SerializeField] private AnswerButton[] textAnswerButtons = new AnswerButton[4];
    private Sprite[] originalButtonBackgrounds = new Sprite[4];

    [Header("Image Answer Buttons")]
    [SerializeField] private ImageAnswerButton[] imageAnswerButtons = new ImageAnswerButton[4];
    private Sprite[] originalImageButtonBackgrounds = new Sprite[4];

    public void ApplyTheme(int questionLevel, bool isImageAnswer)
    {
        if (levelConfig == null)
        {
            Debug.LogError("QuestionLevelConfig não está atribuído no AnswerButtonThemeManager!");
            return;
        }

        if (isImageAnswer)
        {
            ApplyImageButtonTheme(questionLevel);
        }
        else
        {
            ApplyTextButtonTheme(questionLevel);
        }
    }

    private void ApplyTextButtonTheme(int questionLevel)
    {
        var theme = levelConfig.GetThemeForLevel(questionLevel);

        if (theme == null)
        {
            Debug.LogError($"Theme não encontrado para level {questionLevel}");
            return;
        }

        Debug.Log($"Aplicando tema nos botões de texto - Level {questionLevel} ({theme.levelName})");

        for (int i = 0; i < textAnswerButtons.Length; i++)
        {
            var button = textAnswerButtons[i];

            if (button.buttonBackground != null)
            {
                button.buttonBackground.sprite = theme.answerButtonBackground;
                originalButtonBackgrounds[i] = theme.answerButtonBackground;
            }

            if (button.letterText != null)
            {
                button.letterText.color = theme.letterTextColor;
            }

            if (button.answerText != null)
            {
                button.answerText.color = theme.answerTextColor;
            }
        }
    }

    private void ApplyImageButtonTheme(int questionLevel)
    {
        var theme = levelConfig.GetThemeForLevel(questionLevel);

        if (theme == null)
        {
            Debug.LogError($"Theme não encontrado para level {questionLevel}");
            return;
        }

        Debug.Log($"Aplicando tema nos botões de imagem - Level {questionLevel} ({theme.levelName})");

        for (int i = 0; i < imageAnswerButtons.Length; i++)
        {
            if (imageAnswerButtons[i].buttonBackground != null)
            {
                imageAnswerButtons[i].buttonBackground.sprite = theme.answerImageButtonBackground;
                originalImageButtonBackgrounds[i] = theme.answerImageButtonBackground;
            }
        }
    }

    public void MarkButtonAsAnswered(int buttonIndex, bool isCorrect, int questionLevel, bool isImageAnswer)
    {
        if (levelConfig == null)
        {
            Debug.LogError("QuestionLevelConfig não está atribuído!");
            return;
        }

        if (buttonIndex < 0 || buttonIndex >= 4)
        {
            Debug.LogError($"Índice de botão inválido: {buttonIndex}");
            return;
        }

        var theme = levelConfig.GetThemeForLevel(questionLevel);

        if (theme == null)
        {
            Debug.LogError($"Theme não encontrado para level {questionLevel}");
            return;
        }

        if (isImageAnswer)
        {
            MarkImageButtonAsAnswered(buttonIndex, isCorrect, theme);
        }
        else
        {
            MarkTextButtonAsAnswered(buttonIndex, isCorrect, theme);
        }
    }

    private void MarkTextButtonAsAnswered(int buttonIndex, bool isCorrect, QuestionLevelConfig.LevelTheme theme)
    {
        var button = textAnswerButtons[buttonIndex];

        if (button.buttonBackground != null)
        {
            Sprite feedbackSprite = isCorrect ? theme.correctAnswerBackground : theme.incorrectAnswerBackground;

            if (feedbackSprite != null)
            {
                button.buttonBackground.sprite = feedbackSprite;
                Debug.Log($"Botão de texto {buttonIndex} marcado como {(isCorrect ? "CORRETO" : "INCORRETO")}");
            }
            else
            {
                Debug.LogWarning($"Sprite de feedback {(isCorrect ? "correto" : "incorreto")} não está atribuído no level {theme.level}!");
            }
        }
    }

    private void MarkImageButtonAsAnswered(int buttonIndex, bool isCorrect, QuestionLevelConfig.LevelTheme theme)
    {
        var button = imageAnswerButtons[buttonIndex];

        if (button.buttonBackground != null)
        {
            Sprite feedbackSprite = isCorrect ? theme.correctAnswerImageBackground : theme.incorrectAnswerImageBackground;

            if (feedbackSprite != null)
            {
                button.buttonBackground.sprite = feedbackSprite;
                Debug.Log($"Botão de imagem {buttonIndex} marcado como {(isCorrect ? "CORRETO" : "INCORRETO")}");
            }
            else
            {
                Debug.LogWarning($"Sprite de feedback de imagem {(isCorrect ? "correto" : "incorreto")} não está atribuído no level {theme.level}!");
            }
        }
    }

    public void ResetAllButtonBackgrounds(int questionLevel, bool isImageAnswer)
    {
        if (levelConfig == null)
        {
            Debug.LogError("QuestionLevelConfig não está atribuído!");
            return;
        }

        var theme = levelConfig.GetThemeForLevel(questionLevel);

        if (theme == null)
        {
            Debug.LogError($"Theme não encontrado para level {questionLevel}");
            return;
        }

        if (isImageAnswer)
        {
            ResetImageButtonBackgrounds(theme);
        }
        else
        {
            ResetTextButtonBackgrounds(theme);
        }
    }

    private void ResetTextButtonBackgrounds(QuestionLevelConfig.LevelTheme theme)
    {
        Debug.Log($"Resetando backgrounds dos botões de texto para o tema do level {theme.level}");

        for (int i = 0; i < textAnswerButtons.Length; i++)
        {
            var button = textAnswerButtons[i];

            if (button.buttonBackground != null)
            {
                Sprite spriteToUse = originalButtonBackgrounds[i] != null 
                    ? originalButtonBackgrounds[i] 
                    : theme.answerButtonBackground;

                button.buttonBackground.sprite = spriteToUse;
            }
        }
    }

    private void ResetImageButtonBackgrounds(QuestionLevelConfig.LevelTheme theme)
    {
        Debug.Log($"Resetando backgrounds dos botões de imagem para o tema do level {theme.level}");

        for (int i = 0; i < imageAnswerButtons.Length; i++)
        {
            if (imageAnswerButtons[i].buttonBackground != null)
            {
                Sprite spriteToUse = originalImageButtonBackgrounds[i] != null 
                    ? originalImageButtonBackgrounds[i] 
                    : theme.answerImageButtonBackground;

                imageAnswerButtons[i].buttonBackground.sprite = spriteToUse;
            }
        }
    }

    private void OnValidate()
    {
        if (textAnswerButtons.Length != 4)
        {
            Debug.LogWarning("TextAnswerButtons deve ter exatamente 4 elementos (A, B, C, D)!");
        }

        if (imageAnswerButtons.Length != 4)
        {
            Debug.LogWarning("ImageAnswerButtons deve ter exatamente 4 elementos!");
        }

        string[] letters = { "A", "B", "C", "D" };
        for (int i = 0; i < Mathf.Min(textAnswerButtons.Length, 4); i++)
        {
            if (textAnswerButtons[i].letterText != null)
            {
                textAnswerButtons[i].letterText.text = letters[i];
            }
        }
    }
}