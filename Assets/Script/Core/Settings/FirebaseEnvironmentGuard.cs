using System;
using System.IO;
using Firebase.Auth;
using UnityEngine;

/// <summary>
/// Impede que uma sessao ou cache de um projeto Firebase seja reutilizado
/// depois de uma troca explicita entre Dev e Prod.
/// </summary>
public static class FirebaseEnvironmentGuard
{
    internal const string EnvironmentPrefsKey = "FirebaseEnvironment";

    private static readonly string[] EnvironmentScopedPrefsKeys =
    {
        "UserId",
        "UserEmail",
        "UserNickname",
        LocalSessionState.ActiveKey,
        "QuestionCache_Version",
        "RankingCache_LastSyncUtcTicks"
    };

    public static void Apply(FirebaseEnvironment currentEnvironment)
    {
        string current = currentEnvironment.ToString();
        string previous = PlayerPrefs.GetString(EnvironmentPrefsKey, string.Empty);

        // Primeiro uso (incluindo clone novo): apenas registra o ambiente.
        if (string.IsNullOrEmpty(previous))
        {
            SaveCurrentEnvironment(current);
            Debug.Log($"[FirebaseEnvironmentGuard] Ambiente inicial registrado: {current}.");
            return;
        }

        if (string.Equals(previous, current, StringComparison.Ordinal))
            return;

        Debug.LogWarning(
            $"[FirebaseEnvironmentGuard] Ambiente Firebase mudou de {previous} para {current}. " +
            "Limpando sessao e caches vinculados ao projeto anterior."
        );

        try
        {
            ClearEnvironmentScopedData();
            SaveCurrentEnvironment(current);
            Debug.Log("[FirebaseEnvironmentGuard] Troca de ambiente concluida. Um novo login sera necessario.");
        }
        catch (Exception e)
        {
            // Nao registra o novo ambiente se a limpeza falhar. Na proxima
            // inicializacao, a protecao tenta novamente.
            throw new InvalidOperationException(
                $"Falha ao trocar o ambiente Firebase de {previous} para {current}.",
                e
            );
        }
    }

    /// <summary>
    /// Limpeza explicita para migrar instalacoes que usaram a logica antiga,
    /// que registrava o novo ambiente sem invalidar a sessao anterior.
    /// </summary>
    public static void ResetCurrentEnvironment(FirebaseEnvironment currentEnvironment)
    {
        ClearEnvironmentScopedData();
        SaveCurrentEnvironment(currentEnvironment.ToString());
        Debug.Log($"[FirebaseEnvironmentGuard] Dados locais redefinidos para {currentEnvironment}.");
    }

    private static void ClearEnvironmentScopedData()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        UserDataStore.CurrentUserData = null;

        foreach (string key in EnvironmentScopedPrefsKeys)
            PlayerPrefs.DeleteKey(key);

        DeleteLiteDbFiles();
        DeleteImageCache();
    }

    private static void SaveCurrentEnvironment(string environment)
    {
        PlayerPrefs.SetString(EnvironmentPrefsKey, environment);
        PlayerPrefs.Save();
    }

    private static void DeleteLiteDbFiles()
    {
        string dbPath = Path.Combine(Application.persistentDataPath, "app_cache.db");
        string[] files =
        {
            dbPath,
            dbPath.Replace(".db", "-log.db"),
            dbPath.Replace(".db", "-tmp.db")
        };

        foreach (string file in files)
        {
            if (File.Exists(file))
                File.Delete(file);
        }
    }

    private static void DeleteImageCache()
    {
        string cachePath = Path.Combine(Application.persistentDataPath, "ImageCache");

        if (!Directory.Exists(cachePath))
            return;

        FileAttributes attributes = File.GetAttributes(cachePath);
        if ((attributes & FileAttributes.ReparsePoint) != 0)
            throw new IOException($"ImageCache aponta para um link e nao sera removido: {cachePath}");

        Directory.Delete(cachePath, recursive: true);
    }
}
