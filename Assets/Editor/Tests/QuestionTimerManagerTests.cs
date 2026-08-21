using NUnit.Framework;

[TestFixture]
public class QuestionTimerManagerTests
{
    [Test]
    public void GetDurationForQuestionLevel_Level1_Retorna30Segundos()
    {
        Assert.AreEqual(30f, QuestionTimerManager.GetDurationForQuestionLevel(1));
    }

    [Test]
    public void GetDurationForQuestionLevel_Level2_Retorna60Segundos()
    {
        Assert.AreEqual(60f, QuestionTimerManager.GetDurationForQuestionLevel(2));
    }

    [Test]
    public void GetDurationForQuestionLevel_Level3_Retorna120Segundos()
    {
        Assert.AreEqual(120f, QuestionTimerManager.GetDurationForQuestionLevel(3));
    }

    [Test]
    public void GetDurationForQuestionLevel_LevelInesperado_Retorna30Segundos()
    {
        Assert.AreEqual(30f, QuestionTimerManager.GetDurationForQuestionLevel(99));
    }

    [Test]
    public void GetDurationForQuestionLevel_PreviewMode_Retorna5Segundos()
    {
        Assert.AreEqual(5f, QuestionTimerManager.GetDurationForQuestionLevel(1, isPreviewMode: true));
        Assert.AreEqual(5f, QuestionTimerManager.GetDurationForQuestionLevel(2, isPreviewMode: true));
        Assert.AreEqual(5f, QuestionTimerManager.GetDurationForQuestionLevel(3, isPreviewMode: true));
    }
}
