using System.Collections.Generic;
using QuestionSystem;
using UnityEngine;

/// <summary>
/// IQuestionSource que carrega questões diretamente das classes C# hardcoded
/// (os arquivos em Script/QuestionSystem/Databases/).
///
/// Usado exclusivamente no questionPreviewMode para que criadores de conteúdo
/// possam visualizar e editar questões sem nenhuma dependência de Firebase,
/// internet ou autenticação.
///
/// Nenhum dado é salvo em Firestore ou LiteDB — totalmente volátil.
/// </summary>
public class HardcodedQuestionSource : IQuestionSource
{
    private readonly Dictionary<string, List<Question>> _cache
        = new Dictionary<string, List<Question>>();

    public HardcodedQuestionSource()
    {
        // Instancia todos os bancos hardcoded e mapeia pelo databankName.
        var databases = new List<IQuestionDatabase>
        {
            new AcidBaseBufferQuestionDatabase(),
            new AminoacidQuestionDatabase(),
            new BiochemistryIntroductionQuestionDatabase(),
            new CarbohydratesQuestionDatabase(),
            new EnzymeQuestionDatabase(),
            new LipidsQuestionDatabase(),
            new MembranesQuestionDatabase(),
            new NucleicAcidsQuestionDatabase(),
            new ProteinQuestionDatabase(),
            new WaterQuestionDatabase(),
        };

        foreach (var db in databases)
        {
            string name = db.GetDatabankName();
            var questions = db.GetQuestions() ?? new List<Question>();

            // Os arquivos hardcoded definem isImageAnswer/isImageQuestion (booleans legados).
            // Derivamos answerType e questionType para que todo código consumidor
            // possa usar os enums sem precisar alterar os 10 arquivos de database.
            foreach (var q in questions)
                NormalizeQuestionTypes(q);

            _cache[name] = questions;
            Debug.Log($"[HardcodedQuestionSource] {name}: {_cache[name].Count} questões carregadas.");
        }
    }

    public List<Question> GetQuestionsForDatabankName(string databankName)
    {
        if (_cache.TryGetValue(databankName, out var questions))
            return questions;

        Debug.LogWarning($"[HardcodedQuestionSource] Banco '{databankName}' não encontrado.");
        return new List<Question>();
    }

    /// <summary>
    /// Deriva answerType e questionType a partir dos booleans legados
    /// isImageAnswer / isImageQuestion, mantendo os dois em sincronia.
    /// Questões que já tiverem answerType != default (Text) são ignoradas,
    /// permitindo que bancos futuros definam o enum diretamente.
    /// </summary>
    private static void NormalizeQuestionTypes(Question q)
    {
        if (q.answerType == AnswerType.Text && q.isImageAnswer)
            q.answerType = AnswerType.Image;

        if (q.questionType == QuestionType.Text && q.isImageQuestion)
            q.questionType = QuestionType.Image;

        // Garante sincronia no sentido inverso: se o banco futuro setar
        // o enum diretamente, os booleans legados acompanham.
        q.isImageAnswer   = (q.answerType   == AnswerType.Image);
        q.isImageQuestion = (q.questionType == QuestionType.Image);
    }
}
