// Assets/Editor/Tests/AssessmentSessionTests.cs
// Testes unitários para AssessmentSession e controle de tentativas de avaliação.

using System.Collections.Generic;
using NUnit.Framework;
using QuestionSystem;
using Edutesc.BioBlocks.Core.Models;

[TestFixture]
public class AssessmentSessionTests
{
    [TearDown]
    public void TearDown()
    {
        AssessmentSession.Clear();
    }

    private static Question MakeSampleQuestion(int number, int correctIndex, int level = 1, string databank = "BioDB", string globalId = null)
    {
        return new Question
        {
            questionNumber = number,
            questionLevel = level,
            questionDatabankName = databank,
            globalId = globalId,
            questionText = $"Texto da questão {number}",
            answers = new[] { "Alternativa A", "Alternativa B", "Alternativa C", "Alternativa D" },
            correctIndex = correctIndex
        };
    }

    // =======================================================
    // StartNew - Inicialização com AssessmentData
    // =======================================================

    [Test]
    public void StartNew_WithAssessmentData_InitializesSessionCorrectly()
    {
        var assessmentData = new AssessmentData
        {
            AssessmentId = "avaliacao-2026-1",
            Title = "Bioquímica Geral",
            TotalQuestions = 3
        };

        var questions = new List<Question>
        {
            MakeSampleQuestion(1, 0, globalId: "Q_1"),
            MakeSampleQuestion(2, 1, globalId: "Q_2")
        };

        AssessmentSession.StartNew(assessmentData, "user_123", questions);

        var session = AssessmentSession.Current;
        Assert.IsNotNull(session, "A sessão ativa não deve ser nula após StartNew.");
        Assert.AreEqual("user_123", session.UserId);
        Assert.AreEqual(assessmentData, session.AssessmentConfig);
        Assert.AreEqual(2, session.Questions.Count);
        Assert.AreEqual(0, session.CurrentQuestionIndex);

        // Attempt
        Assert.IsNotNull(session.CurrentAttempt);
        Assert.AreEqual("avaliacao-2026-1", session.CurrentAttempt.AssessmentId);
        Assert.AreEqual("user_123", session.CurrentAttempt.UserId);
        Assert.AreEqual("in_progress", session.CurrentAttempt.Status);
        Assert.AreEqual(0, session.CurrentAttempt.Score.Total);
        Assert.AreEqual(0, session.CurrentAttempt.Score.Correct);
        Assert.AreEqual(0, session.CurrentAttempt.Score.Wrong);

        // Result
        Assert.IsNotNull(session.CurrentResult);
        Assert.AreEqual("user_123", session.CurrentResult.UserId);
    }

    [Test]
    public void StartNew_WithNullQuestions_InitializesEmptyListWithoutError()
    {
        var assessmentData = new AssessmentData { AssessmentId = "test-assessment" };

        AssessmentSession.StartNew(assessmentData, "user_xyz", null);

        Assert.IsNotNull(AssessmentSession.Current);
        Assert.IsNotNull(AssessmentSession.Current.Questions);
        Assert.AreEqual(0, AssessmentSession.Current.Questions.Count);
    }

    [Test]
    public void StartNew_LegacyWithStudentNameAndRA_InitializesCorrectly()
    {
        var questions = new List<Question> { MakeSampleQuestion(1, 0) };

        AssessmentSession.StartNew("Gabriel", "11202010000", questions);

        var session = AssessmentSession.Current;
        Assert.IsNotNull(session);
        Assert.AreEqual("Gabriel", session.StudentName);
        Assert.AreEqual("11202010000", session.RA);
        Assert.AreEqual("legacy-assessment", session.CurrentAttempt.AssessmentId);
        Assert.AreEqual("Gabriel", session.CurrentResult.StudentName);
        Assert.AreEqual("11202010000", session.CurrentResult.RA);
    }

    // =======================================================
    // Navegação de Questões
    // =======================================================

    [Test]
    public void GetCurrentQuestion_And_MoveToNextQuestion_NavigatesCorrectly()
    {
        var q1 = MakeSampleQuestion(1, 0, globalId: "Q1");
        var q2 = MakeSampleQuestion(2, 1, globalId: "Q2");

        AssessmentSession.StartNew(new AssessmentData(), "u1", new List<Question> { q1, q2 });
        var session = AssessmentSession.Current;

        Assert.AreEqual(q1, session.GetCurrentQuestion());
        Assert.AreEqual(0, session.CurrentQuestionIndex);

        session.MoveToNextQuestion();
        Assert.AreEqual(q2, session.GetCurrentQuestion());
        Assert.AreEqual(1, session.CurrentQuestionIndex);

        session.MoveToNextQuestion();
        Assert.IsNull(session.GetCurrentQuestion(), "Ao ultrapassar a lista de questões, deve retornar null.");
        Assert.AreEqual(2, session.CurrentQuestionIndex);
    }

    // =======================================================
    // Gravação de Respostas (RecordAnswer)
    // =======================================================

    [Test]
    public void RecordAnswer_CorrectAnswer_IncrementsScoreAndTracksQuestionCorrectly()
    {
        var q = MakeSampleQuestion(1, correctIndex: 2, level: 2, databank: "EnzymeDB", globalId: "ENZ_001");
        AssessmentSession.StartNew(new AssessmentData { AssessmentId = "test-1" }, "u1", new List<Question> { q });
        var session = AssessmentSession.Current;

        // Seleciona a resposta correta (índice 2)
        session.RecordAnswer(q, selectedIndex: 2);

        var attempt = session.CurrentAttempt;
        Assert.AreEqual(1, attempt.Score.Total);
        Assert.AreEqual(1, attempt.Score.Correct);
        Assert.AreEqual(0, attempt.Score.Wrong);

        Assert.AreEqual(1, attempt.QuestionIds.Count);
        Assert.AreEqual("ENZ_001", attempt.QuestionIds[0]);
        Assert.Contains("ENZ_001", attempt.CorrectQuestionIds);
        Assert.IsEmpty(attempt.WrongQuestionIds);

        Assert.AreEqual(1, attempt.Answers.Count);
        var answerItem = attempt.Answers[0];
        Assert.AreEqual("ENZ_001", answerItem.QuestionId);
        Assert.AreEqual("EnzymeDB", answerItem.QuestionDatabankName);
        Assert.AreEqual(2, answerItem.QuestionLevel);
        Assert.AreEqual(2, answerItem.SelectedIndex);
        Assert.AreEqual(2, answerItem.CorrectIndex);
        Assert.IsTrue(answerItem.IsCorrect);

        // Result espelho
        Assert.AreEqual(1, session.CurrentResult.Score.Total);
        Assert.AreEqual(1, session.CurrentResult.Score.Correct);
        Assert.AreEqual(1, session.CurrentResult.QuestionsAnswered.Count);
        Assert.AreEqual("Alternativa C", session.CurrentResult.QuestionsAnswered[0].UserAnswer);
        Assert.AreEqual("Alternativa C", session.CurrentResult.QuestionsAnswered[0].CorrectAnswer);
        Assert.IsTrue(session.CurrentResult.QuestionsAnswered[0].IsCorrect);
    }

    [Test]
    public void RecordAnswer_WrongAnswer_IncrementsWrongScoreAndTracksIncorrectQuestion()
    {
        var q = MakeSampleQuestion(1, correctIndex: 0, level: 1, databank: "WaterDB", globalId: "WAT_001");
        AssessmentSession.StartNew(new AssessmentData { AssessmentId = "test-1" }, "u1", new List<Question> { q });
        var session = AssessmentSession.Current;

        // Seleciona resposta errada (índice 3, correta é 0)
        session.RecordAnswer(q, selectedIndex: 3);

        var attempt = session.CurrentAttempt;
        Assert.AreEqual(1, attempt.Score.Total);
        Assert.AreEqual(0, attempt.Score.Correct);
        Assert.AreEqual(1, attempt.Score.Wrong);

        Assert.Contains("WAT_001", attempt.WrongQuestionIds);
        Assert.IsEmpty(attempt.CorrectQuestionIds);

        Assert.AreEqual(1, attempt.Answers.Count);
        Assert.IsFalse(attempt.Answers[0].IsCorrect);
        Assert.AreEqual(3, attempt.Answers[0].SelectedIndex);
        Assert.AreEqual(0, attempt.Answers[0].CorrectIndex);

        // Result
        Assert.IsFalse(session.CurrentResult.QuestionsAnswered[0].IsCorrect);
        Assert.AreEqual("Alternativa D", session.CurrentResult.QuestionsAnswered[0].UserAnswer);
        Assert.AreEqual("Alternativa A", session.CurrentResult.QuestionsAnswered[0].CorrectAnswer);
    }

    [Test]
    public void RecordAnswer_FallbackGlobalId_WhenGlobalIdIsNullOrEmpty()
    {
        var q = MakeSampleQuestion(5, correctIndex: 1, databank: "LipidsDB", globalId: "");
        AssessmentSession.StartNew(new AssessmentData(), "u1", new List<Question> { q });
        var session = AssessmentSession.Current;

        session.RecordAnswer(q, selectedIndex: 1);

        // Deve gerar "LipidsDB_005"
        Assert.AreEqual("LipidsDB_005", session.CurrentAttempt.QuestionIds[0]);
        Assert.AreEqual("LipidsDB_005", session.CurrentAttempt.Answers[0].QuestionId);
    }

    [Test]
    public void RecordAnswer_NullQuestion_DoesNotThrow()
    {
        AssessmentSession.StartNew(new AssessmentData(), "u1", new List<Question>());
        var session = AssessmentSession.Current;

        Assert.DoesNotThrow(() => session.RecordAnswer(null, 0));
        Assert.AreEqual(0, session.CurrentAttempt.Score.Total);
    }

    // =======================================================
    // FinalizeAttempt
    // =======================================================

    [Test]
    public void FinalizeAttempt_MarksStatusCompletedAndSetsDuration()
    {
        var q = MakeSampleQuestion(1, correctIndex: 0);
        AssessmentSession.StartNew(new AssessmentData { AssessmentId = "assessment_final" }, "u_test", new List<Question> { q });
        var session = AssessmentSession.Current;

        session.RecordAnswer(q, 0);
        var completedAttempt = session.FinalizeAttempt();

        Assert.IsNotNull(completedAttempt);
        Assert.AreEqual("completed", completedAttempt.Status);
        Assert.AreNotEqual(default(Firebase.Firestore.Timestamp), completedAttempt.CompletedAt);
        Assert.GreaterOrEqual(completedAttempt.DurationSeconds, 0);
    }

    // =======================================================
    // Clear
    // =======================================================

    [Test]
    public void Clear_ResetsCurrentSessionToNull()
    {
        AssessmentSession.StartNew(new AssessmentData(), "u1", new List<Question>());
        Assert.IsNotNull(AssessmentSession.Current);

        AssessmentSession.Clear();
        Assert.IsNull(AssessmentSession.Current);
    }
}
