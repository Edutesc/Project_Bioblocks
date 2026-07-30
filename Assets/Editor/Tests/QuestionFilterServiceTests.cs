// Assets/Editor/Tests/QuestionFilterServiceTests.cs
// Testes unitários para QuestionFilterService.
//
// Para rodar: Window → General → Test Runner → EditMode → Run All
//
// QuestionFilterService é uma classe estática pura — sem MonoBehaviour,
// sem estado global. Todos os testes rodam em Edit Mode sem [SetUp] especial.
//
// Dependências: FakeQuestionDatabase.cs e QuestionTestHelpers.cs (pasta Helpers/)

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using QuestionSystem;

[TestFixture]
public class QuestionFilterServiceTests
{
    // =======================================================
    // FilterQuestions — entrada inválida
    // =======================================================

    [Test]
    public void FilterQuestions_DatabaseNulo_RetornaListaVazia()
    {
        LogAssert.Expect(LogType.Error, "[QuestionFilterService] Database is null");

        var result = QuestionFilterService.FilterQuestions(null);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void FilterQuestions_DatabaseVazio_RetornaListaVazia()
    {
        var db = FakeQuestionDatabase.Empty();

        var result = QuestionFilterService.FilterQuestions(db);

        Assert.AreEqual(0, result.Count);
    }

    // =======================================================
    // FilterQuestions — modo normal
    // =======================================================

    [Test]
    public void FilterQuestions_RetornaApenasQuestoesVisiveis()
    {
        // 3 de produção, 2 de desenvolvimento
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 3, devCount: 2);

        var result = QuestionFilterService.FilterQuestions(db);

        Assert.AreEqual(3, result.Count);
        Assert.IsTrue(result.All(q => !q.questionInDevelopment),
            "Modo produção não deve retornar questões em desenvolvimento");
    }

    [Test]
    public void FilterQuestions_SemQuestoesEmDev_RetornaTodas()
    {
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 5, devCount: 0);

        var result = QuestionFilterService.FilterQuestions(db);

        Assert.AreEqual(5, result.Count);
    }

    [Test]
    public void FilterQuestions_TodasEmDev_RetornaListaVazia()
    {
        // Banco em modo prod, mas todas as questões marcadas como dev
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 0, devCount: 4);

        var result = QuestionFilterService.FilterQuestions(db);

        Assert.AreEqual(0, result.Count);
    }

    // =======================================================
    // FilterQuestions — databaseInDevelopment não altera visibilidade
    // =======================================================

    [Test]
    public void FilterQuestions_DatabaseInDevelopment_TambemOcultaQuestionsInDevelopment()
    {
        // 3 em dev, 2 de produção
        var db = FakeQuestionDatabase.DevelopmentWith(devCount: 3, prodCount: 2);

        var result = QuestionFilterService.FilterQuestions(db);

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(q => !q.questionInDevelopment),
            "Modo desenvolvimento também não deve retornar questões marcadas como dev");
    }

    [Test]
    public void FilterQuestions_DatabaseInDevelopment_SemDevQuestions_RetornaTodasAsVisiveis()
    {
        // Banco em modo dev, mas nenhuma questão marcada como dev
        var db = FakeQuestionDatabase.DevelopmentWith(devCount: 0, prodCount: 3);

        var result = QuestionFilterService.FilterQuestions(db);

        Assert.AreEqual(3, result.Count);
        Assert.IsTrue(result.All(q => !q.questionInDevelopment));
    }

    [Test]
    public void FilterQuestions_DatabaseInDevelopment_RetornaMesmoConjuntoVisivel()
    {
        // O mesmo conjunto de questões deve ocultar questionInDevelopment
        // tanto em produção quanto em desenvolvimento.
        var questions = new List<Question>
        {
            QuestionTestHelpers.MakeQuestion(1, inDevelopment: false),
            QuestionTestHelpers.MakeQuestion(2, inDevelopment: true),
            QuestionTestHelpers.MakeQuestion(3, inDevelopment: false),
            QuestionTestHelpers.MakeQuestion(4, inDevelopment: true),
        };

        var dbProd = new FakeQuestionDatabase { IsInDevelopmentMode = false, Questions = questions };
        var dbDev  = new FakeQuestionDatabase { IsInDevelopmentMode = true,  Questions = questions };

        var prodResult = QuestionFilterService.FilterQuestions(dbProd);
        var devResult  = QuestionFilterService.FilterQuestions(dbDev);

        Assert.AreEqual(2, prodResult.Count);
        Assert.AreEqual(2, devResult.Count);
        CollectionAssert.AreEquivalent(
            prodResult.Select(q => q.questionNumber).ToList(),
            devResult.Select(q => q.questionNumber).ToList());
    }

    // =======================================================
    // GetQuestionByNumber
    // =======================================================

    [Test]
    public void GetQuestionByNumber_NumeroExistente_RetornaQuestaoCorreta()
    {
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 5);

        var result = QuestionFilterService.GetQuestionByNumber(db, questionNumber: 3);

        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.questionNumber);
    }

    [Test]
    public void GetQuestionByNumber_NumeroInexistente_RetornaNull()
    {
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 3);

        var result = QuestionFilterService.GetQuestionByNumber(db, questionNumber: 99);

        Assert.IsNull(result);
    }

    [Test]
    public void GetQuestionByNumber_QuestionInDevelopment_NaoEncontra()
    {
        // Questão número 4 está marcada como em desenvolvimento
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 3, devCount: 2);
        // As questões em desenvolvimento são os números 4 e 5 (geradas por último no helper)
        int devQuestionNumber = db.Questions.First(q => q.questionInDevelopment).questionNumber;

        var result = QuestionFilterService.GetQuestionByNumber(db, devQuestionNumber);

        Assert.IsNull(result, "Questão em desenvolvimento não deve ser encontrada fora do preview mode");
    }

    [Test]
    public void GetQuestionByNumber_DatabaseNulo_RetornaNull()
    {
        LogAssert.Expect(LogType.Error, "[QuestionFilterService] Database is null");

        var result = QuestionFilterService.GetQuestionByNumber(null, 1);

        Assert.IsNull(result);
    }

    // =======================================================
    // GetQuestionsByLevel
    // =======================================================

    [Test]
    public void GetQuestionsByLevel_RetornaApenasQuestoesDoNivelSolicitado()
    {
        var db = new FakeQuestionDatabase { IsInDevelopmentMode = false };
        db.Questions.AddRange(QuestionTestHelpers.MakeQuestions(nivel1: 3, nivel2: 2));

        var nivel1 = QuestionFilterService.GetQuestionsByLevel(db, level: 1);
        var nivel2 = QuestionFilterService.GetQuestionsByLevel(db, level: 2);

        Assert.AreEqual(3, nivel1.Count);
        Assert.AreEqual(2, nivel2.Count);
        Assert.IsTrue(nivel1.All(q => q.questionLevel == 1));
        Assert.IsTrue(nivel2.All(q => q.questionLevel == 2));
    }

    [Test]
    public void GetQuestionsByLevel_NivelInexistente_RetornaListaVazia()
    {
        var db = new FakeQuestionDatabase { IsInDevelopmentMode = false };
        db.Questions.AddRange(QuestionTestHelpers.MakeQuestions(nivel1: 3));

        var result = QuestionFilterService.GetQuestionsByLevel(db, level: 2);

        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void GetQuestionsByLevel_RespeitaFiltroDeQuestionInDevelopment()
    {
        // Nível 1 tem 2 visíveis + 1 em desenvolvimento; deve retornar só 2
        var db = new FakeQuestionDatabase { IsInDevelopmentMode = false };
        db.Questions.Add(QuestionTestHelpers.MakeQuestion(1, level: 1, inDevelopment: false));
        db.Questions.Add(QuestionTestHelpers.MakeQuestion(2, level: 1, inDevelopment: false));
        db.Questions.Add(QuestionTestHelpers.MakeQuestion(3, level: 1, inDevelopment: true));

        var result = QuestionFilterService.GetQuestionsByLevel(db, level: 1);

        Assert.AreEqual(2, result.Count);
    }

    // =======================================================
    // GetTotalQuestionsCount
    // =======================================================

    [Test]
    public void GetTotalQuestionsCount_ContaApenasQuestoesVisiveis()
    {
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 4, devCount: 2);

        int count = QuestionFilterService.GetTotalQuestionsCount(db);

        Assert.AreEqual(4, count,
            "GetTotalQuestionsCount deve contar apenas questões visíveis, não o total bruto");
    }

    [Test]
    public void GetTotalQuestionsCount_DatabaseInDevelopment_ContaApenasQuestoesVisiveis()
    {
        var db = FakeQuestionDatabase.DevelopmentWith(devCount: 3, prodCount: 5);

        int count = QuestionFilterService.GetTotalQuestionsCount(db);

        Assert.AreEqual(5, count);
    }

    [Test]
    public void GetTotalQuestionsCount_DatabaseNulo_RetornaZero()
    {
        LogAssert.Expect(LogType.Error, "[QuestionFilterService] Database is null");

        int count = QuestionFilterService.GetTotalQuestionsCount(null);

        Assert.AreEqual(0, count);
    }

    // =======================================================
    // GetAvailableQuestionNumbers
    // =======================================================

    [Test]
    public void GetAvailableQuestionNumbers_RetornaNumerosDasQuestoesVisiveis()
    {
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 3, devCount: 2);

        var numbers = QuestionFilterService.GetAvailableQuestionNumbers(db);

        // Questões prod são as de número 1, 2, 3 (geradas primeiro pelo helper)
        Assert.AreEqual(3, numbers.Count);
        CollectionAssert.AreEquivalent(new[] { 1, 2, 3 }, numbers);
    }

    [Test]
    public void GetAvailableQuestionNumbers_SemQuestoesVisiveis_RetornaListaVazia()
    {
        var db = FakeQuestionDatabase.ProductionWith(prodCount: 0, devCount: 3);

        var numbers = QuestionFilterService.GetAvailableQuestionNumbers(db);

        Assert.AreEqual(0, numbers.Count);
    }
}
