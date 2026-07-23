using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITopicReviewManager
{
    Task RegisterCompletedSessionAsync(
        string userId,
        string databankName,
        string displayName,
        List<string> sessionQuestionGlobalIds,
        List<string> correctQuestionGlobalIds,
        List<string> wrongQuestionGlobalIds,
        string source
    );

    Task<List<TopicReviewData>> GetDueTopicReviewsAsync(string userId);
}