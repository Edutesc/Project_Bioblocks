using Firebase.Firestore;
using System.Collections.Generic;

namespace BioBlocks.Tests
{
    [FirestoreData]
    public class TestConfig
    {
        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public bool IsActive { get; set; }
        [FirestoreProperty] public Timestamp StartTime { get; set; }
        [FirestoreProperty] public Timestamp EndTime { get; set; }
        [FirestoreProperty] public List<string> QuestionIds { get; set; }
    }

    [FirestoreData]
    public class TestQuestion
    {
        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public string Text { get; set; }
        [FirestoreProperty] public string ImageUrl { get; set; }
        [FirestoreProperty] public List<string> Options { get; set; }
    }

    [FirestoreData]
    public class TestSubmission
    {
        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public string TestId { get; set; }
        [FirestoreProperty] public string UserId { get; set; }
        [FirestoreProperty] public Dictionary<string, int> Answers { get; set; }
        [FirestoreProperty] public Timestamp SubmittedAt { get; set; }
    }

    [FirestoreData]
    public class TestResult
    {
        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public string TestId { get; set; }
        [FirestoreProperty] public string UserId { get; set; }
        [FirestoreProperty] public int Score { get; set; }
        [FirestoreProperty] public Timestamp SubmittedAt { get; set; }
    }
}
