/// <summary>
/// Resultado da avaliação de uma resposta dissertativa por LLM.
/// </summary>
public class OpenAnswerResult
{
    /// <summary>
    /// Número de prerequisites identificados na resposta do aluno.
    /// </summary>
    public int FoundCount { get; set; }

    /// <summary>
    /// Total de prerequisites exigidos pela questão.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Nota normalizada entre 0 e 1 (FoundCount / TotalCount).
    /// </summary>
    public float Score => TotalCount > 0 ? (float)FoundCount / TotalCount : 0f;

    /// <summary>
    /// Indica se a avaliação foi concluída com sucesso (sem erros de rede/API).
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Mensagem de erro, preenchida quando IsSuccess = false.
    /// </summary>
    public string ErrorMessage { get; set; }

    // ── Factory helpers ──────────────────────────────────────────────────────

    public static OpenAnswerResult Success(int foundCount, int totalCount) => new OpenAnswerResult
    {
        FoundCount = foundCount,
        TotalCount = totalCount,
        IsSuccess  = true
    };

    public static OpenAnswerResult Failure(string errorMessage) => new OpenAnswerResult
    {
        IsSuccess    = false,
        ErrorMessage = errorMessage
    };
}
