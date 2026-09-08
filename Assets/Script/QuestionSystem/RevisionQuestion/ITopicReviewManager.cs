using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITopicReviewManager
{
    Task RegisterCompletedSessionAsync(
    string userId,
    string databankName,
    TopicReviewSessionHistoryItem sessionHistoryItem,
    string globalId
    );

    Task ScheduleNextRevision(
        string userId,
        string databankName,
        string topicId,
        TopicReviewSessionHistoryItem sessionHistoryItem
);
}