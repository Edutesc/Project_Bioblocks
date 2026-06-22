using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class QuestionCounterManagerTests
{
    private GameObject _managerGO;
    private QuestionCounterManager _manager;

    [SetUp]
    public void Setup()
    {
        _managerGO = new GameObject("QuestionCounterManager");
        _manager = _managerGO.AddComponent<QuestionCounterManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_managerGO);
    }

    [Test]
    public void Initialize_ListaNula_ZeraContador()
    {
        _manager.Initialize(null, null);

        Assert.AreEqual(0, _manager.GetAnsweredCount());
    }

    [Test]
    public void Initialize_UsaQuestoesRespondidas()
    {
        _manager.Initialize(null, new List<string> { "1", "2" });

        Assert.AreEqual(2, _manager.GetAnsweredCount());
    }

    [Test]
    public void MarkQuestionAsAnswered_AdicionaQuestao()
    {
        _manager.Initialize(null, new List<string>());

        _manager.MarkQuestionAsAnswered(1);

        Assert.AreEqual(1, _manager.GetAnsweredCount());
    }

    [Test]
    public void MarkQuestionAsAnswered_MesmaQuestao_NaoDuplica()
    {
        _manager.Initialize(null, new List<string>());

        _manager.MarkQuestionAsAnswered(1);
        _manager.MarkQuestionAsAnswered(1);

        Assert.AreEqual(1, _manager.GetAnsweredCount());
    }

    [Test]
    public void UpdateAnsweredQuestions_SubstituiListaAnterior()
    {
        _manager.Initialize(null, new List<string> { "1" });

        _manager.UpdateAnsweredQuestions(new List<string> { "2", "3", "4" });

        Assert.AreEqual(3, _manager.GetAnsweredCount());
    }
}
