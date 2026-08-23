using System.Collections.Generic;
using Firebase.Firestore;

namespace Edutesc.BioBlocks.Core.Models
{
    [FirestoreData]
    public class AssessmentData
    {
        [FirestoreProperty("assessmentId")]
        public string AssessmentId { get; set; }

        [FirestoreProperty("assessmentType")]
        public string AssessmentType { get; set; }

        [FirestoreProperty("academicTerm")]
        public string AcademicTerm { get; set; }

        [FirestoreProperty("courseId")]
        public string CourseId { get; set; }

        [FirestoreProperty("classId")]
        public string ClassId { get; set; }

        [FirestoreProperty("title")]
        public string Title { get; set; }

        [FirestoreProperty("description")]
        public string Description { get; set; }

        [FirestoreProperty("displayTopics")]
        public List<string> DisplayTopics { get; set; } = new List<string>();

        [FirestoreProperty("allowedDatabanks")]
        public List<string> AllowedDatabanks { get; set; } = new List<string>();

        [FirestoreProperty("questionDistribution")]
        public QuestionDistribution QuestionDistribution { get; set; } = new QuestionDistribution();

        [FirestoreProperty("totalQuestions")]
        public int TotalQuestions { get; set; } = 10;

        [FirestoreProperty("durationMinutes")]
        public int DurationMinutes { get; set; } = 15;

        [FirestoreProperty("allowRetakes")]
        public bool AllowRetakes { get; set; } = true;

        [FirestoreProperty("enabled")]
        public bool Enabled { get; set; } = true;

        [FirestoreProperty("opensAt")]
        public Timestamp OpensAt { get; set; }

        [FirestoreProperty("lastStartAt")]
        public Timestamp LastStartAt { get; set; }

        [FirestoreProperty("closesAt")]
        public Timestamp ClosesAt { get; set; }

        [FirestoreProperty("displayTimeZone")]
        public string DisplayTimeZone { get; set; } = "America/Sao_Paulo";

        [FirestoreProperty("archived")]
        public bool Archived { get; set; } = false;

        [FirestoreProperty("createdAt")]
        public Timestamp CreatedAt { get; set; }

        [FirestoreProperty("updatedAt")]
        public Timestamp UpdatedAt { get; set; }
    }

    [FirestoreData]
    public class QuestionDistribution
    {
        [FirestoreProperty("basic")]
        public int Basic { get; set; } = 4;

        [FirestoreProperty("intermediate")]
        public int Intermediate { get; set; } = 3;

        [FirestoreProperty("hard")]
        public int Hard { get; set; } = 3;
    }
}
