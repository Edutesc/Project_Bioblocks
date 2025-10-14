using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using QuestionSystem;

/// <summary>
/// Gerencia o contador de questões por nível
/// Exemplo: "Questão 5 de 30 - Nível Básico"
/// </summary>
public class QuestionCounterManager : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI questionCounterText;

    private List<Question> allDatabaseQuestions;
    private List<string> answeredQuestionsFromFirebase;
    private int currentQuestionLevel;

    /// <summary>
    /// Inicializa o gerenciador com todas as questões do banco
    /// </summary>
    public void Initialize(List<Question> allQuestions, List<string> answeredQuestions)
    {
        allDatabaseQuestions = allQuestions;
        answeredQuestionsFromFirebase = answeredQuestions ?? new List<string>();

        if (questionCounterText == null)
        {
            Debug.LogError("QuestionCounterManager: questionCounterText não está atribuído!");
        }
    }

    /// <summary>
    /// Atualiza o contador baseado na questão atual
    /// </summary>
    public void UpdateCounter(Question currentQuestion)
    {
        if (currentQuestion == null || allDatabaseQuestions == null || questionCounterText == null)
        {
            Debug.LogWarning("QuestionCounterManager: Dados insuficientes para atualizar contador");
            return;
        }

        currentQuestionLevel = GetQuestionLevel(currentQuestion);

        // Filtra questões do mesmo nível da questão atual
        var questionsInCurrentLevel = allDatabaseQuestions
            .Where(q => GetQuestionLevel(q) == currentQuestionLevel)
            .ToList();

        int totalInLevel = questionsInCurrentLevel.Count;

        // Conta quantas questões deste nível já foram respondidas
        HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase);

        int answeredInLevel = questionsInCurrentLevel
            .Count(q => answeredSet.Contains(q.questionNumber.ToString()));

        // Se a questão atual ainda não foi respondida, conta como "próxima"
        // Se já foi respondida (replay), mantém a contagem atual
        int currentPosition = answeredInLevel;

        // Verifica se a questão atual já está nas respondidas
        bool isAlreadyAnswered = answeredSet.Contains(currentQuestion.questionNumber.ToString());

        if (!isAlreadyAnswered)
        {
            // Se ainda não foi respondida, será a próxima
            currentPosition = answeredInLevel + 1;
        }
        else
        {
            // Se já foi respondida, mostra a posição dela
            var answeredInLevelList = questionsInCurrentLevel
                .Where(q => answeredSet.Contains(q.questionNumber.ToString()))
                .OrderBy(q => q.questionNumber)
                .ToList();

            int indexInAnswered = answeredInLevelList.FindIndex(q => q.questionNumber == currentQuestion.questionNumber);
            currentPosition = indexInAnswered + 1;
        }

       // Obtém o nome do nível
       string levelName = GetLevelName(currentQuestionLevel);

        // Formata o texto - mostra apenas questões respondidas corretamente
        string counterText = $"Respostas corretas: {answeredInLevel} de {totalInLevel}\n" + $"{levelName}";
        questionCounterText.text = counterText;

        Debug.Log($"📊 Contador atualizado: {counterText} (Q#{currentQuestion.questionNumber})");
    }

    /// <summary>
    /// Atualiza a lista de questões respondidas (após responder uma nova questão)
    /// </summary>
    public void UpdateAnsweredQuestions(List<string> newAnsweredQuestions)
    {
        answeredQuestionsFromFirebase = newAnsweredQuestions ?? new List<string>();
    }

    /// <summary>
    /// Adiciona uma questão à lista de respondidas (uso local antes de sincronizar com Firebase)
    /// </summary>
    public void MarkQuestionAsAnswered(int questionNumber)
    {
        string questionId = questionNumber.ToString();
        if (!answeredQuestionsFromFirebase.Contains(questionId))
        {
            answeredQuestionsFromFirebase.Add(questionId);
            Debug.Log($"QuestionCounterManager: Questão {questionNumber} marcada como respondida localmente");
        }
    }

    /// <summary>
    /// Obtém o nível de uma questão (trata casos onde questionLevel não foi definido)
    /// </summary>
    private int GetQuestionLevel(Question question)
    {
        if (question.questionLevel <= 0)
        {
            return 1; // Questões sem nível definido = nível 1
        }
        return question.questionLevel;
    }

    /// <summary>
    /// Retorna o nome amigável do nível
    /// </summary>
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

    /// <summary>
    /// Obtém informações detalhadas sobre o progresso do nível atual
    /// </summary>
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

/// <summary>
/// Informações sobre o progresso em um nível
/// </summary>
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