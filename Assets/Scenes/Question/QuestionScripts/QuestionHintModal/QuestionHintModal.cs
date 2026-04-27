using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuestionSystem;
using UnityEngine.SceneManagement;

public class QuestionHintModal : MonoBehaviour
{
    [Header("Canal de dados — mesmo asset HintChannel")]
    [SerializeField] private HintChannelSO hintChannel;

    [Header("UI — referências da QuestionModalScene")]
    [SerializeField] private TextMeshProUGUI hintPanelTitle;  // HintTitle
    [SerializeField] private Transform hintContentParent;      // Content
    [SerializeField] private Button closeHintPanelButton;      // ClosePanelButton

    [Header("Prefabs de Dica")]
    [SerializeField] private GameObject textHintPrefab;
    [SerializeField] private GameObject imageHintPrefab;
    [SerializeField] private GameObject linkHintPrefab;

    private void Start()
    {
        if (closeHintPanelButton != null)
            closeHintPanelButton.onClick.AddListener(CloseModal);

        PopulateFromChannel();
    }

    private void PopulateFromChannel()
    {
        if (hintChannel == null || !hintChannel.isReady)
        {
            Debug.LogWarning("HintModal - Canal sem dados.");
            return;
        }

        if (hintPanelTitle != null)
            hintPanelTitle.text = hintChannel.panelTitle;

        PopulatePanel(hintChannel.hints);
    }

    private void PopulatePanel(System.Collections.Generic.List<Hint> hints)
    {
        ClearPanelContent();
        foreach (var hint in hints)
        {
            switch (hint)
            {
                case TextHint th:  SpawnTextHint(th);  break;
                case ImageHint ih: SpawnImageHint(ih); break;
                case LinkHint lh:  SpawnLinkHint(lh);  break;
            }
        }
    }

    private void ClearPanelContent()
    {
        if (hintContentParent == null) return;
        foreach (Transform child in hintContentParent)
            Destroy(child.gameObject);
    }

    private void SpawnTextHint(TextHint hint)
    {
        if (textHintPrefab == null) return;
        var go = Instantiate(textHintPrefab, hintContentParent);
        var tmp = go.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null) tmp.text = hint.text;
    }

    private void SpawnImageHint(ImageHint hint)
    {
        if (imageHintPrefab == null) return;
        var go = Instantiate(imageHintPrefab, hintContentParent);
        var img = go.GetComponentInChildren<Image>();
        if (img != null) img.sprite = Resources.Load<Sprite>(hint.imagePath);
    }

    private void SpawnLinkHint(LinkHint hint)
    {
        if (linkHintPrefab == null) return;
        var go = Instantiate(linkHintPrefab, hintContentParent);
        var tmp = go.GetComponentInChildren<TextMeshProUGUI>();
        var btn = go.GetComponentInChildren<Button>();
        if (tmp != null) tmp.text = hint.link;
        if (btn != null)
        {
            string url = hint.link;
            btn.onClick.AddListener(() => Application.OpenURL(url));
        }
    }

    private void CloseModal()
    {
        hintChannel.Clear();
        SceneManager.UnloadSceneAsync("QuestionModalScene");
    }
}