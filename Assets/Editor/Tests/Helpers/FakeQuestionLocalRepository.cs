using System;
using System.Collections.Generic;
using System.Linq;
using QuestionSystem;

/// <summary>
/// Fake do IQuestionLocalRepository para testes unitários.
/// Opera inteiramente em memória — sem LiteDB.
///
/// Cenários suportados:
///   - Cache vazio (estado inicial padrão)
///   - Cache populado (via SetQuestions ou ReplaceAllQuestions)
///   - Cache com timestamp configurável (para testar lógica de staleness)
///   - Lançamento de exceção em SaveQuestions ou ReplaceAllQuestions
///
/// Observação:
///   - SaveQuestions() simula upsert incremental.
///   - ReplaceAllQuestions() simula substituição atômica do cache inteiro.
/// </summary>
public class FakeQuestionLocalRepository : IQuestionLocalRepository
{
    // ── Storage em memória ─────────────────────────────────────────────────────
    // Chave: questionDatabankName   Valor: lista de questões daquele banco
    private readonly Dictionary<string, List<Question>> _storage =
        new Dictionary<string, List<Question>>();

    private DateTime _latestCacheTimestamp = DateTime.MinValue;

    // ── Comportamento de erro configurável ────────────────────────────────────

    /// <summary>Se true, SaveQuestions() lança ExceptionToThrow.</summary>
    public bool ShouldThrowOnSave { get; set; } = false;

    /// <summary>Se true, ReplaceAllQuestions() lança ExceptionToThrow.</summary>
    public bool ShouldThrowOnReplace { get; set; } = false;

    public Exception ExceptionToThrow { get; set; } =
        new Exception("Simulated local storage error");

    // ── Versão do cache em memória ─────────────────────────────────────────────
    private long _cachedVersion = -1L;

    // ── Rastreamento de chamadas ───────────────────────────────────────────────
    public int SaveQuestionsCallCount       { get; private set; }
    public int ReplaceAllQuestionsCallCount { get; private set; }
    public int ClearAllCallCount            { get; private set; }
    public int SaveCachedVersionCallCount   { get; private set; }
    public int GetCachedVersionCallCount    { get; private set; }

    /// <summary>Número total de questões passadas para SaveQuestions na última chamada.</summary>
    public int LastSaveCount { get; private set; }

    /// <summary>Número total de questões passadas para ReplaceAllQuestions na última chamada.</summary>
    public int LastReplaceCount { get; private set; }

    /// <summary>Última versão passada para SaveCachedVersion, para asserções em testes.</summary>
    public long LastSavedVersion { get; private set; } = -1L;

    // ── Configuração de cenários ───────────────────────────────────────────────

    /// <summary>
    /// Popula o cache com as questões fornecidas, como se tivessem sido salvas
    /// há <paramref name="savedDaysAgo"/> dias.
    ///
    /// Este método configura estado inicial de teste e não incrementa contadores
    /// de SaveQuestions/ReplaceAllQuestions.
    /// </summary>
    public void SetQuestions(List<Question> questions, double savedDaysAgo = 0)
    {
        _storage.Clear();

        if (questions == null || questions.Count == 0)
        {
            _latestCacheTimestamp = DateTime.MinValue;
            return;
        }

        AddOrUpdateQuestions(questions);

        _latestCacheTimestamp = DateTime.UtcNow.AddDays(-savedDaysAgo);
    }

    /// <summary>
    /// Define diretamente o timestamp retornado por GetLatestCacheTimestamp().
    /// Útil para testar a fronteira exata de expiração sem depender do relógio.
    /// </summary>
    public void SetCacheTimestamp(DateTime timestamp)
    {
        _latestCacheTimestamp = timestamp;
    }

    /// <summary>
    /// Define a versão do cache diretamente, sem incrementar SaveCachedVersionCallCount.
    /// Útil para configurar o estado inicial de um teste.
    /// </summary>
    public void SetCachedVersion(long version)
    {
        _cachedVersion = version;
    }

    // ── Escrita ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Simula upsert incremental de questões no cache.
    /// Não remove questões antigas que não estejam na lista recebida.
    /// </summary>
    public void SaveQuestions(List<Question> questions)
    {
        SaveQuestionsCallCount++;
        LastSaveCount = questions?.Count ?? 0;

        if (ShouldThrowOnSave)
            throw ExceptionToThrow;

        if (questions == null || questions.Count == 0)
            return;

        AddOrUpdateQuestions(questions);

        _latestCacheTimestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Simula substituição atômica do cache inteiro.
    /// Diferente de ClearAll() + SaveQuestions(), este método possui contador próprio
    /// e não incrementa ClearAllCallCount nem SaveQuestionsCallCount.
    /// </summary>
    public void ReplaceAllQuestions(List<Question> questions)
    {
        ReplaceAllQuestionsCallCount++;
        LastReplaceCount = questions?.Count ?? 0;

        if (ShouldThrowOnReplace || ShouldThrowOnSave)
            throw ExceptionToThrow;

        _storage.Clear();

        if (questions == null || questions.Count == 0)
        {
            _latestCacheTimestamp = DateTime.MinValue;
            return;
        }

        AddOrUpdateQuestions(questions);

        _latestCacheTimestamp = DateTime.UtcNow;
    }

    // ── Leitura ────────────────────────────────────────────────────────────────

    public List<Question> GetQuestionsByDatabankName(string databankName)
    {
        if (string.IsNullOrEmpty(databankName))
            return new List<Question>();

        if (_storage.TryGetValue(databankName, out var questions))
            return new List<Question>(questions);

        return new List<Question>();
    }

    public List<Question> GetAllQuestions()
    {
        return _storage.Values
                       .SelectMany(list => list)
                       .ToList();
    }

    // ── Metadados de cache ─────────────────────────────────────────────────────

    public bool HasAnyQuestions()
    {
        return _storage.Values.Any(list => list.Count > 0);
    }

    public DateTime GetLatestCacheTimestamp()
    {
        return _latestCacheTimestamp;
    }

    // ── Limpeza ────────────────────────────────────────────────────────────────

    public void ClearAll()
    {
        ClearAllCallCount++;
        _storage.Clear();
        _latestCacheTimestamp = DateTime.MinValue;
    }

    // ── Versão do cache ────────────────────────────────────────────────────────

    public long GetCachedVersion()
    {
        GetCachedVersionCallCount++;
        return _cachedVersion;
    }

    public void SaveCachedVersion(long version)
    {
        SaveCachedVersionCallCount++;
        LastSavedVersion = version;
        _cachedVersion   = version;
    }

    // ── Utilitários de teste ───────────────────────────────────────────────────

    /// <summary>Zera tudo para reutilização entre testes.</summary>
    public void Reset()
    {
        _storage.Clear();

        _latestCacheTimestamp = DateTime.MinValue;
        _cachedVersion        = -1L;

        ShouldThrowOnSave    = false;
        ShouldThrowOnReplace = false;

        SaveQuestionsCallCount       = 0;
        ReplaceAllQuestionsCallCount = 0;
        ClearAllCallCount            = 0;
        SaveCachedVersionCallCount   = 0;
        GetCachedVersionCallCount    = 0;

        LastSaveCount    = 0;
        LastReplaceCount = 0;
        LastSavedVersion = -1L;
    }

    private void AddOrUpdateQuestions(List<Question> questions)
    {
        foreach (var q in questions)
        {
            if (q == null)
                continue;

            string databankName = q.questionDatabankName ?? string.Empty;

            if (!_storage.ContainsKey(databankName))
                _storage[databankName] = new List<Question>();

            var existing = _storage[databankName]
                .FindIndex(x =>
                    (!string.IsNullOrEmpty(q.globalId) && !string.IsNullOrEmpty(x.globalId))
                        ? string.Equals(x.globalId, q.globalId, StringComparison.OrdinalIgnoreCase)
                        : (q.questionNumber != 0 && x.questionNumber == q.questionNumber));

            if (existing >= 0)
                _storage[databankName][existing] = q;
            else
                _storage[databankName].Add(q);
        }
    }
}
