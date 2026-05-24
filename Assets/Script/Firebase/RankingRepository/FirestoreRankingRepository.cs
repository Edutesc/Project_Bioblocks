using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

/// <summary>
/// Repositório remoto da coleção Rankings.
/// Firestore é a fonte verdade do ranking.
/// </summary>
public class FirestoreRankingRepository : IFirestoreRankingRepository
{
    private readonly FirebaseFirestore _db;

    public FirestoreRankingRepository(FirebaseFirestore db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<List<Ranking>> GetRankingsAsync(int limit = 20)
    {
        try
        {
            QuerySnapshot snap = await _db
                .Collection("Rankings")
                .OrderByDescending("score")
                .Limit(limit)
                .GetSnapshotAsync();

            return ToRankingList(snap);
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreRankingRepository] GetRankingsAsync falhou: {e.Message}");
            throw;
        }
    }

    private static List<Ranking> ToRankingList(QuerySnapshot snap)
    {
        var result = new List<Ranking>(snap.Count);

        foreach (DocumentSnapshot doc in snap.Documents)
        {
            try
            {
                Ranking ranking = doc.ConvertTo<Ranking>();
                result.Add(ranking);
            }
            catch (Exception e)
            {
                Debug.LogWarning(
                    $"[FirestoreRankingRepository] Doc {doc.Id} inválido, ignorando: {e.Message}"
                );
            }
        }

        return result;
    }
}
