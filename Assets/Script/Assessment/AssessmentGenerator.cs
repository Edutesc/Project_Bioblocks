using System;
using System.Collections.Generic;
using System.Linq;
using Edutesc.BioBlocks.Core.Models;
using QuestionSystem;

namespace Edutesc.BioBlocks.Assessment
{
    public class AssessmentGenerator
    {
        private readonly IQuestionLocalRepository _repository;
        private readonly Random _random;

        public AssessmentGenerator(IQuestionLocalRepository repository)
        {
            _repository = repository;
            _random = new Random();
        }

        public List<Question> GenerateAssessment(AssessmentData assessmentData)
        {
            if (assessmentData == null)
            {
                return GenerateAssessment();
            }

            int total = assessmentData.TotalQuestions > 0 ? assessmentData.TotalQuestions : 10;
            return GenerateAssessment(assessmentData.AllowedDatabanks, assessmentData.QuestionDistribution, total);
        }

        public List<Question> GenerateAssessment(List<string> allowedDatabanks = null, QuestionDistribution distribution = null, int totalQuestions = 10)
        {
            var allQuestions = _repository.GetAllQuestions();
            
            if (allQuestions == null || allQuestions.Count == 0)
            {
                return new List<Question>();
            }

            // Filtro por allowedDatabanks (se fornecido)
            if (allowedDatabanks != null && allowedDatabanks.Count > 0)
            {
                allQuestions = allQuestions
                    .Where(q => !string.IsNullOrEmpty(q.questionDatabankName) && 
                                allowedDatabanks.Any(db => string.Equals(db, q.questionDatabankName, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            if (allQuestions.Count == 0)
            {
                return new List<Question>();
            }

            int targetBasic = distribution != null ? distribution.Basic : 4;
            int targetIntermediate = distribution != null ? distribution.Intermediate : 3;
            int targetHard = distribution != null ? distribution.Hard : 3;

            var basic = allQuestions.Where(q => q.questionLevel <= 1).ToList();
            var intermediate = allQuestions.Where(q => q.questionLevel == 2).ToList();
            var hard = allQuestions.Where(q => q.questionLevel >= 3).ToList();

            Shuffle(basic);
            Shuffle(intermediate);
            Shuffle(hard);

            var finalQuestions = new List<Question>();

            var selectedBasic = basic.Take(targetBasic).ToList();
            finalQuestions.AddRange(selectedBasic);

            var selectedIntermediate = intermediate.Take(targetIntermediate).ToList();
            finalQuestions.AddRange(selectedIntermediate);
            
            var selectedHard = hard.Take(targetHard).ToList();
            finalQuestions.AddRange(selectedHard);

            // Fallback se alguma categoria não tiver questões suficientes
            if (finalQuestions.Count < totalQuestions)
            {
                int missing = totalQuestions - finalQuestions.Count;
                
                var remainingBasic = basic.Skip(selectedBasic.Count);
                var remainingIntermediate = intermediate.Skip(selectedIntermediate.Count);
                var remainingHard = hard.Skip(selectedHard.Count);

                var allRemaining = remainingBasic
                    .Concat(remainingIntermediate)
                    .Concat(remainingHard)
                    .ToList();
                
                Shuffle(allRemaining);
                
                finalQuestions.AddRange(allRemaining.Take(missing));
            }

            Shuffle(finalQuestions);

            return finalQuestions;
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
