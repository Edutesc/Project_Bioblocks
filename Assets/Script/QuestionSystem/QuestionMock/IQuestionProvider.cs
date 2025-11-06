using System.Collections.Generic;
using QuestionSystem;

namespace QuestionSystem
{
    public interface IQuestionProvider
    {
        List<Question> GetQuestions(QuestionSet questionSet);
        
        Question GetQuestionByNumber(QuestionSet questionSet, int questionNumber);
        
        List<Question> GetQuestionsByLevel(QuestionSet questionSet, int level);
        
        IQuestionDatabase GetDatabase(QuestionSet questionSet);
        
        bool IsQuestionAnswered(QuestionSet questionSet, int questionNumber);
        
        void MarkQuestionAsAnswered(QuestionSet questionSet, int questionNumber);
        
        int GetTotalQuestions(QuestionSet questionSet);
        
        int GetAnsweredQuestionsCount(QuestionSet questionSet);
        
        List<int> GetAnsweredQuestionNumbers(QuestionSet questionSet);
        
        void ResetAnsweredQuestions(QuestionSet questionSet);
        
        bool IsUsingMockData();
    }
}
