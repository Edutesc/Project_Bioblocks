using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Serviço de banco de dados local usando LiteDB.
///
/// Registrado no AppContext como ILiteDBService.
/// NÃO use LiteDBService.Instance diretamente fora desta classe —
/// acesse sempre via AppContext.LocalDB.
/// </summary>
public class LiteDBService : ILiteDBService, IDisposable
{
    private LiteDatabase                  _db;
    private ILiteCollection<UserDataDB>   _users;
    private ILiteCollection<RankingDB>    _rankings;

    public LiteDBService()
    {
        var dbPath = $"{Application.persistentDataPath}/localdata.db";
        _db       = new LiteDatabase(dbPath);

        _users    = _db.GetCollection<UserDataDB>("users");
        _users.EnsureIndex(x => x.UserId);

        _rankings = _db.GetCollection<RankingDB>("rankings");
        _rankings.EnsureIndex(x => x.UserId);

        Debug.Log($"[LiteDBService] Banco inicializado em: {dbPath}");
    }

    // ─────────────────────────────────────────────────────
    // Users — fonte da verdade local
    // ─────────────────────────────────────────────────────

    public void SaveUser(UserDataDB user)
    {
        user.LastModifiedLocal = DateTime.UtcNow;
        user.IsDirty           = true;
        user.SyncStatus        = SyncStatus.PendingUpload;
        _users.Upsert(user);
    }

    public UserDataDB GetUser(string userId)
        => _users.FindById(userId);

    public void MarkUserAsSynced(string userId)
    {
        var user = GetUser(userId);
        if (user == null) return;

        user.IsDirty      = false;
        user.LastSyncedAt = DateTime.UtcNow;
        user.SyncStatus   = SyncStatus.Synced;
        _users.Update(user);
    }

    public List<UserDataDB> GetAllDirtyUsers()
        => _users.Find(u => u.IsDirty).ToList();

    // ─────────────────────────────────────────────────────
    // Rankings — cache de leitura (Firestore é a fonte da verdade)
    // ─────────────────────────────────────────────────────

    public void SaveRankings(List<RankingDB> rankings)
    {
        var now = DateTime.UtcNow;
        foreach (var r in rankings)
        {
            r.CachedAt = now;
            _rankings.Upsert(r);
        }
        Debug.Log($"[LiteDBService] {rankings.Count} rankings salvos em cache.");
    }

    public List<RankingDB> GetCachedRankings()
        => _rankings.FindAll()
                    .OrderByDescending(r => r.Score)
                    .ToList();

    public RankingDB GetCachedUserRanking(string userId)
        => _rankings.FindById(userId);

    public void ClearRankingCache()
    {
        _rankings.DeleteAll();
        Debug.Log("[LiteDBService] Cache de rankings limpo.");
    }

    // ─────────────────────────────────────────────────────
    // Dispose
    // ─────────────────────────────────────────────────────

    public void Dispose()
    {
        _db?.Dispose();
        _db = null;
        Debug.Log("[LiteDBService] Banco de dados fechado.");
    }
}