using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using QuestionSystem;

[TestFixture]
public class QuestionSessionSelectorTests
{
    [Test]
    public void SelectQuestionsForSession_ListaNula_RetornaListaVazia()
    {
        var result = QuestionSessionSelector.SelectQuestionsForSession(null, null);

        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void SelectQuestionsForSession_RemoveQuestoesRespondidas()
    {
        var questions = QuestionTestHelpers.MakeQuestions(nivel1: 5, nivel2: 3, nivel3: 2);

        var result = QuestionSessionSelector.SelectQuestionsForSession(
            questions,
            new List<string> { "1", "6", "9" },
            sessionSize: 10);

        CollectionAssert.DoesNotContain(result.Select(q => q.questionNumber), 1);
        CollectionAssert.DoesNotContain(result.Select(q => q.questionNumber), 6);
        CollectionAssert.DoesNotContain(result.Select(q => q.questionNumber), 9);
    }

    [Test]
    public void SelectQuestionsForSession_ComQuestoesSuficientes_AplicaMixPadrao()
    {
        var questions = QuestionTestHelpers.MakeQuestions(nivel1: 10, nivel2: 10, nivel3: 10);

        var result = QuestionSessionSelector.SelectQuestionsForSession(
            questions,
            new List<string>(),
            sessionSize: 10);

        Assert.AreEqual(10, result.Count);
        Assert.AreEqual(5, result.Count(q => q.questionLevel == 1));
        Assert.AreEqual(3, result.Count(q => q.questionLevel == 2));
        Assert.AreEqual(2, result.Count(q => q.questionLevel == 3));
    }

    [Test]
    public void SelectQuestionsForSession_QuandoFaltaNivel_RedistribuiVagas()
    {
        var questions = QuestionTestHelpers.MakeQuestions(nivel1: 2, nivel2: 10, nivel3: 10);

        var result = QuestionSessionSelector.SelectQuestionsForSession(
            questions,
            new List<string>(),
            sessionSize: 10);

        Assert.AreEqual(10, result.Count);
        Assert.AreEqual(2, result.Count(q => q.questionLevel == 1));
        Assert.Greater(result.Count(q => q.questionLevel == 2), 3);
        Assert.AreEqual(2, result.Count(q => q.questionLevel == 3));
    }

    [Test]
    public void SelectQuestionsForSession_QuandoTotalMenorQueSessao_RetornaTodasNaoRespondidas()
    {
        var questions = QuestionTestHelpers.MakeQuestions(nivel1: 2, nivel2: 2, nivel3: 1);

        var result = QuestionSessionSelector.SelectQuestionsForSession(
            questions,
            new List<string> { "1" },
            sessionSize: 10);

        Assert.AreEqual(4, result.Count);
        CollectionAssert.AreEquivalent(new[] { 2, 3, 4, 5 }, result.Select(q => q.questionNumber));
    }

    [Test]
    public void SelectQuestionsForSession_QuestionLevelZero_ContaComoNivelUm()
    {
        var questions = new List<Question>
        {
            QuestionTestHelpers.MakeQuestion(1, level: 0),
            QuestionTestHelpers.MakeQuestion(2, level: 2),
            QuestionTestHelpers.MakeQuestion(3, level: 3)
        };

        var result = QuestionSessionSelector.SelectQuestionsForSession(
            questions,
            new List<string>(),
            sessionSize: 10);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(0, result[0].questionLevel);
    }
}
