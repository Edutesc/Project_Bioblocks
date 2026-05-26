using System.Collections.Generic;
using QuestionSystem;

public class BiochemistryIntroductionQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    private List<Question> questions = new List<Question>
    {
        // // Questão 08
        // new Question
        // {
        //     questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
        //     questionText = "Identifique a estrutura que representa um hidrocarboneto ramificado",
        //     answers = new string[] {
        //         "AnswerImages/IntroductionDB/benzeno",
        //         "AnswerImages/IntroductionDB/2-butanol",
        //         "AnswerImages/IntroductionDB/2-3-dimetil-pentano",
        //         "AnswerImages/IntroductionDB/propanamina"
        //     },
        //     correctIndex = 2,
        //     questionNumber = 8,
        //     answerType = AnswerType.Image,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "biochem_008",
        //     topic = "biochem",
        //     subtopic = null,
        //     displayName = "Introdução à Bioquímica",
        //     bloomLevel = BloomLevel.Remember,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = new QuestionHint
        //     {
        //         text = "Hidrocarboneto: molécula formada apenas por carbono e hidrogênio (sem outros elementos como O, N ou S). Ramificado: a cadeia carbônica principal possui ramificações (carbonos que saem da cadeia principal). Elimine: benzeno (aromático, não ramificado), 2-butanol (tem -OH, não é hidrocarboneto), propanamina (tem N, não é hidrocarboneto). 2,3-dimetilpentano: é apenas C e H, com grupos metil saindo da cadeia principal — hidrocarboneto ramificado.",
        //         imagePath = null,
        //         videoUrl = null,
        //         link = null
        //     }
        // },

        
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.biochem;
    public string GetDatabankName() => "BiochemistryIntroductionQuestionDatabase";
    public string GetDisplayName() => "Introdução à Bioquímica";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}