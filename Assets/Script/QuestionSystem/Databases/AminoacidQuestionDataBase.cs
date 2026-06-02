using System.Collections.Generic;
using QuestionSystem;

public class AminoacidQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que é um aminoácido?",
            answers = new string[0],
            correctIndex = 0,
            questionNumber = 1,
            questionType = QuestionType.Text,
            answerType = AnswerType.Open,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_001",
            topic = "aminoacids",
            subtopic = "amino acid structure",
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string>
            {
                "amino acids",
                "protein building blocks",
                "functional groups"
            },
            prerequisites = new List<string>
            {
                "Identificar aminoácidos como moléculas orgânicas.",
                "Citar a presença de um grupo amino.",
                "Citar a presença de um grupo carboxila.",
                "Reconhecer que aminoácidos são unidades formadoras de proteínas ou peptídeos.",
                "Mencionar que a cadeia lateral ou grupo R diferencia os aminoácidos."
            },
            questionHint = new QuestionHint
            {
                text = "Pense nos grupos funcionais comuns e no papel dos aminoácidos nas proteínas."
            }
        },
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.aminoacids;
    public string GetDatabankName()  => "AminoacidQuestionDatabase";
    public string GetDisplayName()   => "Aminoácidos e peptídeos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
