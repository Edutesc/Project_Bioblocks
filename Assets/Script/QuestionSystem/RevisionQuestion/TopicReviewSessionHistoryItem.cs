using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
[Serializable]
public class TopicReviewSessionHistoryItem
{
    [FirestoreProperty] public string sessionId { get; set; }
    [FirestoreProperty] public DateTime completedAt { get; set; }
    [FirestoreProperty] public string source { get; set; }

    [FirestoreProperty] public List<string> questionGlobalIds { get; set; }
    [FirestoreProperty] public List<string> correctQuestionGlobalIds { get; set; }
    [FirestoreProperty] public List<string> wrongQuestionGlobalIds { get; set; }

    public TopicReviewSessionHistoryItem()
    {
        sessionId = string.Empty;
        completedAt = DateTime.MinValue;
        source = "normal";

        questionGlobalIds = new List<string>();
        correctQuestionGlobalIds = new List<string>();
        wrongQuestionGlobalIds = new List<string>();
    }
}