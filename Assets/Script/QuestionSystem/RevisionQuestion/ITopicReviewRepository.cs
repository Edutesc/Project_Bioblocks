using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITopicReviewRepository
{
    Task UpsertTopicReviewAsync(string userId,
        string databankName,
        string topicId,
        TopicReviewSessionHistoryItem sessionHistoryItem,
        DateTime nextReviewAt);

    // Task<TopicReviewData> GetTopicReviewAsync(
    //     string userId,
    //     string topicId
    // );


    Task<List<TopicReviewData>> GetDueTopicReviewsAsync(
        string userId,
        DateTime nowUtc
    );

    // ── Novos métodos usados pelo TopicReviewSyncService ──────────────
    Task<TopicReviewData> GetTopicReviewData(string userId, string databankName);
    Task UpdateTopicReviewData(TopicReviewData data);
}