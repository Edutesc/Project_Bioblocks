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
        Debug.Log("[PlayerLevelManager] Start() chamado");
        UserDataStore.OnUserDataChanged += OnUserDataChanged;
        currentUserData = UserDataStore.CurrentUserData;
        
        if (currentUserData == null)
        {
            Debug.LogWarning("[PlayerLevelManager] CurrentUserData é null no Start(). Aguardando evento OnUserDataChanged...");
        }
        else
        {
            Debug.Log($"[PlayerLevelManager] CurrentUserData encontrado: {currentUserData.UserId}, Level: {currentUserData.PlayerLevel}");
            PerformMigrationIfNeeded();
        }
        
        isInitialized = true;
        Debug.Log("[PlayerLevelManager] Inicialização completa");
    }

    private void OnDestroy()
    {
        UserDataStore.OnUserDataChanged -= OnUserDataChanged;
        if (_instance == this) _instance = null;
    }

    public void OnUserDataLoaded(UserData userData)
    {
        Debug.Log($"[PlayerLevelManager] OnUserDataLoaded chamado diretamente. UserId: {userData?.UserId}, Level: {userData?.PlayerLevel}");
        
        currentUserData = userData;
        
        if (currentUserData != null && isInitialized)
        {
            PerformMigrationIfNeeded();
        }
    }

    private void OnUserDataChanged(UserData userData)
    {
        Debug.Log($"[PlayerLevelManager] OnUserDataChanged chamado. UserId: {userData?.UserId}, Level: {userData?.PlayerLevel}");

        bool wasNull = (currentUserData == null);
        currentUserData = userData;

        if (wasNull && currentUserData != null && isInitialized)
        {
            Debug.Log("[PlayerLevelManager] Dados carregados pela primeira vez. Verificando migração...");
            PerformMigrationIfNeeded();
        }
    }

    private async void PerformMigrationIfNeeded()
    {
        Debug.Log("[PlayerLevelManager] PerformMigrationIfNeeded() INICIADO");
    
        if (currentUserData == null)
        {
            Debug.LogWarning("[PlayerLevelManager] CurrentUserData é null. Abortando migração.");
            return;
        }

        Debug.Log($"[PlayerLevelManager] Verificando migração. PlayerLevel atual: {currentUserData.PlayerLevel}");

        if (currentUserData.PlayerLevel <= 1 && currentUserData.TotalValidQuestionsAnswered == 0)
        {
            Debug.Log("[PlayerLevelManager] ⚠️ PlayerLevel = 0. Iniciando migração...");
            try
            {
                Debug.Log("[PlayerLevelManager] 1/5 - Verificando ResetDatabankFlags...");
                if (currentUserData.ResetDatabankFlags == null)
                {
                    currentUserData.ResetDatabankFlags = new Dictionary<string, bool>();
                    Debug.Log("[PlayerLevelManager] ResetDatabankFlags criado");
                }

                Debug.Log("[PlayerLevelManager] 2/5 - Calculando questões respondidas válidas...");
                int totalAnswered = await CalculateValidAnsweredQuestions(currentUserData.UserId);
                currentUserData.TotalValidQuestionsAnswered = totalAnswered;
                Debug.Log($"[PlayerLevelManager] Total de questões válidas: {totalAnswered}");

                Debug.Log("[PlayerLevelManager] 3/5 - Obtendo total de questões nos bancos...");
                int totalQuestions = currentUserData.TotalQuestionsInAllDatabanks;
                if (totalQuestions <= 0)
                {
                    if (DatabaseStatisticsManager.Instance != null)
                    {
                        totalQuestions = DatabaseStatisticsManager.Instance.GetTotalQuestionsCount();
                        Debug.Log($"[PlayerLevelManager] Total obtido do DatabaseStatisticsManager: {totalQuestions}");
                    }
                    else
                    {
                        Debug.LogError("[PlayerLevelManager] DatabaseStatisticsManager.Instance é NULL!");
                        totalQuestions = 100; // fallback
                    }
                }

                Debug.Log($"[PlayerLevelManager] Total de questões nos bancos: {totalQuestions}");
                Debug.Log("[PlayerLevelManager] 4/5 - Calculando level...");
                int calculatedLevel = PlayerLevelConfig.CalculateLevel(totalAnswered, totalQuestions);
                currentUserData.PlayerLevel = calculatedLevel;
                Debug.Log($"[PlayerLevelManager] Level calculado: {calculatedLevel}");
                Debug.Log("[PlayerLevelManager] 5/5 - Salvando no Firebase...");

                await FirestoreRepository.Instance.UpdateUserField(
                    currentUserData.UserId,
                    "PlayerLevel",
                    calculatedLevel
                );
                Debug.Log("[PlayerLevelManager] PlayerLevel salvo no Firebase");

                await FirestoreRepository.Instance.UpdateUserField(
                    currentUserData.UserId,
                    "TotalValidQuestionsAnswered",
                    totalAnswered
                );

                Debug.Log("[PlayerLevelManager] TotalValidQuestionsAnswered salvo no Firebase");
                Debug.Log("[PlayerLevelManager] 🔄 Atualizando UserDataStore para disparar evento...");
                UserDataStore.CurrentUserData = currentUserData;

                Debug.Log($"[PlayerLevelManager] Migração concluída! Level: {calculatedLevel}, Questões: {totalAnswered}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[PlayerLevelManager] Erro na migração: {e.Message}");
                Debug.LogError($"[PlayerLevelManager] Stack trace: {e.StackTrace}");
                currentUserData.PlayerLevel = 1;
                currentUserData.TotalValidQuestionsAnswered = 0;
            }
        }
        else
        {
            Debug.Log($"[PlayerLevelManager] PlayerLevel já está definido ({currentUserData.PlayerLevel}). Migração não necessária.");
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
        Debug.Log($"[PlayerLevelManager] CalculateValidAnsweredQuestions() INICIADO para userId: {userId}");
        UserData userData = await FirestoreRepository.Instance.GetUserData(userId);
        
        if (userData == null)
        {
            Debug.LogError("[PlayerLevelManager] GetUserData retornou NULL!");
            return 0;
        }

        Debug.Log($"[PlayerLevelManager] UserData obtido. AnsweredQuestions count: {userData.AnsweredQuestions?.Count ?? 0}");
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
                int count = distinctQuestions.Count;
                total += count;
                Debug.Log($"[PlayerLevelManager] Banco '{databankName}': {count} questões válidas (não resetado)");
            }
            else
            {
                Debug.Log($"[PlayerLevelManager] Banco '{databankName}': ignorado (resetado)");
            }
        }

        Debug.Log($"[PlayerLevelManager] Total calculado: {total} questões");
        return total;
    }

    public int GetCurrentLevel() => currentUserData?.PlayerLevel ?? 1;
    public int GetTotalValidAnswered() => currentUserData?.TotalValidQuestionsAnswered ?? 0;
    public int GetTotalQuestionsInAllDatabanks() => currentUserData?.TotalQuestionsInAllDatabanks ?? 0;
}
