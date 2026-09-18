using System;
using System.Collections.Generic;

public interface ITopicReviewLocalRepository
{
    // ── Leitura ──────────────────────────────────────────────────────
    TopicReviewData GetTopicReview(string userId, string databankName);
    List<TopicReviewData> GetAllTopicReviewsForUser(string userId);
    List<TopicReviewData> GetDirtyTopicReviewsForUser(string userId);

    bool IsDirty(string userId, string databankName);
    DateTime GetLastSyncedAt(string userId, string databankName);

    // ── Escrita ──────────────────────────────────────────────────────
    void UpdateTopicReview(TopicReviewData data);

    void MarkAsDirty(string userId, string databankName);
    void MarkAsSynced(string userId, string databankName);
    bool MarkAsSyncedIfUpdatedAtMatches(string userId, string databankName, DateTime expectedUpdatedAt);
}