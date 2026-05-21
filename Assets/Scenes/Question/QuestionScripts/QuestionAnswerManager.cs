using QuestionSystem;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAnswerManager : MonoBehaviour
{
    [Header("Answer Buttons")]
    [SerializeField] private Button[] textAnswerButtons;
    [SerializeField] private Button[] imageAnswerButtons;

    [Header("Open Answer")]
    [SerializeField] private GameObject openAnswerPanel;
    [SerializeField] private TMP_InputField openAnswerInput;
    [SerializeField] private Button submitOpenAnswerButton;

    [Header("Theme Configuration")]
    [SerializeField] private QuestionLevelConfig levelConfig;
    [SerializeField] private AnswerButtonThemeManager answerButtonThemeManager;

    [Header("Text Button Components")]
    [SerializeField] private Image[] textButtonBackgrounds;
    [SerializeField] private TextMeshProUGUI[] letterTexts;

    [Header("Image Button Components")]
    [SerializeField] private Image[] imageButtonBackgrounds;
    [SerializeField] private Image[] imageButtonContents;

    private TextMeshProUGUI[] buttonTexts;
    private int currentQuestionLevel = 1;
    private AnswerType currentAnswerType = AnswerType.Text;

    private CancellationTokenSource _activeLoadCts;
    private Sprite[] _ownedAnswerSprites;

    public event System.Action<int> OnAnswerSelected;
    public event System.Action<string> OnOpenAnswerSubmitted;

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
                    Debug.LogError($"TextMeshProUGUI não encontrado no botão {i}");
            }
            else
            {
                Debug.LogError($"Botão de texto {i} não está atribuído no QuestionAnswerManager");
            }
        }

        for (int i = 0; i < imageAnswerButtons.Length; i++)
        {
            if (imageAnswerButtons[i] != null)
            {
                int index = i;
                imageAnswerButtons[i].onClick.AddListener(() => HandleAnswerClick(index));
            }
            else
            {
                Debug.LogError($"Botão de imagem {i} não está atribuído no QuestionAnswerManager");
            }
        }

        if (submitOpenAnswerButton != null)
            submitOpenAnswerButton.onClick.AddListener(HandleOpenAnswerSubmit);
    }

    private void HandleAnswerClick(int selectedIndex)
    {
        Debug.Log($"Botão {selectedIndex} clicado");
        OnAnswerSelected?.Invoke(selectedIndex);
    }

    private void HandleOpenAnswerSubmit()
    {
        string text = openAnswerInput != null ? openAnswerInput.text.Trim() : "";
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("[QuestionAnswerManager] Resposta dissertativa vazia — ignorada.");
            return;
        }

        Debug.Log($"[QuestionAnswerManager] Resposta dissertativa enviada: \"{text}\"");
        OnOpenAnswerSubmitted?.Invoke(text);
    }

    public void SetupAnswerButtons(Question question)
    {
        if (question == null || question.answers == null)
        {
            Debug.LogError("Question ou answers é null em SetupAnswerButtons");
            return;
        }

        currentQuestionLevel = question.questionLevel;
        currentAnswerType    = question.answerType;

        SetAnswerPanelVisibility(question.answerType);

        switch (question.answerType)
        {
            case AnswerType.Image:
                ApplyTheme(question.questionLevel, isImageAnswer: true);
                SetupImageAnswers(question);
                break;

            case AnswerType.Open:
                SetupOpenAnswer();
                break;

            case AnswerType.Text:
            default:
                ApplyTheme(question.questionLevel, isImageAnswer: false);
                SetupTextAnswers(question);
                break;
        }
    }

    // ── Panel visibility ────────────────────────────────────────────────────────

    private void SetAnswerPanelVisibility(AnswerType answerType)
    {
        bool showText  = (answerType == AnswerType.Text);
        bool showImage = (answerType == AnswerType.Image);
        bool showOpen  = (answerType == AnswerType.Open);

        foreach (var btn in textAnswerButtons)
            if (btn != null) btn.gameObject.SetActive(showText);

        foreach (var btn in imageAnswerButtons)
            if (btn != null) btn.gameObject.SetActive(showImage);

        if (openAnswerPanel != null)
            openAnswerPanel.SetActive(showOpen);
    }

    // ── Open answer setup ───────────────────────────────────────────────────────

    private void SetupOpenAnswer()
    {
        if (openAnswerInput != null)
        {
            openAnswerInput.text = "";
            openAnswerInput.interactable = true;
        }

        if (submitOpenAnswerButton != null)
            submitOpenAnswerButton.interactable = true;
    }

    // ── Mark / Reset ────────────────────────────────────────────────────────────

    public void MarkSelectedButton(int buttonIndex, bool isCorrect)
    {
        if (answerButtonThemeManager == null)
        {
            Debug.LogWarning("AnswerButtonThemeManager não está atribuído! Não é possível marcar o botão.");
            return;
        }

        answerButtonThemeManager.MarkButtonAsAnswered(
            buttonIndex, isCorrect, currentQuestionLevel,
            currentAnswerType == AnswerType.Image);
    }

    public void ResetButtonBackgrounds()
    {
        if (answerButtonThemeManager == null)
        {
            Debug.LogWarning("AnswerButtonThemeManager não está atribuído! Não é possível resetar os botões.");
            return;
        }

        answerButtonThemeManager.ResetAllButtonBackgrounds(
            currentQuestionLevel,
            currentAnswerType == AnswerType.Image);
    }

    // ── Theme ───────────────────────────────────────────────────────────────────

    private void ApplyTheme(int questionLevel, bool isImageAnswer)
    {
        if (answerButtonThemeManager != null)
        {
            answerButtonThemeManager.ApplyTheme(questionLevel, isImageAnswer);
            return;
        }

        if (levelConfig == null)
        {
            Debug.LogError("QuestionLevelConfig não está atribuído no QuestionAnswerManager!");
            return;
        }

        var theme = levelConfig.GetThemeForLevel(questionLevel);
        if (theme == null)
        {
            Debug.LogError($"Theme não encontrado para level {questionLevel}");
            return;
        }

        if (isImageAnswer)
            ApplyImageButtonTheme(theme);
        else
            ApplyTextButtonTheme(theme);
    }

    private void ApplyTextButtonTheme(QuestionLevelConfig.LevelTheme theme)
    {
        Debug.Log($"Aplicando tema nos botões de texto - Level {theme.level} ({theme.levelName})");

        for (int i = 0; i < textButtonBackgrounds.Length; i++)
            if (textButtonBackgrounds[i] != null)
                textButtonBackgrounds[i].sprite = theme.answerButtonBackground;

        for (int i = 0; i < letterTexts.Length; i++)
            if (letterTexts[i] != null)
                letterTexts[i].color = theme.letterTextColor;

        for (int i = 0; i < buttonTexts.Length; i++)
            if (buttonTexts[i] != null)
                buttonTexts[i].color = theme.answerTextColor;
    }

    private void ApplyImageButtonTheme(QuestionLevelConfig.LevelTheme theme)
    {
        Debug.Log($"Aplicando tema nos botões de imagem - Level {theme.level} ({theme.levelName})");

        for (int i = 0; i < imageButtonBackgrounds.Length; i++)
        {
            if (imageButtonBackgrounds[i] != null)
            {
                imageButtonBackgrounds[i].sprite = theme.answerImageButtonBackground;
                Debug.Log($"Background do botão de imagem {i} aplicado");
            }
            else
            {
                Debug.LogWarning($"imageButtonBackgrounds[{i}] é null!");
            }
        }
    }

    // ── Answer setup helpers ────────────────────────────────────────────────────

    private void SetupImageAnswers(Question question)
    {
        // Cancela qualquer carga anterior e libera as sprites próprias antes de
        // disparar as novas requisições assíncronas.
        CancelActiveLoad();
        DisposeOwnedSprites();

        _activeLoadCts = new CancellationTokenSource();
        _ownedAnswerSprites = new Sprite[imageAnswerButtons.Length];

        int count = Mathf.Min(imageAnswerButtons.Length, question.answers?.Length ?? 0);

        for (int i = 0; i < count; i++)
        {
            if (imageAnswerButtons[i] == null)
            {
                Debug.LogError($"imageAnswerButtons[{i}] é null!");
                continue;
            }
            if (imageButtonContents[i] == null)
            {
                Debug.LogError($"imageButtonContents[{i}] é null!");
                continue;
            }

            string imagePath = question.answers[i];
            if (!QuestionStorageKeys.LooksLikeImagePath(imagePath))
            {
                Debug.LogWarning($"Resposta {i} não parece um path de imagem: '{imagePath}'.");
                continue;
            }

            // imagePath já é:
            //   - Preview mode: path legado do C# database (ex: "AnswerImages/AminoacidsDB/.../isoleucina")
            //   - Dev/Prod mode: storage key do Firestore (ex: "aminoacids/isoleucina")
            // LoadAnswerImageAsync distingue os dois casos via AppContext.ImageSync.
            int buttonIndex = i;  // captura para o lambda
            imageAnswerButtons[i].interactable = false;
            _ = LoadAnswerImageAsync(buttonIndex, imagePath, _activeLoadCts.Token);
        }
    }

    private async Task LoadAnswerImageAsync(int buttonIndex, string imagePath, CancellationToken ct)
    {
        Texture2D texture = null;

        try
        {
            // Preview mode: AppContext.ImageSync é null — lê direto de Resources.
            // imagePath é o path legado do C# database (ex: "AnswerImages/AminoacidsDB/.../isoleucina").
            if (AppContext.ImageSync == null)
            {
                texture = Resources.Load<Texture2D>(imagePath);
                if (texture == null)
                {
                    Debug.LogWarning($"[QuestionAnswerManager] Preview mode — imagem não encontrada em Resources: '{imagePath}'.");
                    return;
                }
            }
            else
            {
                // Dev/Prod mode: imagePath é storage key (ex: "aminoacids/isoleucina").
                texture = await AppContext.ImageSync.GetImageAsync(imagePath, ct);
                if (ct.IsCancellationRequested || texture == null)
                {
                    if (texture != null) Destroy(texture);
                    return;
                }
            }

            // Pode ter caído fora do range se a UI foi reconfigurada nesse meio tempo.
            if (buttonIndex >= imageButtonContents.Length || imageButtonContents[buttonIndex] == null)
            {
                if (AppContext.ImageSync != null) Destroy(texture); // não destruir textures de Resources
                return;
            }

            var sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f);

            // Limpa a sprite anterior (se a tivermos criado nós mesmos)
            if (_ownedAnswerSprites != null &&
                buttonIndex < _ownedAnswerSprites.Length &&
                _ownedAnswerSprites[buttonIndex] != null)
            {
                DestroySpriteAndTexture(_ownedAnswerSprites[buttonIndex]);
            }

            imageButtonContents[buttonIndex].sprite = sprite;
            if (_ownedAnswerSprites != null && buttonIndex < _ownedAnswerSprites.Length)
                _ownedAnswerSprites[buttonIndex] = sprite;

            if (buttonIndex < imageAnswerButtons.Length && imageAnswerButtons[buttonIndex] != null)
                imageAnswerButtons[buttonIndex].interactable = true;

            Debug.Log($"Imagem carregada para o botão {buttonIndex}: {imagePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao carregar imagem da resposta '{imagePath}': {e.Message}");
        }
    }

    private void CancelActiveLoad()
    {
        if (_activeLoadCts == null) return;
        try { _activeLoadCts.Cancel(); } catch { /* noop */ }
        _activeLoadCts.Dispose();
        _activeLoadCts = null;
    }

    private void DisposeOwnedSprites()
    {
        if (_ownedAnswerSprites == null) return;
        for (int i = 0; i < _ownedAnswerSprites.Length; i++)
        {
            if (_ownedAnswerSprites[i] != null)
                DestroySpriteAndTexture(_ownedAnswerSprites[i]);
            _ownedAnswerSprites[i] = null;
        }
        _ownedAnswerSprites = null;
    }

    private void DestroySpriteAndTexture(Sprite sprite)
    {
        if (sprite == null) return;
        var tex = sprite.texture;
        Destroy(sprite);
        if (tex != null) Destroy(tex);
    }

    private void OnDestroy()
    {
        CancelActiveLoad();
        DisposeOwnedSprites();
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

    // ── Enable / Disable ────────────────────────────────────────────────────────

    public void DisableAllButtons()
    {
        foreach (var button in textAnswerButtons)
            if (button != null) button.interactable = false;

        foreach (var button in imageAnswerButtons)
            if (button != null) button.interactable = false;

        if (openAnswerInput != null)        openAnswerInput.interactable        = false;
        if (submitOpenAnswerButton != null) submitOpenAnswerButton.interactable = false;
    }

    public void EnableAllButtons()
    {
        switch (currentAnswerType)
        {
            case AnswerType.Image:
                foreach (var button in imageAnswerButtons)
                    if (button != null) button.interactable = true;
                break;

            case AnswerType.Open:
                if (openAnswerInput        != null) openAnswerInput.interactable        = true;
                if (submitOpenAnswerButton != null) submitOpenAnswerButton.interactable = true;
                break;

            case AnswerType.Text:
            default:
                foreach (var button in textAnswerButtons)
                    if (button != null) button.interactable = true;
                break;
        }
    }

    // ── Editor helpers ──────────────────────────────────────────────────────────

    private void OnValidate()
    {
        if (letterTexts != null && letterTexts.Length == 4)
        {
            string[] letters = { "A", "B", "C", "D" };
            for (int i = 0; i < letterTexts.Length; i++)
            {
                if (letterTexts[i] != null && string.IsNullOrEmpty(letterTexts[i].text))
                    letterTexts[i].text = letters[i];
            }
        }
    }
}
