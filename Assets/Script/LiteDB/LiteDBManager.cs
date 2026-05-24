using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using LiteDB;

public class LiteDBManager : MonoBehaviour, ILiteDBManager
{
    private LiteDatabase _db;
    private readonly SemaphoreSlim _dbGate = new SemaphoreSlim(1, 1);

    private const string DB_NAME = "app_cache.db";

    public bool IsInitialized { get; private set; }

    public LiteDatabase Database
    {
        get
        {
            if (_db == null)
                throw new Exception("[LiteDBManager] Banco não inicializado.");

            return _db;
        }
    }

    // Mantenha por enquanto, mas evite usar diretamente fora do LiteDBManager.
    public ILiteCollection<UserDataDB>    Users        => Database.GetCollection<UserDataDB>("users");
    public ILiteCollection<RankingDB>     Rankings     => Database.GetCollection<RankingDB>("rankings");
    public ILiteCollection<CachedImageDB> CachedImages => Database.GetCollection<CachedImageDB>("cached_images");
    public ILiteCollection<QuestionDB>    Questions    => Database.GetCollection<QuestionDB>("questions");

    public void Initialize()
    {
        if (IsInitialized) return;

        string path = System.IO.Path.Combine(Application.persistentDataPath, DB_NAME);

        _dbGate.Wait();

        try
        {
            try
            {
                OpenDatabase(path);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[LiteDBManager] Banco corrompido ({e.Message}) — recriando banco limpo...");

                SafeDisposeDatabase();
                DeleteDatabaseFiles(path);

                OpenDatabase(path);
                Debug.Log("[LiteDBManager] Banco recriado com sucesso.");
            }
        }
        finally
        {
            _dbGate.Release();
        }
    }

    public async Task ExecuteWriteAsync(Action<LiteDatabase> action, CancellationToken ct = default)
    {
        await _dbGate.WaitAsync(ct);

        try
        {
            EnsureInitialized();
            action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    public async Task<T> ExecuteReadAsync<T>(Func<LiteDatabase, T> action, CancellationToken ct = default)
    {
        await _dbGate.WaitAsync(ct);

        try
        {
            EnsureInitialized();
            return action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    public void ExecuteWrite(Action<LiteDatabase> action)
    {
        _dbGate.Wait();

        try
        {
            EnsureInitialized();
            action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    public T ExecuteRead<T>(Func<LiteDatabase, T> action)
    {
        _dbGate.Wait();

        try
        {
            EnsureInitialized();
            return action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    private void OpenDatabase(string path)
    {
        var mapper = new BsonMapper();

        mapper.ResolveMember += (type, memberInfo, memberMapper) =>
        {
            if (memberMapper.DataType == typeof(DateTime))
            {
                memberMapper.Serialize = (obj, m) =>
                    new BsonValue(((DateTime)obj).ToUniversalTime());

                memberMapper.Deserialize = (val, m) =>
                    DateTime.SpecifyKind(val.AsDateTime, DateTimeKind.Utc);
            }
        };

        SafeDisposeDatabase();

        _db = new LiteDatabase(path, mapper);

        EnsureIndexes();

        IsInitialized = true;
    }

    private void EnsureIndexes()
    {
        Users.EnsureIndex(x => x.UserId, unique: true);

        CachedImages.EnsureIndex(x => x.ImageUrl, unique: true);
        CachedImages.EnsureIndex(x => x.Topic);

        Rankings.EnsureIndex(x => x.Score);
        Rankings.EnsureIndex(x => x.WeekScore);

        Questions.EnsureIndex(x => x.QuestionDatabankName);
        Questions.EnsureIndex(x => x.Topic);
        Questions.EnsureIndex(x => x.CachedAt);
    }

    private void EnsureInitialized()
    {
        if (!IsInitialized || _db == null)
            throw new InvalidOperationException("[LiteDBManager] Banco não inicializado.");
    }

    private void SafeDisposeDatabase()
    {
        if (_db != null)
        {
            _db.Dispose();
            _db = null;
        }

        IsInitialized = false;
    }

    private static void DeleteDatabaseFiles(string dbPath)
    {
        string[] relatedFiles =
        {
            dbPath,
            dbPath.Replace(".db", "-log.db"),
            dbPath.Replace(".db", "-tmp.db")
        };

        foreach (var file in relatedFiles)
        {
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
                Debug.Log($"[LiteDBManager] Arquivo deletado: {file}");
            }
        }
    }

    private void OnDestroy() => Close();

    private void OnApplicationQuit() => Close();

    public void Close()
    {
        _dbGate.Wait();

        try
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
                IsInitialized = false;
                Debug.Log("[LiteDBManager] Banco fechado.");
            }
        }
        finally
        {
            _dbGate.Release();
        }
    }
}