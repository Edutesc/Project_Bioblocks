using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Edutesc.BioBlocks.Assessment;
using Edutesc.BioBlocks.Core.Models;

public class AssessmentRegisterManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_InputField inputRA;
    [SerializeField] private Button btnStartAssessment;
    [SerializeField] private Button btnBackMenu;
    [SerializeField] private TMP_Text errorText;

    private AssessmentGenerator _generator;

    private void Start()
    {
        var questionRepo = AppContext.QuestionLocal;
        _generator = new AssessmentGenerator(questionRepo);

        if (btnStartAssessment != null)
        {
            btnStartAssessment.onClick.AddListener(OnStartAssessmentClicked);
        }

        if (btnBackMenu != null)
        {
            btnBackMenu.onClick.AddListener(OnBackClicked);
        }

        if (errorText != null)
        {
            errorText.gameObject.SetActive(false);
        }
    }

    public void OnStartAssessmentClicked()
    {
        string studentName = inputName != null ? inputName.text.Trim() : "";
        string ra = inputRA != null ? inputRA.text.Trim() : "";

        if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(ra))
        {
            ShowError("Por favor, preencha o Nome e o RA.");
            return;
        }

        var questions = _generator.GenerateAssessment();

        if (questions == null || questions.Count == 0)
        {
            ShowError("Nenhuma questão encontrada no banco de dados local.");
            return;
        }

        AssessmentSession.StartNew(studentName, ra, questions);

        SceneManager.LoadScene("AssessmentScene");
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("PathwayScene");
    }

    private void ShowError(string message)
    {
        if (errorText != null)
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError($"[AssessmentRegister] {message}");
        }
    }
}
