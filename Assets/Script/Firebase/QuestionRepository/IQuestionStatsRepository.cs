using System.Threading.Tasks;

/// <summary>
/// Acesso remoto a Config/QuestionStats.
/// Cliente apenas lê; quem escreve é a Cloud Function.
/// </summary>
public interface IFirestoreQuestionStatsRepository
{
    Task<QuestionStats> GetQuestionStats();
}
