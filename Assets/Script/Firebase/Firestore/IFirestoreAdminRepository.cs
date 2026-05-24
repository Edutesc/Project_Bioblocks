using System.Threading.Tasks;

/// <summary>
/// Operações administrativas/genéricas no Firestore.
/// Evite usar no fluxo normal do app.
/// </summary>
public interface IFirestoreAdminRepository
{
    Task EnsureWeekScoreField();

    Task DeleteDocument(string collection, string documentId);
}
