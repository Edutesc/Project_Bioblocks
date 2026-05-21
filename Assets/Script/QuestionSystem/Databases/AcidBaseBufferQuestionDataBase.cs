using System.Collections.Generic;
using QuestionSystem;

public class AcidBaseBufferQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
        // new Question
        // {
        //     questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //     questionText = "Segundo Arrhenius, o que caracteriza um ácido?",
        //     answers = new string[] {
        //         "Libera íons H+ em solução aquosa.",
        //         "Recebe prótons (H+) em solução aquosa.",
        //         "Libera íons OH- em solução aquosa.",
        //         "Recebe íons OH- em solução aquosa."
        //     },
        //     correctIndex = 0,
        //     questionNumber = 1,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "acidsBase_001",
        //     topic = "acidsBase",
        //     subtopic = null,
        //     displayName = "Ácidos, Bases e Tampões",
        //     bloomLevel = BloomLevel.Unclassified,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = null
        // },
        
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.acidsBase;
    public string GetDatabankName()  => "AcidBaseBufferQuestionDatabase";
    public string GetDisplayName()   => "Ácidos, Bases e Tampões";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}