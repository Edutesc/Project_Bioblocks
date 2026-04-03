using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Garante que o Canvas da ModalScene seja renderizado por cima de tudo
/// e que o Overlay bloqueie os cliques na MainScene.
///
/// Coloque este script no mesmo GameObject que possui o componente Canvas
/// dentro da ModalScene.
///
/// Configurações recomendadas no Inspector:
///   - Canvas Render Mode: Screen Space - Overlay
///   - Sort Order: 10  (valor alto para ficar acima dos Canvas da MainScene)
/// </summary>
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public class ModalCanvasSetup : MonoBehaviour
{
    [Tooltip("Sort Order do Canvas do modal. Use um valor alto (ex: 10) para ficar acima da cena principal.")]
    [SerializeField] private int sortOrder = 10;

    [Tooltip("Referência ao painel de overlay que bloqueia interações com a cena de fundo.")]
    [SerializeField] private Image overlayImage;

    [Tooltip("Cor do overlay (use alfa para transparência).")]
    [SerializeField] private Color overlayColor = new Color(0f, 0f, 0f, 0.6f);

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = sortOrder;

        ConfigureOverlay();
    }

    private void ConfigureOverlay()
    {
        if (overlayImage == null)
        {
            Debug.LogWarning("Overlay Image não atribuído no ModalCanvasSetup. O bloqueio de fundo pode não funcionar.");
            return;
        }

        // Aplica cor semi-transparente definida no Inspector
        overlayImage.color = overlayColor;

        // Garante que o Overlay ocupa a tela inteira
        RectTransform rt = overlayImage.GetComponent<RectTransform>();

        if (rt == null)
        {
            Debug.LogError("Overlay não possui RectTransform.");
            return;
        }

        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // Garante que o Raycast Target esteja ativo para bloquear cliques no fundo
        overlayImage.raycastTarget = true;
    }
}