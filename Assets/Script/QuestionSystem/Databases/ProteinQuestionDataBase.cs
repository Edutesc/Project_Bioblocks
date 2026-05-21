using System.Collections.Generic;
using QuestionSystem;

public class ProteinQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        //  new Question
        // {
        //     questionDatabankName = "ProteinQuestionDatabase",
        //     questionText = "",
        //     answers = new string[] {
        //         "A = Ponte de Hidrogênio, B = Interação Eletrostática, C = Interação Hidrofóbica, D = Ponte Dissulfeto",
        //         "A = Ponte de Dissulfeto, B = Interação Eletrostática, C = Interação Hidrofóbica, D = Ponte de Hidrogênio",
        //         "A = Interação Hidrofóbica, B = Ponte de Hidrogênio, C = Ponte Dissulfeto, D = Interação Eletrostática",
        //         "A = Interação Eletrostática, B = Ponte de Hidrogênio, C = Interação Hidrofóbica, D = Ponte Dissulfeto",
        //     },
        //     correctIndex = 2,
        //     questionNumber = 1,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Image,
        //     questionImagePath = "AnswerImages/ProteinDB/proteinQuestion_1",
        //     questionLevel = 3,
        //     questionInDevelopment = false,
        //     globalId = "proteins_001",
        //     topic = "proteins",
        //     subtopic = null,
        //     displayName = "Proteínas",
        //     bloomLevel = BloomLevel.Unclassified,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = null
        // },
        // 
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.proteins;
    public string GetDatabankName()  => "ProteinQuestionDatabase";
    public string GetDisplayName()   => "Proteínas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
