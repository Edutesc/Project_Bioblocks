using System;
using System.Collections.Generic;
using QuestionSystem;

public interface IQuestionLocalRepository
{
    // ── Escrita ────────────────────────────────────────────────────────────────
    void SaveQuestions(List<Question> questions);

    /// <summary>
    /// Substitui todo o cache local de questões de forma atômica.
    /// Preferível a chamar ClearAll() + SaveQuestions(), pois evita que alguma
    /// leitura enxergue o cache vazio entre as duas operações.
    /// </summary>
    void ReplaceAllQuestions(List<Question> questions);

    // ── Leitura ────────────────────────────────────────────────────────────────
    List<Question> GetQuestionsByDatabankName(string databankName);
    List<Question> GetAllQuestions();

    // ── Metadados de cache ─────────────────────────────────────────────────────
    bool HasAnyQuestions();
    DateTime GetLatestCacheTimestamp();

    // ── Limpeza ────────────────────────────────────────────────────────────────
    void ClearAll();

    // ── Versão do cache ────────────────────────────────────────────────────────
    /// <summary>
    /// Retorna a versão do banco de questões que foi gravada no último sync bem-sucedido.
    /// Retorna -1 se nenhuma versão foi gravada ainda.
    /// </summary>
    long GetCachedVersion();

    /// <summary>
    /// Persiste a versão do banco de questões após um sync bem-sucedido.
    /// </summary>
    void SaveCachedVersion(long version);
}