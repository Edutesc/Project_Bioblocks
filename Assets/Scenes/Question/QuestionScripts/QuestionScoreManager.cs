using System.Threading.Tasks;
using UnityEngine;
using System;
using QuestionSystem;

public class QuestionScoreManager : MonoBehaviour
{
    private UserData currentUserData;
    private AnsweredQuestionsManager answeredQuestionsManager;
    private QuestionBonusManager questionBonusManager;

    private void Start()
    {
        currentUserData = UserDataStore.CurrentUserData;
        answeredQuestionsManager = AnsweredQuestionsManager.Instance;
        questionBonusManager = FindFirstObjectByType<QuestionBonusManager>();

        if (currentUserData == null)
        {
            Debug.LogError("CurrentUserData é null no ScoreManager");
        }

        if (answeredQuestionsManager == null)
        {
            Debug.LogError("AnsweredQuestionsManager não encontrado");
        }

        if (questionBonusManager == null)
        {
            Debug.LogWarning("QuestionBonusManager não encontrado. O sistema de bônus não estará disponível.");
        }
    }

    public async Task UpdateScore(int scoreChange, bool isCorrect, Question answeredQuestion)
    {
        try
        {
            if (AuthenticationRepository.Instance.Auth.CurrentUser == null)
            {
                Debug.LogError("Usuário não autenticado");
                return;
            }

            string userId = AuthenticationRepository.Instance.Auth.CurrentUser.UserId;
            UserData userData = await FirestoreRepository.Instance.GetUserData(userId);

            if (userData == null)
            {
                Debug.LogError("Dados do usuário não encontrados");
                return;
            }

            int actualScoreChange = scoreChange;
            
            if (isCorrect && UserTopBarManager.Instance != null && UserTopBarManager.Instance.IsAnyBonusActive())
            {
                actualScoreChange = UserTopBarManager.Instance.ApplyTotalBonus(scoreChange);
            }
            else if (isCorrect && questionBonusManager != null && questionBonusManager.IsBonusActive())
            {
                actualScoreChange = questionBonusManager.ApplyBonusToScore(scoreChange);
            }

            if (isCorrect)
            {
                string databankName = answeredQuestion.questionDatabankName;
                int questionNumber = answeredQuestion.questionNumber;

                try
                {
                    await FirestoreRepository.Instance.UpdateUserScores(
                        userId,
                        actualScoreChange,
                        questionNumber,
                        databankName,
                        true
                    );

                    if (answeredQuestionsManager != null && answeredQuestionsManager.IsManagerInitialized)
                    {
                        await answeredQuestionsManager.ForceUpdate();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Falha ao atualizar scores e marcar questão como respondida: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    await FirestoreRepository.Instance.UpdateUserScores(
                        userId,
                        actualScoreChange,
                        0,
                        "",
                        false
                    );
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Falha ao atualizar o score no Firestore: {ex.Message}");
                }
            }

            UserData updatedUserData = await FirestoreRepository.Instance.GetUserData(userId);

            if (updatedUserData != null)
            {
                UserDataStore.CurrentUserData = updatedUserData;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar score: {ex.Message}\n{ex.StackTrace}");

            if (currentUserData != null && scoreChange != 0)
            {
                int clientSideScore = currentUserData.Score + scoreChange;
                int clientSideWeekScore = currentUserData.WeekScore + scoreChange;

                currentUserData.Score = clientSideScore;
                currentUserData.WeekScore = clientSideWeekScore;
                UserDataStore.CurrentUserData = currentUserData;
            }
        }
    }

    private void OnEnable()
    {
        UserDataStore.OnUserDataChanged += OnUserDataChanged;
    }

    private void OnDisable()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
    }

    private void OnUserDataChanged(UserData userData)
    {
        currentUserData = userData;
    }

    public bool HasBonusActive()
    {
        if (UserTopBarManager.Instance != null && UserTopBarManager.Instance.IsAnyBonusActive())
        {
            return true;
        }

        return questionBonusManager != null && questionBonusManager.IsBonusActive();
    }

    public int CalculateBonusScore(int baseScore)
    {
        if (UserTopBarManager.Instance != null && UserTopBarManager.Instance.IsAnyBonusActive())
        {
            return UserTopBarManager.Instance.ApplyTotalBonus(baseScore);
        }

        if (questionBonusManager != null && questionBonusManager.IsBonusActive())
        {
            return questionBonusManager.ApplyBonusToScore(baseScore);
        }

        return baseScore;
    }
}