using System;
using System.Threading.Tasks;

public interface IUserSyncService
{
    event Action        OnSyncStarted;
    event Action<bool>  OnSyncCompleted;

    Task LoadUserAsync(string userId);
    Task SaveUserAsync(UserDataDB user);
    Task SyncPendingUsersAsync();
}