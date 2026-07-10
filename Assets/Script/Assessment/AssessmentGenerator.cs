using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Question> GenerateAssessment(int totalQuestions = 10)
        {
            var allQuestions = _repository.GetAllQuestions();
            
            if (allQuestions == null || allQuestions.Count == 0)
            {
                return new List<Question>();
            }

            var basic = allQuestions.Where(q => q.questionLevel <= 1).ToList();
            var intermediate = allQuestions.Where(q => q.questionLevel == 2).ToList();
            var hard = allQuestions.Where(q => q.questionLevel >= 3).ToList();

            Shuffle(basic);
            Shuffle(intermediate);
            Shuffle(hard);

            int targetBasic = 4;
            int targetIntermediate = 3;
            int targetHard = 3;

            var finalQuestions = new List<Question>();

            var selectedBasic = basic.Take(targetBasic).ToList();
            finalQuestions.AddRange(selectedBasic);

            var selectedIntermediate = intermediate.Take(targetIntermediate).ToList();
            finalQuestions.AddRange(selectedIntermediate);
            
            var selectedHard = hard.Take(targetHard).ToList();
            finalQuestions.AddRange(selectedHard);

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
