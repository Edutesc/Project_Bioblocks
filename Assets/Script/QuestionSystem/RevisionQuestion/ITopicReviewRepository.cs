using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITopicReviewRepository
{
    Task UpsertTopicReviewAsync(string userId, TopicReviewData topicReview);

    Task<TopicReviewData> GetTopicReviewAsync(
        string userId,
        string databankName
    );

    Task<List<TopicReviewData>> GetDueTopicReviewsAsync(
        string userId,
        DateTime nowUtc
    );
}