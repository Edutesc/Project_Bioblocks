using System;
using System.Collections.Generic;
using Firebase.Firestore;
using QuestionSystem;

namespace Edutesc.BioBlocks.Core.Models
{
    public class AssessmentSession
    {
        private static AssessmentSession _current;
        public static AssessmentSession Current => _current;

        public AssessmentData AssessmentConfig { get; private set; }
        public string UserId { get; private set; }
        public List<Question> Questions { get; private set; }
        public int CurrentQuestionIndex { get; private set; }
        public DateTime StartedAtUtc { get; private set; }
        public AssessmentAttempt CurrentAttempt { get; private set; }

        public string StudentName { get; private set; }
        public string RA { get; private set; }
        public AssessmentResult CurrentResult { get; private set; }

        public static void StartNew(AssessmentData assessment, string userId, List<Question> questions)
        {
            var nowUtc = DateTime.UtcNow;
            var startTimestamp = Timestamp.FromDateTime(nowUtc);

            _current = new AssessmentSession
            {
                AssessmentConfig = assessment,
                UserId = userId,
                Questions = questions ?? new List<Question>(),
                CurrentQuestionIndex = 0,
                StartedAtUtc = nowUtc,
                CurrentAttempt = new AssessmentAttempt
                {
                    AssessmentId = assessment?.AssessmentId ?? "unknown-assessment",
                    UserId = userId,
                    Status = "in_progress",
                    StartedAt = startTimestamp,
                    QuestionIds = new List<string>(),
                    CorrectQuestionIds = new List<string>(),
                    WrongQuestionIds = new List<string>(),
                    Answers = new List<AssessmentAnswerItem>(),
                    Score = new AssessmentScoreSummary { Correct = 0, Wrong = 0, Total = 0 }
                },
                CurrentResult = new AssessmentResult
                {
                    UserId = userId,
                    Score = new AssessmentScore(),
                    QuestionsAnswered = new List<AssessmentAnswerDetail>()
                }
            };
        }

        public static void StartNew(string name, string ra, List<Question> questions)
        {
            var nowUtc = DateTime.UtcNow;
            _current = new AssessmentSession
            {
                StudentName = name,
                RA = ra,
                Questions = questions ?? new List<Question>(),
                CurrentQuestionIndex = 0,
                StartedAtUtc = nowUtc,
                CurrentAttempt = new AssessmentAttempt
                {
                    AssessmentId = "legacy-assessment",
                    UserId = ra,
                    Status = "in_progress",
                    StartedAt = Timestamp.FromDateTime(nowUtc),
                    QuestionIds = new List<string>(),
                    CorrectQuestionIds = new List<string>(),
                    WrongQuestionIds = new List<string>(),
                    Answers = new List<AssessmentAnswerItem>(),
                    Score = new AssessmentScoreSummary()
                },
                CurrentResult = new AssessmentResult
                {
                    StudentName = name,
                    RA = ra,
                    Score = new AssessmentScore(),
                    QuestionsAnswered = new List<AssessmentAnswerDetail>()
                }
            };
        }

        public static void Clear()
        {
            _current = null;
        }

        public Question GetCurrentQuestion()
        {
            if (Questions == null || CurrentQuestionIndex >= Questions.Count)
                return null;
            
            return Questions[CurrentQuestionIndex];
        }

        public void MoveToNextQuestion()
        {
            CurrentQuestionIndex++;
        }

        public void RecordAnswer(Question question, int selectedIndex)
        {
            if (question == null) return;

            bool isCorrect = (selectedIndex == question.correctIndex);
            string qId = !string.IsNullOrEmpty(question.globalId) 
                ? question.globalId 
                : $"{question.questionDatabankName}_{question.questionNumber:D3}";

            if (CurrentAttempt != null)
            {
                CurrentAttempt.Answers.Add(new AssessmentAnswerItem
                {
                    QuestionId = qId,
                    QuestionDatabankName = question.questionDatabankName ?? "",
                    QuestionLevel = question.questionLevel,
                    SelectedIndex = selectedIndex,
                    CorrectIndex = question.correctIndex,
                    IsCorrect = isCorrect
                });

                CurrentAttempt.QuestionIds.Add(qId);

                if (isCorrect)
                {
                    CurrentAttempt.CorrectQuestionIds.Add(qId);
                    CurrentAttempt.Score.Correct++;
                }
                else
                {
                    CurrentAttempt.WrongQuestionIds.Add(qId);
                    CurrentAttempt.Score.Wrong++;
                }

                CurrentAttempt.Score.Total++;
            }

            if (CurrentResult != null)
            {
                string userAns = (question.answers != null && question.answers.Length > selectedIndex) ? question.answers[selectedIndex] : selectedIndex.ToString();
                string correctAns = (question.answers != null && question.answers.Length > question.correctIndex) ? question.answers[question.correctIndex] : question.correctIndex.ToString();

                CurrentResult.QuestionsAnswered.Add(new AssessmentAnswerDetail
                {
                    QuestionId = qId,
                    Difficulty = question.questionLevel.ToString(),
                    IsCorrect = isCorrect,
                    UserAnswer = userAns,
                    CorrectAnswer = correctAns
                });

                CurrentResult.Score.Total++;
                if (isCorrect) CurrentResult.Score.Correct++;
            }
        }

        public void RecordAnswer(string questionId, string difficulty, bool isCorrect, string userAnswer, string correctAnswer)
        {
            if (CurrentResult != null)
            {
                CurrentResult.QuestionsAnswered.Add(new AssessmentAnswerDetail
                {
                    QuestionId = questionId,
                    Difficulty = difficulty,
                    IsCorrect = isCorrect,
                    UserAnswer = userAnswer,
                    CorrectAnswer = correctAnswer
                });

                CurrentResult.Score.Total++;
                if (isCorrect) CurrentResult.Score.Correct++;
            }
        }

        public AssessmentAttempt FinalizeAttempt()
        {
            if (CurrentAttempt == null) return null;

            var completedAtUtc = DateTime.UtcNow;
            CurrentAttempt.CompletedAt = Timestamp.FromDateTime(completedAtUtc);
            CurrentAttempt.DurationSeconds = (int)Math.Max(0, (completedAtUtc - StartedAtUtc).TotalSeconds);
            CurrentAttempt.Status = "completed";

            return CurrentAttempt;
        }
    }
}
