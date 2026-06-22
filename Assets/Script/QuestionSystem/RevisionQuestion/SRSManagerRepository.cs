using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
 
public class SRSManagerRepository : MonoBehaviour, ISRSManagerRepository
{
    private FirebaseFirestore db;
    private bool isInitialized;
 
    public bool IsInitialized => isInitialized;
 
    public void Initialize()
    {
        if (isInitialized) return;
 
        try
        {
            db = FirebaseFirestore.DefaultInstance;
            isInitialized = true;
            Debug.Log("[SRSManagerRepository] Firestore inicializado com sucesso.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[SRSManagerRepository] Falha ao inicializar o Firestore: {e.Message}");
            throw;
        }
    }
 
    // -------------------------------------------------------
    // Escrita (Salvar na Nuvem)
    // -------------------------------------------------------
 
    // CORRIGIDO: tipo do parâmetro alterado de SubjectData para SRSManager.SubjectData
    // para referenciar a classe interna sem precisar de arquivo separado
    public async Task SaveProgressAsync(string userId, Dictionary<string, SRSManager.SubjectData> userProgress)
    {
        if (!isInitialized) Initialize();
        if (!isInitialized) throw new Exception("Firestore não inicializado.");
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("UserId não pode ser nulo ou vazio.");
 
        try
        {
            Dictionary<string, object> rootMap = new Dictionary<string, object>();
 
            foreach (var kvp in userProgress)
            {
                string subjectId = kvp.Key;
                SRSManager.SubjectData data = kvp.Value;
 
                // CORRIGIDO: lastCompletionDate e nextRevisionDate agora são DateTime (não nullable),
                // então não precisam de .Value nem de verificação de null.
                // DateTime.SpecifyKind garante que o Firebase receba sempre UTC.
                Dictionary<string, object> subjectMap = new Dictionary<string, object>
                {
                    { "subjectId",          subjectId },
                    { "questionCounter",    data.questionCounter },
                    { "easeFactor",         data.easeFactor },
                    { "currentInterval",    data.currentInterval },
                    { "lastCompletionDate", Timestamp.FromDateTime(DateTime.SpecifyKind(data.lastCompletionDate, DateTimeKind.Utc)) },
                    { "nextRevisionDate",   Timestamp.FromDateTime(DateTime.SpecifyKind(data.nextRevisionDate,   DateTimeKind.Utc)) }
                };
 
                rootMap[subjectId] = subjectMap;
            }
 
            DocumentReference docRef = db.Collection("SubjectProgress").Document(userId);
            await docRef.SetAsync(rootMap, SetOptions.MergeAll);
            Debug.Log($"[SRSManagerRepository] Progresso do usuário {userId} salvo com sucesso.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[SRSManagerRepository] Erro ao salvar progresso: {e.Message}");
            throw;
        }
    }
 
    // -------------------------------------------------------
    // Leitura (Carregar da Nuvem)
    // -------------------------------------------------------
 
    // CORRIGIDO: tipo de retorno alterado para SRSManager.SubjectData pelo mesmo motivo acima
    public async Task<Dictionary<string, SRSManager.SubjectData>> LoadProgressAsync(string userId)
    {
        if (!isInitialized) Initialize();
        if (!isInitialized) throw new Exception("Firestore não inicializado.");
 
        try
        {
            DocumentReference docRef = db.Collection("SubjectProgress").Document(userId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
 
            Dictionary<string, SRSManager.SubjectData> userProgress = new Dictionary<string, SRSManager.SubjectData>();
 
            if (!snapshot.Exists)
            {
                Debug.LogWarning($"[SRSManagerRepository] Nenhum progresso encontrado para o usuário {userId}. Retornando dicionário vazio.");
                return userProgress;
            }
 
            Dictionary<string, object> rootMap = snapshot.ToDictionary();
 
            foreach (var kvp in rootMap)
            {
                string subjectId = kvp.Key;
 
                if (kvp.Value is Dictionary<string, object> subjectMap)
                {
                    SRSManager.SubjectData data = new SRSManager.SubjectData();
 
                    data.subjectId       = subjectId;
                    data.questionCounter = subjectMap.ContainsKey("questionCounter") ? Convert.ToInt32(subjectMap["questionCounter"])   : 0;
                    data.easeFactor      = subjectMap.ContainsKey("easeFactor")      ? Convert.ToSingle(subjectMap["easeFactor"])       : 2.5f;
                    data.currentInterval = subjectMap.ContainsKey("currentInterval") ? Convert.ToInt32(subjectMap["currentInterval"])   : 1;
 
                    // CORRIGIDO: campos com nome correto (lastCompletionDate / nextRevisionDate)
                    // e atribuição direta sem .Value (não são mais nullable)
                    if (subjectMap.ContainsKey("lastCompletionDate") && subjectMap["lastCompletionDate"] is Timestamp lastTime)
                        data.lastCompletionDate = lastTime.ToDateTime();
 
                    if (subjectMap.ContainsKey("nextRevisionDate") && subjectMap["nextRevisionDate"] is Timestamp nextTime)
                        data.nextRevisionDate = nextTime.ToDateTime();
 
                    userProgress[subjectId] = data;
                }
            }
 
            Debug.Log($"[SRSManagerRepository] Progresso de {userProgress.Count} tópicos carregado com sucesso.");
            return userProgress;
        }
        catch (Exception e)
        {
            Debug.LogError($"[SRSManagerRepository] Erro ao carregar progresso: {e.Message}");
            throw;
        }
    }
}