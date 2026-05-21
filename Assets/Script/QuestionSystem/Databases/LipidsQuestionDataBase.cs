using System.Collections.Generic;
using QuestionSystem;

public class LipidsQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        // // Question Example
        // new Question
        // {
        //     questionDatabankName = "LipidsQuestionDatabase",
        //     questionText = "O que são lipídios?",
        //     answers = new string[] {
        //         "Moléculas polares, que se associam através de interações eletrostáticas",
        //         "Moléculas apolares, que se associam através de interações hidrofóbicas",
        //         "Moléculas anfipáticas, que se associam através de interações hidrofóbicas",
        //         "Moléculas anfipáticas, que se associam através da pontes de hidrogênio"
        //     },
        //     correctIndex = 2,
        //     questionNumber = 1,
        //     isImageAnswer = false,
        //     isImageQuestion = false,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "lipids_001",
        //     topic = "lipids",
        //     subtopic = null,
        //     displayName = "Lipídeos",
        //     bloomLevel = "unclassified",
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = null
        // },
        
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.lipids;
    public string GetDatabankName()  => "LipidsQuestionDatabase";
    public string GetDisplayName()   => "Lipídeos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}