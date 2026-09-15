using UnityEngine;

/// <summary>
/// Identidade local que autoriza a abertura offline do app.
/// O LiteDB, sozinho, é apenas cache e nunca deve ressuscitar uma sessão após logout.
/// </summary>
public static class LocalSessionState
{
    public const string ActiveKey = "LocalSessionActive";
    private const int Active = 1;
    private const int SignedOut = 0;

    public static bool CanRestore(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return false;

        if (PlayerPrefs.HasKey(ActiveKey))
            return PlayerPrefs.GetInt(ActiveKey, SignedOut) == Active;

        // Migração de instalações anteriores: nelas, a existência de UserId
        // significava que o último login não havia sido encerrado explicitamente.
        return true;
    }

    public static void MarkAuthenticated(string userId, string email, string nickname)
    {
        if (string.IsNullOrEmpty(userId))
            return;

        PlayerPrefs.SetString("UserId", userId);
        PlayerPrefs.SetString("UserEmail", email ?? string.Empty);
        PlayerPrefs.SetString("UserNickname", nickname ?? string.Empty);
        PlayerPrefs.SetInt(ActiveKey, Active);
        PlayerPrefs.Save();
    }

    public static void MarkSignedOut()
    {
        PlayerPrefs.DeleteKey("UserId");
        PlayerPrefs.DeleteKey("UserEmail");
        PlayerPrefs.DeleteKey("UserNickname");
        PlayerPrefs.SetInt(ActiveKey, SignedOut);
        PlayerPrefs.Save();
    }
}
