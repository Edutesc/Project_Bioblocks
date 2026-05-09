namespace QuestionSystem
{
    /// <summary>
    /// Define o tipo de resposta de uma questão.
    /// Substitui o booleano isImageAnswer e abre espaço para novos formatos.
    ///
    /// Compatibilidade com Firestore:
    ///   - Documentos antigos sem o campo "answerType" continuam funcionando:
    ///     FirestoreQuestionRepository deriva o valor de isImageAnswer.
    ///   - Documentos novos (ou após migração) gravam a string "Text", "Image" ou "Open".
    /// </summary>
    public enum AnswerType
    {
        /// <summary>Quatro botões com texto (comportamento padrão atual).</summary>
        Text,

        /// <summary>Quatro botões com imagens carregadas de Resources.</summary>
        Image,

        /// <summary>Campo de texto livre avaliado posteriormente por LLM.</summary>
        Open
    }

    /// <summary>
    /// Define o tipo de enunciado de uma questão.
    /// Substitui o booleano isImageQuestion.
    ///
    /// Compatibilidade com Firestore:
    ///   - Documentos antigos sem o campo "questionType" derivam o valor de isImageQuestion.
    /// </summary>
    public enum QuestionType
    {
        /// <summary>Enunciado exibido como texto (comportamento padrão atual).</summary>
        Text,

        /// <summary>Enunciado exibido como imagem carregada de Resources ou Firebase Storage.</summary>
        Image
    }

    /// <summary>
    /// Taxonomia de Bloom aplicada à questão.
    /// Substitui o campo string "bloomLevel".
    ///
    /// Compatibilidade com Firestore:
    ///   - Documentos antigos com a string "unclassified" / "remember" / etc.
    ///     são convertidos via Enum.TryParse com fallback para Unclassified.
    ///   - O valor numérico reflete a ordem hierárquica da taxonomia.
    /// </summary>
    public enum BloomLevel
    {
        Unclassified = 0,
        Remember     = 1,   // Lembrar
        Understand   = 2,   // Compreender
        Apply        = 3,   // Aplicar
        Analyze      = 4,   // Analisar
        Evaluate     = 5,   // Avaliar
        Create       = 6    // Criar
    }
}
