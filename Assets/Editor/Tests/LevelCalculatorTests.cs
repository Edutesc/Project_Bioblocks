using NUnit.Framework;
using System.Collections.Generic;
using QuestionSystem;

[TestFixture]
public class LevelCalculatorTests
{
    [Test]
    public void GetLevelStats_ListaVazia_RetornaDicionarioVazio()
    {
        var stats = LevelCalculator.GetLevelStats(
            new List<Question>(), new List<string>());

        Assert.AreEqual(0, stats.Count);
    }

    [Test]
    public void GetLevelStats_SemRespostas_PercentualZero()
    {
        var questions = QuestionTestHelpers.MakeQuestions(nivel1: 4);
        var stats = LevelCalculator.GetLevelStats(questions, new List<string>());

        Assert.AreEqual(0f, stats[1].ProgressPercentage, delta: 0.01f);
        Assert.AreEqual(0, stats[1].AnsweredQuestions);
        Assert.IsFalse(stats[1].IsComplete);
    }

    [Test]
    public void GetLevelStats_MetadeRespondida_Percentual50()
    {
        var questions = QuestionTestHelpers.MakeQuestions(nivel1: 4);
        var answered = new List<string> { "1", "2" };

        var stats = LevelCalculator.GetLevelStats(questions, answered);

        Assert.AreEqual(50f, stats[1].ProgressPercentage, delta: 0.01f);
    }

    [Test]
    public void GetLevelStats_Nivel1Completo_IsCompleteTrue()
    {
        var questions = QuestionTestHelpers.MakeQuestions(nivel1: 2, nivel2: 2);
        var answered = QuestionTestHelpers.ToAnsweredIdsForLevel(questions, 1);

        var stats = LevelCalculator.GetLevelStats(questions, answered);

        Assert.IsTrue(stats[1].IsComplete);
        Assert.IsFalse(stats[2].IsComplete);
    }

    [Test]
    public void GetLevelStats_ContemTodosOsNiveis()
    {
        var questions = QuestionTestHelpers.MakeQuestions(
            nivel1: 2, nivel2: 3, nivel3: 1);

        var stats = LevelCalculator.GetLevelStats(
            questions, new List<string>());

        Assert.IsTrue(stats.ContainsKey(1));
        Assert.IsTrue(stats.ContainsKey(2));
        Assert.IsTrue(stats.ContainsKey(3));
        Assert.AreEqual(2, stats[1].TotalQuestions);
        Assert.AreEqual(3, stats[2].TotalQuestions);
        Assert.AreEqual(1, stats[3].TotalQuestions);
    }
}
