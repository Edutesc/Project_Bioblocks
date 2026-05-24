using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

public class FirestoreAdminRepository : IFirestoreAdminRepository
{
    private readonly FirebaseFirestore _db;

    public FirestoreAdminRepository(FirebaseFirestore db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task DeleteDocument(string collection, string documentId)
    {
        try
        {
            var user = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
            if (user == null)
                throw new Exception("Usuário não está autenticado");

            string token = await user.TokenAsync(true);
            DocumentReference docRef = _db.Collection(collection).Document(documentId);

            int maxRetries = 3;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    await docRef.DeleteAsync();
                    Debug.Log($"Documento {documentId} deletado com sucesso da coleção {collection}");
                    return;
                }
                catch (Exception e) when (i < maxRetries - 1)
                {
                    Debug.LogWarning($"Tentativa {i + 1} falhou: {e.Message}. Tentando novamente...");
                    await Task.Delay(1000);
                    token = await user.TokenAsync(true);
                }
            }

            throw new Exception($"Falha ao deletar documento após {maxRetries} tentativas");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreAdminRepository] Erro ao deletar documento: {e.Message}");
            throw;
        }
    }

    public async Task EnsureWeekScoreField()
    {
        try
        {
            QuerySnapshot querySnapshot = await _db.Collection("Users").GetSnapshotAsync();

            WriteBatch batch = _db.StartBatch();
            int userCount = 0;

            foreach (DocumentSnapshot doc in querySnapshot.Documents)
            {
                if (!doc.ContainsField("WeekScore"))
                {
                    batch.Update(doc.Reference, "WeekScore", 0);
                    userCount++;

                    if (userCount >= 450)
                    {
                        await batch.CommitAsync();
                        batch = _db.StartBatch();
                        userCount = 0;
                    }
                }
            }

            if (userCount > 0)
                await batch.CommitAsync();

            Debug.Log("[FirestoreAdminRepository] Verificação de WeekScore concluída.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreAdminRepository] Erro ao verificar campo WeekScore: {e.Message}");
            throw;
        }
    }
}
