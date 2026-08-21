using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITopicReviewManager
{
    Task RegisterCompletedSessionAsync(
        string userId,
        string topicId,
        List<string> correctQuestionGlobalIds,
        List<string> wrongQuestionGlobalIds,
        string globalId,
        string source
    );

    Task ScheduleNextRevision(
        string userId,
        string databankName,
        string topicId,
        List<string> correctQuestionGlobalIds,
        List<string> wrongQuestionGlobalIds);
}