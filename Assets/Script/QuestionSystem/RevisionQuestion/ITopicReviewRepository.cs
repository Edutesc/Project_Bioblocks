using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITopicReviewRepository
{
    Task UpsertTopicReviewAsync(string userId, string topicId, DateTime nextReviewAt);

    // Task<TopicReviewData> GetTopicReviewAsync(
    //     string userId,
    //     string topicId
    // );


    Task<List<TopicReviewData>> GetDueTopicReviewsAsync(
        string userId,
        DateTime nowUtc
    );
}