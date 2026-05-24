using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Orquestra ranking remoto + cache LiteDB.
///
/// Fonte verdade:
///   - Firestore, via IFirestoreRankingRepository.
///
/// Cache:
///   - LiteDB, via IRankingLocalRepository.
///   - Usado apenas como fallback offline/local mirror.
/// </summary>
public class RankingSyncService : MonoBehaviour, IRankingSyncService
{
    [SerializeField] private float cacheValidMinutes = 5f;
    [SerializeField] private int defaultLimit = 20;

    private IFirestoreRankingRepository _remoteRepo;
    private IRankingLocalRepository     _localRepo;
    private ConnectivityMonitor         _connectivity;

    private Task<bool> _runningSyncTask;
    private readonly object _syncLock = new object();

    public bool IsSyncing { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Injeção principal usada pelo AppContext.
    /// O AppContext é responsável por construir FirestoreRankingRepository e RankingLocalRepository.
    /// </summary>
    public void InjectDependencies(
        IFirestoreRankingRepository remoteRepo,
        IRankingLocalRepository localRepo,
        ConnectivityMonitor connectivity = null)
    {
        _remoteRepo   = remoteRepo   ?? throw new ArgumentNullException(nameof(remoteRepo));
        _localRepo    = localRepo    ?? throw new ArgumentNullException(nameof(localRepo));
        _connectivity = connectivity;
    }

    // -------------------------------------------------------------------------
    // API pública
    // -------------------------------------------------------------------------

    /// <summary>
    /// Retorna imediatamente o ranking salvo no LiteDB.
    /// Não acessa o Firestore.
    /// Útil para popular a tela sem flash enquanto o refresh remoto acontece.
    /// </summary>
    public List<Ranking> GetCachedRankings(int limit = 20)
    {
        int safeLimit = NormalizeLimit(limit);

        if (!EnsureInitialized())
        {
            Debug.LogWarning("[RankingSyncService] Não inicializado — cache de ranking indisponível.");
            return new List<Ranking>();
        }

        return _localRepo.GetRankings(safeLimit);
    }

    /// <summary>
    /// Retorna o ranking geral com fallback offline.
    ///
    /// Online:
    ///   - se o cache local estiver válido, retorna LiteDB e atualiza em background;
    ///   - se o cache estiver vazio ou stale, busca do Firestore e atualiza LiteDB.
    ///
    /// Offline:
    ///   - retorna LiteDB.
    /// </summary>
    public async Task<List<Ranking>> GetRankingsWithFallback(int limit = 20)
    {
        int safeLimit = NormalizeLimit(limit);

        if (!EnsureInitialized())
        {
            Debug.LogWarning("[RankingSyncService] Não inicializado — retornando lista vazia.");
            return new List<Ranking>();
        }

        var cached = _localRepo.GetRankings(safeLimit);

        if (!IsOnline())
        {
            Debug.Log("[RankingSyncService] Offline — usando ranking do LiteDB.");
            return cached;
        }

        if (cached.Count > 0 && !IsCacheStale())
        {
            Debug.Log("[RankingSyncService] Cache de ranking válido — usando LiteDB e atualizando em background.");
            StartBackgroundSync(safeLimit);
            return cached;
        }

        Debug.Log("[RankingSyncService] Cache de ranking vazio/stale — sincronizando com Firestore.");

        bool synced = await SyncFromFirestoreAsync(safeLimit);

        return synced
            ? _localRepo.GetRankings(safeLimit)
            : cached;
    }

    /// <summary>
    /// Força atualização do ranking geral a partir do Firestore.
    /// Usado pelo botão de refresh da RankingScene.
    /// </summary>
    public async Task<bool> ForceRefresh(int limit = 20)
    {
        int safeLimit = NormalizeLimit(limit);

        if (!EnsureInitialized())
            return false;

        if (!IsOnline())
        {
            Debug.LogWarning("[RankingSyncService] Sem internet — refresh cancelado.");
            return false;
        }

        return await SyncFromFirestoreAsync(safeLimit);
    }

    /// <summary>
    /// Retorna o timestamp do último sync remoto bem-sucedido.
    /// </summary>
    public DateTime GetLastSyncedAt()
    {
        if (!EnsureInitialized())
            return DateTime.MinValue;

        return _localRepo.GetLastSyncedAt();
    }

    // -------------------------------------------------------------------------
    // Inicialização
    // -------------------------------------------------------------------------

    /// <summary>
    /// Garante que as dependências foram injetadas.
    ///
    /// Fluxo principal:
    ///   - AppContext chama InjectDependencies().
    ///
    /// Fallback:
    ///   - se algum código chamar este serviço antes da injeção explícita,
    ///     tentamos recuperar as dependências já expostas pelo AppContext.
    /// </summary>
    private bool EnsureInitialized()
    {
        if (_remoteRepo != null && _localRepo != null)
            return true;

        if (!AppContext.IsReady)
            return false;

        if (AppContext.RankingRemote == null)
        {
            Debug.LogWarning("[RankingSyncService] AppContext.RankingRemote está nulo.");
            return false;
        }

        if (AppContext.RankingLocal == null)
        {
            Debug.LogWarning("[RankingSyncService] AppContext.RankingLocal está nulo.");
            return false;
        }

        _remoteRepo   = AppContext.RankingRemote;
        _localRepo    = AppContext.RankingLocal;
        _connectivity = AppContext.Connectivity;

        Debug.Log("[RankingSyncService] Inicializado via AppContext.");
        return true;
    }

    // -------------------------------------------------------------------------
    // Sincronização
    // -------------------------------------------------------------------------

    /// <summary>
    /// Evita múltiplas sincronizações simultâneas.
    /// Se já houver uma sincronização em andamento, retorna a mesma Task.
    /// </summary>
    private Task<bool> SyncFromFirestoreAsync(int limit)
    {
        lock (_syncLock)
        {
            if (_runningSyncTask != null && !_runningSyncTask.IsCompleted)
                return _runningSyncTask;

            _runningSyncTask = SyncFromFirestoreCoreAsync(limit);
            return _runningSyncTask;
        }
    }

    private async Task<bool> SyncFromFirestoreCoreAsync(int limit)
    {
        IsSyncing = true;

        try
        {
            var rankings = await _remoteRepo.GetRankingsAsync(limit);

            if (rankings == null || rankings.Count == 0)
            {
                Debug.LogWarning("[RankingSyncService] Firestore retornou ranking vazio.");
                return false;
            }

            _localRepo.ReplaceRankings(rankings);
            _localRepo.SaveLastSyncedAt(DateTime.UtcNow);

            Debug.Log($"[RankingSyncService] Cache atualizado com {rankings.Count} registros de ranking.");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingSyncService] Sync falhou: {e.Message}");
            return false;
        }
        finally
        {
            IsSyncing = false;
        }
    }

    private void StartBackgroundSync(int limit)
    {
        _ = SyncInBackgroundAsync(limit);
    }

    private async Task SyncInBackgroundAsync(int limit)
    {
        try
        {
            await SyncFromFirestoreAsync(limit);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[RankingSyncService] Background sync falhou: {e.Message}");
        }
    }

    // -------------------------------------------------------------------------
    // Utilitários
    // -------------------------------------------------------------------------

    private bool IsCacheStale()
    {
        DateTime lastSync = _localRepo.GetLastSyncedAt();

        if (lastSync == DateTime.MinValue)
            return true;

        return (DateTime.UtcNow - lastSync.ToUniversalTime()).TotalMinutes > cacheValidMinutes;
    }

    private bool IsOnline()
    {
        // Se não houver ConnectivityMonitor, tentamos a consulta remota.
        // Tratar null como offline poderia impedir atualização por falha de injeção.
        return _connectivity == null || _connectivity.IsOnline;
    }

    private int NormalizeLimit(int limit)
    {
        if (limit > 0)
            return limit;

        return defaultLimit > 0 ? defaultLimit : 20;
    }
}
