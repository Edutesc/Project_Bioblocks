using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class PathwayManager : MonoBehaviour
{
    private void Start()
    {
        if (UserDataStore.CurrentUserData != null)
        {
            FirestoreRepository.Instance.ListenToUserData(
                UserDataStore.CurrentUserData.UserId,
                null,
                null,
                null
            );

            InitializeTopBar();

            AnsweredQuestionsManager.OnAnsweredQuestionsUpdated += HandleAnsweredQuestionsUpdated;

            if (DatabaseStatisticsManager.Instance.IsInitialized)
            {
                UpdateAnsweredQuestionsPercentages();
            }
            else
            {
                DatabaseStatisticsManager.OnStatisticsReady += OnDatabaseStatisticsReady;
                StartCoroutine(InitializeDatabaseStatistics());
            }
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
        }
    }

    private void InitializeTopBar()
    {
        if (TopBarManager.Instance != null)
        {
            TopBarManager.Instance.AddSceneToButtonVisibility("HubButton", "ProfileScene");
            TopBarManager.Instance.AddSceneToButtonVisibility("EngineButton", "ProfileScene");
        }
    }

    private IEnumerator InitializeDatabaseStatistics()
    {
        yield return null;
        var task = DatabaseStatisticsManager.Instance.Initialize();
        while (!task.IsCompleted)
        {
            yield return null;
        }
    }

    private void OnDatabaseStatisticsReady()
    {
        UpdateAnsweredQuestionsPercentages();
        DatabaseStatisticsManager.OnStatisticsReady -= OnDatabaseStatisticsReady;
    }

    private void OnDestroy()
    {
        AnsweredQuestionsManager.OnAnsweredQuestionsUpdated -= HandleAnsweredQuestionsUpdated;
        DatabaseStatisticsManager.OnStatisticsReady -= OnDatabaseStatisticsReady;
    }

    private void HandleAnsweredQuestionsUpdated(Dictionary<string, int> answeredCounts)
    {
        if (this == null) return;

        if (UserDataStore.CurrentUserData != null)
        {
            string userId = UserDataStore.CurrentUserData.UserId;
            foreach (var kvp in answeredCounts)
            {
                AnsweredQuestionsListStore.UpdateAnsweredQuestionsCount(userId, kvp.Key, kvp.Value);
            }
        }

        UpdateAnsweredQuestionsPercentages();
    }

    private void UpdateAnsweredQuestionsPercentages()
    {
        if (UserDataStore.CurrentUserData == null) return;

        string userId = UserDataStore.CurrentUserData.UserId;
        var userCounts = AnsweredQuestionsListStore.GetAnsweredQuestionsCountForUser(userId);

        string[] allDatabases = new string[]
        {
        "AcidBaseBufferQuestionDatabase",
        "AminoacidQuestionDatabase",
        "BiochemistryIntroductionQuestionDatabase",
        "CarbohydratesQuestionDatabase",
        "EnzymeQuestionDatabase",
        "LipidsQuestionDatabase",
        "MembranesQuestionDatabase",
        "NucleicAcidsQuestionDatabase",
        "ProteinQuestionDatabase",
        "WaterQuestionDatabase"
        };

        foreach (string databankName in allDatabases)
        {
            int count = userCounts.ContainsKey(databankName) ? userCounts[databankName] : 0;
            int totalQuestions = QuestionBankStatistics.GetTotalQuestions(databankName);
            if (totalQuestions <= 0) totalQuestions = 50;

            int percentageAnswered = totalQuestions > 0 ? (count * 100) / totalQuestions : 0;

            percentageAnswered = Mathf.Min(percentageAnswered, 100);

            string objectName = $"{databankName}PorcentageText";
            GameObject textObject = GameObject.Find(objectName);

            if (textObject != null)
            {
                TextMeshProUGUI tmpText = textObject.GetComponent<TextMeshProUGUI>();
                if (tmpText != null)
                {
                    tmpText.text = $"{percentageAnswered}%";
                }

                CircularProgressIndicator progressIndicator = textObject.GetComponentInParent<CircularProgressIndicator>();
                if (progressIndicator != null)
                {
                    progressIndicator.SetProgress(percentageAnswered);
                }
            }
        }
    }

    public void Navigate(string sceneName)
    {
        NavigationManager.Instance.NavigateTo(sceneName);
    }
}