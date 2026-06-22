using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
 
public class SRSManager : MonoBehaviour, ISRSManager
{
    private ISRSManagerRepository progressRepository;
    private Dictionary<string, SubjectData> userProgress = new();
 
    public class SubjectData
    {
        public string subjectId;
        public int questionCounter;
        public float easeFactor;
        public int currentInterval;
        public DateTime lastCompletionDate;
        public DateTime nextRevisionDate;
    }
    
    // private void Awake()
    // {
    //     CreateDictionary();
    // }
 
    // private void CreateDictionary()
    // {
    //     foreach (var subject in grafo.nodes)
    //     {
    //         userProgress[subject.id] = new SubjectData
    //         {
    //             subjectId = subject.id,
    //             questionCounter = 0,
    //             easeFactor = 2.5f,
    //             currentInterval = 1,
    //             lastCompletionDate = DateTime.MinValue,
    //             nextRevisionDate = DateTime.MinValue
    //         };
    //     }
    // }
 
    public void ScheduleRevision(string subjectId)
    {

        if (!userProgress.TryGetValue(subjectId, out SubjectData data))
        {
            Debug.LogWarning($"[SRSManager] SubjectId '{subjectId}' não encontrado no dicionário.");
            return;
        } 
        data.questionCounter++;
 
        if (data.questionCounter == 10)
        {
            data.questionCounter = 0;
            data.lastCompletionDate = DateTime.UtcNow;
            data.nextRevisionDate = DateTime.UtcNow.AddDays(7);
 
            Debug.Log($"[SRSManager] Revisão criada para {subjectId}");
            Debug.Log($"[SRSManager] Próxima revisão: {data.nextRevisionDate}");
        }
    }

    public async Task SynctoFirestorm(string userID)
    {
        if (progressRepository == null)
        {
            progressRepository = GetComponent<ISRSManagerRepository>();
        }
 
        if (progressRepository == null)
        {
            Debug.LogError("[SRSManager] Erro: Nenhum componente de repositório encontrado no GameObject!");
            return;
        }
 
        try
        {
            await progressRepository.SaveProgressAsync(userID, userProgress);
            Debug.Log("[SRSManager] Sincronização com Firebase concluída!");
        }
        catch (Exception e)
        {
            Debug.LogError($"[SRSManager] Erro ao sincronizar: {e.Message}");
        }
    }
}