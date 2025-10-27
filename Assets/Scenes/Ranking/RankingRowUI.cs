using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingRowUI : MonoBehaviour
{
    [Header("Text Components")]
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] public TMP_Text totalScoreText;
    [SerializeField] public TMP_Text weekScoreText;
    [SerializeField] private Image backgroundImage;

    [Header("Profile Image")]
    [SerializeField] private ProfileImageLoader imageLoader;

    private bool isExtraRow = false;

    private void Awake()
    {
        ValidateReferences();
    }

    private void ValidateReferences()
    {
        if (imageLoader == null)
        {
            Debug.LogError($"[RankingRowUI] ProfileImageLoader não está atribuído no prefab '{gameObject.name}'! " +
                          "Configure no Inspector antes de usar.");
        }
        
        if (rankText == null || nickNameText == null || weekScoreText == null)
        {
            Debug.LogError($"[RankingRowUI] Componentes de texto obrigatórios não estão atribuídos no prefab '{gameObject.name}'!");
        }
    }

    public void Setup(int rank, string userName, int score, string profileImageUrl, bool isCurrentUser)
    {
        Setup(rank, userName, score, 0, profileImageUrl, isCurrentUser);
    }

    public void Setup(int rank, string userName, int totalScore, int weekScore, string profileImageUrl, bool isCurrentUser)
    {
        if (imageLoader == null)
        {
            Debug.LogError($"[RankingRowUI] Não é possível configurar row para '{userName}' - ProfileImageLoader não encontrado!");
            return;
        }

        rankText.text = isExtraRow ? "..." : $"{rank}.";
        nickNameText.text = userName;

        if (totalScoreText != null)
            totalScoreText.gameObject.SetActive(true);
            totalScoreText.text = $"{totalScore}";

        if (weekScoreText != null)
            weekScoreText.gameObject.SetActive(true); 
            weekScoreText.text = $"{weekScore}";

        SetupColors(rank, isCurrentUser);
        Debug.Log($"[RankingRowUI] Carregando imagem para '{userName}': {profileImageUrl}");
        imageLoader.LoadProfileImage(profileImageUrl);
    }

    public void SetupAsExtraRow(int actualRank, string userName, int score, string profileImageUrl)
    {
        isExtraRow = true;
        Setup(actualRank, userName, score, 0, profileImageUrl, true);
    }

    public void SetupAsExtraRow(int actualRank, string userName, int totalScore, int weekScore, string profileImageUrl)
    {
        isExtraRow = true;
        Setup(actualRank, userName, totalScore, weekScore, profileImageUrl, true);
    }

    private void SetupColors(int rank, bool isCurrentUser)
    {
        Color textColor;
        Color backgroundColor;

        if (rank <= 3)
        {
            switch (rank)
            {
                case 1: // Primeiro lugar - Verde
                    ColorUtility.TryParseHtmlString("#004699", out textColor);
                    ColorUtility.TryParseHtmlString("#91FF7D", out backgroundColor);
                    break;
                case 2: // Segundo lugar - Azul
                    ColorUtility.TryParseHtmlString("#004699", out textColor);
                    ColorUtility.TryParseHtmlString("#7DF7FF", out backgroundColor);
                    break;
                case 3: // Terceiro lugar - Rosa
                    ColorUtility.TryParseHtmlString("#004699", out textColor);
                    ColorUtility.TryParseHtmlString("#F2D4EC", out backgroundColor);
                    break;
                default:
                    ColorUtility.TryParseHtmlString("#004699", out textColor);
                    ColorUtility.TryParseHtmlString("#ffffff", out backgroundColor);
                    break;
            }
        }
        else
        {
            if (isCurrentUser)
            {
                ColorUtility.TryParseHtmlString("#004699", out textColor);
                ColorUtility.TryParseHtmlString("#E5E5E5", out backgroundColor);
            }
            else
            {
                ColorUtility.TryParseHtmlString("#004699", out textColor);
                ColorUtility.TryParseHtmlString("#ffffff", out backgroundColor);
            }
        }

        if (rankText) rankText.color = textColor;
        if (nickNameText) nickNameText.color = textColor;
        if (totalScoreText) totalScoreText.color = textColor;
        if (weekScoreText) weekScoreText.color = textColor;
        if (backgroundImage) backgroundImage.color = backgroundColor;
    }
}