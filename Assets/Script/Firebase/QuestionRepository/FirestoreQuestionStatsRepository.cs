using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

public class FirestoreQuestionStatsRepository : IFirestoreQuestionStatsRepository
{
    private readonly FirebaseFirestore _db;

    public FirestoreQuestionStatsRepository(FirebaseFirestore db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<QuestionStats> GetQuestionStats()
    {
        try
        {
            DocumentSnapshot snapshot = await _db.Collection("Config")
                .Document("QuestionStats")
                .GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Debug.LogWarning("[FirestoreQuestionStatsRepository] Config/QuestionStats não encontrado.");
                return null;
            }

            var data = snapshot.ToDictionary();

            var stats = new QuestionStats
            {
                TotalQuestions = data.ContainsKey("TotalQuestions")
                    ? Convert.ToInt32(data["TotalQuestions"])
                    : 0,

                Version = data.ContainsKey("Version")
                    ? Convert.ToInt64(data["Version"])
                    : 0,

                UpdatedAt = data.ContainsKey("UpdatedAt") && data["UpdatedAt"] is Timestamp ts
                    ? ts.ToDateTime()
                    : DateTime.MinValue
            };

            if (data.ContainsKey("PerBank") &&
                data["PerBank"] is Dictionary<string, object> perBankMap)
            {
                foreach (var kvp in perBankMap)
                    stats.PerBank[kvp.Key] = Convert.ToInt32(kvp.Value);
            }

            Debug.Log(
                $"[FirestoreQuestionStatsRepository] Config/QuestionStats carregado — " +
                $"Total={stats.TotalQuestions}, Version={stats.Version}"
            );

            return stats;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[FirestoreQuestionStatsRepository] Erro ao ler Config/QuestionStats: {e.Message}");
            return null;
        }
    }
}
