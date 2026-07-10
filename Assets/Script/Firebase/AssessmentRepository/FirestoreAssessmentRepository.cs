using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

public class FirestoreAssessmentRepository : IFirestoreAssessmentRepository
{
    private readonly FirebaseFirestore _db;
    private readonly string _collectionName = "assessments";

    public FirestoreAssessmentRepository()
    {
        _db = FirebaseFirestore.DefaultInstance;
    }

    public async Task SaveAssessmentAsync(AssessmentResult result)
    {
        try
        {
            if (result.CompletedAt == default)
            {
                result.CompletedAt = Timestamp.GetCurrentTimestamp();
            }

            CollectionReference collection = _db.Collection(_collectionName);
            
            await collection.AddAsync(result);
            
            Debug.Log($"[FirestoreAssessmentRepository] Assessment result for user {result.UserId} saved successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreAssessmentRepository] Failed to save assessment result: {e.Message}");
            throw;
        }
    }
}
