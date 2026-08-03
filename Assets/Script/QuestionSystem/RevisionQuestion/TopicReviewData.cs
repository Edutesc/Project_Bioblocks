using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
[Serializable]
public class TopicReviewData
{
    [FirestoreProperty] public string userId { get; set; }
    [FirestoreProperty] public string databankName { get; set; }

    [FirestoreProperty] public DateTime lastInteractionAt { get; set; }
    [FirestoreProperty] public DateTime nextReviewAt { get; set; }

    [FirestoreProperty] public List<TopicReviewSessionHistoryItem> sessionHistory { get; set; }

    [FirestoreProperty] public int totalSessionsCompleted { get; set; }
    [FirestoreProperty] public DateTime updatedAt { get; set; }

    public TopicReviewData()
    {
        userId = string.Empty;
        databankName = string.Empty;
        userId = string.Empty;

        lastInteractionAt = DateTime.MinValue;
        nextReviewAt = DateTime.MinValue;

        sessionHistory = new List<TopicReviewSessionHistoryItem>();

        totalSessionsCompleted = 0;
        updatedAt = DateTime.MinValue;
    }

    public bool IsDueAt(DateTime nowUtc)
    {
        return nextReviewAt != DateTime.MinValue && nextReviewAt <= nowUtc;
    }
}