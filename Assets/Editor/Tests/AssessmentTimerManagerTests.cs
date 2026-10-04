using NUnit.Framework;

[TestFixture]
public class AssessmentTimerManagerTests
{
    [Test]
    public void FormatTime_AboveSixtySeconds_ReturnsMMSSFormat()
    {
        // 15 minutos exatos
        Assert.AreEqual("15:00", AssessmentTimerManager.FormatTime(900f));

        // 14 minutos e 59 segundos
        Assert.AreEqual("14:59", AssessmentTimerManager.FormatTime(899f));

        // 10 minutos
        Assert.AreEqual("10:00", AssessmentTimerManager.FormatTime(600f));

        // 1 minuto e 1 segundo (61 segundos)
        Assert.AreEqual("01:01", AssessmentTimerManager.FormatTime(61f));
    }

    [Test]
    public void FormatTime_AtOrBelowSixtySeconds_ReturnsSecondsFormat()
    {
        // Exatamente 60 segundos
        Assert.AreEqual("60s", AssessmentTimerManager.FormatTime(60f));

        // 59 segundos
        Assert.AreEqual("59s", AssessmentTimerManager.FormatTime(59f));

        // 10 segundos
        Assert.AreEqual("10s", AssessmentTimerManager.FormatTime(10f));

        // 1 segundo
        Assert.AreEqual("1s", AssessmentTimerManager.FormatTime(1f));
    }

    [Test]
    public void FormatTime_ZeroOrNegative_ReturnsZeroSeconds()
    {
        Assert.AreEqual("0s", AssessmentTimerManager.FormatTime(0f));
        Assert.AreEqual("0s", AssessmentTimerManager.FormatTime(-5f));
    }
}
