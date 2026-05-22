using System.Threading.Tasks;
using System.Collections.Generic;

namespace BioBlocks.Tests
{
    public class FakeTestService : ITestService
    {
        public List<TestConfig> MockActiveTests = new List<TestConfig>();
        public Dictionary<string, TestQuestion> MockQuestions = new Dictionary<string, TestQuestion>();
        public TestSubmission LastSubmittedSubmission { get; private set; }
        public bool MockHasTakenTest = false;

        public Task<List<TestConfig>> GetActiveTestsAsync()
        {
            return Task.FromResult(MockActiveTests);
        }

        public Task<TestQuestion> GetQuestionAsync(string questionId)
        {
            MockQuestions.TryGetValue(questionId, out var q);
            return Task.FromResult(q);
        }

        public Task SubmitSubmissionAsync(TestSubmission submission)
        {
            LastSubmittedSubmission = submission;
            MockHasTakenTest = true;
            return Task.CompletedTask;
        }

        public Task<bool> HasUserTakenTestAsync(string testId, string userId)
        {
            return Task.FromResult(MockHasTakenTest);
        }
    }
}
