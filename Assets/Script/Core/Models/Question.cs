using QuestionSystem;
using System.Collections.Generic;

namespace QuestionSystem
{
    public enum BloomLevel
    {
        Unclassified,
        Remember,
        Understand,
        Apply,
        Analyze,
        Evaluate,
        Create
    }

    public enum QuestionType 
    { 
        Text = 0, 
        Image = 1 
    }

    public enum AnswerType 
    { 
        Text = 0, 
        Image = 1, 
        TextField = 2 
    }

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
        public AnswerType   answerType;
        public QuestionType questionType;
        public string questionImagePath;
        public int questionLevel;
        public bool questionInDevelopment;
        public BloomLevel bloomLevel;
        public List<string> conceptTags;
        public List<string> prerequisites;
        public QuestionHint questionHint;
    }
}

