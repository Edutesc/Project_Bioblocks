using System.Threading.Tasks;

/// <summary>
/// Acesso remoto à coleção UserBonus.
/// </summary>
public interface IUserBonusRepository
{
    Task<bool> IsDatabankEligibleForBonus(string userId, string databankName);

    Task MarkDatabankAsCompleted(string userId, string databankName);
}
