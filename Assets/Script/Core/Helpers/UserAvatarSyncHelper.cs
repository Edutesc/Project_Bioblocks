using UnityEngine;

/// <summary>
/// Helper est�tico para sincronizar mudan�as de avatar entre ProfileScene e UserTopBar
/// Segue o mesmo padr�o do projeto para manter consist�ncia
/// </summary>
public static class UserAvatarSyncHelper
{
    /// <summary>
    /// Notifica a UserTopBar que o avatar foi atualizado
    /// Deve ser chamado ap�s o upload bem-sucedido de uma nova imagem no ProfileImageManager
    /// </summary>
    public static void NotifyAvatarChanged(string newImageUrl)
    {
        if (UserHeaderManager.Instance != null)
        {
            UserHeaderManager.Instance.UpdateAvatarFromUrl(newImageUrl);
            Debug.Log($"[AvatarSync] UserTopBar notificada sobre mudan�a de avatar: {newImageUrl}");
        }
        else
        {
            Debug.LogWarning("[AvatarSync] UserTopBarManager n�o est� dispon�vel para atualizar o avatar");
        }
    }

    /// <summary>
    /// For�a reload completo do avatar na UserTopBar usando os dados atuais do UserData
    /// �til quando h� mudan�as no UserData ou quando a TopBar � reativada
    /// </summary>
    public static void RefreshAvatar()
    {
        if (UserHeaderManager.Instance != null)
        {
            UserHeaderManager.Instance.RefreshUserAvatar();
            Debug.Log("[AvatarSync] Avatar da UserTopBar atualizado");
        }
        else
        {
            Debug.LogWarning("[AvatarSync] UserTopBarManager n�o est� dispon�vel");
        }
    }
}