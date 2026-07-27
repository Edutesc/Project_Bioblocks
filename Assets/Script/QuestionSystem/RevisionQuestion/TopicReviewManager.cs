using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class TopicReviewManager : MonoBehaviour, ITopicReviewManager
{
    private ITopicReviewRepository progressRepository;
    private Dictionary<string, TopicReviewData> userProgress = new();

    // A cada quantas sessões concluídas o tópico entra em revisão
    private const int SessionsRequiredForRevision = 3;

    public async Task RegisterCompletedSessionAsync(
        string userId,
        string databankName,
        string displayName,
        List<string> sessionQuestionGlobalIds,
        List<string> correctQuestionGlobalIds,
        List<string> wrongQuestionGlobalIds,
        string source)
    {
        Debug.Log($"[TopicReviewManager] Registrando sessão para o usuário '{userId}' no tópico '{databankName}'.");

        TopicReviewData data = GetOrCreateTopicData(userId, databankName, displayName);

        DateTime now = DateTime.UtcNow;

        data.lastInteractionAt = now;
        data.updatedAt = now;
        data.lastSessionQuestionGlobalIds = sessionQuestionGlobalIds ?? new List<string>();
        data.recentCorrectQuestionGlobalIds = correctQuestionGlobalIds ?? new List<string>();
        data.recentWrongQuestionGlobalIds = wrongQuestionGlobalIds ?? new List<string>();
        data.totalSessionsCompleted++;

        data.sessionHistory.Add(new TopicReviewSessionHistoryItem
        {
            sessionId = Guid.NewGuid().ToString(),
            completedAt = now,
            source = string.IsNullOrEmpty(source) ? "normal" : source,
            questionGlobalIds = data.lastSessionQuestionGlobalIds,
            correctQuestionGlobalIds = data.recentCorrectQuestionGlobalIds,
            wrongQuestionGlobalIds = data.recentWrongQuestionGlobalIds
        });

        ScheduleRevision(data);

        await SyncToFirestore(userId, data);
    }

    public async Task<List<TopicReviewData>> GetDueTopicReviewsAsync(string userId)
    {
        EnsureRepository();
        if (progressRepository == null)
            return new List<TopicReviewData>();

        return await progressRepository.GetDueTopicReviewsAsync(userId, DateTime.UtcNow);
    }

    private TopicReviewData GetOrCreateTopicData(string userId, string databankName, string displayName)
    {
        if (!userProgress.TryGetValue(databankName, out TopicReviewData data))
        {
            data = new TopicReviewData
            {
                userId = userId,
                databankName = databankName,
                displayName = displayName
            };
            userProgress[databankName] = data;
        }
        else
        {
            data.displayName = displayName;
        }

        return data;
    }

    public void ScheduleNextRevision()
        {

                DateTime nextReviewAt = DateTime.UtcNow.AddDays(7);
                Debug.Log($"[TopicReviewManager] Revisão criada para  {DateTime.UtcNow}");
                Debug.Log($"[TopicReviewManager] Próxima revisão: {nextReviewAt}");
            
        }



    private void ScheduleRevision(TopicReviewData data)
    {
        if (data.totalSessionsCompleted % SessionsRequiredForRevision == 0)
        {
            data.nextReviewAt = DateTime.UtcNow.AddDays(7);

            Debug.Log($"[TopicReviewManager] Revisão criada para {data.databankName}");
            Debug.Log($"[TopicReviewManager] Próxima revisão: {data.nextReviewAt}");
        }
    }

    private void EnsureRepository()
    {
        if (progressRepository == null)
            progressRepository = GetComponent<ITopicReviewRepository>();
    }

    private async Task SyncToFirestore(string userId, TopicReviewData data)
    {
        EnsureRepository();

        if (progressRepository == null)
        {
            Debug.LogError("[TopicReviewManager] Erro: Nenhum componente de repositório encontrado no GameObject!");
            return;
        }

        try
        {
            await progressRepository.UpsertTopicReviewAsync(userId, data);
            Debug.Log("[TopicReviewManager] Sincronização com Firebase concluída!");
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewManager] Erro ao sincronizar: {e.Message}");
        }
    }
}