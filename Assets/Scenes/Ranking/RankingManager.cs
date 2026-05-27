using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject    rankingRowPrefab;
    [SerializeField] protected RectTransform rankingTableContent;
    [SerializeField] protected ScrollRect    scrollRect;

    [Header("Ranking")]
    [SerializeField] private int rankingLimit = 20;

    [Header("Week Reset Information")]
    [SerializeField] private TMP_Text weekResetCountdownText;
    private WeekResetCountdown _resetCountdown;

    [Header("Loading Status")]
    [SerializeField] private GameObject loadingIndicator;
    [SerializeField] private TMP_Text   lastUpdateText;

    // ─── Estado interno ───────────────────────────────────────
    protected IRankingSyncService _rankingSyncService;
    protected List<Ranking>       _rankings = new List<Ranking>();

    private INavigationService _navigation;
    private DateTime           _lastFetchTime = DateTime.MinValue;
    private bool               _isFetching    = false;

    // ─────────────────────────────────────────────────────────
    // Unity lifecycle
    // ─────────────────────────────────────────────────────────
    protected virtual void Start()
    {
        _navigation = AppContext.Navigation;

        if (rankingRowPrefab == null || rankingTableContent == null || scrollRect == null)
        {
            Debug.LogError("[RankingManager] Referências obrigatórias não configuradas!");
            return;
        }

        InitializeRepository();
        InitializeWeekResetCountdown();
    }

    // ─────────────────────────────────────────────────────────
    // Inicialização
    // ─────────────────────────────────────────────────────────

    private void InitializeWeekResetCountdown()
    {
        if (weekResetCountdownText != null)
        {
            _resetCountdown = gameObject.AddComponent<WeekResetCountdown>();
            _resetCountdown.Initialize(weekResetCountdownText);
        }
    }

    protected virtual void InitializeRepository()
    {
        _rankingSyncService = AppContext.RankingSync;

        if (_rankingSyncService == null)
        {
            Debug.LogWarning("[RankingManager] AppContext.RankingSync está nulo. Ranking usará fallback remoto direto se possível.");
        }
        else
        {
            // Popula a tabela imediatamente com cache LiteDB.
            // Não acessa Firestore aqui.
            _rankings = _rankingSyncService.GetCachedRankings(rankingLimit);

            if (_rankings != null && _rankings.Count > 0)
            {
                _lastFetchTime = _rankingSyncService.GetLastSyncedAt();
                UpdateRankingTable();
                UpdateLastFetchLabel();
            }
        }

        _ = InitializeAsync();
    }

    protected virtual async Task InitializeAsync()
    {
        try
        {
            await FetchRankings();
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingManager] Falha na inicialização: {e.Message}");
        }
        finally
        {
            ShowLoadingIndicator(false);
        }
    }

    // ─────────────────────────────────────────────────────────
    // Fetch
    // ─────────────────────────────────────────────────────────

    public virtual async Task FetchRankings()
    {
        if (_isFetching)
        {
            Debug.LogWarning("[RankingManager] Busca de rankings já em andamento.");
            return;
        }

        try
        {
            _isFetching = true;

            if (_rankings == null || _rankings.Count == 0)
                ShowLoadingIndicator(true);

            List<Ranking> result;

            if (_rankingSyncService != null)
            {
                result = await _rankingSyncService.GetRankingsWithFallback(rankingLimit);
                _lastFetchTime = _rankingSyncService.GetLastSyncedAt();
            }
            else
            {
                Debug.LogWarning("[RankingManager] RankingSyncService indisponível — retornando lista vazia.");
                result = new List<Ranking>();
            }

            if (result != null && result.Count > 0)
            {
                _rankings = result;
                UpdateRankingTable();
            }
            else
            {
                Debug.LogWarning("[RankingManager] Ranking retornou vazio.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingManager] Erro ao buscar rankings: {e.Message}\n{e.StackTrace}");
            _rankings = new List<Ranking>();
        }
        finally
        {
            _isFetching = false;
            ShowLoadingIndicator(false);
            UpdateLastFetchLabel();
        }
    }

    // ─────────────────────────────────────────────────────────
    // UI
    // ─────────────────────────────────────────────────────────

    protected virtual void UpdateRankingTable()
    {
        if (rankingTableContent == null)
            return;

        foreach (Transform child in rankingTableContent)
            Destroy(child.gameObject);

        var top = (_rankings ?? new List<Ranking>())
            .Take(rankingLimit)
            .ToList();

        for (int i = 0; i < top.Count; i++)
        {
            CreateRankingRow(i + 1, top[i], false);
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rankingTableContent);
    }

    protected virtual void CreateRankingRow(int rank, Ranking ranking, bool isCurrentUser)
    {
        GameObject rowGO = Instantiate(rankingRowPrefab, rankingTableContent);
        var rowUI = rowGO.GetComponent<RankingRowUI>();

        if (rowUI != null)
        {
            rowUI.Setup(
                rank,
                ranking.userName,
                ranking.userScore,
                ranking.userWeekScore,
                ranking.profileImageUrl
            );
        }
        else
        {
            Debug.LogError("[RankingManager] RankingRowUI não encontrado no prefab!");
        }
    }

    private void ShowLoadingIndicator(bool show)
    {
        if (loadingIndicator != null)
            loadingIndicator.SetActive(show);
    }

    private void UpdateLastFetchLabel()
    {
        if (lastUpdateText == null)
            return;

        lastUpdateText.text = _lastFetchTime == DateTime.MinValue
            ? "Nunca atualizado"
            : FormatElapsedTime(DateTime.UtcNow - _lastFetchTime.ToUniversalTime());
    }

    private string FormatElapsedTime(TimeSpan elapsed)
    {
        if (elapsed.TotalMinutes < 1)  return "Atualizado agora";
        if (elapsed.TotalMinutes < 60) return $"Atualizado há {(int)elapsed.TotalMinutes} min";
        if (elapsed.TotalHours   < 24) return $"Atualizado há {(int)elapsed.TotalHours}h";
        return                                $"Atualizado há {(int)elapsed.TotalDays}d";
    }

    // ─────────────────────────────────────────────────────────
    // Botões / navegação
    // ─────────────────────────────────────────────────────────

    public async void OnRefreshButtonClicked()
    {
        if (_isFetching)
        {
            Debug.LogWarning("[RankingManager] Refresh ignorado — busca já em andamento.");
            return;
        }

        try
        {
            ShowLoadingIndicator(true);

            if (_rankingSyncService == null)
            {
                Debug.LogWarning("[RankingManager] RankingSyncService indisponível — refresh cancelado.");
                return;
            }

            bool refreshed = await _rankingSyncService.ForceRefresh(rankingLimit);

            if (!refreshed)
            {
                Debug.LogWarning("[RankingManager] Refresh remoto falhou — mantendo ranking atual/cache.");
            }

            _rankings = _rankingSyncService.GetCachedRankings(rankingLimit);
            _lastFetchTime = _rankingSyncService.GetLastSyncedAt();

            if (_rankings != null && _rankings.Count > 0)
                UpdateRankingTable();

            UpdateLastFetchLabel();
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingManager] Refresh falhou: {e.Message}");
        }
        finally
        {
            ShowLoadingIndicator(false);
        }
    }

    public virtual void Navigate(string sceneName)
    {
        _navigation.NavigateTo(sceneName);
    }
}
