using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

public class FirestoreUserBonusRepository : IUserBonusRepository
{
    private readonly FirebaseFirestore _db;

    public FirestoreUserBonusRepository(FirebaseFirestore db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<bool> IsDatabankEligibleForBonus(string userId, string databankName)
    {
        try
        {
            DocumentSnapshot snapshot = await _db.Collection("UserBonus")
                .Document(userId)
                .GetSnapshotAsync();

            if (!snapshot.Exists)
                return true;

            var data = snapshot.ToDictionary();

            if (!data.ContainsKey("CompletedDatabanks"))
                return true;

            if (data["CompletedDatabanks"] is IEnumerable<object> completedDatabanks)
            {
                return !completedDatabanks
                    .Select(x => x.ToString())
                    .Contains(databankName);
            }

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(
                $"[FirestoreUserBonusRepository] Erro ao verificar elegibilidade do databank: {e.Message}"
            );

            return false;
        }
    }

    public async Task MarkDatabankAsCompleted(string userId, string databankName)
    {
        try
        {
            DocumentReference docRef = _db.Collection("UserBonus").Document(userId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            var completedDatabanks = new List<string>();

            if (snapshot.Exists)
            {
                var data = snapshot.ToDictionary();

                if (data.ContainsKey("CompletedDatabanks") &&
                    data["CompletedDatabanks"] is IEnumerable<object> existingList)
                {
                    completedDatabanks = existingList
                        .Select(i => i.ToString())
                        .ToList();
                }
            }

            if (completedDatabanks.Contains(databankName))
            {
                Debug.LogWarning(
                    $"[FirestoreUserBonusRepository] Databank '{databankName}' já está em CompletedDatabanks."
                );

                return;
            }

            completedDatabanks.Add(databankName);

            if (snapshot.Exists)
            {
                await docRef.UpdateAsync(new Dictionary<string, object>
                {
                    { "CompletedDatabanks", completedDatabanks }
                });
            }
            else
            {
                await docRef.SetAsync(new Dictionary<string, object>
                {
                    { "CompletedDatabanks", completedDatabanks }
                });
            }

            Debug.Log(
                $"[FirestoreUserBonusRepository] Databank '{databankName}' marcado como completo para userId={userId}."
            );
        }
        catch (Exception e)
        {
            Debug.LogError(
                $"[FirestoreUserBonusRepository] Erro ao marcar databank como completo: {e.Message}"
            );

            throw;
        }
    }
}
