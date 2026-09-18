using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class TopicReviewSyncService : MonoBehaviour, ITopicReviewSyncService
{
    [SerializeField] private float cacheValidMinutes = 5f;

    private ITopicReviewLocalRepository _localRepository;
    private ITopicReviewRepository _firestore;
    private IAuthGate _authGate;

    private readonly SemaphoreSlim _syncGate = new SemaphoreSlim(1, 1);

    public bool IsSyncing { get; private set; }

    public void InjectDependencies(
        ITopicReviewLocalRepository localRepository,
        ITopicReviewRepository firestore,
        IAuthGate authGate = null)
    {
        _localRepository = localRepository;
        _firestore       = firestore;
        _authGate        = authGate;
    }

    // ── Sincronização de um tópico específico ───────────────────────────────────

    public async Task SyncFromFirestore(string userId, string databankName)
    {
        if (!HasDependencies("SyncFromFirestore") || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName))
            return;

        await _syncGate.WaitAsync();
        IsSyncing = true;

        try
        {
            if (_authGate != null)
                await _authGate.WaitForAuthenticatedAsync();

            var topicReviewData = await _firestore.GetTopicReviewData(userId, databankName);

            if (topicReviewData == null)
            {
                Debug.LogWarning($"[TopicReviewSyncService] Tópico '{databankName}' do usuário {userId} não encontrado no Firestore.");
                return;
            }

            topicReviewData.updatedAt = topicReviewData.updatedAt != DateTime.MinValue
                ? topicReviewData.updatedAt.ToUniversalTime()
                : DateTime.UtcNow;

            _localRepository.UpdateTopicReview(topicReviewData);
            _localRepository.MarkAsSynced(userId, databankName);

            Debug.Log($"[TopicReviewSyncService] Tópico '{databankName}' sincronizado do Firestore para o cache local.");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[TopicReviewSyncService] Falha ao sincronizar tópico '{databankName}' do Firestore: {e.Message}");
            throw;
        }
        finally
        {
            IsSyncing = false;
            _syncGate.Release();
        }
    }

    public async Task SyncToFirestore(string userId, string databankName)
    {
        if (!HasDependencies("SyncToFirestore") || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName))
            return;

        await _syncGate.WaitAsync();
        IsSyncing = true;

        try
        {
            if (_authGate != null)
                await _authGate.WaitForAuthenticatedAsync();

            var topicReviewData = _localRepository.GetTopicReview(userId, databankName);
            if (topicReviewData == null)
            {
                Debug.LogWarning($"[TopicReviewSyncService] Tópico '{databankName}' do usuário {userId} não encontrado no cache local.");
                return;
            }

            await _firestore.UpdateTopicReviewData(topicReviewData);

            MarkAsSyncedIfNoNewerLocalChanges(userId, databankName, topicReviewData);

            Debug.Log($"[TopicReviewSyncService] Tópico '{databankName}' enviado ao Firestore com sucesso.");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[TopicReviewSyncService] Falha ao enviar tópico '{databankName}' ao Firestore: {e.Message}");
            _localRepository.MarkAsDirty(userId, databankName);
            throw;
        }
        finally
        {
            IsSyncing = false;
            _syncGate.Release();
        }
    }

    // ── Sincronização de todos os tópicos de um usuário ─────────────────────────

    /// <summary>
    /// Chamado no login / abertura do app. Varre todos os TopicReviews locais do
    /// usuário: os dirty são enviados, os stale são atualizados via Firestore.
    /// Não bloqueia o app em caso de falha — sempre cai de volta pro cache local.
    /// </summary>
    public async Task SyncPendingForUser(string userId)
    {
        if (!HasDependencies("SyncPendingForUser") || string.IsNullOrEmpty(userId))
            return;

        try
        {
            List<TopicReviewData> localTopics = _localRepository.GetAllTopicReviewsForUser(userId);

            if (localTopics == null || localTopics.Count == 0)
            {
                Debug.Log("[TopicReviewSyncService] Nenhum tópico local — nada a puxar por enquanto (tópicos surgem conforme o usuário estuda).");
                return;
            }

            foreach (var topic in localTopics)
            {
                string databankName = topic.databankName;

                try
                {
                    if (_localRepository.IsDirty(userId, databankName))
                    {
                        Debug.Log($"[TopicReviewSyncService] Tópico '{databankName}' com alterações pendentes — enviando...");
                        await SyncToFirestore(userId, databankName);
                    }
                    else if (IsCacheStale(userId, databankName))
                    {
                        Debug.Log($"[TopicReviewSyncService] Tópico '{databankName}' desatualizado — comparando com Firestore...");
                        await MergeTopicWithFirestore(userId, databankName);
                    }
                }
                catch (Exception e)
                {
                    // Uma falha em um tópico não deve interromper a sincronização dos demais.
                    Debug.LogWarning($"[TopicReviewSyncService] Falha ao sincronizar tópico '{databankName}': {e.Message}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[TopicReviewSyncService] SyncPendingForUser falhou — mantendo cache local: {e.Message}");
        }
    }

    /// <summary>
    /// Envia ao Firestore todos os tópicos marcados como dirty do usuário.
    /// Útil para chamar ao pausar/fechar o app (OnApplicationPause/Quit).
    /// </summary>
    public async Task PushPendingChanges(string userId)
    {
        if (!HasDependencies("PushPendingChanges") || string.IsNullOrEmpty(userId))
            return;

        List<TopicReviewData> dirtyTopics;

        try
        {
            dirtyTopics = _localRepository.GetDirtyTopicReviewsForUser(userId);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[TopicReviewSyncService] Falha ao listar tópicos pendentes: {e.Message}");
            return;
        }

        if (dirtyTopics == null || dirtyTopics.Count == 0)
            return;

        foreach (var topic in dirtyTopics)
        {
            try
            {
                await SyncToFirestore(userId, topic.databankName);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[TopicReviewSyncService] Falha ao enviar tópico '{topic.databankName}': {e.Message}");
            }
        }
    }

    // ── Merge local/remoto de um tópico ──────────────────────────────────────────

    private async Task MergeTopicWithFirestore(string userId, string databankName)
    {
        await _syncGate.WaitAsync();
        IsSyncing = true;

        try
        {
            var localData = _localRepository.GetTopicReview(userId, databankName);
            TopicReviewData remoteData = null;

            try
            {
                if (_authGate != null)
                    await _authGate.WaitForAuthenticatedAsync();

                remoteData = await _firestore.GetTopicReviewData(userId, databankName);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[TopicReviewSyncService] Firestore indisponível para tópico '{databankName}': {e.Message}");
            }

            if (remoteData == null)
            {
                Debug.Log($"[TopicReviewSyncService] Firestore indisponível — mantendo LiteDB para tópico '{databankName}'.");
                return;
            }

            if (localData == null)
            {
                remoteData.updatedAt = DateTime.UtcNow;
                _localRepository.UpdateTopicReview(remoteData);
                _localRepository.MarkAsSynced(userId, databankName);
                return;
            }

            DateTime localUpdatedAt = localData.updatedAt != DateTime.MinValue
                ? localData.updatedAt.ToUniversalTime()
                : DateTime.MinValue;

            DateTime remoteUpdatedAt = remoteData.updatedAt != DateTime.MinValue
                ? remoteData.updatedAt.ToUniversalTime()
                : DateTime.MinValue;

            if (localUpdatedAt >= remoteUpdatedAt)
            {
                if (localUpdatedAt > remoteUpdatedAt)
                {
                    _localRepository.MarkAsDirty(userId, databankName);
                    Debug.Log($"[TopicReviewSyncService] Cache local do tópico '{databankName}' é mais recente — marcado dirty para envio futuro.");
                }

                return;
            }

            remoteData.updatedAt = remoteUpdatedAt != DateTime.MinValue
                ? remoteUpdatedAt
                : DateTime.UtcNow;

            _localRepository.UpdateTopicReview(remoteData);
            _localRepository.MarkAsSynced(userId, databankName);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[TopicReviewSyncService] MergeTopicWithFirestore falhou para '{databankName}': {e.Message}");
        }
        finally
        {
            IsSyncing = false;
            _syncGate.Release();
        }
    }

    private bool IsCacheStale(string userId, string databankName)
    {
        var lastSync = _localRepository.GetLastSyncedAt(userId, databankName);
        if (lastSync == DateTime.MinValue) return true;

        return (DateTime.UtcNow - lastSync.ToUniversalTime()).TotalMinutes > cacheValidMinutes;
    }

    private void MarkAsSyncedIfNoNewerLocalChanges(string userId, string databankName, TopicReviewData capturedData)
    {
        if (capturedData == null)
        {
            Debug.LogWarning("[TopicReviewSyncService] Snapshot enviado ao Firestore é nulo — mantendo dirty como proteção.");
            _localRepository.MarkAsDirty(userId, databankName);
            return;
        }

        bool marked = _localRepository.MarkAsSyncedIfUpdatedAtMatches(userId, databankName, capturedData.updatedAt);

        if (!marked)
        {
            Debug.Log($"[TopicReviewSyncService] Há alterações locais mais recentes no tópico '{databankName}' — mantendo dirty para sync futuro.");
            _localRepository.MarkAsDirty(userId, databankName);
        }
    }

    private bool HasDependencies(string caller)
    {
        if (_localRepository != null && _firestore != null)
            return true;

        Debug.LogWarning($"[TopicReviewSyncService] {caller} chamado antes da injeção de dependências.");
        return false;
    }
}