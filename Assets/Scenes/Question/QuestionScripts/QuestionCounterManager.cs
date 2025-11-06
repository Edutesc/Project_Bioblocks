using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using QuestionSystem;

public class QuestionCounterManager : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI questionCounterText;
    [SerializeField] private ProgressBarManager progressBarManager;

    private List<Question> allDatabaseQuestions;
    private List<string> answeredQuestionsFromFirebase;
    private int currentQuestionLevel;

    public void Initialize(List<Question> allQuestions, List<string> answeredQuestions)
    {
        allDatabaseQuestions = allQuestions;
        answeredQuestionsFromFirebase = answeredQuestions ?? new List<string>();

        if (progressBarManager == null && questionCounterText == null)
        {
            Debug.LogError("QuestionCounterManager: Nenhuma UI configurada (progressBarManager ou questionCounterText)!");
        }
    }

    public void UpdateCounter(Question currentQuestion)
    {
        if (currentQuestion == null || allDatabaseQuestions == null)
        {
            Debug.LogWarning("QuestionCounterManager: Dados insuficientes para atualizar contador");
            return;
        }

        currentQuestionLevel = GetQuestionLevel(currentQuestion);

        var questionsInCurrentLevel = allDatabaseQuestions
            .Where(q => GetQuestionLevel(q) == currentQuestionLevel)
            .ToList();

        int totalInLevel = questionsInCurrentLevel.Count;
        HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase);

        int answeredInLevel = questionsInCurrentLevel
            .Count(q => answeredSet.Contains(q.questionNumber.ToString()));

        int currentPosition = answeredInLevel;
        bool isAlreadyAnswered = answeredSet.Contains(currentQuestion.questionNumber.ToString());

        if (!isAlreadyAnswered)
        {
            currentPosition = answeredInLevel + 1;
        }
        else
        {
            var answeredInLevelList = questionsInCurrentLevel
                .Where(q => answeredSet.Contains(q.questionNumber.ToString()))
                .OrderBy(q => q.questionNumber)
                .ToList();

            int indexInAnswered = answeredInLevelList.FindIndex(q => q.questionNumber == currentQuestion.questionNumber);
            currentPosition = indexInAnswered + 1;
        }

        string levelName = GetLevelName(currentQuestionLevel);

        if (progressBarManager != null)
        {
            string progressTextValue = $"{answeredInLevel} de {totalInLevel}";
            string labelTextValue = levelName;
            
            progressBarManager.UpdateProgress(
                answeredInLevel,        // current
                totalInLevel,           // total
                progressTextValue,      // progressText: "25 de 68"
                labelTextValue          // labelText: "Nível Básico"
            );
        }

        if (questionCounterText != null)
        {
            string counterText = $"Respostas corretas: {answeredInLevel} de {totalInLevel}\n{levelName}";
            questionCounterText.text = counterText;
        }

        Debug.Log($" Contador atualizado: {answeredInLevel}/{totalInLevel} - {levelName} (Q#{currentQuestion.questionNumber})");
    }

    public void UpdateAnsweredQuestions(List<string> newAnsweredQuestions)
    {
        answeredQuestionsFromFirebase = newAnsweredQuestions ?? new List<string>();
    }

    public void MarkQuestionAsAnswered(int questionNumber)
    {
        string questionId = questionNumber.ToString();
        if (!answeredQuestionsFromFirebase.Contains(questionId))
        {
            answeredQuestionsFromFirebase.Add(questionId);
            Debug.Log($"QuestionCounterManager: Questão {questionNumber} marcada como respondida localmente");
        }
    }

    private int GetQuestionLevel(Question question)
    {
        if (question.questionLevel <= 0)
        {
            return 1;
        }
        return question.questionLevel;
    }

    private string GetLevelName(int level)
    {
        return level switch
        {
            1 => "Nível Básico",
            2 => "Nível Intermediário",
            3 => "Nível Difícil",
            4 => "Nível Avançado",
            5 => "Nível Expert",
            _ => $"Nível {level}"
        };
    }

    public LevelProgressInfo GetCurrentLevelProgress()
    {
        if (allDatabaseQuestions == null || currentQuestionLevel <= 0)
        {
            return null;
        }

        var questionsInLevel = allDatabaseQuestions
            .Where(q => GetQuestionLevel(q) == currentQuestionLevel)
            .ToList();

        HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase);

        int answered = questionsInLevel
            .Count(q => answeredSet.Contains(q.questionNumber.ToString()));

        return new LevelProgressInfo
        {
            Level = currentQuestionLevel,
            LevelName = GetLevelName(currentQuestionLevel),
            AnsweredQuestions = answered,
            TotalQuestions = questionsInLevel.Count,
            ProgressPercentage = questionsInLevel.Count > 0
                ? (float)answered / questionsInLevel.Count * 100f
                : 0f
        };
    }
}

public class LevelProgressInfo
{
    public int Level;
    public string LevelName;
    public int AnsweredQuestions;
    public int TotalQuestions;
    public float ProgressPercentage;

    public override string ToString()
    {
        return $"{LevelName}: {AnsweredQuestions}/{TotalQuestions} ({ProgressPercentage:F0}%)";
    }
}