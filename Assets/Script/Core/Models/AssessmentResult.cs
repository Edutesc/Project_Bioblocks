using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
public class AssessmentResult
{
    [FirestoreProperty("userId")]
    public string UserId { get; set; }

    [FirestoreProperty("studentName")]
    public string StudentName { get; set; }

    [FirestoreProperty("ra")]
    public string RA { get; set; }

    [FirestoreProperty("completedAt")]
    public Timestamp CompletedAt { get; set; }

    [FirestoreProperty("score")]
    public AssessmentScore Score { get; set; }

    [FirestoreProperty("questionsAnswered")]
    public List<AssessmentAnswerDetail> QuestionsAnswered { get; set; }
}

[FirestoreData]
public class AssessmentScore
{
    [FirestoreProperty("total")]
    public int Total { get; set; }

    [FirestoreProperty("correct")]
    public int Correct { get; set; }
}

[FirestoreData]
public class AssessmentAnswerDetail
{
    [FirestoreProperty("questionId")]
    public string QuestionId { get; set; }

    [FirestoreProperty("difficulty")]
    public string Difficulty { get; set; }

    [FirestoreProperty("isCorrect")]
    public bool IsCorrect { get; set; }

    [FirestoreProperty("userAnswer")]
    public string UserAnswer { get; set; }

    [FirestoreProperty("correctAnswer")]
    public string CorrectAnswer { get; set; }
}
