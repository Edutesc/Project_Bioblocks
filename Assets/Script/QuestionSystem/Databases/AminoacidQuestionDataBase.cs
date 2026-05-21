using System.Collections.Generic;
using QuestionSystem;

public class AminoacidQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
           // // Question Example
        // new Question
        // {
        //     questionDatabankName = "AminoacidQuestionDatabase",
        //     questionText = "O que define um aminoácido?",
        //     answers = new string[] {
        //         "Uma molécula orgânica com um grupo amino e um grupo carboxila.",
        //         "Uma molécula inorgânica com um grupo amino e um grupo carboxila.",
        //         "Uma molécula orgânica com apenas um grupo amino.",
        //         "Uma molécula inorgânica com apenas um grupo carboxila."
        //     },
        //     correctIndex = 0,
        //     questionNumber = 1,
        //     isImageAnswer = false,
        //     isImageQuestion = false,
        //     questionImagePath = "",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "aminoacids_001",
        //     topic = "aminoacids",
        //     subtopic = null,
        //     displayName = "Aminoácidos e peptídeos",
        //     bloomLevel = "unclassified",
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = null
        // },

    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.aminoacids;
    public string GetDatabankName()  => "AminoacidQuestionDatabase";
    public string GetDisplayName()   => "Aminoácidos e peptídeos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}