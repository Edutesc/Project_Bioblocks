using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class UserDataSyncService : MonoBehaviour, IUserDataSyncService
{
    [SerializeField] private float cacheValidMinutes = 5f;

    private IUserDataLocalRepository _localRepository;
    private IFirestoreRepository _firestore;

    private readonly SemaphoreSlim _syncGate = new SemaphoreSlim(1, 1);

    public bool IsSyncing { get; private set; }

    public void InjectDependencies(
        IUserDataLocalRepository localRepository,
        IFirestoreRepository firestore)
    {
        _localRepository = localRepository;
        _firestore       = firestore;
    }

    // ── Sincronização principal ───────────────────────────────────────────────

    public async Task SyncFromFirestore(string userId)
    {
        if (!HasDependencies("SyncFromFirestore") || string.IsNullOrEmpty(userId))
            return;

        await _syncGate.WaitAsync();
        IsSyncing = true;

        try
        {
            var userData = await _firestore.GetUserData(userId);

            if (userData == null)
            {
                Debug.LogWarning($"[SyncService] Usuário {userId} não encontrado no Firestore.");
                return;
            }

            userData.SavedAt = userData.SavedAt != DateTime.MinValue
                ? userData.SavedAt.ToUniversalTime()
                : DateTime.UtcNow;

            _localRepository.UpdateUser(userData);
            _localRepository.MarkAsSynced(userId);
            UserDataStore.CurrentUserData = userData;

            Debug.Log("[SyncService] Dados sincronizados do Firestore para o cache local.");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[SyncService] Falha ao sincronizar do Firestore: {e.Message}");
            throw;
        }
        finally
        {
            IsSyncing = false;
            _syncGate.Release();
        }
    }

    public async Task SyncToFirestore(string userId)
    {
        if (!HasDependencies("SyncToFirestore") || string.IsNullOrEmpty(userId))
            return;

        await _syncGate.WaitAsync();
        IsSyncing = true;

        try
        {
            var userData = _localRepository.GetUser(userId);
            if (userData == null)
            {
                Debug.LogWarning($"[SyncService] Usuário {userId} não encontrado no cache local.");
                return;
            }

            await _firestore.UpdateUserData(userData);

            MarkAsSyncedIfNoNewerLocalChanges(userId, userData);

            Debug.Log("[SyncService] Dados locais enviados ao Firestore com sucesso.");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[SyncService] Falha ao enviar dados ao Firestore: {e.Message}");
            _localRepository.MarkAsDirty(userId);
            throw;
        }
        finally
        {
            IsSyncing = false;
            _syncGate.Release();
        }
    }

    public async Task TrySyncPendingData(string userId)
    {
        if (!HasDependencies("TrySyncPendingData") || string.IsNullOrEmpty(userId))
            return;

        try
        {
            bool hasLocal = _localRepository.HasUser(userId);

            // Sem cache local — dispositivo novo, busca do Firestore.
            if (!hasLocal)
            {
                Debug.Log("[SyncService] Sem cache local — buscando do Firestore...");
                await SyncFromFirestore(userId);
                return;
            }

            // Cache dirty — há dados locais para enviar.
            if (_localRepository.IsDirty(userId))
            {
                Debug.Log("[SyncService] Dados pendentes encontrados — enviando ao Firestore...");
                await SyncToFirestore(userId);
                return;
            }

            // Cache stale — verifica se Firestore tem dados mais recentes.
            if (IsCacheStale(userId))
            {
                Debug.Log("[SyncService] Cache desatualizado — comparando com Firestore...");
                await MergeWithFirestore(userId);
                return;
            }

            // Cache válido — usa LiteDB diretamente.
            Debug.Log("[SyncService] Cache válido — carregando do local.");
            var cached = _localRepository.GetUser(userId);
            if (cached != null)
                UserDataStore.CurrentUserData = cached;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[SyncService] TrySyncPendingData falhou — usando cache local: {e.Message}");

            var cached = _localRepository.GetUser(userId);
            if (cached != null)
                UserDataStore.CurrentUserData = cached;
        }
    }

    // ── Merge local/remoto ────────────────────────────────────────────────────

    private async Task MergeWithFirestore(string userId)
    {
        await _syncGate.WaitAsync();
        IsSyncing = true;

        try
        {
            var localData = _localRepository.GetUser(userId);
            UserData remoteData = null;

            try
            {
                remoteData = await _firestore.GetUserData(userId);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[SyncService] Firestore indisponível: {e.Message}");
            }

            if (remoteData == null)
            {
                Debug.Log("[SyncService] Firestore indisponível — usando LiteDB.");

                if (localData != null)
                    UserDataStore.CurrentUserData = localData;

                return;
            }

            if (localData == null)
            {
                remoteData.SavedAt = DateTime.UtcNow;
                _localRepository.UpdateUser(remoteData);
                _localRepository.MarkAsSynced(userId);
                UserDataStore.CurrentUserData = remoteData;
                return;
            }

            DateTime localSavedAt = localData.SavedAt != DateTime.MinValue
                ? localData.SavedAt.ToUniversalTime()
                : DateTime.MinValue;

            DateTime remoteSavedAt = remoteData.SavedAt != DateTime.MinValue
                ? remoteData.SavedAt.ToUniversalTime()
                : DateTime.MinValue;

            if (localSavedAt >= remoteSavedAt)
            {
                UserDataStore.CurrentUserData = localData;

                if (localSavedAt > remoteSavedAt)
                {
                    _localRepository.MarkAsDirty(userId);
                    Debug.Log("[SyncService] Cache local é mais recente — marcado como dirty para envio futuro.");
                }

                return;
            }

            remoteData.SavedAt = remoteSavedAt != DateTime.MinValue
                ? remoteSavedAt
                : DateTime.UtcNow;

            _localRepository.UpdateUser(remoteData);
            _localRepository.MarkAsSynced(userId);
            UserDataStore.CurrentUserData = remoteData;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[SyncService] MergeWithFirestore falhou: {e.Message}");

            var cached = _localRepository.GetUser(userId);
            if (cached != null)
                UserDataStore.CurrentUserData = cached;
        }
        finally
        {
            IsSyncing = false;
            _syncGate.Release();
        }
    }

    private bool IsCacheStale(string userId)
    {
        var lastSync = _localRepository.GetLastSyncedAt(userId);
        if (lastSync == DateTime.MinValue) return true;

        return (DateTime.UtcNow - lastSync.ToUniversalTime()).TotalMinutes > cacheValidMinutes;
    }

    // ── Atualização de pontuação ──────────────────────────────────────────────

    public Task UpdateUserScores(
        string userId,
        int additionalScore,
        int questionNumber,
        string databankName,
        bool isCorrect)
    {
        if (!HasDependencies("UpdateUserScores") || string.IsNullOrEmpty(userId))
            return Task.CompletedTask;

        var localUser = _localRepository.GetUser(userId);
        if (localUser == null)
        {
            Debug.LogWarning("[SyncService] Usuário não encontrado no cache local.");
            return Task.CompletedTask;
        }

        int newScore     = Math.Max(0, localUser.Score + additionalScore);
        int newWeekScore = Math.Max(0, localUser.WeekScore + additionalScore);

        // Atualiza primeiro o LiteDB. As operações internas passam pelo gate do LiteDBManager.
        _localRepository.UpdateScore(userId, newScore, newWeekScore);

        if (isCorrect && !string.IsNullOrEmpty(databankName) && questionNumber > 0)
            _localRepository.AddAnsweredQuestion(userId, databankName, questionNumber);

        // Captura o snapshot local já atualizado. Este snapshot é enviado ao Firestore
        // e também serve para decidir se ainda é seguro limpar IsDirty depois do envio.
        var capturedUserData = _localRepository.GetUser(userId);

        UpdateCurrentUserDataSnapshot(
            capturedUserData,
            newScore,
            newWeekScore,
            questionNumber,
            databankName,
            isCorrect
        );

        _ = SyncToFirestoreBackground(
            userId,
            additionalScore,
            questionNumber,
            databankName,
            isCorrect,
            capturedUserData
        );

        return Task.CompletedTask;
    }

    private async Task SyncToFirestoreBackground(
        string userId,
        int additionalScore,
        int questionNumber,
        string databankName,
        bool isCorrect,
        UserData capturedUserData)
    {
        await _syncGate.WaitAsync();
        IsSyncing = true;

        try
        {
            await _firestore.UpdateUserScores(
                userId,
                additionalScore,
                questionNumber,
                databankName,
                isCorrect,
                capturedUserData
            );

            MarkAsSyncedIfNoNewerLocalChanges(userId, capturedUserData);
            Debug.Log("[SyncService] Score sincronizado com Firestore.");
        }
        catch (Exception e)
        {
            _localRepository.MarkAsDirty(userId);

            Debug.LogError($"[SyncBackground] FALHA — {e.GetType().Name}: {e.Message}");
            Debug.LogError($"[SyncBackground] StackTrace: {e.StackTrace}");
        }
        finally
        {
            IsSyncing = false;
            _syncGate.Release();
        }
    }

    private void MarkAsSyncedIfNoNewerLocalChanges(string userId, UserData capturedUserData)
    {
        if (capturedUserData == null)
        {
            Debug.LogWarning("[SyncService] Snapshot enviado ao Firestore é nulo — mantendo dirty como proteção.");
            _localRepository.MarkAsDirty(userId);
            return;
        }

        bool marked = _localRepository.MarkAsSyncedIfSavedAtMatches(
            userId,
            capturedUserData.SavedAt
        );

        if (!marked)
        {
            Debug.Log("[SyncService] Há alterações locais mais recentes — mantendo dirty para sync futuro.");
            _localRepository.MarkAsDirty(userId);
        }
    }

    private static void UpdateCurrentUserDataSnapshot(
        UserData capturedUserData,
        int newScore,
        int newWeekScore,
        int questionNumber,
        string databankName,
        bool isCorrect)
    {
        var current = UserDataStore.CurrentUserData ?? capturedUserData;
        if (current == null)
            return;

        current.Score = newScore;
        current.WeekScore = newWeekScore;

        if (capturedUserData != null)
        {
            current.SavedAt = capturedUserData.SavedAt;
            current.AnsweredQuestions = capturedUserData.AnsweredQuestions;
        }
        else if (isCorrect && !string.IsNullOrEmpty(databankName) && questionNumber > 0)
        {
            current.AnsweredQuestions ??= new Dictionary<string, List<int>>();

            if (!current.AnsweredQuestions.ContainsKey(databankName))
                current.AnsweredQuestions[databankName] = new List<int>();

            if (!current.AnsweredQuestions[databankName].Contains(questionNumber))
                current.AnsweredQuestions[databankName].Add(questionNumber);
        }

        UserDataStore.CurrentUserData = current;
    }

    private bool HasDependencies(string caller)
    {
        if (_localRepository != null && _firestore != null)
            return true;

        Debug.LogWarning($"[SyncService] {caller} chamado antes da injeção de dependências.");
        return false;
    }
}
