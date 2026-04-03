// using UnityEngine;
// using System.Threading.Tasks;
// public class UserSyncTest : MonoBehaviour
// {
// private const string MOCK_USER_ID = "mock-user-123";
// async void Start()
// {
// // TESTE 1 — Carrega usuário (online: Firestore; offline: LiteDB)
// await UserSyncService.Instance.LoadUserAsync(MOCK_USER_ID);
// Debug.Log($"[TESTE 1] Usuário carregado: "
// + UserDataStore.CurrentUserData?.NickName);
// // TESTE 2 — Salva ação offline (simula sem rede)
// var user = LiteDBService.Instance.GetUser(MOCK_USER_ID);
// if (user != null)
// {
// user.Score += 50;
// await UserSyncService.Instance.SaveUserAsync(user);
// Debug.Log($"[TESTE 2] Score salvo: {user.Score}, "
// + $"IsDirty: {LiteDBService.Instance.GetUser(MOCK_USER_ID).IsDirty}");
// }
// // TESTE 3 — Força sincronização de pendentes
// await UserSyncService.Instance.SyncPendingUsersAsync();
// var synced = LiteDBService.Instance.GetUser(MOCK_USER_ID);
// Debug.Log($"[TESTE 3] Após sync — IsDirty: {synced?.IsDirty}, "
// + $"Status: {synced?.SyncStatus}");
// }
// }