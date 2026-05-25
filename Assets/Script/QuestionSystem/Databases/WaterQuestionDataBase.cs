using System.Collections.Generic;
using QuestionSystem;

public class WaterQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        // new Question
        // {
        //     questionDatabankName = "WaterQuestionDatabase",
        //     questionText = "Qual a principal razão para a alta capacidade calorífica da água?",
        //     answers = new string[] {
        //         "Fortes ligações covalentes entre átomos de hidrogênio e oxigênio.",
        //         "Intensas ligações de hidrogênio entre moléculas de água.",
        //         "Alto peso molecular das moléculas de água.",
        //         "Seu estado líquido em temperatura ambiente."
        //     },
        //     correctIndex = 1,
        //     questionNumber = 1,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "water_001",
        //     topic = "water",
        //     subtopic = null,
        //     displayName = "Água",
        //     bloomLevel = BloomLevel.Unclassified,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = null
        // },

    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.water;
    public string GetDatabankName()  => "WaterQuestionDatabase";
    public string GetDisplayName()   => "Água";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}