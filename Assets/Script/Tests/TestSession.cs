using System.Collections.Generic;

namespace BioBlocks.Tests
{
    public class TestSession
    {
        private readonly TestConfig _currentTest;
        private readonly List<TestQuestion> _questions;
        private Dictionary<string, int> _userAnswers;

        public TestSession(TestConfig test, List<TestQuestion> questions)
        {
            _currentTest = test;
            _questions = questions;
            _userAnswers = new Dictionary<string, int>();
        }

        public void AnswerQuestion(string questionId, int selectedOptionIndex)
        {
            _userAnswers[questionId] = selectedOptionIndex;
        }

        public Dictionary<string, int> GetAnswers()
        {
            return _userAnswers;
        }
    }
}
