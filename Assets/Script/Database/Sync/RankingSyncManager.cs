using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class RankingSyncManager : MonoBehaviour
{
    private static RankingSyncManager _instance;
    public static RankingSyncManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("RankingSyncManager");
                _instance = go.AddComponent<RankingSyncManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private LocalRankingRepository _localRepo;
    private LocalSyncMetadataRepository _syncMetadataRepo;
    private IRankingRepository _remoteRepo;
    
    private bool _isSyncing = false;
    private const string RANKINGS_ENTITY_TYPE = "Rankings";
    private TimeSpan _cacheValidityDuration = TimeSpan.FromMinutes(5);

    public event Action OnSyncStarted;
    public event Action<bool> OnSyncCompleted;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        Initialize();
    }

    private void Initialize()
    {
        _localRepo = new LocalRankingRepository();
        _syncMetadataRepo = new LocalSyncMetadataRepository();
        
        if (BioBlocksSettings.Instance.IsDebugMode())
        {
            _remoteRepo = new MockRankingRepository();
        }
        else
        {
            _remoteRepo = new RankingRepository();
        }
    }

    public async Task<List<Ranking>> GetRankingsWithCache()
    {
        var cachedRankings = _localRepo.GetAllRankings();
        
        if (cachedRankings.Count > 0)
        {
            Debug.Log($"[RankingSyncManager] Returning {cachedRankings.Count} cached rankings");
            
            _ = TrySyncInBackground();
            
            return cachedRankings.Select(e => RankingDTO.ToRanking(e)).ToList();
        }
        else
        {
            Debug.Log("[RankingSyncManager] No cache available, forcing sync");
            bool success = await SyncRankings();
            
            if (success)
            {
                cachedRankings = _localRepo.GetAllRankings();
                return cachedRankings.Select(e => RankingDTO.ToRanking(e)).ToList();
            }
            else
            {
                return new List<Ranking>();
            }
        }
    }

    private async Task TrySyncInBackground()
    {
        if (!ConnectivityMonitor.Instance.IsOnline)
        {
            Debug.Log("[RankingSyncManager] Offline - skipping background sync");
            return;
        }

        if (_syncMetadataRepo.ShouldSync(RANKINGS_ENTITY_TYPE, _cacheValidityDuration))
        {
            Debug.Log("[RankingSyncManager] Cache expired, syncing in background");
            await SyncRankings();
        }
        else
        {
            Debug.Log("[RankingSyncManager] Cache still valid");
        }
    }

    public async Task<bool> SyncRankings()
    {
        if (_isSyncing)
        {
            Debug.Log("[RankingSyncManager] Sync already in progress");
            return false;
        }

        if (!ConnectivityMonitor.Instance.IsOnline)
        {
            Debug.Log("[RankingSyncManager] Cannot sync - device is offline");
            return false;
        }

        _isSyncing = true;
        OnSyncStarted?.Invoke();

        try
        {
            Debug.Log("[RankingSyncManager] Starting sync...");
            
            var remoteRankings = await _remoteRepo.GetRankingsAsync();
            
            if (remoteRankings == null || remoteRankings.Count == 0)
            {
                Debug.LogWarning("[RankingSyncManager] No rankings received from remote");
                _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, false, "No data received");
                return false;
            }

            var currentUser = await _remoteRepo.GetCurrentUserDataAsync();
            
            var entities = remoteRankings.Select(r => 
            {
                var userId = remoteRankings.IndexOf(r).ToString();
                
                if (currentUser != null && r.userName == currentUser.NickName)
                {
                    userId = currentUser.UserId;
                }
                
                return RankingDTO.ToEntity(r, userId);
            }).ToList();

            _localRepo.SaveRankings(entities);
            
            _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, true);
            
            Debug.Log($"[RankingSyncManager] Sync completed successfully - {entities.Count} rankings");
            
            OnSyncCompleted?.Invoke(true);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncManager] Sync failed: {e.Message}");
            _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, false, e.Message);
            
            OnSyncCompleted?.Invoke(false);
            return false;
        }
        finally
        {
            _isSyncing = false;
        }
    }

    public async Task<bool> ForceRefresh()
    {
        Debug.Log("[RankingSyncManager] Force refresh requested");
        return await SyncRankings();
    }

    public bool IsCacheValid()
    {
        return !_syncMetadataRepo.ShouldSync(RANKINGS_ENTITY_TYPE, _cacheValidityDuration);
    }

    public DateTime GetLastSyncTime()
    {
        return _syncMetadataRepo.GetLastSyncTime(RANKINGS_ENTITY_TYPE);
    }

    public int GetCachedRankingsCount()
    {
        return _localRepo.GetRankingsCount();
    }
}