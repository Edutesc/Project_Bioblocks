using LiteDB;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LiteDBService : IDisposable
{
    private static LiteDBService _instance;
    public static LiteDBService Instance => _instance ??= new LiteDBService();
    private LiteDatabase _db;
    private ILiteCollection<UserDataDB> _users;
    private LiteDBService()
    {
        var dbPath = $"{Application.persistentDataPath}/localdata.db";
        _db = new LiteDatabase(dbPath);
        _users = _db.GetCollection<UserDataDB>("users");
        _users.EnsureIndex(x => x.UserId);
    } 

    public void SaveUser(UserDataDB user)
    {
        user.LastModifiedLocal = DateTime.UtcNow;
        user.IsDirty = true;
        _users.Upsert(user);
    }
    public UserDataDB GetUser(string userId)
    {
        return _users.FindById(userId);
    }
    public void MarkAsSynced(string userId)
    {
        var user = GetUser(userId);
        if (user == null) return;
        user.IsDirty = false;
        user.LastSyncedAt = DateTime.UtcNow;
        user.SyncStatus = SyncStatus.Synced;
        _users.Update(user);
    }

    public List<UserDataDB> GetAllDirtyUsers()
    {
        return _users.Find(u => u.IsDirty).ToList();
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
    
}