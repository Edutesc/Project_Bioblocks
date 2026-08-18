using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

public class TopicReviewRepository : MonoBehaviour, ITopicReviewRepository
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
            if (db == null) throw new Exception("FirebaseFirestore.DefaultInstance retornou nulo.");
            isInitialized = true;
            Debug.Log("[TopicReviewRepository] Firestore inicializado com sucesso.");
        }
        catch (Exception e)
        {
            isInitialized = false;
            Debug.LogError($"[TopicReviewRepository] Falha ao inicializar o Firestore: {e.Message}");
            throw;
        }
    }

        public async Task UpsertTopicReviewAsync(
        string userId,
        string databankName,
        string topicId,
        DateTime nextReviewAt)
    {
        Debug.Log("entrei no topicreviewrepository");
        if (!isInitialized)
            Initialize();

        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("userId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(topicId))
            throw new ArgumentException("topicId não pode ser vazio.");

         Debug.Log($"Dados que chegaram no TopicReviewRepository {userId}, {databankName}, {topicId}, {nextReviewAt}"); 

        DocumentReference docRef = db
            .Collection("Users")
            .Document(userId)
            .Collection("TopicReviews")
            .Document(databankName);

        var data = new Dictionary<string, object>
        {
            { "userId", userId },
            { "questionDatabankName", topicId },
            { "lastInteractionAt", Timestamp.GetCurrentTimestamp() },
            { "nextReviewAt", Timestamp.FromDateTime(nextReviewAt.ToUniversalTime()) },
        };

        Debug.Log($"Esperando no TopicReviewRepository"); 

        await docRef.SetAsync(data, SetOptions.MergeAll);

        Debug.Log(
            $"[TopicReviewRepository] Revisão salva: " +
            $"Users/{userId}/TopicReviews/{topicId}/GlobalId/{databankName}");
    }

    
    public async Task<List<TopicReviewData>> GetDueTopicReviewsAsync(string userId, DateTime nowUtc)
    {
        if (!isInitialized) Initialize();
        if (!isInitialized) throw new Exception("Firestore não inicializado.");
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("UserId não pode ser nulo ou vazio.");

        List<TopicReviewData> dueReviews = new List<TopicReviewData>();

        try
        {
            CollectionReference topicReviewsRef = db
                .Collection("Users").Document(userId)
                .Collection("TopicReviews");

            Query query = topicReviewsRef
                .WhereGreaterThan("nextReviewAt", Timestamp.FromDateTime(new DateTime(1, 1, 2, 0, 0, 0, DateTimeKind.Utc)))
                .WhereLessThanOrEqualTo("nextReviewAt", Timestamp.FromDateTime(DateTime.SpecifyKind(nowUtc, DateTimeKind.Utc)));

            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            if (querySnapshot == null || querySnapshot.Count == 0) return dueReviews;

            foreach (DocumentSnapshot doc in querySnapshot.Documents)
            {
                if (!doc.Exists) continue;

                try
                {
                    dueReviews.Add(doc.ConvertTo<TopicReviewData>());
                }
                catch (Exception exDoc)
                {
                    Debug.LogWarning($"[TopicReviewRepository] Falha ao converter documento '{doc.Id}': {exDoc.Message}");
                }
            }

            return dueReviews;
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewRepository] Erro ao carregar revisões pendentes: {e.Message}");
            throw;
        }
    }
}