using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Contrato para avaliação de respostas dissertativas via LLM.
/// A implementação padrão é <see cref="AnthropicEvaluator"/>.
/// </summary>
public interface IOpenAnswerEvaluator
{
    /// <summary>
    /// Avalia a resposta do aluno contra a lista de prerequisites da questão.
    /// </summary>
    /// <param name="answerText">Texto livre digitado pelo aluno.</param>
    /// <param name="prerequisites">Conceitos que devem estar presentes na resposta.</param>
    /// <returns>Resultado com score e metadados da avaliação.</returns>
    Task<OpenAnswerResult> EvaluateAsync(string answerText, List<string> prerequisites);
}
