using System.Collections.Generic;
using QuestionSystem;

public class CarbohydratesQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        // new Question {
        //     questionDatabankName = "CarbohydratesQuestionDatabase",
        //     questionText = "Qual a fórmula geral dos monossacarídeos?",
        //     answers = new string[] {
        //         "(CH<sub>2</sub> O)<sub>n</sub>",
        //         "C<sub>n</sub> H<sub>2n</sub> O<sub>n</sub>",
        //         "C<sub>n</sub> H<sub>2n-2</sub> O<sub>n</sub>",
        //         "C<sub>n</sub> H<sub>2n+2</sub> O<sub>n</sub>"
        //     },
        //     correctIndex = 0,
        //     questionNumber = 1,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "carbohydrates_001",
        //     topic = "carbohydrates",
        //     subtopic = null,
        //     displayName = "Carboidratos",
        //     bloomLevel = BloomLevel.Unclassified,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = null
        // },
        // 
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.carbohydrates;
    public string GetDatabankName()  => "CarbohydratesQuestionDatabase";
    public string GetDisplayName()   => "Carboidratos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}