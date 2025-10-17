using UnityEngine;

/// <summary>
/// Helper estático para sincronizar mudanças de avatar entre ProfileScene e UserTopBar
/// Segue o mesmo padrão do projeto para manter consistência
/// </summary>
public static class UserAvatarSyncHelper
{
    /// <summary>
    /// Notifica a UserTopBar que o avatar foi atualizado
    /// Deve ser chamado após o upload bem-sucedido de uma nova imagem no ProfileImageManager
    /// </summary>
    public static void NotifyAvatarChanged(string newImageUrl)
    {
        if (UserTopBarManager.Instance != null)
        {
            UserTopBarManager.Instance.UpdateAvatarFromUrl(newImageUrl);
            Debug.Log($"[AvatarSync] UserTopBar notificada sobre mudança de avatar: {newImageUrl}");
        }
        else
        {
            Debug.LogWarning("[AvatarSync] UserTopBarManager não está disponível para atualizar o avatar");
        }
    }

    /// <summary>
    /// Força reload completo do avatar na UserTopBar usando os dados atuais do UserData
    /// Útil quando há mudanças no UserData ou quando a TopBar é reativada
    /// </summary>
    public static void RefreshAvatar()
    {
        if (UserTopBarManager.Instance != null)
        {
            UserTopBarManager.Instance.RefreshUserAvatar();
            Debug.Log("[AvatarSync] Avatar da UserTopBar atualizado");
        }
        else
        {
            Debug.LogWarning("[AvatarSync] UserTopBarManager não está disponível");
        }
    }
}