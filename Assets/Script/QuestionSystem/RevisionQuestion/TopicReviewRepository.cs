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

    public async Task UpsertTopicReviewAsync(string userId, TopicReviewData topicReview)
    {
        if (!isInitialized) Initialize();
        if (!isInitialized) throw new Exception("Firestore não inicializado.");
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("UserId não pode ser nulo ou vazio.");
        if (topicReview == null) throw new ArgumentNullException(nameof(topicReview));
        if (string.IsNullOrEmpty(topicReview.databankName))
            throw new ArgumentException("databankName não pode ser nulo ou vazio.");

        try
        {
            DocumentReference docRef = db
                .Collection("Users").Document(userId)
                .Collection("TopicReviews").Document(topicReview.databankName);

            await docRef.SetAsync(topicReview, SetOptions.MergeAll);

            Debug.Log($"[TopicReviewRepository] Progresso do tópico '{topicReview.databankName}' salvo para o usuário {userId}.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewRepository] Erro ao salvar progresso: {e.Message}");
            throw;
        }
    }

    public async Task<TopicReviewData> GetTopicReviewAsync(string userId, string databankName)
    {
        if (!isInitialized) Initialize();
        if (!isInitialized) throw new Exception("Firestore não inicializado.");
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException("UserId não pode ser nulo ou vazio.");
        if (string.IsNullOrEmpty(databankName)) throw new ArgumentException("databankName não pode ser nulo ou vazio.");

        try
        {
            DocumentReference docRef = db
                .Collection("Users").Document(userId)
                .Collection("TopicReviews").Document(databankName);

            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot == null || !snapshot.Exists) return null;

            return snapshot.ConvertTo<TopicReviewData>();
        }
        catch (Exception e)
        {
            Debug.LogError($"[TopicReviewRepository] Erro ao carregar tópico '{databankName}': {e.Message}");
            throw;
        }
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

            // Exclui quem nunca teve revisão agendada (nextReviewAt == MinValue)
            // e traz só quem já venceu.
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