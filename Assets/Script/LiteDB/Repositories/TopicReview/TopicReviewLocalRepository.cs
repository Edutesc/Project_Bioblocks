using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using UnityEngine;

public class TopicReviewLocalRepository : MonoBehaviour, ITopicReviewLocalRepository
{
    private const string TOPIC_REVIEWS_COLLECTION = "topicReviews";

    private ILiteDBManager _db;

    public void InjectDependencies(ILiteDBManager db)
    {
        _db = db;
    }

    // ── Leitura ────────────────────────────────────────────────────────────────

    public TopicReviewData GetTopicReview(string userId, string databankName)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName))
        {
            Debug.LogWarning("[TopicReviewLocalRepository] GetTopicReview chamado com userId ou databankName vazio.");
            return null;
        }

        EnsureInjected();

        try
        {
            return _db.ExecuteRead(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);
                var doc = collection.FindById(TopicReviewDB.BuildId(userId, databankName));
                return doc?.ToDomain();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro ao buscar tópico '{databankName}': {e.Message}");
            return null;
        }
    }

    public List<TopicReviewData> GetAllTopicReviewsForUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("[TopicReviewLocalRepository] GetAllTopicReviewsForUser chamado com userId vazio.");
            return new List<TopicReviewData>();
        }

        EnsureInjected();

        try
        {
            return _db.ExecuteRead(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);

                return collection
                    .Find(t => t.UserId == userId)
                    .Select(d => d.ToDomain())
                    .ToList();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro em GetAllTopicReviewsForUser: {e.Message}");
            return new List<TopicReviewData>();
        }
    }

    public List<TopicReviewData> GetDirtyTopicReviewsForUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("[TopicReviewLocalRepository] GetDirtyTopicReviewsForUser chamado com userId vazio.");
            return new List<TopicReviewData>();
        }

        EnsureInjected();

        try
        {
            return _db.ExecuteRead(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);

                return collection
                    .Find(t => t.UserId == userId && t.IsDirty)
                    .Select(d => d.ToDomain())
                    .ToList();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro em GetDirtyTopicReviewsForUser: {e.Message}");
            return new List<TopicReviewData>();
        }
    }

    public bool IsDirty(string userId, string databankName)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName))
            return false;

        EnsureInjected();

        try
        {
            return _db.ExecuteRead(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);
                var doc = collection.FindById(TopicReviewDB.BuildId(userId, databankName));
                return doc?.IsDirty ?? false;
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro ao verificar dirty do tópico '{databankName}': {e.Message}");
            return false;
        }
    }

    public DateTime GetLastSyncedAt(string userId, string databankName)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName))
            return DateTime.MinValue;

        EnsureInjected();

        try
        {
            return _db.ExecuteRead(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);
                var doc = collection.FindById(TopicReviewDB.BuildId(userId, databankName));

                if (doc == null || doc.LastSyncedAt == DateTime.MinValue)
                    return DateTime.MinValue;

                return DateTime.SpecifyKind(doc.LastSyncedAt, DateTimeKind.Utc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro ao buscar LastSyncedAt do tópico '{databankName}': {e.Message}");
            return DateTime.MinValue;
        }
    }

    // ── Escrita ────────────────────────────────────────────────────────────────

    public void UpdateTopicReview(TopicReviewData data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        EnsureInjected();

        try
        {
            _db.ExecuteWrite(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);

                string id = TopicReviewDB.BuildId(data.userId, data.databankName);
                var existing = collection.FindById(id);

                var doc = TopicReviewDB.FromDomain(data);

                if (existing != null)
                {
                    // Preserva metadados locais de sincronização.
                    doc.IsDirty      = existing.IsDirty;
                    doc.LastSyncedAt = existing.LastSyncedAt;
                }

                collection.Upsert(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro ao atualizar tópico '{data.databankName}': {e.Message}");
            throw;
        }
    }

    public void MarkAsDirty(string userId, string databankName)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName))
            return;

        EnsureInjected();

        try
        {
            _db.ExecuteWrite(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);
                var doc = collection.FindById(TopicReviewDB.BuildId(userId, databankName));
                if (doc == null) return;

                doc.IsDirty = true;
                collection.Update(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro ao marcar dirty o tópico '{databankName}': {e.Message}");
            throw;
        }
    }

    public void MarkAsSynced(string userId, string databankName)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName))
            return;

        EnsureInjected();

        try
        {
            _db.ExecuteWrite(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);
                var doc = collection.FindById(TopicReviewDB.BuildId(userId, databankName));
                if (doc == null) return;

                doc.IsDirty      = false;
                doc.LastSyncedAt = DateTime.UtcNow;

                collection.Update(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro ao marcar synced o tópico '{databankName}': {e.Message}");
            throw;
        }
    }

    /// <summary>
    /// Marca como sincronizado apenas se o registro local ainda corresponde ao snapshot
    /// que acabou de ser enviado ao Firestore (mesmo padrão de UserDataLocalRepository).
    /// </summary>
    public bool MarkAsSyncedIfUpdatedAtMatches(string userId, string databankName, DateTime expectedUpdatedAt)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName) || expectedUpdatedAt == DateTime.MinValue)
            return false;

        EnsureInjected();

        bool marked = false;

        try
        {
            _db.ExecuteWrite(db =>
            {
                var collection = db.GetCollection<TopicReviewDB>(TOPIC_REVIEWS_COLLECTION);
                var doc = collection.FindById(TopicReviewDB.BuildId(userId, databankName));
                if (doc == null) return;

                DateTime currentUpdatedAt  = DateTime.SpecifyKind(doc.UpdatedAt, DateTimeKind.Utc);
                DateTime expectedUtcTime   = expectedUpdatedAt.ToUniversalTime();

                if (currentUpdatedAt != expectedUtcTime)
                    return;

                doc.IsDirty      = false;
                doc.LastSyncedAt = DateTime.UtcNow;

                collection.Update(doc);
                marked = true;
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewLocalRepository] Erro em MarkAsSyncedIfUpdatedAtMatches para '{databankName}': {e.Message}");
        }

        return marked;
    }

    // ── Auxiliares ─────────────────────────────────────────────────────────────

    private void EnsureInjected()
    {
        if (_db == null)
            Debug.LogWarning("[TopicReviewLocalRepository] Chamado antes da injeção de ILiteDBManager.");
    }
}