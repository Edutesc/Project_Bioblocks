using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtonThemeManager : MonoBehaviour
{
    [System.Serializable]
    public class AnswerButton
    {
        [Tooltip("Background do botão (imagem com círculo embutido)")]
        public Image buttonBackground;

        [Tooltip("Texto da letra (A, B, C, D)")]
        public TextMeshProUGUI letterText;

        [Tooltip("Texto da resposta")]
        public TextMeshProUGUI answerText;
    }

    [Header("Configuration")]
    [SerializeField] private QuestionLevelConfig levelConfig;

    [Header("Text Answer Buttons (apenas para respostas de texto)")]
    [SerializeField] private AnswerButton[] textAnswerButtons = new AnswerButton[4];

    public void ApplyTheme(int questionLevel, bool isImageAnswer)
    {
        // Só aplica tema se for resposta de TEXTO
        if (isImageAnswer)
        {
            Debug.Log("Respostas são imagens, não aplica tema nos botões");
            return;
        }

        if (levelConfig == null)
        {
            Debug.LogError("QuestionLevelConfig não está atribuído no AnswerButtonThemeManager!");
            return;
        }

        var theme = levelConfig.GetThemeForLevel(questionLevel);

        if (theme == null)
        {
            Debug.LogError($"Theme não encontrado para level {questionLevel}");
            return;
        }

        Debug.Log($"Aplicando tema nos botões - Level {questionLevel} ({theme.levelName})");

        // Aplica tema nos botões de texto
        for (int i = 0; i < textAnswerButtons.Length; i++)
        {
            var button = textAnswerButtons[i];

            if (button.buttonBackground != null)
            {
                button.buttonBackground.sprite = theme.answerButtonBackground;
                Debug.Log($"Botão {i} background aplicado: {theme.answerButtonBackground.name}");
            }
            else
            {
                Debug.LogWarning($"Button {i} - buttonBackground não está atribuído!");
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

    private void OnValidate()
    {
        if (textAnswerButtons.Length != 4)
        {
            Debug.LogWarning("TextAnswerButtons deve ter exatamente 4 elementos (A, B, C, D)!");
        }

        // Auto-preenche as letras
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