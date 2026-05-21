using System;
using System.Collections.Generic;
using QuestionSystem;

public interface IQuestionLocalRepository
{
    void SaveQuestions(List<Question> questions);
    List<Question> GetQuestionsByDatabankName(string databankName);
    List<Question> GetAllQuestions();
    bool HasAnyQuestions();
    DateTime GetLatestCacheTimestamp();
    void ClearAll();

    /// <summary>
    /// Retorna a versão do banco de questões que foi gravada no último sync bem-sucedido.
    /// Retorna -1 se nenhuma versão foi gravada ainda (primeiro uso, cache limpo, etc.).
    /// </summary>
    long GetCachedVersion();

    /// <summary>
    /// Persiste a versão do banco de questões após um sync bem-sucedido,
    /// para ser comparada com a versão remota nas próximas inicializações.
    /// </summary>
    void SaveCachedVersion(long version);
}