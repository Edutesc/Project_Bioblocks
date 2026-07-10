using System.Collections.Generic;
using QuestionSystem;

namespace Edutesc.BioBlocks.Core.Models
{
    public class AssessmentSession
    {
        private static AssessmentSession _current;
        public static AssessmentSession Current => _current;

        public string StudentName { get; private set; }
        public string RA { get; private set; }
        public List<Question> Questions { get; private set; }
        
        public int CurrentQuestionIndex { get; private set; }
        public AssessmentResult CurrentResult { get; private set; }

        public static void StartNew(string name, string ra, List<Question> questions)
        {
            _current = new AssessmentSession
            {
                StudentName = name,
                RA = ra,
                Questions = questions,
                CurrentQuestionIndex = 0,
                CurrentResult = new AssessmentResult
                {
                    StudentName = name,
                    RA = ra,
                    Score = new AssessmentScore(),
                    QuestionsAnswered = new List<AssessmentAnswerDetail>()
                }
            };
        }

        public static void Clear()
        {
            _current = null;
        }

        public Question GetCurrentQuestion()
        {
            if (Questions == null || CurrentQuestionIndex >= Questions.Count)
                return null;
            
            return Questions[CurrentQuestionIndex];
        }

        public void MoveToNextQuestion()
        {
            CurrentQuestionIndex++;
        }

        public void RecordAnswer(string questionId, string difficulty, bool isCorrect, string userAnswer, string correctAnswer)
        {
            if (CurrentResult == null) return;

            CurrentResult.QuestionsAnswered.Add(new AssessmentAnswerDetail
            {
                QuestionId = questionId,
                Difficulty = difficulty,
                IsCorrect = isCorrect,
                UserAnswer = userAnswer,
                CorrectAnswer = correctAnswer
            });

            CurrentResult.Score.Total++;
            if (isCorrect)
            {
                CurrentResult.Score.Correct++;
            }
        }
    }
}
