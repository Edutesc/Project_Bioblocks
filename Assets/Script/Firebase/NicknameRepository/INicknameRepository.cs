using System.Threading.Tasks;

/// <summary>
/// Acesso remoto à coleção Nicknames.
/// </summary>
public interface INicknameRepository
{
    Task<bool> AreNicknameTaken(string nickName);

    Task ReserveNickname(string nickName, string userId);
}
