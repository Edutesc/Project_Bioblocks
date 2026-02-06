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
        try
        {
            _localRepo = new LocalRankingRepository();
            _syncMetadataRepo = new LocalSyncMetadataRepository();  

            var settings = BioBlocksSettings.Instance;
            bool debugMode = settings != null && settings.IsDebugMode();

            _remoteRepo = debugMode ? new MockRankingRepository() : new RankingRepository();
        }
        catch (Exception e)
        {   
          Debug.LogException(e);
        }          
    }

    public async Task<List<Ranking>> GetRankingsWithCache()
    {
        if (_localRepo == null || _syncMetadataRepo == null || _remoteRepo == null)
        Initialize();

        if (_localRepo == null)
        return new List<Ranking>();

        var cachedRankings = _localRepo.GetAllRankings() ?? new List<RankingEntity>(); // ajuste o tipo se prec
        
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
                Debug.LogWarning("[RankingSyncManager] Sync failed, falling back to direct Firebase fetch");
                
                try
                {
                    var firebaseRankings = await _remoteRepo.GetRankingsAsync();
                    
                    if (firebaseRankings != null && firebaseRankings.Count > 0)
                    {
                        Debug.Log($"[RankingSyncManager] Fallback successful: {firebaseRankings.Count} rankings from Firebase");
                        return firebaseRankings;
                    }
                    else
                    {
                        Debug.LogError("[RankingSyncManager] Fallback also failed - no data available");
                        return new List<Ranking>();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[RankingSyncManager] Fallback fetch failed: {e.Message}");
                    return new List<Ranking>();
                }
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

        Debug.Log($"[RankingSyncManager] Connectivity check: {ConnectivityMonitor.Instance.IsOnline}");

        if (!ConnectivityMonitor.Instance.IsOnline)
        {
            Debug.LogWarning("[RankingSyncManager] Cannot sync - device is offline");
            return false;
        }

        _isSyncing = true;
        OnSyncStarted?.Invoke();

        try
        {
            Debug.Log("[RankingSyncManager] Starting sync...");
            Debug.Log("[RankingSyncManager] Calling GetRankingsAsync...");
            
            var remoteRankings = await _remoteRepo.GetRankingsAsync();
            
            Debug.Log($"[RankingSyncManager] Remote rankings received: {remoteRankings?.Count ?? 0}");
            
            if (remoteRankings == null || remoteRankings.Count == 0)
            {
                Debug.LogWarning("[RankingSyncManager] No rankings received from remote");
                _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, false, "No data received");
                return false;
            }

            Debug.Log("[RankingSyncManager] Getting current user data...");
            var currentUser = await _remoteRepo.GetCurrentUserDataAsync();
            Debug.Log($"[RankingSyncManager] Current user: {currentUser?.NickName ?? "null"}");
            
            var entities = remoteRankings.Select(r => 
            {
                var userId = remoteRankings.IndexOf(r).ToString();
                
                if (currentUser != null && r.userName == currentUser.NickName)
                {
                    userId = currentUser.UserId;
                }
                
                return RankingDTO.ToEntity(r, userId);
            }).ToList();

            Debug.Log($"[RankingSyncManager] Saving {entities.Count} entities to local DB...");
            _localRepo.SaveRankings(entities);
            
            _syncMetadataRepo.UpdateSyncMetadata(RANKINGS_ENTITY_TYPE, true);
            
            Debug.Log($"[RankingSyncManager] Sync completed successfully - {entities.Count} rankings");
            
            OnSyncCompleted?.Invoke(true);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncManager] Sync failed: {e.Message}");
            Debug.LogError($"[RankingSyncManager] Stack trace: {e.StackTrace}");
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