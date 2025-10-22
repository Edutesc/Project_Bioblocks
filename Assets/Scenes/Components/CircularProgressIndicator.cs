using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CircularProgressIndicator : MonoBehaviour
{
    [Header("Configurações de Visual")]
    [SerializeField] private Image backgroundRing; // circle_mask (anel branco de fundo)
    [SerializeField] private Image fillImage; // circle_fill (anel colorido que preenche)
    [SerializeField] private TMP_Text percentageText;

    [Header("Animação")]
    [SerializeField] private float fillAnimationDuration = 0.5f;

    [Header("Detecção Automática")]
    [SerializeField] private string databaseNameSuffix = "PorcentageText";
    [SerializeField] private bool autoSetupImages = true;

    private float targetFillAmount = 0f;
    private float currentFillAmount = 0f;
    private float fillVelocity = 0f;
    private string databaseName;

    private void Awake()
    {
        // Auto-detectar o banco de dados pelo nome do texto
        if (percentageText == null)
        {
            percentageText = GetComponentInChildren<TMP_Text>();
        }

        if (percentageText != null)
        {
            string objName = percentageText.gameObject.name;
            if (objName.EndsWith(databaseNameSuffix))
            {
                databaseName = objName.Substring(0, objName.Length - databaseNameSuffix.Length);
                Debug.Log($"CircularProgressIndicator detectou banco: {databaseName}");
            }
        }

        // Tentar encontrar as imagens automaticamente se não foram atribuídas
        if (autoSetupImages)
        {
            AutoSetupImages();
        }

        // Configurar a imagem de preenchimento
        SetupFillImage();
    }

    private void AutoSetupImages()
    {
        // Buscar todas as imagens filhas
        Image[] images = GetComponentsInChildren<Image>(true);

        foreach (Image img in images)
        {
            // Identificar pelo nome do sprite ou do GameObject
            if (img.sprite != null)
            {
                string spriteName = img.sprite.name.ToLower();

                if (spriteName.Contains("mask") && backgroundRing == null)
                {
                    backgroundRing = img;
                    Debug.Log($"Auto-detectado background ring: {img.name}");
                }
                else if (spriteName.Contains("fill") && fillImage == null)
                {
                    fillImage = img;
                    Debug.Log($"Auto-detectado fill image: {img.name}");
                }
            }
        }
    }

    private void SetupFillImage()
    {
        if (fillImage == null)
        {
            Debug.LogError($"Fill Image não foi atribuída para {gameObject.name}!");
            return;
        }

        // Configurar como preenchimento radial
        fillImage.type = Image.Type.Filled;
        fillImage.fillMethod = Image.FillMethod.Radial360;
        fillImage.fillOrigin = (int)Image.Origin360.Top;
        fillImage.fillClockwise = true;
        fillImage.fillAmount = 0f;
        fillImage.enabled = true;

        // Remover Mask se existir
        Mask mask = fillImage.GetComponent<Mask>();
        if (mask != null)
        {
            Debug.LogWarning($"Removendo Mask de {fillImage.name} - não é necessário!");
            DestroyImmediate(mask);
        }

        // Configurar o background ring
        if (backgroundRing != null)
        {
            backgroundRing.type = Image.Type.Simple;
            backgroundRing.enabled = true;

            // Remover Mask do background
            Mask bgMask = backgroundRing.GetComponent<Mask>();
            if (bgMask != null)
            {
                Debug.LogWarning($"Removendo Mask de {backgroundRing.name}");
                DestroyImmediate(bgMask);
            }

            // Garantir que o fillImage renderiza por cima
            fillImage.transform.SetAsLastSibling();
        }
    }

    private void Start()
    {
        AnsweredQuestionsManager.OnAnsweredQuestionsUpdated += HandleAnsweredQuestionsUpdated;

        // Inicializar com 0%
        UpdateVisuals(0f);
    }

    private void OnDestroy()
    {
        AnsweredQuestionsManager.OnAnsweredQuestionsUpdated -= HandleAnsweredQuestionsUpdated;
    }

    private void Update()
    {
        // Animação suave do preenchimento
        if (Mathf.Abs(currentFillAmount - targetFillAmount) > 0.001f)
        {
            currentFillAmount = Mathf.SmoothDamp(
                currentFillAmount,
                targetFillAmount,
                ref fillVelocity,
                fillAnimationDuration);

            if (Mathf.Abs(currentFillAmount - targetFillAmount) < 0.01f)
            {
                currentFillAmount = targetFillAmount;
            }

            UpdateVisuals(currentFillAmount);
        }
    }

    private void HandleAnsweredQuestionsUpdated(Dictionary<string, int> answeredCounts)
    {
        if (string.IsNullOrEmpty(databaseName) || !answeredCounts.ContainsKey(databaseName))
        {
            return;
        }

        int count = answeredCounts[databaseName];
        int totalQuestions = QuestionBankStatistics.GetTotalQuestions(databaseName);

        if (totalQuestions <= 0) totalQuestions = 50;

        int percentage = totalQuestions > 0 ? (count * 100) / totalQuestions : 0;
        percentage = Mathf.Min(percentage, 100);

        if (percentageText != null)
        {
            percentageText.text = $"{percentage}%";
        }

        targetFillAmount = percentage / 100f;

        Debug.Log($"CircularProgress {databaseName}: {percentage}% ({count}/{totalQuestions}) - FillAmount: {targetFillAmount}");
    }

    public void SetProgress(int percentage)
    {
        percentage = Mathf.Clamp(percentage, 0, 100);

        if (percentageText != null)
        {
            percentageText.text = $"{percentage}%";
        }

        targetFillAmount = percentage / 100f;

        Debug.Log($"SetProgress: {percentage}% - FillAmount: {targetFillAmount}");
    }

    private void UpdateVisuals(float fillAmount)
    {
        if (fillImage == null) return;

        // Simplesmente atualizar o preenchimento radial
        // A cor permanece a original do sprite (preserva gradiente e efeito 3D)
        fillImage.fillAmount = fillAmount;
    }

    // Método para debug no editor
    private void OnValidate()
    {
        if (fillImage != null && Application.isPlaying == false)
        {
            fillImage.type = Image.Type.Filled;
            fillImage.fillMethod = Image.FillMethod.Radial360;
            fillImage.fillOrigin = (int)Image.Origin360.Top;
            fillImage.fillClockwise = true;
        }

        if (backgroundRing != null && Application.isPlaying == false)
        {
            backgroundRing.type = Image.Type.Simple;
        }
    }
}