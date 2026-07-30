using System.Collections.Generic;
using QuestionSystem;

public class AminoacidQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
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
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "aminoacids_001",
        //     topic = "aminoacids",
        //     subtopic = null,
        //     displayName = "Aminoácidos e peptídeos",
        //     bloomLevel = BloomLevel.Remember,
        //     conceptTags = null,
        //     prerequisites = null,
        //     questionHint = new QuestionHint
        //     {
        //         text = "Um aminoácido pode ser definido como uma molécula orgânica que possui simultaneamente um grupo amino (-NH₂) e um grupo carboxila (-COOH) ligados ao mesmo carbono central (carbono alfa), além de um átomo de hidrogênio e uma cadeia lateral variável (radical R). Essas moléculas são consideradas orgânicas porque são formadas por átomos de carbono ligados covalentemente entre si ou a outros elementos como hidrogênio, oxigênio, nitrogênio, etc. A presença conjunta do grupo amino (de caráter básico) e do grupo carboxila (de caráter ácido) é a característica fundamental que define um aminoácido. Retirado de “Princípios de Bioquímica de Lehninger”, ed. 6, pg. 76",
        //         imagePath = null,
        //         videoUrl = null,
        //         link = null
        //     }
        // },

    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.aminoacids;
    public string GetDatabankName()  => "AminoacidQuestionDatabase";
    public string GetDisplayName()   => "Aminoácidos e peptídeos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}