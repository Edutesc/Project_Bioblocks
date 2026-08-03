using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITopicReviewManager
{
    Task RegisterCompletedSessionAsync(
        string userId,
        string topicId,
        string correctQuestionGlobalIds,
        string wrongQuestionGlobalIds,
        string globalId,
        string source
    );

    Task ScheduleNextRevision(
        string userId,
        string globalId,
        string topicId);
}