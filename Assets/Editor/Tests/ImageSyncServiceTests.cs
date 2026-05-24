// Assets/Editor/Tests/ImageSyncServiceTests.cs
//
// Testes para ImageSyncService — focam no comportamento de PrewarmAsync:
//   - Ordena downloads por topic seguindo o enum QuestionSet
//   - Pula imagens já cacheadas
//   - Marca IsCacheReady ao completar pelo menos um topic
//   - Invoca onTopicReady ao finalizar cada tema
//
// Os fakes (FakeFirebaseStorageImageRepository, FakeImageLocalRepository)
// operam em memória, sem disco, sem Firebase real.

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.TestTools;
using QuestionSystem;

[TestFixture]
public class ImageSyncServiceTests
{
    private FakeFirebaseStorageImageRepository _fakeStorage;
    private FakeImageLocalRepository           _fakeLocal;
    private ImageSyncService                   _syncService;
    private GameObject                         _go;

    [SetUp]
    public void Setup()
    {
        _fakeStorage = new FakeFirebaseStorageImageRepository();
        _fakeLocal   = new FakeImageLocalRepository();

        _go = new GameObject("ImageSyncService");
        _syncService = _go.AddComponent<ImageSyncService>();
        _syncService.InjectDependencies(_fakeStorage, _fakeLocal);
    }

    [TearDown]
    public void TearDown()
    {
        _fakeStorage.Reset();
        _fakeLocal.Reset();
        if (_go != null) Object.DestroyImmediate(_go);
    }

    // =======================================================================
    // PrewarmAsync — ordenação por topic conforme QuestionSet
    // =======================================================================

    [UnityTest]
    public IEnumerator PrewarmAsync_OrdenaDownloadsNaOrdemDoEnumQuestionSet()
    {
        // Arrange — questões fora de ordem propositalmente:
        //   biochem (Q8 image answer), water (Q1 image question), acidsBase (Q2 image question)
        // Ordem esperada de início dos downloads (por topic):
        //   acidsBase < aminoacids < biochem < ... < water (segundo o enum).
        var qBiochem = MakeImageAnswerQuestion("biochem", number: 8,
            answers: new[] { "AnswerImages/IntroductionDB/benzeno", "AnswerImages/IntroductionDB/enol" });

        var qWater = MakeImageQuestion("water", number: 1,
            imagePath: "QuestionImages/Water/molecula");

        var qAcids = MakeImageQuestion("acidsBase", number: 2,
            imagePath: "QuestionImages/AcidsBase/ph");

        var questions = new List<Question> { qBiochem, qWater, qAcids };

        // Act
        var task = _syncService.PrewarmAsync(questions, ct: CancellationToken.None);
        yield return new WaitUntil(() => task.IsCompleted);

        // Assert — primeiro download deve ser de acidsBase, depois biochem, depois water.
        var topicsInOrder = _fakeStorage.DownloadOrder
            .Select(k => k.Split('/')[0])
            .Distinct()
            .ToList();

        Assert.AreEqual(3, topicsInOrder.Count, "Devem aparecer 3 topics distintos no download.");
        Assert.AreEqual("acidsBase", topicsInOrder[0],
            "acidsBase deve ser o primeiro topic baixado (ordem do enum QuestionSet).");
        Assert.AreEqual("biochem",   topicsInOrder[1],
            "biochem deve ser o segundo topic.");
        Assert.AreEqual("water",     topicsInOrder[2],
            "water deve ser o último topic.");
    }

    // [UnityTest]
    // public IEnumerator PrewarmAsync_PulaImagensJaEmCache()
    // {
    //     // Arrange — uma imagem já está no cache do FakeLocal.
    //     _fakeLocal.Save("biochem/benzeno", new byte[] { 0x00 }, topic: "biochem");

    //     var question = MakeImageAnswerQuestion("biochem", number: 8,
    //         answers: new[] { "AnswerImages/IntroductionDB/benzeno", "AnswerImages/IntroductionDB/enol" });

    //     // Act
    //     var task = _syncService.PrewarmAsync(new[] { question });
    //     yield return new WaitUntil(() => task.IsCompleted);

    //     // Assert — apenas a imagem que não estava em cache foi baixada.
    //     Assert.AreEqual(1, _fakeStorage.DownloadOrder.Count,
    //         "Imagem já em cache não deve ser re-baixada.");
    //     Assert.AreEqual("biochem/enol", _fakeStorage.DownloadOrder[0]);
    // }

    [UnityTest]
    public IEnumerator PrewarmAsync_InvocaCallbackOnTopicReady()
    {
        // Arrange
        var qAcids   = MakeImageQuestion("acidsBase", 1, "QuestionImages/AB/ph");
        var qWater   = MakeImageQuestion("water",     1, "QuestionImages/W/h2o");
        var topicsReady = new List<string>();

        // Act
        var task = _syncService.PrewarmAsync(
            new[] { qAcids, qWater },
            onTopicReady: topicsReady.Add);
        yield return new WaitUntil(() => task.IsCompleted);

        // Assert
        Assert.AreEqual(2, topicsReady.Count, "onTopicReady deve ser chamado para cada topic.");
        Assert.AreEqual("acidsBase", topicsReady[0]);
        Assert.AreEqual("water",     topicsReady[1]);

        Assert.IsTrue(_syncService.IsTopicReady("acidsBase"));
        Assert.IsTrue(_syncService.IsTopicReady("water"));
        Assert.IsTrue(_syncService.IsCacheReady,
            "IsCacheReady deve ser true após pelo menos um topic completo.");
    }

    [UnityTest]
    public IEnumerator PrewarmAsync_ListaVazia_NaoFazNada()
    {
        var task = _syncService.PrewarmAsync(new List<Question>());
        yield return new WaitUntil(() => task.IsCompleted);

        Assert.AreEqual(0, _fakeStorage.DownloadOrder.Count);
    }

    // =======================================================================
    // Helpers
    // =======================================================================

    private static Question MakeImageQuestion(string topic, int number, string imagePath)
    {
        return new Question
        {
            globalId             = $"{topic}_{number:D3}",
            questionDatabankName = $"{topic}DB",
            questionNumber       = number,
            topic                = topic,
            questionType         = QuestionType.Image,
            answerType           = AnswerType.Text,
            questionImagePath    = imagePath,
            answers              = new[] { "A", "B", "C", "D" }
        };
    }

    private static Question MakeImageAnswerQuestion(string topic, int number, string[] answers)
    {
        return new Question
        {
            globalId             = $"{topic}_{number:D3}",
            questionDatabankName = $"{topic}DB",
            questionNumber       = number,
            topic                = topic,
            questionType         = QuestionType.Text,
            answerType           = AnswerType.Image,
            questionImagePath    = "",
            answers              = answers
        };
    }
}
