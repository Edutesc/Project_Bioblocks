using System.Collections.Generic;
using QuestionSystem;

public class BiochemistryIntroductionQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    private List<Question> questions = new List<Question>
    {
        // // Question Example
        // new Question
        // {
        //     questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
        //     questionText = "Um estudante afirma que todas as opções abaixo descrevem características de seres vivos. Identifique a afirmação INCORRETA",
        //     answers = new string[] {
        //         "Utilizam energia do ambiente para manter suas funções",
        //         "Possuem organização molecular e celular",
        //         "São capazes de se autorreplicar",
        //         "São indiferentes a mudanças no ambiente"
        //     },
        //     correctIndex = 3,
        //     questionNumber = 1,
        //     isImageAnswer = false,
        //     isImageQuestion = false,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "biochem_001",
        //     topic = "biochem",
        //     subtopic = null,
        //     displayName = "Introdução à Bioquímica",
        //     bloomLevel = "lembrar",
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = new QuestionHint
        //     {
        //         text = "Seres vivos possuem propriedades fundamentais que os distinguem da matéria inanimada. Entre essas propriedades estão: organização molecular e celular, metabolismo (uso de energia), reprodução e resposta a estímulos do ambiente. A capacidade de detectar e responder a mudanças no ambiente (irritabilidade) é essencial para a sobrevivência. Um ser vivo que não respondesse a nenhuma mudança ambiental não conseguiria se adaptar nem sobreviver.",
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