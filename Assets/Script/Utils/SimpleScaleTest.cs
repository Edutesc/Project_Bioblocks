using UnityEngine;
using UnityEngine.UI;

public class SimpleScaleTest : MonoBehaviour
{
    private Button button;
    private RectTransform rect;

    private void Start()
    {
        button = GetComponent<Button>();
        rect = GetComponent<RectTransform>();

        Debug.Log("Scale inicial: " + rect.localScale);
        button.onClick.AddListener(TestScale);
    }

    private void TestScale()
    {
        Debug.Log("=== TESTE DE ESCALA ===");
        Debug.Log("Escala ANTES: " + rect.localScale);

        rect.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Debug.Log("Escala DEPOIS: " + rect.localScale);
    }
}