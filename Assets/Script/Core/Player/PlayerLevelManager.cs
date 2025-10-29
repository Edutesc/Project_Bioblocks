using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PlayerLevelManager : MonoBehaviour
{
    private static PlayerLevelManager _instance;
    public static PlayerLevelManager Instance => _instance;

    public static event Action<int, int> OnLevelChanged;
    public static event Action<int> OnLevelProgressUpdated;

    private UserData currentUserData;
    private bool isInitialized = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentUserData = UserDataStore.CurrentUserData;
        UserDataStore.OnUserDataChanged += OnUserDataChanged;
        
        if (currentUserData != null)
        {
            PerformMigrationIfNeeded();
        }
        
        isInitialized = true;
    }

    private void OnDestroy()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
        if (_instance == this) _instance = null;
    }

    private void OnUserDataChanged(UserData userData)
    {
        currentUserData = userData;
    }

    private async void PerformMigrationIfNeeded()
    {
        if (currentUserData == null) return;

        if (currentUserData.PlayerLevel == 0)
        {
            Debug.Log("[PlayerLevelManager] Migração: Calculando level para usuário existente...");

            try
            {
                if (currentUserData.ResetDatabankFlags == null)
                {
                    currentUserData.ResetDatabankFlags = new Dictionary<string, bool>();
                }

                int totalAnswered = await CalculateValidAnsweredQuestions(currentUserData.UserId);
                currentUserData.TotalValidQuestionsAnswered = totalAnswered;

                int totalQuestions = currentUserData.TotalQuestionsInAllDatabanks;
                if (totalQuestions <= 0)
                {
                    totalQuestions = DatabaseStatisticsManager.Instance.GetTotalQuestionsCount();
                }

                int calculatedLevel = PlayerLevelConfig.CalculateLevel(totalAnswered, totalQuestions);
                currentUserData.PlayerLevel = calculatedLevel;

                await FirestoreRepository.Instance.UpdateUserField(
                    currentUserData.UserId,
                    "PlayerLevel",
                    calculatedLevel
                );

                await FirestoreRepository.Instance.UpdateUserField(
                    currentUserData.UserId,
                    "TotalValidQuestionsAnswered",
                    totalAnswered
                );

                UserDataStore.CurrentUserData = currentUserData;

                Debug.Log($"[PlayerLevelManager] Migração concluída! Level: {calculatedLevel}, Questões: {totalAnswered}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[PlayerLevelManager] Erro na migração: {e.Message}");
                currentUserData.PlayerLevel = 1;
                currentUserData.TotalValidQuestionsAnswered = 0;
            }
        }
    }

    public async Task IncrementTotalAnswered()
    {
        if (!isInitialized || currentUserData == null) return;

        currentUserData.TotalValidQuestionsAnswered++;

        await FirestoreRepository.Instance.UpdateUserField(
            currentUserData.UserId,
            "TotalValidQuestionsAnswered",
            currentUserData.TotalValidQuestionsAnswered
        );

        UserDataStore.UpdateTotalValidQuestionsAnswered(currentUserData.TotalValidQuestionsAnswered);

        OnLevelProgressUpdated?.Invoke(currentUserData.TotalValidQuestionsAnswered);
        
        Debug.Log($"[PlayerLevelManager] Total válido: {currentUserData.TotalValidQuestionsAnswered}");
    }

    public async Task CheckAndHandleLevelUp()
    {
        if (!isInitialized || currentUserData == null) return;

        int totalQuestions = currentUserData.TotalQuestionsInAllDatabanks;
        if (totalQuestions <= 0)
        {
            totalQuestions = DatabaseStatisticsManager.Instance.GetTotalQuestionsCount();
        }

        int oldLevel = currentUserData.PlayerLevel;
        int newLevel = PlayerLevelConfig.CalculateLevel(
            currentUserData.TotalValidQuestionsAnswered,
            totalQuestions
        );

        if (newLevel > oldLevel)
        {
            Debug.Log($"[PlayerLevelManager] 🎉 LEVEL UP! {oldLevel} → {newLevel}");

            currentUserData.PlayerLevel = newLevel;

            int totalBonus = 0;
            for (int level = oldLevel + 1; level <= newLevel; level++)
            {
                int bonus = PlayerLevelConfig.GetBonusForLevel(level);
                totalBonus += bonus;
                Debug.Log($"[PlayerLevelManager] Bônus do nível {level}: {bonus} pontos");
            }

            await GrantLevelUpBonus(totalBonus);

            await FirestoreRepository.Instance.UpdateUserField(
                currentUserData.UserId,
                "PlayerLevel",
                newLevel
            );

            UserDataStore.UpdatePlayerLevel(newLevel);

            OnLevelChanged?.Invoke(oldLevel, newLevel);

            Debug.Log($"[PlayerLevelManager] Level atualizado no Firebase e UserDataStore");
        }
    }

    private async Task GrantLevelUpBonus(int bonusPoints)
    {
        currentUserData.Score += bonusPoints;
        currentUserData.WeekScore += bonusPoints;

        await FirestoreRepository.Instance.UpdateUserScores(
            currentUserData.UserId,
            bonusPoints,
            0,
            "",
            false
        );

        UserDataStore.CurrentUserData = currentUserData;
        Debug.Log($"[PlayerLevelManager] Bônus concedido: {bonusPoints} pontos");
    }

    public float GetProgressInCurrentLevel()
    {
        if (currentUserData == null) return 0f;

        int totalQuestions = currentUserData.TotalQuestionsInAllDatabanks;
        if (totalQuestions <= 0)
        {
            totalQuestions = DatabaseStatisticsManager.Instance.GetTotalQuestionsCount();
        }

        int currentLevel = currentUserData.PlayerLevel;
        var threshold = PlayerLevelConfig.GetThresholdForLevel(currentLevel);

        float currentPercentage = (float)currentUserData.TotalValidQuestionsAnswered / totalQuestions;

        float levelRange = threshold.MaxPercentage - threshold.MinPercentage;
        float progressInLevel = (currentPercentage - threshold.MinPercentage) / levelRange;

        return Mathf.Clamp01(progressInLevel);
    }

    public int GetQuestionsUntilNextLevel()
    {
        if (currentUserData == null) return 0;
        if (currentUserData.PlayerLevel >= 10) return 0;

        int totalQuestions = currentUserData.TotalQuestionsInAllDatabanks;
        if (totalQuestions <= 0)
        {
            totalQuestions = DatabaseStatisticsManager.Instance.GetTotalQuestionsCount();
        }

        int nextLevel = currentUserData.PlayerLevel + 1;
        var nextThreshold = PlayerLevelConfig.GetThresholdForLevel(nextLevel);

        int questionsNeeded = nextThreshold.GetRequiredQuestions(totalQuestions);
        int remaining = questionsNeeded - currentUserData.TotalValidQuestionsAnswered;

        return Mathf.Max(0, remaining);
    }

    public async Task RecalculateTotalAnswered()
    {
        if (!isInitialized || currentUserData == null) return;

        int validTotal = await CalculateValidAnsweredQuestions(currentUserData.UserId);

        int oldTotal = currentUserData.TotalValidQuestionsAnswered;
        currentUserData.TotalValidQuestionsAnswered = validTotal;

        await FirestoreRepository.Instance.UpdateUserField(
            currentUserData.UserId,
            "TotalValidQuestionsAnswered",
            validTotal
        );

        UserDataStore.UpdateTotalValidQuestionsAnswered(validTotal);

        Debug.Log($"[PlayerLevelManager] Recalculado: {oldTotal} → {validTotal}");
        OnLevelProgressUpdated?.Invoke(validTotal);
    }

    private async Task<int> CalculateValidAnsweredQuestions(string userId)
    {
        UserData userData = await FirestoreRepository.Instance.GetUserData(userId);
        if (userData == null) return 0;

        int total = 0;

        foreach (var kvp in userData.AnsweredQuestions)
        {
            string databankName = kvp.Key;

            bool isReset = userData.ResetDatabankFlags != null &&
                          userData.ResetDatabankFlags.ContainsKey(databankName) &&
                          userData.ResetDatabankFlags[databankName];

            if (!isReset)
            {
                var distinctQuestions = new HashSet<int>(kvp.Value);
                total += distinctQuestions.Count;
            }
        }

        return total;
    }

    public int GetCurrentLevel() => currentUserData?.PlayerLevel ?? 1;
    public int GetTotalValidAnswered() => currentUserData?.TotalValidQuestionsAnswered ?? 0;
    public int GetTotalQuestionsInAllDatabanks() => currentUserData?.TotalQuestionsInAllDatabanks ?? 0;
}
