using Unity.Burst.CompilerServices;
using System.Collections.Generic;
using QuestionSystem;

namespace QuestionSystem
{
    [System.Serializable]
    public class Question
    {
        public string questionDatabankName;
        public string questionText;
        public string[] answers;
        public int correctIndex;
        public int questionNumber;
        public bool isImageAnswer;
        public bool isImageQuestion;
        public string questionImagePath;
        public int questionLevel;
        public bool questionInDevelopment;
        public List<Hint> hint;
    }
}

