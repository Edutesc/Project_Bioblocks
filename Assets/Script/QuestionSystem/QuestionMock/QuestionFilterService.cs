using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QuestionSystem;

namespace QuestionSystem
{
    public static class QuestionFilterService
    {
        public static List<Question> FilterQuestions(IQuestionDatabase database)
        {
            if (database == null)
            {
                Debug.LogError("[QuestionFilterService] Database is null");
                return new List<Question>();
            }

            List<Question> allQuestions = database.GetQuestions() ?? new List<Question>();
            var visibleQuestions = allQuestions
                .Where(q => q != null && !q.questionInDevelopment)
                .ToList();

            Debug.Log($"[QuestionFilterService] Database '{database.GetDatabankName()}': {visibleQuestions.Count} visible questions (filtered out {allQuestions.Count - visibleQuestions.Count} in-development questions)");

            return visibleQuestions;
        }

        public static Question GetQuestionByNumber(IQuestionDatabase database, int questionNumber)
        {
            List<Question> filteredQuestions = FilterQuestions(database);
            return filteredQuestions.FirstOrDefault(q => q.questionNumber == questionNumber);
        }

        public static List<Question> GetQuestionsByLevel(IQuestionDatabase database, int level)
        {
            List<Question> filteredQuestions = FilterQuestions(database);
            return filteredQuestions.Where(q => q.questionLevel == level).ToList();
        }

        public static int GetTotalQuestionsCount(IQuestionDatabase database)
        {
            return FilterQuestions(database).Count;
        }

        public static List<int> GetAvailableQuestionNumbers(IQuestionDatabase database)
        {
            List<Question> filteredQuestions = FilterQuestions(database);
            return filteredQuestions.Select(q => q.questionNumber).ToList();
        }
    }
}
