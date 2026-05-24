using System.Collections.Generic;
using QuestionSystem;

public class BiochemistryIntroductionQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    private List<Question> questions = new List<Question>
    {
        // Questão 08
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Identifique a estrutura que representa um hidrocarboneto ramificado",
            answers = new string[] {
                "AnswerImages/IntroductionDB/benzeno",
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/2-3-dimetil-pentano",
                "AnswerImages/IntroductionDB/propanamina"
            },
            correctIndex = 2,
            questionNumber = 8,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_008",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Hidrocarboneto: molécula formada apenas por carbono e hidrogênio (sem outros elementos como O, N ou S). Ramificado: a cadeia carbônica principal possui ramificações (carbonos que saem da cadeia principal). Elimine: benzeno (aromático, não ramificado), 2-butanol (tem -OH, não é hidrocarboneto), propanamina (tem N, não é hidrocarboneto). 2,3-dimetilpentano: é apenas C e H, com grupos metil saindo da cadeia principal — hidrocarboneto ramificado.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 60
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Os aminoácidos são os monômeros que formam todas as proteínas que conhecemos na natureza. Assinale a opção que apresenta um aminoácido que nao tem carbono quiral",
            answers = new string[] {
                "AnswerImages/IntroductionDB/histidina",
                "AnswerImages/IntroductionDB/cisteina",
                "AnswerImages/IntroductionDB/glicina",
                "AnswerImages/IntroductionDB/metionina"
            },
            correctIndex = 2,
            questionNumber = 60,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_060",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Carbono quiral: ligado a 4 grupos DIFERENTES. Na maioria dos aminoácidos, o carbono α (central) é quiral. Histidina, cisteína, metionina: carbono α ligado a -NH₂, -COOH, -H e cadeias laterais diferentes → quiral (L-aminoácido). Glicina: o único aminoácido sem carbono quiral. Seu carbono α está ligado a -NH₂, -COOH e DOIS -H. Com dois H iguais no mesmo carbono, ele tem dois substituintes idênticos → não é quiral → glicina não tem isômero L ou D.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 76
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Observe as quatro estruturas moleculares. Identifique qual delas possui simultaneamente um grupo carbonila e uma hidroxila ligados ao mesmo carbono",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-butanona",
                "AnswerImages/IntroductionDB/propanal",
                "AnswerImages/IntroductionDB/propanol",
                "AnswerImages/IntroductionDB/acido-propanoico"
            },
            correctIndex = 3,
            questionNumber = 76,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_076",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = BloomLevel.Apply,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão pede: C=O e -OH no MESMO carbono. 2-butanona: apenas C=O (cetona), sem -OH. Propanol: apenas -OH (álcool), sem C=O. Propanal: C=O com H na extremidade (aldeído) — o carbono tem C=O e H, não -OH. Ácido propanoico (-COOH): o carbono carboxílico tem C=O E -OH no mesmo carbono = grupo carboxila. Apenas nele as duas funções coexistem no mesmo carbono.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.biochem;
    public string GetDatabankName() => "BiochemistryIntroductionQuestionDatabase";
    public string GetDisplayName() => "Introdução à Bioquímica";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}