using System.Collections.Generic;
using Firebase.Firestore;

namespace Edutesc.BioBlocks.Core.Models
{
    [FirestoreData]
    public class AssessmentAttempt
    {
        [FirestoreProperty("attemptId")]
        public string AttemptId { get; set; }

        [FirestoreProperty("assessmentId")]
        public string AssessmentId { get; set; }

        [FirestoreProperty("userId")]
        public string UserId { get; set; }

        [FirestoreProperty("status")]
        public string Status { get; set; } = "completed";

        [FirestoreProperty("startedAt")]
        public Timestamp StartedAt { get; set; }

        [FirestoreProperty("completedAt")]
        public Timestamp CompletedAt { get; set; }

        [FirestoreProperty("durationSeconds")]
        public int DurationSeconds { get; set; }

        [FirestoreProperty("questionIds")]
        public List<string> QuestionIds { get; set; } = new List<string>();

        [FirestoreProperty("correctQuestionIds")]
        public List<string> CorrectQuestionIds { get; set; } = new List<string>();

        [FirestoreProperty("wrongQuestionIds")]
        public List<string> WrongQuestionIds { get; set; } = new List<string>();

        [FirestoreProperty("answers")]
        public List<AssessmentAnswerItem> Answers { get; set; } = new List<AssessmentAnswerItem>();

        [FirestoreProperty("score")]
        public AssessmentScoreSummary Score { get; set; } = new AssessmentScoreSummary();
    }

    [FirestoreData]
    public class AssessmentAnswerItem
    {
        [FirestoreProperty("questionId")]
        public string QuestionId { get; set; }

        [FirestoreProperty("questionDatabankName")]
        public string QuestionDatabankName { get; set; }

        [FirestoreProperty("questionLevel")]
        public int QuestionLevel { get; set; }

        [FirestoreProperty("selectedIndex")]
        public int SelectedIndex { get; set; }

        [FirestoreProperty("correctIndex")]
        public int CorrectIndex { get; set; }

        [FirestoreProperty("isCorrect")]
        public bool IsCorrect { get; set; }
    }

    [FirestoreData]
    public class AssessmentScoreSummary
    {
        [FirestoreProperty("correct")]
        public int Correct { get; set; }

        [FirestoreProperty("wrong")]
        public int Wrong { get; set; }

        [FirestoreProperty("total")]
        public int Total { get; set; }
    }
}
