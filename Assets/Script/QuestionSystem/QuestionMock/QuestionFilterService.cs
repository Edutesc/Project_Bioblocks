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

            List<Question> allQuestions = database.GetQuestions();
            bool isDatabaseInDev = database.IsDatabaseInDevelopment();

            if (isDatabaseInDev)
            {
                var devQuestions = allQuestions
                    .Where(q => q.questionInDevelopment)
                    .ToList();

                Debug.Log($"[QuestionFilterService] Database '{database.GetDatabankName()}' is in DEVELOPMENT mode");
                Debug.Log($"[QuestionFilterService] Showing {devQuestions.Count} questions in development (out of {allQuestions.Count} total)");
                
                return devQuestions;
            }
            else
            {
                var prodQuestions = allQuestions
                    .Where(q => !q.questionInDevelopment)
                    .ToList();

                Debug.Log($"[QuestionFilterService] Database '{database.GetDatabankName()}' is in PRODUCTION mode");
                Debug.Log($"[QuestionFilterService] Showing {prodQuestions.Count} production questions (filtered out {allQuestions.Count - prodQuestions.Count} dev questions)");
                
                return prodQuestions;
            }
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

        public static bool ShouldSaveToFirebase(IQuestionDatabase database)
        {
            bool isDatabaseInDev = database.IsDatabaseInDevelopment();
            
            if (isDatabaseInDev)
            {
                Debug.LogWarning($"[QuestionFilterService] Database '{database.GetDatabankName()}' is in DEVELOPMENT mode - Firebase save BLOCKED");
            }
            
            return !isDatabaseInDev;
        }

        public static List<int> GetAvailableQuestionNumbers(IQuestionDatabase database)
        {
            List<Question> filteredQuestions = FilterQuestions(database);
            return filteredQuestions.Select(q => q.questionNumber).ToList();
        }

        public static void LogDatabaseStatus(IQuestionDatabase database)
        {
            bool isDatabaseInDev = database.IsDatabaseInDevelopment();
            List<Question> allQuestions = database.GetQuestions();
            int devQuestionsCount = allQuestions.Count(q => q.questionInDevelopment);
            int prodQuestionsCount = allQuestions.Count - devQuestionsCount;

            Debug.Log("=================================================");
            Debug.Log($"[DATABASE STATUS] {database.GetDatabankName()}");
            Debug.Log($"Mode: {(isDatabaseInDev ? "DEVELOPMENT" : "PRODUCTION")}");
            Debug.Log($"Total Questions: {allQuestions.Count}");
            Debug.Log($"├─ Production Questions: {prodQuestionsCount}");
            Debug.Log($"└─ Development Questions: {devQuestionsCount}");
            
            if (isDatabaseInDev)
            {
                Debug.Log($"Visible Questions: {devQuestionsCount} (dev only)");
                Debug.Log("Firebase Save: DISABLED");
            }
            else
            {
                Debug.Log($"Visible Questions: {allQuestions.Count} (all)");
                Debug.Log("Firebase Save: ENABLED");
            }
            Debug.Log("=================================================");
        }
    }
}
