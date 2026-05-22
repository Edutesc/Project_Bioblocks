using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingSpinnerComponent : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject spinnerContainer;
    [SerializeField] private Image spinnerBackground;
    [SerializeField] private Image spinnerBorder;
    [SerializeField] private TMP_Text messageLabel;

    [Header("Configuration")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private bool rotateBackground = false;
    [SerializeField] private bool showOnAwake = false;

    private CanvasGroup canvasGroup;
    private GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        graphicRaycaster = GetComponent<GraphicRaycaster>();

        if (spinnerContainer == null)
        {
            Debug.LogError("[LoadingSpinner] spinnerContainer não foi vinculado no prefab.");
        }

        if (showOnAwake)
        {
            ShowSpinner();
        }
        else
        {
            HideSpinner();
        }
    }

    private void Update()
    {
        if (spinnerContainer == null || !spinnerContainer.activeSelf)
            return;

        if (spinnerBorder != null)
        {
            spinnerBorder.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        if (rotateBackground && spinnerBackground != null)
        {
            spinnerBackground.transform.Rotate(0, 0, rotationSpeed * 0.2f * Time.deltaTime);
        }
    }

    public void ShowSpinner()
    {
        if (spinnerContainer != null)
        {
            spinnerContainer.SetActive(true);
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        if (graphicRaycaster != null)
        {
            graphicRaycaster.enabled = true;
        }

        SetRaycastTargets(true);

        Debug.Log("[LoadingSpinner] Spinner local mostrado.");
    }

    public void HideSpinner()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (graphicRaycaster != null)
        {
            graphicRaycaster.enabled = false;
        }

        SetRaycastTargets(false);

        if (spinnerContainer != null)
        {
            spinnerContainer.SetActive(false);
        }

        Debug.Log("[LoadingSpinner] Spinner local escondido.");
    }

    public void SetMessage(string message)
    {
        if (messageLabel != null)
        {
            messageLabel.text = message;
        }
    }

    private void SetRaycastTargets(bool enabled)
    {
        if (spinnerContainer == null)
            return;

        Graphic[] graphics = spinnerContainer.GetComponentsInChildren<Graphic>(true);

        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = enabled;
        }
    }
}