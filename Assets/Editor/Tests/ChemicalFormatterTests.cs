using NUnit.Framework;

[TestFixture]
public class ChemicalFormatterTests
{
    [Test]
    public void Format_UnicodeSuperscriptMinus_ConvertsToTmpSupTag()
    {
        string input = "Uma solucao com ions OH\u207B";

        string result = ChemicalFormatter.Format(input);

        Assert.AreEqual("Uma solucao com ions OH<sup>-</sup>", result);
    }

    [Test]
    public void Format_UnicodeSubscriptDigit_ConvertsToTmpSubTag()
    {
        string input = "H\u2082O";

        string result = ChemicalFormatter.Format(input);

        Assert.AreEqual("H<sub>2</sub>O", result);
    }

    [Test]
    public void Format_CaretChargeSyntax_StillConvertsToTmpSupTag()
    {
        string input = "OH^-";

        string result = ChemicalFormatter.Format(input);

        Assert.AreEqual("OH<sup>-</sup>", result);
    }
}
