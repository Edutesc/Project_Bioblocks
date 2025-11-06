using System.IO;
using UnityEngine;

public static class DatabaseConfig
{
    private static string _databasePath;

    public static string DatabasePath
    {
        get
        {
            if (string.IsNullOrEmpty(_databasePath))
            {
                _databasePath = Path.Combine(Application.persistentDataPath, "bioblocks.db");
            }
            return _databasePath;
        }
    }

    public static string DatabaseName => "bioblocks.db";

    public static int DatabaseVersion => 1;

    public static void EnsureDatabaseDirectory()
    {
        string directory = Path.GetDirectoryName(DatabasePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public static bool DatabaseExists()
    {
        return File.Exists(DatabasePath);
    }

    public static void DeleteDatabase()
    {
        if (DatabaseExists())
        {
            File.Delete(DatabasePath);
            Debug.Log($"Database deleted: {DatabasePath}");
        }
    }
}
