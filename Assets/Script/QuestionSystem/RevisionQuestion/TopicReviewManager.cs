using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class TopicReviewManager : MonoBehaviour, ITopicReviewManager
{
    private ITopicReviewRepository progressRepository;
    private Dictionary<string, TopicReviewData> userProgress = new();

    public async Task RegisterCompletedSessionAsync(
    string userId,
    string databankName,
    string correctQuestionGlobalIds,
    string wrongQuestionGlobalIds,
    string globalId,
    string source)
    {
        Debug.Log($"[TopicReviewManager] Sessão concluída.");
        Debug.Log($"[TopicReviewManager] Usuário: {userId}");
        Debug.Log($"[TopicReviewManager] Tópico: {databankName}");

        await ScheduleNextRevision(userId, globalId, databankName);
    }


    public async Task ScheduleNextRevision(string userId, string databankname, string topicId)
    {
        Debug.Log("chegou");
        EnsureRepository();

        DateTime nextReviewAt = DateTime.UtcNow.AddDays(7);
        Debug.Log($"[TopicReviewManager] Próxima revisão: {nextReviewAt}");
        Debug.Log($"[TopicReviewManager] Databank: {topicId}");
        Debug.Log($"[TopicReviewManager] Próxima revisão: {nextReviewAt}"); 

        await progressRepository.UpsertTopicReviewAsync(userId, databankname, topicId, nextReviewAt);        
            
        }

    private void EnsureRepository()
    {
        if (progressRepository == null)
            progressRepository = GetComponent<ITopicReviewRepository>();
    }

}