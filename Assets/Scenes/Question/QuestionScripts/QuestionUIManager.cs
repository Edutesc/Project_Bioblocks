using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuestionSystem;

public class QuestionUIManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image questionImage;

    [Header("Theme Management")]
    [SerializeField] private AnswerButtonThemeManager answerButtonThemeManager;
    [SerializeField] private QuestionBackgroundThemeManager questionBackgroundThemeManager;

    private Sprite preloadedQuestionImage;
    private bool isPreloading = false;

    private CancellationTokenSource _activeLoadCts;

    private void Start()
    {
        ValidateComponents();
    }

    private void ValidateComponents()
    {
        if (questionText == null) Debug.LogError("QuestionText não atribuído");
        if (questionImage == null) Debug.LogError("QuestionImage não atribuído");
        if (answerButtonThemeManager == null) Debug.LogWarning("AnswerButtonThemeManager não atribuído");
        if (questionBackgroundThemeManager == null) Debug.LogWarning("QuestionBackgroundThemeManager não atribuído");
    }

    public void ShowQuestion(Question question)
    {
        ApplyTheme(question);

        if (question.questionType == QuestionType.Image)
        {
            ShowImageQuestion(question);
        }
        else
        {
            ShowTextQuestion(question);
        }
    }

    private void ApplyTheme(Question question)
    {
        if (answerButtonThemeManager != null)
        {
            answerButtonThemeManager.ApplyTheme(question.questionLevel, question.answerType == AnswerType.Image);
        }

        if (questionBackgroundThemeManager != null)
        {
            questionBackgroundThemeManager.ApplyTheme(question.questionLevel, question.questionType == QuestionType.Image);
        }
    }

    private void ShowImageQuestion(Question question)
    {
        questionText.text = question.questionText;

        if (preloadedQuestionImage != null && !string.IsNullOrEmpty(question.questionImagePath))
        {
            AssignSprite(preloadedQuestionImage);
            preloadedQuestionImage = null;
            return;
        }

        if (string.IsNullOrEmpty(question.questionImagePath))
        {
            questionImage.gameObject.SetActive(false);
            return;
        }

        // Sem preload: dispara fetch async e mantém escondido enquanto baixa.
        questionImage.gameObject.SetActive(false);
        _ = LoadQuestionImageAsync(question);
    }

    private void ShowTextQuestion(Question question)
    {
        questionText.text = question.questionText;
        questionImage.gameObject.SetActive(false);
    }

    private async Task LoadQuestionImageAsync(Question question)
    {
        CancelActiveLoad();
        _activeLoadCts = new CancellationTokenSource();
        var ct = _activeLoadCts.Token;

        string imagePath = question.questionImagePath;

        // Preview mode: AppContext.ImageSync não está disponível — lê direto de Resources.
        // O path no C# database é um caminho Resources.Load válido (ex: "QuestionImages/AminoacidsDB/...").
        if (AppContext.ImageSync == null)
        {
            Texture2D resourceTexture = Resources.Load<Texture2D>(imagePath);
            if (resourceTexture == null)
            {
                Debug.LogWarning($"[QuestionUIManager] Preview mode — imagem não encontrada em Resources: '{imagePath}'.");
                return;
            }
            var resourceSprite = Sprite.Create(
                resourceTexture,
                new Rect(0, 0, resourceTexture.width, resourceTexture.height),
                new Vector2(0.5f, 0.5f),
                100f);
            AssignSprite(resourceSprite);
            return;
        }

        // Dev/Prod mode: imagePath é uma storage key (ex: "aminoacids/aminoacidDB_ImageQuestionContainer10").
        // O Firestore já armazena storage keys após a migração do UploadQuestionBanksEditor.
        try
        {
            Texture2D texture = await AppContext.ImageSync.GetImageAsync(imagePath, ct);
            if (ct.IsCancellationRequested || texture == null)
            {
                if (texture != null) Destroy(texture);
                return;
            }

            var sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f);

            AssignSprite(sprite);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[QuestionUIManager] Erro ao carregar imagem da questão '{imagePath}': {e.Message}");
        }
    }

    public async Task PreloadQuestionImage(Question questionToPreload)
    {
        if (questionToPreload.questionType != QuestionType.Image || string.IsNullOrEmpty(questionToPreload.questionImagePath))
        {
            preloadedQuestionImage = null;
            return;
        }

        if (isPreloading) return;
        isPreloading = true;

        string imagePath = questionToPreload.questionImagePath;

        try
        {
            // Preview mode: lê de Resources usando o path legado do C# database.
            if (AppContext.ImageSync == null)
            {
                Texture2D resourceTexture = Resources.Load<Texture2D>(imagePath);
                if (resourceTexture == null)
                {
                    Debug.LogWarning($"[QuestionUIManager] Preload preview mode — imagem não encontrada em Resources: '{imagePath}'.");
                    preloadedQuestionImage = null;
                    return;
                }
                preloadedQuestionImage = Sprite.Create(
                    resourceTexture,
                    new Rect(0, 0, resourceTexture.width, resourceTexture.height),
                    new Vector2(0.5f, 0.5f),
                    100f);
                return;
            }

            // Dev/Prod mode: imagePath é storage key, já armazenada assim no Firestore.
            Texture2D texture = await AppContext.ImageSync.GetImageAsync(imagePath);
            if (texture == null)
            {
                Debug.LogWarning($"[QuestionUIManager] Preload: imagem '{imagePath}' não disponível.");
                preloadedQuestionImage = null;
                return;
            }

            preloadedQuestionImage = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[QuestionUIManager] Erro no preload: {e.Message}");
            preloadedQuestionImage = null;
        }
        finally
        {
            isPreloading = false;
        }
    }

    public void ClearPreloadedResources()
    {
        if (preloadedQuestionImage != null)
        {
            DestroySpriteAndTexture(preloadedQuestionImage);
            preloadedQuestionImage = null;
        }
    }

    // ── Atribuição/limpeza segura de sprites próprios ──────────────────────────

    private void AssignSprite(Sprite sprite)
    {
        if (questionImage == null) return;

        var previous = questionImage.sprite;
        questionImage.sprite = sprite;
        questionImage.gameObject.SetActive(true);

        if (previous != null && previous != sprite)
            DestroySpriteAndTexture(previous);
    }

    private void DestroySpriteAndTexture(Sprite sprite)
    {
        if (sprite == null) return;
        var tex = sprite.texture;
        Destroy(sprite);
        if (tex != null) Destroy(tex);
    }

    private void CancelActiveLoad()
    {
        if (_activeLoadCts == null) return;
        try { _activeLoadCts.Cancel(); } catch { /* noop */ }
        _activeLoadCts.Dispose();
        _activeLoadCts = null;
    }

    private void OnDestroy()
    {
        CancelActiveLoad();
        ClearPreloadedResources();

        if (questionImage != null && questionImage.sprite != null)
            DestroySpriteAndTexture(questionImage.sprite);
    }
}
