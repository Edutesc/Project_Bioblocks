using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Edutesc.BioBlocks.Assessment;
using Edutesc.BioBlocks.Core.Models;

public class AssessmentIntroManager : MonoBehaviour
{
    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI topicsText;
    [SerializeField] private TextMeshProUGUI totalQuestionsText;
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private TextMeshProUGUI disclaimerText;
    [SerializeField] private TextMeshProUGUI errorText;

    [Header("Buttons")]
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnBack;

    [Header("Containers / Loading")]
    [SerializeField] private GameObject contentContainer;
    [SerializeField] private GameObject loadingContainer;

    [Header("Fallback & Config Settings")]
    [SerializeField] private string defaultAssessmentId = "2026-3-aminoacidos-proteinas-enzimas";
    [SerializeField] private bool useDocumentTitle = false;

    private AssessmentGenerator _generator;
    private AssessmentData _loadedAssessment;

    private async void Start()
    {
        var questionRepo = AppContext.QuestionLocal;
        _generator = new AssessmentGenerator(questionRepo);

        if (btnStart != null)
        {
            btnStart.onClick.AddListener(OnStartClicked);
            btnStart.interactable = false;
        }

        if (btnBack != null)
        {
            btnBack.onClick.AddListener(OnBackClicked);
        }

        if (errorText != null)
        {
            errorText.gameObject.SetActive(false);
        }

        await LoadAssessmentData();
    }

    private async Task LoadAssessmentData()
    {
        SetLoading(true);

        try
        {
            var repo = AppContext.FirestoreAssessment;
            if (repo != null)
            {
                if (!string.IsNullOrEmpty(defaultAssessmentId))
                {
                    _loadedAssessment = await repo.GetAssessmentAsync(defaultAssessmentId);
                }
                else
                {
                    _loadedAssessment = await repo.GetActiveAssessmentAsync();
                }
            }

            if (_loadedAssessment == null)
            {
                _loadedAssessment = CreateDefaultAssessment(defaultAssessmentId);
            }

            UpdateUI(_loadedAssessment);

            if (btnStart != null)
            {
                btnStart.interactable = true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[AssessmentIntroManager] Erro ao carregar dados da avaliação: {e.Message}");
            _loadedAssessment = CreateDefaultAssessment(defaultAssessmentId);
            UpdateUI(_loadedAssessment);
            
            if (btnStart != null)
            {
                btnStart.interactable = true;
            }
        }
        finally
        {
            SetLoading(false);
        }
    }

    private void UpdateUI(AssessmentData data)
    {
        if (data == null) return;

        if (titleText != null)
        {
            titleText.text = (useDocumentTitle && !string.IsNullOrEmpty(data.Title))
                ? data.Title
                : "Avaliação formativa";
        }

        if (topicsText != null)
        {
            if (data.DisplayTopics != null && data.DisplayTopics.Count > 0)
            {
                var sb = new StringBuilder("Temas:\n");
                foreach (var topic in data.DisplayTopics)
                {
                    sb.AppendLine($"• {topic}");
                }
                topicsText.text = sb.ToString().TrimEnd();
            }
            else
            {
                topicsText.text = "Temas:\n• Aminoácidos\n• Proteínas\n• Enzimas";
            }
        }

        if (totalQuestionsText != null)
        {
            int total = data.TotalQuestions > 0 ? data.TotalQuestions : 10;
            totalQuestionsText.text = $"Esta atividade contém {total} questões:";
        }

        if (durationText != null)
        {
            int duration = data.DurationMinutes > 0 ? data.DurationMinutes : 15;
            durationText.text = $"Prazo para conclusão: {duration} minutos";
        }

        if (disclaimerText != null)
        {
            disclaimerText.text = "Seu progresso será associado à sua conta do BioBlocks.\nO resultado não será utilizado para atribuição de conceitos na disciplina.";
        }
    }

    private void OnStartClicked()
    {
        if (_loadedAssessment == null)
        {
            _loadedAssessment = CreateDefaultAssessment(defaultAssessmentId);
        }

        var questions = _generator.GenerateAssessment(_loadedAssessment);

        if (questions == null || questions.Count == 0)
        {
            ShowError("Nenhuma questão encontrada para os temas desta avaliação no banco local.");
            return;
        }

        string userId = UserDataStore.CurrentUserData?.UserId ?? "guest-user";

        AssessmentSession.StartNew(_loadedAssessment, userId, questions);

        SceneManager.LoadScene("AssessmentScene");
    }

    private void OnBackClicked()
    {
        SceneManager.LoadScene("PathwayScene");
    }

    private void SetLoading(bool isLoading)
    {
        if (loadingContainer != null)
        {
            loadingContainer.SetActive(isLoading);
        }

        if (contentContainer != null)
        {
            contentContainer.SetActive(!isLoading);
        }
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
            Debug.LogError($"[AssessmentIntroManager] {message}");
        }
    }

    private AssessmentData CreateDefaultAssessment(string assessmentId = null)
    {
        string id = !string.IsNullOrEmpty(assessmentId) ? assessmentId : defaultAssessmentId;

        switch (id)
        {
            case "2026-3-introducao-acidos-bases-tampoes":
                return new AssessmentData
                {
                    AssessmentId = id,
                    AssessmentType = "introducao-acidos-bases-tampoes",
                    AcademicTerm = "2026-3",
                    CourseId = "bioquimica",
                    Title = "Avaliação formativa",
                    Description = "Atividade formativa para testar seus conhecimentos.",
                    DisplayTopics = new List<string> { "Introdução à Bioquímica", "Água", "Ácidos, bases e tampões" },
                    AllowedDatabanks = new List<string> { "BiochemistryIntroductionQuestionDatabase", "WaterQuestionDataBase", "AcidBaseBufferQuestionDataBase" },
                    QuestionDistribution = new QuestionDistribution { Basic = 4, Intermediate = 3, Hard = 3 },
                    TotalQuestions = 10,
                    DurationMinutes = 15,
                    AllowRetakes = true,
                    Enabled = true
                };

            case "2026-3-lipideos-mambranas":
            case "2026-3-lipideos-membranas":
                return new AssessmentData
                {
                    AssessmentId = id,
                    AssessmentType = "lipideos-membranas",
                    AcademicTerm = "2026-3",
                    CourseId = "bioquimica",
                    Title = "Avaliação formativa",
                    Description = "Atividade formativa para testar seus conhecimentos.",
                    DisplayTopics = new List<string> { "Lipídeos", "Membranas" },
                    AllowedDatabanks = new List<string> { "LipidsQuestionDataBase", "MembranesQuestionDatabase" },
                    QuestionDistribution = new QuestionDistribution { Basic = 4, Intermediate = 3, Hard = 3 },
                    TotalQuestions = 10,
                    DurationMinutes = 15,
                    AllowRetakes = true,
                    Enabled = true
                };

            case "2026-3-carboidratos":
                return new AssessmentData
                {
                    AssessmentId = id,
                    AssessmentType = "carboidratos",
                    AcademicTerm = "2026-3",
                    CourseId = "bioquimica",
                    Title = "Avaliação formativa",
                    Description = "Atividade formativa para testar seus conhecimentos.",
                    DisplayTopics = new List<string> { "Carboidratos" },
                    AllowedDatabanks = new List<string> { "CarbohydratesQuestionDataBase" },
                    QuestionDistribution = new QuestionDistribution { Basic = 4, Intermediate = 3, Hard = 3 },
                    TotalQuestions = 10,
                    DurationMinutes = 15,
                    AllowRetakes = true,
                    Enabled = true
                };

            case "2026-3-acidos-nucleios":
            case "2026-3-acidos-nucleicos":
                return new AssessmentData
                {
                    AssessmentId = id,
                    AssessmentType = "acidos-nucleicos",
                    AcademicTerm = "2026-3",
                    CourseId = "bioquimica",
                    Title = "Avaliação formativa",
                    Description = "Atividade formativa para testar seus conhecimentos.",
                    DisplayTopics = new List<string> { "Ácidos nucleicos" },
                    AllowedDatabanks = new List<string> { "NucleicAcidsQuestionDataBase" },
                    QuestionDistribution = new QuestionDistribution { Basic = 4, Intermediate = 3, Hard = 3 },
                    TotalQuestions = 10,
                    DurationMinutes = 15,
                    AllowRetakes = true,
                    Enabled = true
                };

            case "2026-3-aminoacidos-proteinas-enzimas":
            default:
                return new AssessmentData
                {
                    AssessmentId = id,
                    AssessmentType = "aminoacidos-proteinas-enzimas",
                    AcademicTerm = "2026-3",
                    CourseId = "bioquimica",
                    Title = "Aminoácidos, proteínas e enzimas",
                    Description = "Atividade formativa para testar seus conhecimentos.",
                    DisplayTopics = new List<string> { "Aminoácidos", "Proteínas", "Enzimas" },
                    AllowedDatabanks = new List<string> { "AminoacidQuestionDataBase", "ProteinQuestionDataBase", "EnzymeQuestionDataBase" },
                    QuestionDistribution = new QuestionDistribution { Basic = 4, Intermediate = 3, Hard = 3 },
                    TotalQuestions = 10,
                    DurationMinutes = 15,
                    AllowRetakes = true,
                    Enabled = true
                };
        }
    }
}
