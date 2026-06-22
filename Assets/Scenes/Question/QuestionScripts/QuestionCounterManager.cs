using System.Collections.Generic;
using UnityEngine;

public class QuestionCounterManager : MonoBehaviour
{
    private List<string> answeredQuestionsFromFirebase = new List<string>();

    public void Initialize(List<QuestionSystem.Question> allQuestions, List<string> answeredQuestions)
    {
        answeredQuestionsFromFirebase = answeredQuestions ?? new List<string>();
    }

    public void UpdateAnsweredQuestions(List<string> newAnsweredQuestions)
    {
        answeredQuestionsFromFirebase = newAnsweredQuestions ?? new List<string>();
    }

    public void MarkQuestionAsAnswered(int questionNumber)
    {
        string questionId = questionNumber.ToString();
        if (!answeredQuestionsFromFirebase.Contains(questionId))
            answeredQuestionsFromFirebase.Add(questionId);
    }

    public int GetAnsweredCount()
    {
        return answeredQuestionsFromFirebase.Count;
    }
}
