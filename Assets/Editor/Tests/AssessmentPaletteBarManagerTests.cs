using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class AssessmentPaletteBarManagerTests
{
    [Test]
    public void GetColorForLevel_ReturnsCorrectColorsAccordingToDifficulty()
    {
        var go = new GameObject("TestPaletteManager");
        var manager = go.AddComponent<AssessmentPaletteBarManager>();

        Color basic = manager.GetColorForLevel(1);
        Color intermediate = manager.GetColorForLevel(2);
        Color hard = manager.GetColorForLevel(3);

        // Nível 1 - Básico (Verde)
        Assert.AreEqual(0.18f, basic.r, 0.05f);
        Assert.AreEqual(0.8f, basic.g, 0.05f);

        // Nível 2 - Intermediário (Laranja)
        Assert.AreEqual(0.95f, intermediate.r, 0.05f);
        Assert.AreEqual(0.61f, intermediate.g, 0.05f);

        // Nível 3 - Difícil (Roxo)
        Assert.AreEqual(0.61f, hard.r, 0.05f);
        Assert.AreEqual(0.35f, hard.g, 0.05f);
        Assert.AreEqual(0.71f, hard.b, 0.05f);

        // Níveis acima de 3 devem cair no Difícil
        Assert.AreEqual(hard, manager.GetColorForLevel(4));

        Object.DestroyImmediate(go);
    }
}
