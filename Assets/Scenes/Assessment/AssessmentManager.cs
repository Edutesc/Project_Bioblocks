using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
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

    [Header("Navigation Buttons")]
    [Header("Navigation Buttons")]
    [SerializeField] private Button btnPrevious;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnExit;
    [SerializeField] private TMP_Text nextButtonText;

    [Header("Question Palette")]
    [SerializeField] private AssessmentPaletteBarManager paletteBarManager;

    [Header("Timer")]
    [SerializeField] private AssessmentTimerManager timerManager;

    private bool _isFinishing;

    private void Start()
    {
        if (AssessmentSession.Current == null || AssessmentSession.Current.Questions == null || AssessmentSession.Current.Questions.Count == 0)
        {
            Debug.LogError("[AssessmentManager] Nenhuma sessão ativa encontrada. Voltando ao Menu.");
            SceneManager.LoadScene("PathwayScene");
            return;
        }

        // Desativa managers que não devem rodar em Assessment
        var hintManager = FindObjectOfType<QuestionHintButtonManager>();
        if (hintManager != null) hintManager.gameObject.SetActive(false);

        var bonusManager = FindObjectOfType<QuestionBonusManager>();
        if (bonusManager != null) bonusManager.gameObject.SetActive(false);

        // Fallbacks defensivos para componentes de UI
        ResolveUIReferences();

        // Configuração dos ouvintes de eventos
        if (answerManager != null)
        {
            answerManager.OnAnswerSelected += HandleAnswerSelected;
        }

        if (btnPrevious != null)
        {
            btnPrevious.onClick.AddListener(OnPreviousClicked);
        }

        if (btnNext != null)
        {
            btnNext.onClick.AddListener(OnNextClicked);
        }

        if (btnExit != null)
        {
            btnExit.onClick.AddListener(OnExitClicked);
        }

        // Inicialização da Paleta de Questões
        if (paletteBarManager != null)
        {
            paletteBarManager.Initialize(AssessmentSession.Current);
            paletteBarManager.OnQuestionSelected += HandlePaletteQuestionSelected;
        }

        // Inicialização do Timer
        InitializeTimer();

        // Exibe a primeira questão
        ShowCurrentQuestion();
    }

    private void ResolveUIReferences()
    {
        if (btnNext == null)
        {
            var nextObj = GameObject.Find("NextQuestionButton");
            if (nextObj != null) btnNext = nextObj.GetComponent<Button>();
        }

        if (nextButtonText == null && btnNext != null)
        {
            nextButtonText = btnNext.GetComponentInChildren<TMP_Text>();
        }

        if (btnPrevious == null)
        {
            var prevObj = GameObject.Find("PreviousQuestionButton") ?? GameObject.Find("BackButton");
            if (prevObj != null) btnPrevious = prevObj.GetComponent<Button>();
        }

        if (btnExit == null)
        {
            var exitObj = GameObject.Find("ExitButton");
            if (exitObj != null) btnExit = exitObj.GetComponent<Button>();
        }

        if (timerManager == null)
        {
            timerManager = FindObjectOfType<AssessmentTimerManager>();
            if (timerManager == null)
            {
                var timerObj = GameObject.Find("QuestionTimerManager") ?? GameObject.Find("TimerManager");
                if (timerObj != null)
                {
                    timerManager = timerObj.GetComponent<AssessmentTimerManager>();
                    if (timerManager == null)
                    {
                        timerManager = timerObj.AddComponent<AssessmentTimerManager>();
                    }
                }
            }
        }

        if (paletteBarManager == null)
        {
            paletteBarManager = FindObjectOfType<AssessmentPaletteBarManager>();
        }
    }

    private void InitializeTimer()
    {
        if (timerManager != null)
        {
            timerManager.OnTimerComplete += HandleTimerExpired;

            var session = AssessmentSession.Current;
            float durationSeconds = (session?.AssessmentConfig != null && session.AssessmentConfig.DurationMinutes > 0)
                ? session.AssessmentConfig.DurationMinutes * 60f
                : AssessmentTimerManager.DefaultDurationSeconds;

            timerManager.StartTimer(durationSeconds);
        }
    }

    private void ShowCurrentQuestion()
    {
        var session = AssessmentSession.Current;
        var question = session?.GetCurrentQuestion();

        if (question == null)
        {
            Debug.LogError("[AssessmentManager] Erro ao carregar a questão atual.");
            return;
        }

        if (progressText != null)
        {
            progressText.text = $"Questão {session.CurrentQuestionIndex + 1}/{session.Questions.Count}";
        }

        if (questionUIManager != null)
        {
            questionUIManager.ShowQuestion(question);
        }

        if (answerManager != null)
        {
            answerManager.SetupAnswerButtons(question);
            answerManager.EnableAllButtons();

            // Restaura visualmente a alternativa selecionada caso a questão já tenha sido respondida
            int selectedIndex = session.GetSelectedAnswer(session.CurrentQuestionIndex);
            if (selectedIndex >= 0)
            {
                answerManager.HighlightSelectedAnswer(selectedIndex);
            }
            else
            {
                answerManager.ResetButtonBackgrounds();
            }
        }

        if (canvasGroupManager != null)
        {
            canvasGroupManager.ShowQuestion(
                isImageQuestion: question.questionType == QuestionType.Image,
                isImageAnswer: question.answerType == AnswerType.Image,
                questionLevel: question.questionLevel
            );
        }

        UpdateNavigationButtons();

        if (paletteBarManager != null)
        {
            paletteBarManager.RefreshPalette();
        }
    }

    private void HandleAnswerSelected(int selectedIndex)
    {
        if (_isFinishing) return;

        var session = AssessmentSession.Current;
        var question = session?.GetCurrentQuestion();

        if (question != null)
        {
            session.RecordAnswer(question, selectedIndex);
        }

        if (answerManager != null)
        {
            answerManager.HighlightSelectedAnswer(selectedIndex);
        }

        UpdateNavigationButtons();

        if (paletteBarManager != null)
        {
            paletteBarManager.RefreshPalette();
        }
    }

    private void HandlePaletteQuestionSelected(int index)
    {
        if (_isFinishing) return;

        var session = AssessmentSession.Current;
        if (session == null) return;

        session.MoveToQuestion(index);
        ShowCurrentQuestion();
    }

    private void UpdateNavigationButtons()
    {
        var session = AssessmentSession.Current;
        if (session == null) return;

        // Botão Voltar: interativo apenas se houver questão anterior
        if (btnPrevious != null)
        {
            btnPrevious.interactable = session.CanMoveToPrevious && !_isFinishing;
        }

        // Botão Próximo / Finalizar
        if (btnNext != null)
        {
            int currentAnswer = session.GetSelectedAnswer(session.CurrentQuestionIndex);
            // Permite avançar quando o aluno tiver marcado uma resposta
            btnNext.interactable = (currentAnswer >= 0) && !_isFinishing;
        }

        if (nextButtonText != null)
        {
            nextButtonText.text = session.IsLastQuestion ? "Finalizar" : "Próxima";
        }
    }

    private void OnPreviousClicked()
    {
        if (_isFinishing) return;

        var session = AssessmentSession.Current;
        if (session != null && session.CanMoveToPrevious)
        {
            session.MoveToPreviousQuestion();
            ShowCurrentQuestion();
        }
    }

    private async void OnNextClicked()
    {
        if (_isFinishing) return;

        var session = AssessmentSession.Current;
        if (session == null) return;

        if (session.IsLastQuestion)
        {
            await FinishAssessment();
        }
        else
        {
            session.MoveToNextQuestion();
            ShowCurrentQuestion();
        }
    }

    private void OnExitClicked()
    {
        if (_isFinishing) return;

        if (timerManager != null)
        {
            timerManager.StopTimer();
        }

        AssessmentSession.Clear();
        SceneManager.LoadScene("PathwayScene");
    }

    private async void HandleTimerExpired()
    {
        Debug.LogWarning("[AssessmentManager] Tempo esgotado! Finalizando avaliação automaticamente.");
        await FinishAssessment();
    }

    private async Task FinishAssessment()
    {
        if (_isFinishing) return;
        _isFinishing = true;

        if (timerManager != null)
        {
            timerManager.StopTimer();
        }

        if (answerManager != null)
        {
            answerManager.DisableAllButtons();
        }

        if (btnPrevious != null) btnPrevious.interactable = false;
        if (btnNext != null) btnNext.interactable = false;
        if (btnExit != null) btnExit.interactable = false;

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
            catch (Exception e)
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

        if (timerManager != null)
        {
            timerManager.OnTimerComplete -= HandleTimerExpired;
        }

        if (btnPrevious != null)
        {
            btnPrevious.onClick.RemoveListener(OnPreviousClicked);
        }

        if (btnNext != null)
        {
            btnNext.onClick.RemoveListener(OnNextClicked);
        }

        if (btnExit != null)
        {
            btnExit.onClick.RemoveListener(OnExitClicked);
        }

        if (paletteBarManager != null)
        {
            paletteBarManager.OnQuestionSelected -= HandlePaletteQuestionSelected;
        }
    }
}
