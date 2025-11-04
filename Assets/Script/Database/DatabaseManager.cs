using UnityEngine;
using SQLite4Unity3d;
using System;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager _instance;
    public static DatabaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("DatabaseManager");
                _instance = go.AddComponent<DatabaseManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private SQLiteConnection _connection;
    private readonly object _lock = new object();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        try
        {
            DatabaseConfig.EnsureDatabaseDirectory();
            
            _connection = new SQLiteConnection(DatabaseConfig.DatabasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            
            CreateTables();
            
            Debug.Log($"[DatabaseManager] Database initialized at: {DatabaseConfig.DatabasePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[DatabaseManager] Failed to initialize database: {e.Message}");
            throw;
        }
    }

    private void CreateTables()
    {
        lock (_lock)
        {
            _connection.CreateTable<RankingEntity>();
            _connection.CreateTable<CachedImageEntity>();
            _connection.CreateTable<SyncMetadataEntity>();
            
            Debug.Log("[DatabaseManager] Tables created successfully");
        }
    }

    public SQLiteConnection GetConnection()
    {
        if (_connection == null)
        {
            InitializeDatabase();
        }
        return _connection;
    }

    public void ExecuteInTransaction(Action action)
    {
        lock (_lock)
        {
            _connection.RunInTransaction(action);
        }
    }

    public void ClearAllData()
    {
        lock (_lock)
        {
            _connection.DeleteAll<RankingEntity>();
            _connection.DeleteAll<CachedImageEntity>();
            _connection.DeleteAll<SyncMetadataEntity>();
            
            Debug.Log("[DatabaseManager] All data cleared");
        }
    }

    public void DeleteDatabase()
    {
        lock (_lock)
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
            
            DatabaseConfig.DeleteDatabase();
            Debug.Log("[DatabaseManager] Database deleted");
        }
    }

    private void OnApplicationQuit()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }

    private void OnDestroy()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }
}