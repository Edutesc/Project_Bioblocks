using System.Collections.Generic;
using QuestionSystem;

public class NucleicAcidsQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        // new Question {
        //     questionDatabankName = "NucleicAcidsQuestionDatabase",
        //     questionText = "Quem primeiro isolou o ácido nucléico?",
        //     answers = new string[] { "Watson", "Crick", "Friedrich Miescher", "Chargaff" },
        //     correctIndex = 2,
        //     questionNumber = 1,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "nucleicAcids_001",
        //     topic = "nucleicAcids",
        //     subtopic = null,
        //     displayName = "Ácidos Nucleicos",
        //     bloomLevel = BloomLevel.Remember,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = new QuestionHint
        //     {
        //         text = "O cientista suíço Friedrich Miescher foi o primeiro pesquisador a isolar o ácido nucléico, em 1868. Durante seus estudos com células presentes em pus coletado de bandagens cirúrgicas, ele identificou uma substância rica em fósforo localizada no núcleo celular, que chamou de “nucleína”. Posteriormente, essa substância passou a ser conhecida como ácido nucleico, componente fundamental do DNA e do RNA.",
        //         imagePath = null,
        //         videoUrl = null,
        //         link = null
        //     }
        // },

    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.nucleicAcids;
    public string GetDatabankName()  => "NucleicAcidsQuestionDatabase";
    public string GetDisplayName()   => "Ácidos Nucleicos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}