using System.Collections.Generic;
using QuestionSystem;

public class MembranesQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
    //     new Question
    //     {
    //         questionDatabankName = "MembranesQuestionDatabase",
    //         questionText = "Qual o principal componente de uma membrana biológica?",
    //         answers = new string[] {
    //             "Carboidratos", 
    //             "Lipídeos", 
    //             "Proteínas", 
    //             "Ácidos Nucleicos"},
    //         correctIndex = 1,
    //         questionNumber = 1,
    //         answerType = AnswerType.Text,
    //         questionType = QuestionType.Text,
    //         questionImagePath = "",
    //         questionLevel = 1,
    //         questionInDevelopment = false,
    //         globalId = "membranes_001",
    //         topic = "membranes",
    //         subtopic = null,
    //         displayName = "Membranas Biológicas",
    //         bloomLevel = BloomLevel.Unclassified,
    //         conceptTags = null,
    //         prerequisites = null,
    //         questionHint = null
    //    },

    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.membranes;
    public string GetDatabankName()  => "MembranesQuestionDatabase";
    public string GetDisplayName()   => "Membranas Biológicas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}