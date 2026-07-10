using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QuestionSystem;
using Edutesc.BioBlocks.Assessment;

public class AssessmentGeneratorTests
{
    private FakeQuestionLocalRepository _fakeRepository;
    private AssessmentGenerator _generator;

    [SetUp]
    public void Setup()
    {
        _fakeRepository = new FakeQuestionLocalRepository();
        _generator = new AssessmentGenerator(_fakeRepository);
    }

    [Test]
    public void GenerateAssessment_ShouldReturnExactProportions_WhenEnoughQuestionsExist()
    {
        var questions = new List<Question>();
        for (int i = 0; i < 20; i++) questions.Add(new Question { globalId = $"B_{i}", questionLevel = 1 });
        for (int i = 0; i < 20; i++) questions.Add(new Question { globalId = $"I_{i}", questionLevel = 2 });
        for (int i = 0; i < 20; i++) questions.Add(new Question { globalId = $"H_{i}", questionLevel = 3 });
        
        _fakeRepository.SaveQuestions(questions);

        var assessment = _generator.GenerateAssessment();

        Assert.AreEqual(10, assessment.Count, "O assessment deve conter exatamente 10 questões.");
        
        int basicCount = assessment.Count(q => q.questionLevel <= 1);
        int intermediateCount = assessment.Count(q => q.questionLevel == 2);
        int hardCount = assessment.Count(q => q.questionLevel >= 3);

        Assert.AreEqual(4, basicCount, "Devem haver 4 questões básicas (fáceis).");
        Assert.AreEqual(3, intermediateCount, "Devem haver 3 questões intermediárias.");
        Assert.AreEqual(3, hardCount, "Devem haver 3 questões difíceis.");
    }

    [Test]
    public void GenerateAssessment_ShouldFallback_WhenNotEnoughQuestions()
    {
        var questions = new List<Question>();
        for (int i = 0; i < 20; i++) questions.Add(new Question { globalId = $"B_{i}", questionLevel = 1 });
        for (int i = 0; i < 20; i++) questions.Add(new Question { globalId = $"I_{i}", questionLevel = 2 });
        for (int i = 0; i < 2; i++) questions.Add(new Question { globalId = $"H_{i}", questionLevel = 3 }); // ONLY 2
        
        _fakeRepository.SaveQuestions(questions);

        var assessment = _generator.GenerateAssessment();

        Assert.AreEqual(10, assessment.Count, "Mesmo com fallback, deve garantir 10 questões.");
        
        int hardCount = assessment.Count(q => q.questionLevel >= 3);
        Assert.AreEqual(2, hardCount, "Trouxe as únicas 2 difíceis possíveis.");
    }

    [Test]
    public void GenerateAssessment_ShouldProduceDifferentAssessments_WhenRunMultipleTimes()
    {
        var questions = new List<Question>();
        for (int i = 0; i < 50; i++) questions.Add(new Question { globalId = $"B_{i}", questionLevel = 1 });
        for (int i = 0; i < 50; i++) questions.Add(new Question { globalId = $"I_{i}", questionLevel = 2 });
        for (int i = 0; i < 50; i++) questions.Add(new Question { globalId = $"H_{i}", questionLevel = 3 });
        
        _fakeRepository.SaveQuestions(questions);

        var run1 = _generator.GenerateAssessment();
        var run2 = _generator.GenerateAssessment();

        string ids1 = string.Join(",", run1.Select(q => q.globalId));
        string ids2 = string.Join(",", run2.Select(q => q.globalId));

        Assert.AreNotEqual(ids1, ids2, "O embaralhador deve garantir que provas em sequência tenham IDs/ordens diferentes.");
    }
}
