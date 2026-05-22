using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Firestore;

namespace BioBlocks.Tests
{
    public interface ITestService
    {
        Task<List<TestConfig>> GetActiveTestsAsync();
        Task<TestQuestion> GetQuestionAsync(string questionId);
        Task SubmitSubmissionAsync(TestSubmission submission);
        Task<bool> HasUserTakenTestAsync(string testId, string userId);
    }

    public class TestService : ITestService
    {
        private readonly FirebaseFirestore _db = FirebaseFirestore.DefaultInstance;

        public async Task<List<TestConfig>> GetActiveTestsAsync()
        {
            Query query = _db.Collection("Tests").WhereEqualTo("IsActive", true);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            
            List<TestConfig> activeTests = new List<TestConfig>();
            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                activeTests.Add(doc.ConvertTo<TestConfig>());
            }
            return activeTests;
        }

        public async Task<TestQuestion> GetQuestionAsync(string questionId)
        {
            DocumentReference docRef = _db.Collection("TestQuestions").Document(questionId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists ? snapshot.ConvertTo<TestQuestion>() : null;
        }

        public async Task SubmitSubmissionAsync(TestSubmission submission)
        {
            submission.Id = $"{submission.TestId}_{submission.UserId}";
            submission.SubmittedAt = Timestamp.GetCurrentTimestamp();
            
            DocumentReference docRef = _db.Collection("TestSubmissions").Document(submission.Id);
            await docRef.SetAsync(submission);
        }

        public async Task<bool> HasUserTakenTestAsync(string testId, string userId)
        {
            string docId = $"{testId}_{userId}";
            DocumentReference docRef = _db.Collection("TestSubmissions").Document(docId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists;
        }
    }
}
