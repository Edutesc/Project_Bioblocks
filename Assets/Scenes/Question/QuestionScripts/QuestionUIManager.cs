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

        if (question.isImageQuestion)
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
            answerButtonThemeManager.ApplyTheme(question.questionLevel, question.isImageAnswer);
        }

        if (questionBackgroundThemeManager != null)
        {
            questionBackgroundThemeManager.ApplyTheme(question.questionLevel, question.isImageQuestion);
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

        string key = QuestionStorageKeys.Resolve(question.questionImagePath, question.topic);
        if (string.IsNullOrEmpty(key))
        {
            Debug.LogWarning($"[QuestionUIManager] Não foi possível resolver storageKey para questão '{question.globalId}'.");
            return;
        }

        if (AppContext.ImageSync == null)
        {
            Debug.LogError("[QuestionUIManager] AppContext.ImageSync indisponível.");
            return;
        }

        try
        {
            Texture2D texture = await AppContext.ImageSync.GetImageAsync(key, ct);
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
            Debug.LogError($"[QuestionUIManager] Erro ao carregar imagem da questão '{key}': {e.Message}");
        }
    }

    public async Task PreloadQuestionImage(Question questionToPreload)
    {
        if (!questionToPreload.isImageQuestion || string.IsNullOrEmpty(questionToPreload.questionImagePath))
        {
            preloadedQuestionImage = null;
            return;
        }

        if (isPreloading) return;
        isPreloading = true;

        try
        {
            string key = QuestionStorageKeys.Resolve(questionToPreload.questionImagePath, questionToPreload.topic);
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning($"[QuestionUIManager] Preload: storageKey não resolvido para '{questionToPreload.globalId}'.");
                preloadedQuestionImage = null;
                return;
            }

            if (AppContext.ImageSync == null)
            {
                Debug.LogError("[QuestionUIManager] AppContext.ImageSync indisponível no preload.");
                preloadedQuestionImage = null;
                return;
            }

            Texture2D texture = await AppContext.ImageSync.GetImageAsync(key);
            if (texture == null)
            {
                Debug.LogWarning($"[QuestionUIManager] Preload: imagem '{key}' não disponível.");
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
