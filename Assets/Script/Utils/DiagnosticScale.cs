using UnityEngine;
using UnityEngine.UI;

public class DiagnosticScale : MonoBehaviour
{
    private RectTransform targetTransform;
    private Vector3 lastScale;

    private void Start()
    {
        targetTransform = GetComponent<RectTransform>();
        lastScale = targetTransform.localScale;
    }

    private void Update()
    {
        if (targetTransform.localScale != lastScale)
        {
            Debug.LogWarning("ESCALA MUDOU! De " + lastScale + " para " + targetTransform.localScale +
                           " (algo está resetando a escala)");
            lastScale = targetTransform.localScale;
        }
    }

    public void TestScale()
    {
        Debug.Log("Tentando definir escala para (0.5, 0.5, 0.5)");
        targetTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Debug.Log("Escala após tentativa: " + targetTransform.localScale);
    }
}