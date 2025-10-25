using QuestionSystem;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAnswerManager : MonoBehaviour
{
    [Header("Answer Buttons")]
    [SerializeField] private Button[] textAnswerButtons;
    [SerializeField] private Button[] imageAnswerButtons;

    [Header("Theme Configuration")]
    [SerializeField] private QuestionLevelConfig levelConfig;

    [Header("Text Button Components (para aplicar tema)")]
    [SerializeField] private Image[] textButtonBackgrounds; // Backgrounds dos 4 botões de texto
    [SerializeField] private TextMeshProUGUI[] letterTexts; // A, B, C, D

    private TextMeshProUGUI[] buttonTexts;
    private Image[] buttonImages;

    public event System.Action<int> OnAnswerSelected;

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        buttonTexts = new TextMeshProUGUI[textAnswerButtons.Length];
        for (int i = 0; i < textAnswerButtons.Length; i++)
        {
            if (textAnswerButtons[i] != null)
            {
                buttonTexts[i] = textAnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                int index = i;
                textAnswerButtons[i].onClick.AddListener(() => HandleAnswerClick(index));

                if (buttonTexts[i] == null)
                {
                    Debug.LogError($"TextMeshProUGUI não encontrado no botão {i}");
                }
            }
            else
            {
                Debug.LogError($"Botão de texto {i} não está atribuído no QuestionAnswerManager");
            }
        }

        buttonImages = new Image[imageAnswerButtons.Length];
        for (int i = 0; i < imageAnswerButtons.Length; i++)
        {
            if (imageAnswerButtons[i] != null)
            {
                buttonImages[i] = imageAnswerButtons[i].GetComponent<Image>();
                int index = i;
                imageAnswerButtons[i].onClick.AddListener(() => HandleAnswerClick(index));

                if (buttonImages[i] == null)
                {
                    Debug.LogError($"Image não encontrado no botão {i}");
                }
            }
            else
            {
                Debug.LogError($"Botão de imagem {i} não está atribuído no QuestionAnswerManager");
            }
        }
    }

    private void HandleAnswerClick(int selectedIndex)
    {
        Debug.Log($"Botão {selectedIndex} clicado");
        OnAnswerSelected?.Invoke(selectedIndex);
    }

    public void SetupAnswerButtons(Question question)
    {
        if (question == null || question.answers == null)
        {
            Debug.LogError("Question ou answers é null em SetupAnswerButtons");
            return;
        }

        // PRIMEIRO: Aplica o tema baseado no level
        ApplyTheme(question.questionLevel, question.isImageAnswer);

        // DEPOIS: Configura o conteúdo
        if (question.isImageAnswer)
        {
            SetupImageAnswers(question);
        }
        else
        {
            SetupTextAnswers(question);
        }
    }

    /// <summary>
    /// Aplica o tema visual nos botões de resposta baseado no nível da questão
    /// </summary>
    private void ApplyTheme(int questionLevel, bool isImageAnswer)
    {
        // Só aplica tema se for resposta de TEXTO
        if (isImageAnswer)
        {
            Debug.Log("🔘 Respostas são imagens, não aplica tema nos botões");
            return;
        }

        if (levelConfig == null)
        {
            Debug.LogError("⚠️ QuestionLevelConfig não está atribuído no QuestionAnswerManager!");
            return;
        }

        var theme = levelConfig.GetThemeForLevel(questionLevel);

        if (theme == null)
        {
            Debug.LogError($"⚠️ Theme não encontrado para level {questionLevel}");
            return;
        }

        Debug.Log($"🔘 Aplicando tema nos botões - Level {questionLevel} ({theme.levelName})");

        // Aplica tema nos backgrounds dos botões de texto
        for (int i = 0; i < textButtonBackgrounds.Length; i++)
        {
            if (textButtonBackgrounds[i] != null)
            {
                textButtonBackgrounds[i].sprite = theme.answerButtonBackground;
                Debug.Log($"✅ Botão {i} background aplicado: {theme.answerButtonBackground.name}");
            }
            else
            {
                Debug.LogWarning($"⚠️ Background do botão {i} não está atribuído!");
            }
        }

        // Aplica cor nas letras (A, B, C, D)
        for (int i = 0; i < letterTexts.Length; i++)
        {
            if (letterTexts[i] != null)
            {
                letterTexts[i].color = theme.letterTextColor;
            }
        }

        // Aplica cor nos textos das respostas
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (buttonTexts[i] != null)
            {
                buttonTexts[i].color = theme.answerTextColor;
            }
        }
    }

    private void SetupImageAnswers(Question question)
    {
        for (int i = 0; i < imageAnswerButtons.Length && i < question.answers.Length; i++)
        {
            if (imageAnswerButtons[i] != null && buttonImages[i] != null)
            {
                // Carrega a imagem do caminho especificado em answers
                Sprite sprite = Resources.Load<Sprite>(question.answers[i]);
                if (sprite != null)
                {
                    buttonImages[i].sprite = sprite;
                    imageAnswerButtons[i].interactable = true;
                    Debug.Log($"Imagem carregada para o botão {i}: {question.answers[i]}");
                }
                else
                {
                    Debug.LogError($"Falha ao carregar imagem: {question.answers[i]}");
                }
            }
        }
    }

    private void SetupTextAnswers(Question question)
    {
        for (int i = 0; i < textAnswerButtons.Length && i < question.answers.Length; i++)
        {
            if (textAnswerButtons[i] != null && buttonTexts[i] != null)
            {
                buttonTexts[i].text = question.answers[i];
                textAnswerButtons[i].interactable = true;
                Debug.Log($"Botão {i} configurado com texto: {question.answers[i]}");
            }
        }
    }

    public void DisableAllButtons()
    {
        // Desativa botões de texto
        foreach (var button in textAnswerButtons)
        {
            if (button != null)
            {
                button.interactable = false;
            }
        }

        // Desativa botões de imagem
        foreach (var button in imageAnswerButtons)
        {
            if (button != null)
            {
                button.interactable = false;
            }
        }
    }

    public void EnableAllButtons()
    {
        // Ativa botões de texto
        foreach (var button in textAnswerButtons)
        {
            if (button != null)
            {
                button.interactable = true;
            }
        }

        // Ativa botões de imagem
        foreach (var button in imageAnswerButtons)
        {
            if (button != null)
            {
                button.interactable = true;
            }
        }
    }

    private void OnValidate()
    {
        // Auto-preenche as letras A, B, C, D
        if (letterTexts != null && letterTexts.Length == 4)
        {
            string[] letters = { "A", "B", "C", "D" };
            for (int i = 0; i < letterTexts.Length; i++)
            {
                if (letterTexts[i] != null && string.IsNullOrEmpty(letterTexts[i].text))
                {
                    letterTexts[i].text = letters[i];
                }
            }
        }
    }
}