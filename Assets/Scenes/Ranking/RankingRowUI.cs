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
    
    [Header("Background Images")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image rankBadgeImage;

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

        if (rankBadgeImage == null)
        {
            Debug.LogWarning($"[RankingRowUI] rankBadgeImage não está atribuído no prefab '{gameObject.name}'!");
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

        rankText.text = isExtraRow ? "..." : $"{rank}";
        nickNameText.text = userName;

        if (totalScoreText != null)
        {
            totalScoreText.gameObject.SetActive(true);
            totalScoreText.text = $"{totalScore}";
        }

        if (weekScoreText != null)
        {
            weekScoreText.gameObject.SetActive(true);
            weekScoreText.text = $"{weekScore}";
        }

        SetupRankBadge(rank);
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

    private void SetupRankBadge(int rank)
    {
        if (rankBadgeImage == null) return;

        if (rank <= 3)
        {
            rankBadgeImage.gameObject.SetActive(true);
            
            Color badgeColor;
            
            switch (rank)
            {
                case 1:
                    ColorUtility.TryParseHtmlString("#ece057ff", out badgeColor);
                    break;
                case 2:
                    ColorUtility.TryParseHtmlString("#98a1a2ff", out badgeColor);
                    break;
                case 3:
                    ColorUtility.TryParseHtmlString("#252325ff", out badgeColor);
                    break;
                default:
                    badgeColor = Color.white;
                    break;
            }
            
            rankBadgeImage.color = badgeColor;
        }
        else
        {
            rankBadgeImage.gameObject.SetActive(false);
        }
    }
}