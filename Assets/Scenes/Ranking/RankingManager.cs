using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject rankingRowPrefab;
    [SerializeField] protected RectTransform rankingTableContent;
    [SerializeField] protected ScrollRect scrollRect;

    [Header("Week Reset Information")]
    [SerializeField] private TMP_Text weekResetCountdownText;
    private WeekResetCountdown resetCountdown;

    [Header("Sync Status")]
    [SerializeField] private GameObject syncIndicator;
    [SerializeField] private TMP_Text lastUpdateText;

    protected UserData currentUserData;
    protected List<Ranking> rankings;
    protected IRankingRepository rankingRepository;

    private bool isLoadingFromCache = false;

    protected virtual void Start()
    {
        if (rankingRowPrefab == null || rankingTableContent == null || scrollRect == null)
        {
            Debug.LogError("RankingManager: Referências obrigatórias não configuradas!");
            return;
        }

        InitializeRepository();
        InitializeWeekResetCountdown();
        SubscribeToSyncEvents();
    }

    private void InitializeWeekResetCountdown()
    {
        if (weekResetCountdownText != null)
        {
            resetCountdown = gameObject.AddComponent<WeekResetCountdown>();
            resetCountdown.Initialize(weekResetCountdownText);
        }
    }

    private void SubscribeToSyncEvents()
    {
        RankingSyncManager.Instance.OnSyncStarted += OnSyncStarted;
        RankingSyncManager.Instance.OnSyncCompleted += OnSyncCompleted;
    }

    protected virtual void InitializeRepository()
    {
        if (BioBlocksSettings.Instance.IsDebugMode())
        {
            Debug.Log("BioBlocks: Usando dados mock para desenvolvimento");
            rankingRepository = new MockRankingRepository();
            _ = InitializeRankingManager();
            return;
        }

        if (!FirestoreRepository.Instance.IsInitialized)
        {
            Debug.LogError("FirestoreRepository não está inicializado");
            return;
        }

        rankingRepository = new RankingRepository();
        _ = InitializeRankingManager();
    }

    protected virtual async Task InitializeRankingManager()
    {
        currentUserData = await rankingRepository.GetCurrentUserDataAsync();
        if (currentUserData != null)
        {
            UserDataStore.OnUserDataChanged += OnUserDataChanged;
            await FetchRankings();
        }
        else
        {
            Debug.LogError("User data not loaded. Redirecting to Login.");
        }
    }

    protected virtual void OnDestroy()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
        RankingSyncManager.Instance.OnSyncStarted -= OnSyncStarted;
        RankingSyncManager.Instance.OnSyncCompleted -= OnSyncCompleted;
    }

    protected virtual void OnUserDataChanged(UserData userData)
    {
        currentUserData = userData;
        _ = FetchRankings();
    }

    public virtual async Task FetchRankings()
    {
        try
        {
            ShowSyncIndicator(true);
            
            isLoadingFromCache = true;
            rankings = await RankingSyncManager.Instance.GetRankingsWithCache();
            isLoadingFromCache = false;

            Debug.Log($"Total de rankings obtidos: {rankings.Count}");

            if (rankings.Count > 0)
            {
                rankings = rankings
                    .OrderByDescending(r => r.userWeekScore)
                    .ThenByDescending(r => r.userScore)
                    .ToList();

                Debug.Log("Rankings ordenados por WeekScore com desempate pelo TotalScore");

                UpdateRankingTable();
                UpdateLastSyncTime();
            }
            else
            {
                Debug.LogWarning("Nenhum ranking foi adicionado à lista!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao buscar rankings: {e.Message}");
            rankings = new List<Ranking>();
        }
        finally
        {
            ShowSyncIndicator(false);
        }
    }

    private void OnSyncStarted()
    {
        if (!isLoadingFromCache)
        {
            ShowSyncIndicator(true);
        }
    }

    private void OnSyncCompleted(bool success)
    {
        ShowSyncIndicator(false);
        
        if (success)
        {
            _ = RefreshRankingsFromCache();
        }
    }

    private async Task RefreshRankingsFromCache()
    {
        var localRepo = new LocalRankingRepository();
        var cachedRankings = localRepo.GetAllRankings();
        
        rankings = cachedRankings.Select(e => RankingDTO.ToRanking(e)).ToList();
        
        rankings = rankings
            .OrderByDescending(r => r.userWeekScore)
            .ThenByDescending(r => r.userScore)
            .ToList();
        
        UpdateRankingTable();
        UpdateLastSyncTime();
    }

    private void ShowSyncIndicator(bool show)
    {
        if (syncIndicator != null)
        {
            syncIndicator.SetActive(show);
        }
    }

    private void UpdateLastSyncTime()
    {
        if (lastUpdateText != null)
        {
            var lastSync = RankingSyncManager.Instance.GetLastSyncTime();
            
            if (lastSync == DateTime.MinValue)
            {
                lastUpdateText.text = "Nunca sincronizado";
            }
            else
            {
                var timeSince = DateTime.UtcNow - lastSync;
                
                if (timeSince.TotalMinutes < 1)
                {
                    lastUpdateText.text = "Atualizado agora";
                }
                else if (timeSince.TotalMinutes < 60)
                {
                    lastUpdateText.text = $"Atualizado há {(int)timeSince.TotalMinutes} min";
                }
                else if (timeSince.TotalHours < 24)
                {
                    lastUpdateText.text = $"Atualizado há {(int)timeSince.TotalHours}h";
                }
                else
                {
                    lastUpdateText.text = $"Atualizado há {(int)timeSince.TotalDays}d";
                }
            }
        }
    }

    protected virtual void UpdateRankingTable()
    {
        if (rankingTableContent == null)
        {
            Debug.LogError("rankingTableContent é null!");
            return;
        }

        Debug.Log($"Atualizando ranking table com {rankings.Count} rankings");
        
        foreach (Transform child in rankingTableContent)
        {
            Destroy(child.gameObject);
        }

        var top20Rankings = rankings.Take(20).ToList();

        for (int i = 0; i < top20Rankings.Count; i++)
        {
            var ranking = top20Rankings[i];
            bool isCurrentUser = ranking.userName == currentUserData.NickName;
            bool applyCurrentUserStyle = isCurrentUser && (i + 1) > 3;
            CreateRankingRow(i + 1, ranking, applyCurrentUserStyle);
        }

        if (!top20Rankings.Any(r => r.userName == currentUserData.NickName))
        {
            int currentUserRank = rankings.FindIndex(r => r.userName == currentUserData.NickName) + 1;
            var currentUserRanking = rankings.Find(r => r.userName == currentUserData.NickName);

            if (currentUserRanking != null && currentUserRank > 20)
            {
                GameObject separatorGO = Instantiate(rankingRowPrefab, rankingTableContent);
                var separatorUI = separatorGO.GetComponent<RankingRowUI>();
                if (separatorUI != null)
                {
                    separatorUI.SetupAsExtraRow(currentUserRank, currentUserRanking.userName,
                        currentUserRanking.userScore, currentUserRanking.userWeekScore,
                        currentUserRanking.profileImageUrl);
                }
            }
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rankingTableContent);

        if (scrollRect != null)
        {
            StartCoroutine(ScrollToCurrentUser());
        }
    }

    protected virtual IEnumerator ScrollToCurrentUser()
    {
        yield return new WaitForEndOfFrame();

        int currentUserRank = rankings.FindIndex(r => r.userName == currentUserData.NickName) + 1;
        if (currentUserRank > 15)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    protected virtual void CreateRankingRow(int rank, Ranking ranking, bool isCurrentUser)
    {
        GameObject rowGO = Instantiate(rankingRowPrefab, rankingTableContent);
        var rowUI = rowGO.GetComponent<RankingRowUI>();
        if (rowUI != null)
        {
            rowUI.Setup(rank, ranking.userName, ranking.userScore,
                        ranking.userWeekScore, ranking.profileImageUrl, isCurrentUser);
        }
        else
        {
            Debug.LogError("RankingRowUI component not found on prefab!");
        }
    }

    protected virtual void OnRankingRowClicked(Ranking ranking)
    {
        Debug.Log($"Clicked on ranking for user: {ranking.userName}");
    }

    public virtual void Navigate(string sceneName)
    {
        Debug.Log($"Navigating to {sceneName}");
        NavigationManager.Instance.NavigateTo(sceneName);
    }

    public async void OnRefreshButtonClicked()
    {
        await RankingSyncManager.Instance.ForceRefresh();
    }
}