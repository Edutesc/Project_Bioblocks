using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Edutesc.BioBlocks.Assessment;
using QuestionSystem;
using Edutesc.BioBlocks.Core.Models;

public class AssessmentManager : MonoBehaviour
{
    [Header("UI Managers")]
    [SerializeField] private QuestionUIManager questionUIManager;
    [SerializeField] private QuestionAnswerManager answerManager;
    [SerializeField] private QuestionCanvasGroupManager canvasGroupManager;
    [SerializeField] private TMP_Text progressText;

    private void Start()
    {
        if (AssessmentSession.Current == null || AssessmentSession.Current.Questions == null || AssessmentSession.Current.Questions.Count == 0)
        {
            Debug.LogError("[AssessmentManager] Nenhuma sessão ativa encontrada. Voltando ao Menu.");
            SceneManager.LoadScene("PathwayScene");
            return;
        }

        var hintManager = FindObjectOfType<QuestionHintButtonManager>();
        if (hintManager != null) hintManager.gameObject.SetActive(false);

        answerManager.OnAnswerSelected += HandleAnswerSelected;

        ShowCurrentQuestion();
    }

    private void ShowCurrentQuestion()
    {
        var session = AssessmentSession.Current;
        var question = session.GetCurrentQuestion();

        if (question == null)
        {
            Debug.LogError("[AssessmentManager] Erro ao carregar a questão atual.");
            return;
        }

        if (progressText != null)
        {
            progressText.text = $"Questão {session.CurrentQuestionIndex + 1}/{session.Questions.Count}";
        }

        answerManager.ResetButtonBackgrounds();
    
        questionUIManager.ShowQuestion(question);
    
        answerManager.SetupAnswerButtons(question);
    
        answerManager.EnableAllButtons();

        if (canvasGroupManager != null)
        {
            canvasGroupManager.ShowQuestion(
                isImageQuestion: question.questionType == QuestionType.Image,
                isImageAnswer: question.answerType == AnswerType.Image,
                questionLevel: question.questionLevel
            );
        }
    }

    private async void HandleAnswerSelected(int selectedIndex)
    {
        answerManager.DisableAllButtons();

        var session = AssessmentSession.Current;
        var question = session.GetCurrentQuestion();

        if (question != null)
        {
            session.RecordAnswer(question, selectedIndex);
        }

        await Task.Delay(250);

        if (session.CurrentQuestionIndex < session.Questions.Count - 1)
        {
            session.MoveToNextQuestion();
            ShowCurrentQuestion();
        }
        else
        {
            await FinishAssessment();
        }
    }

    private async Task FinishAssessment()
    {
        if (progressText != null)
        {
            progressText.text = "Salvando Resultado...";
        }

        var session = AssessmentSession.Current;
        var firestoreRepo = AppContext.FirestoreAssessment;

        if (firestoreRepo != null && session != null)
        {
            var attempt = session.FinalizeAttempt();
            string assessmentId = session.AssessmentConfig?.AssessmentId ?? attempt.AssessmentId;

            try
            {
                await firestoreRepo.SaveAssessmentAttemptAsync(assessmentId, attempt);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[AssessmentManager] Erro ao salvar tentativa no Firestore: {e.Message}");
            }
        }
        else
        {
            Debug.LogError("[AssessmentManager] Repositório Firestore nulo! O resultado não será salvo na nuvem.");
        }

        AssessmentSession.Clear();

        SceneManager.LoadScene("PathwayScene");
    }

    private void OnDestroy()
    {
        if (answerManager != null)
        {
            answerManager.OnAnswerSelected -= HandleAnswerSelected;
        }
    }
}
