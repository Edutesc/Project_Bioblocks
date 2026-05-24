using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

/// <summary>
/// Repositório remoto da coleção Users.
/// Não lida com Nicknames, Rankings, Config, UserBonus ou listeners.
/// </summary>
public class FirestoreUserRepository : IFirestoreUserRepository
{
    private readonly FirebaseFirestore _db;

    public FirestoreUserRepository(FirebaseFirestore db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<UserData> GetUserData(string userId)
    {
        try
        {
            DocumentSnapshot snapshot = await _db.Collection("Users")
                .Document(userId)
                .GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Debug.LogError($"Documento do usuário {userId} não encontrado");
                return null;
            }

            UserData user = FirestoreUserDataMapper.FromDictionary(snapshot.ToDictionary());

            Debug.Log(
                $"[FirestoreUserRepository] UserData carregado - " +
                $"NickName: {user.NickName}, Score: {user.Score}"
            );

            return user;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao buscar dados do usuário: {ex.Message}");
            throw;
        }
    }

    public async Task<List<UserData>> GetAllUsersData()
    {
        try
        {
            QuerySnapshot querySnapshot = await _db.Collection("Users").GetSnapshotAsync();
            List<UserData> users = new List<UserData>();

            foreach (DocumentSnapshot doc in querySnapshot.Documents)
                users.Add(FirestoreUserDataMapper.FromDictionary(doc.ToDictionary()));

            return users;
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao buscar todos os usuários: {e.Message}");
            throw;
        }
    }

    public async Task CreateUserDocument(UserData userData)
    {
        try
        {
            if (string.IsNullOrEmpty(userData.UserId))
                throw new ArgumentException("UserId não pode ser vazio");

            DocumentReference docRef = _db.Collection("Users").Document(userData.UserId);
            await docRef.SetAsync(FirestoreUserDataMapper.ToNewUserDocument(userData));

            Debug.Log($"[FirestoreUserRepository] Documento do usuário criado: {userData.UserId}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao criar documento do usuário: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserScores(
        string userId,
        int additionalScore,
        int questionNumber,
        string databankName,
        bool isCorrect,
        UserData capturedUserData)
    {
        if (capturedUserData == null || capturedUserData.UserId != userId)
        {
            Debug.LogError("[FirestoreUserRepository] capturedUserData inválido em UpdateUserScores.");
            return;
        }

        var updates = new Dictionary<string, object>
        {
            { "Score", capturedUserData.Score },
            { "WeekScore", capturedUserData.WeekScore },
            { "SavedAt", FieldValue.ServerTimestamp }
        };

        if (isCorrect && !string.IsNullOrEmpty(databankName) && questionNumber > 0)
        {
            updates[$"AnsweredQuestions.{databankName}"] =
                FieldValue.ArrayUnion(questionNumber);
        }

        DocumentReference docRef = _db.Collection("Users").Document(userId);
        await docRef.UpdateAsync(updates);

        Debug.Log(
            $"[FirestoreUserRepository] UpdateUserScores concluído. " +
            $"Score={capturedUserData.Score}, WeekScore={capturedUserData.WeekScore}"
        );
    }

    public async Task UpdateUserScore(
        string userId,
        int newScore,
        int questionNumber,
        string databankName,
        bool isCorrect)
    {
        var updates = new Dictionary<string, object>
        {
            { "Score", newScore },
            { "SavedAt", FieldValue.ServerTimestamp }
        };

        if (isCorrect && !string.IsNullOrEmpty(databankName) && questionNumber > 0)
        {
            updates[$"AnsweredQuestions.{databankName}"] =
                FieldValue.ArrayUnion(questionNumber);
        }

        DocumentReference docRef = _db.Collection("Users").Document(userId);
        await docRef.UpdateAsync(updates);

        Debug.Log($"[FirestoreUserRepository] UpdateUserScore concluído. Score={newScore}");
    }

    public async Task UpdateUserWeekScore(string userId, int weekScore)
    {
        DocumentReference docRef = _db.Collection("Users").Document(userId);

        await docRef.UpdateAsync(new Dictionary<string, object>
        {
            { "WeekScore", weekScore },
            { "SavedAt", FieldValue.ServerTimestamp }
        });

        Debug.Log($"[FirestoreUserRepository] WeekScore atualizado: {weekScore}");
    }

    public async Task UpdateUserData(UserData userData)
    {
        try
        {
            if (string.IsNullOrEmpty(userData.UserId))
                throw new ArgumentException("UserId não pode ser vazio");

            DocumentReference docRef = _db.Collection("Users").Document(userData.UserId);
            await docRef.UpdateAsync(FirestoreUserDataMapper.ToDictionary(userData));

            Debug.Log($"[FirestoreUserRepository] Dados do usuário {userData.UserId} atualizados.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao atualizar dados do usuário: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserProgress(string userId, int progress)
    {
        try
        {
            DocumentReference docRef = _db.Collection("Users").Document(userId);

            await docRef.UpdateAsync(new Dictionary<string, object>
            {
                { "Progress", progress }
            });

            Debug.Log($"[FirestoreUserRepository] Progresso do usuário atualizado para {progress}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao atualizar progresso do usuário: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserProfileImageUrl(string userId, string imageUrl)
    {
        try
        {
            DocumentReference userRef = _db.Collection("Users").Document(userId);

            await userRef.UpdateAsync(new Dictionary<string, object>
            {
                { "ProfileImageUrl", imageUrl },
                { "SavedAt", FieldValue.ServerTimestamp }
            });
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao atualizar URL da imagem de perfil: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateUserField(string userId, string fieldName, object value)
    {
        try
        {
            DocumentReference docRef = _db.Collection("Users").Document(userId);

            await docRef.UpdateAsync(new Dictionary<string, object>
            {
                { fieldName, value }
            });

            Debug.Log($"[FirestoreUserRepository] {fieldName} atualizado para {value}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao atualizar {fieldName}: {e.Message}");
            throw;
        }
    }

    public async Task ResetAnsweredQuestions(string userId, string databankName)
    {
        try
        {
            DocumentReference docRef = _db.Collection("Users").Document(userId);

            await _db.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(docRef);

                if (!snapshot.Exists)
                {
                    Debug.LogError($"Usuário {userId} não encontrado!");
                    return;
                }

                Dictionary<string, List<int>> answeredQuestions =
                    snapshot.GetValue<Dictionary<string, List<int>>>("AnsweredQuestions");

                if (answeredQuestions != null && answeredQuestions.ContainsKey(databankName))
                {
                    answeredQuestions.Remove(databankName);
                    transaction.Update(docRef, "AnsweredQuestions", answeredQuestions);
                }
                else
                {
                    Debug.LogWarning(
                        $"AnsweredQuestions para {databankName} não encontrada para o usuário {userId}"
                    );
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreUserRepository] Erro ao remover AnsweredQuestions: {e.Message}");
            throw;
        }
    }
}
