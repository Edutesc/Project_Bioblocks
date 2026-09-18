using System.Threading.Tasks;

public interface ITopicReviewSyncService
{
    bool IsSyncing { get; }

    Task SyncFromFirestore(string userId, string databankName);
    Task SyncToFirestore(string userId, string databankName);
    Task SyncPendingForUser(string userId);
    Task PushPendingChanges(string userId);
}