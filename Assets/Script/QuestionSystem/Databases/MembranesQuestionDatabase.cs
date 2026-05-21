using System.Collections.Generic;
using QuestionSystem;

public class MembranesQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        
        // new Question
        // {
        //     questionDatabankName = "MembranesQuestionDatabase",
        //     questionText = "Quando a célula engloba partículas grandes por meio da membrana, esse processo é chamado de:",
        //     answers = new string[] { 
        //         "Exocitose", 
        //         "Pinocitose", 
        //         "Fagocitose", 
        //         "Difusão" },
        //     correctIndex = 2,
        //     questionNumber = 70,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 3,
        //     questionInDevelopment = false,
        //     globalId = "membranes_070",
        //     topic = "membranes",
        //     subtopic = null,
        //     displayName = "Membranas Biológicas",
        //     bloomLevel = BloomLevel.Unclassified,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = null
        // }
    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.membranes;
    public string GetDatabankName()  => "MembranesQuestionDatabase";
    public string GetDisplayName()   => "Membranas Biológicas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}