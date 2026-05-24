using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

public class FirestoreNicknameRepository : INicknameRepository
{
    private readonly FirebaseFirestore _db;

    public FirestoreNicknameRepository(FirebaseFirestore db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<bool> AreNicknameTaken(string nickName)
    {
        DocumentSnapshot snapshot = await _db.Collection("Nicknames")
            .Document(nickName.ToLower())
            .GetSnapshotAsync();

        return snapshot.Exists;
    }

    public async Task ReserveNickname(string nickName, string userId)
    {
        await _db.Collection("Nicknames")
            .Document(nickName.ToLower())
            .SetAsync(new Dictionary<string, object>
            {
                { "userId", userId }
            });
    }
}
