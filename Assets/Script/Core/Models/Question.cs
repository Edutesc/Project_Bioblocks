using QuestionSystem;
using System.Collections.Generic;

namespace QuestionSystem
{
    [System.Serializable]
    public class Question
    {
        public string globalId;
        public string questionDatabankName;
        public string topic;
        public string subtopic;
        public string displayName;
        public string questionText;
        public string[] answers;
        public int correctIndex;
        public int questionNumber;

        // ── Tipo de enunciado ────────────────────────────────────────────────
        // QuestionType substitui isImageQuestion.
        // isImageQuestion é mantido para compatibilidade com bases hardcoded e
        // documentos Firestore antigos; FirestoreQuestionRepository e
        // HardcodedQuestionSource garantem que os dois fiquem sincronizados.
        public QuestionType questionType;
        public bool isImageQuestion;

        // ── Tipo de resposta ─────────────────────────────────────────────────
        // AnswerType substitui isImageAnswer.
        // Mesmo critério de compatibilidade acima.
        // AnswerType.Open indica questão dissertativa (avaliação por LLM).
        public AnswerType answerType;
        public bool isImageAnswer;

        public string questionImagePath;
        public int questionLevel;
        public bool questionInDevelopment;
        public BloomLevel bloomLevel;
        public List<string> conceptTags;
        public List<string> prerequisites;
        public QuestionHint questionHint;
    }
}

