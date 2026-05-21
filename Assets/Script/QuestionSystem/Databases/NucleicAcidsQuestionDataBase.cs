using System.Collections.Generic;
using QuestionSystem;

public class NucleicAcidsQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
    //    // Question Example
    //     new Question {
    //         questionDatabankName = "NucleicAcidsQuestionDatabase",
    //         questionText = "Quem descreveu o modelo da dupla hélice do DNA em 1953?",
    //         answers = new string[] {
    //             "Darwin e Mendel",
    //             "Watson e Crick",
    //             "Franklin e Chargaff",
    //             "Pauling e Wöhler"
    //         },
    //         correctIndex = 1,
    //         questionNumber = 63,
    //         isImageAnswer = false,
    //         isImageQuestion = false,
    //         questionImagePath = "",
    //         questionLevel = 1,
    //         questionInDevelopment = false,
    //         globalId = "nucleicAcids_063",
    //         topic = "nucleicAcids",
    //         subtopic = null,
    //         displayName = "Ácidos Nucleicos",
    //         bloomLevel = "unclassified",
    //         conceptTags = null,
    //         prerequisites = null,
    //         questionHint = null
    //     },
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.nucleicAcids;
    public string GetDatabankName()  => "NucleicAcidsQuestionDatabase";
    public string GetDisplayName()   => "Ácidos Nucleicos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}