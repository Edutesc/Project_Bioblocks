using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;

public interface IFirestoreQuestionRepository
{
    Task<List<Question>> GetAllQuestions();
    Task<List<Question>> GetQuestionsByDatabankName(string databankName);

    /// <summary>
    /// Retorna a versão atual do banco de questões armazenada em Config/QuestionStats.
    /// Retorna -1 em caso de erro (sem conexão, sem permissão, etc.) para que o
    /// chamador possa tratar como "não foi possível verificar" e não invalidar o cache.
    /// </summary>
    Task<long> GetRemoteVersion();
}