using System.Collections.Generic;
using QuestionSystem;

public class EnzymeQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
    //    new Question
    //     {
    //         questionDatabankName = "EnzymeQuestionDatabase",
    //         questionText = "O que são enzimas?",
    //         answers = new string[] {
    //             "Catalisadores químicos inorgânicos.",
    //             "Catalisadores biológicos, principalmente proteínas.",
    //             "Substratos que participam de reações químicas.",
    //             "Produtos de reações químicas."
    //         },
    //         correctIndex = 1,
    //         questionNumber = 1,
    //         answerType = AnswerType.Text,
    //         questionType = QuestionType.Text,
    //         questionImagePath = "",
    //         questionLevel = 1,
    //         questionInDevelopment = false,
    //         globalId = "enzymes_001",
    //         topic = "enzymes",
    //         subtopic = null,
    //         displayName = "Enzimas",
    //         bloomLevel = BloomLevel.Unclassified,
    //         conceptTags = null,
    //         prerequisites = null,
    //         questionHint = null
    //     },

    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.enzymes;
    public string GetDatabankName()  => "EnzymeQuestionDatabase";
    public string GetDisplayName()   => "Enzimas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
