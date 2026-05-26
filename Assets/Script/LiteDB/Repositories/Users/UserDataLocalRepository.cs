using System;
using System.Collections.Generic;
using UnityEngine;

public class UserDataLocalRepository : MonoBehaviour, IUserDataLocalRepository
{
    private ILiteDBManager _db;

    public void InjectDependencies(ILiteDBManager db)
    {
        _db = db;
    }

    // ── Leitura ────────────────────────────────────────────────────────────────

    public UserData GetUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("[UserDataLocalRepository] GetUser chamado com userId vazio.");
            return null;
        }

        try
        {
            return _db.ExecuteRead(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);
                return doc?.ToDomain();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao buscar usuário: {e.Message}");
            return null;
        }
    }

    public bool HasUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return false;

        try
        {
            return _db.ExecuteRead(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                return users.FindById(userId) != null;
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao verificar usuário: {e.Message}");
            return false;
        }
    }

    public bool IsDirty(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return false;

        try
        {
            return _db.ExecuteRead(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);
                return doc?.IsDirty ?? false;
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao verificar dirty: {e.Message}");
            return false;
        }
    }

    public DateTime GetLastSyncedAt(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return DateTime.MinValue;

        try
        {
            return _db.ExecuteRead(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);

                if (doc == null || doc.LastSyncedAt == DateTime.MinValue)
                    return DateTime.MinValue;

                return DateTime.SpecifyKind(doc.LastSyncedAt, DateTimeKind.Utc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao buscar LastSyncedAt: {e.Message}");
            return DateTime.MinValue;
        }
    }

    // ── Escrita ────────────────────────────────────────────────────────────────

    public void SaveUser(UserData userData)
    {
        if (userData == null)
            throw new ArgumentNullException(nameof(userData));

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");

                var doc = UserDataDB.FromDomain(userData);
                doc.SavedAt = DateTime.UtcNow;

                users.Insert(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao salvar usuário: {e.Message}");
            throw;
        }
    }

    public void UpdateUser(UserData userData)
    {
        if (userData == null)
            throw new ArgumentNullException(nameof(userData));

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var existing = users.FindById(userData.UserId);

                var doc = UserDataDB.FromDomain(userData);

                if (existing != null)
                {
                    // Preserva metadados locais do cache.
                    doc.IsDirty      = existing.IsDirty;
                    doc.LastSyncedAt = existing.LastSyncedAt;
                }

                doc.SavedAt = userData.SavedAt != DateTime.MinValue
                    ? userData.SavedAt.ToUniversalTime()
                    : DateTime.UtcNow;

                users.Upsert(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao atualizar usuário: {e.Message}");
            throw;
        }
    }

    public void MarkAsDirty(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return;

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);
                if (doc == null) return;

                doc.IsDirty = true;
                users.Update(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao marcar dirty: {e.Message}");
            throw;
        }
    }

    public void MarkAsSynced(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return;

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);
                if (doc == null) return;

                doc.IsDirty     = false;
                doc.LastSyncedAt = DateTime.UtcNow;

                users.Update(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao marcar synced: {e.Message}");
            throw;
        }
    }

    /// <summary>
    /// Marca como sincronizado apenas se o registro local ainda corresponde ao snapshot
    /// que acabou de ser enviado ao Firestore. Evita limpar IsDirty quando uma alteração
    /// local mais nova já ocorreu enquanto o envio em background ainda estava em andamento.
    /// </summary>
    public bool MarkAsSyncedIfSavedAtMatches(string userId, DateTime expectedSavedAt)
    {
        if (string.IsNullOrEmpty(userId) || expectedSavedAt == DateTime.MinValue)
            return false;

        bool marked = false;

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);
                if (doc == null) return;

                DateTime currentSavedAt  = doc.SavedAt.ToUniversalTime();
                DateTime expectedUtcTime = expectedSavedAt.ToUniversalTime();

                if (currentSavedAt != expectedUtcTime)
                    return;

                doc.IsDirty     = false;
                doc.LastSyncedAt = DateTime.UtcNow;

                users.Update(doc);
                marked = true;
            });

            return marked;
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro em MarkAsSyncedIfSavedAtMatches: {e.Message}");
            throw;
        }
    }

    public void DeleteUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return;

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                users.Delete(userId);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao deletar usuário: {e.Message}");
            throw;
        }
    }

    public void UpdateScore(string userId, int newScore, int newWeekScore)
    {
        if (string.IsNullOrEmpty(userId))
            return;

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);
                if (doc == null) return;

                doc.Score     = newScore;
                doc.WeekScore = newWeekScore;
                doc.IsDirty   = true;
                doc.SavedAt   = DateTime.UtcNow;

                users.Update(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao atualizar score: {e.Message}");
            throw;
        }
    }

    public void AddAnsweredQuestion(string userId, string databankName, int questionNumber)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(databankName) || questionNumber <= 0)
            return;

        try
        {
            _db.ExecuteWrite(db =>
            {
                var users = db.GetCollection<UserDataDB>("users");
                var doc = users.FindById(userId);
                if (doc == null) return;

                doc.AnsweredQuestions ??= new Dictionary<string, List<int>>();

                if (!doc.AnsweredQuestions.ContainsKey(databankName))
                    doc.AnsweredQuestions[databankName] = new List<int>();

                if (!doc.AnsweredQuestions[databankName].Contains(questionNumber))
                    doc.AnsweredQuestions[databankName].Add(questionNumber);

                doc.IsDirty = true;
                doc.SavedAt = DateTime.UtcNow;

                users.Update(doc);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[UserDataLocalRepository] Erro ao adicionar questão respondida: {e.Message}");
            throw;
        }
    }
}
