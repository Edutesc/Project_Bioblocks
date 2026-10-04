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

        public bool CanMoveToPrevious => CurrentQuestionIndex > 0;
        public bool CanMoveToNext => Questions != null && CurrentQuestionIndex < Questions.Count - 1;
        public bool IsLastQuestion => Questions != null && Questions.Count > 0 && CurrentQuestionIndex == Questions.Count - 1;

        public static void Clear()
        {
            _current = null;
        }

        public Question GetCurrentQuestion()
        {
            if (Questions == null || CurrentQuestionIndex >= Questions.Count || CurrentQuestionIndex < 0)
                return null;
            
            return Questions[CurrentQuestionIndex];
        }

        public void MoveToNextQuestion()
        {
            CurrentQuestionIndex++;
        }

        public void MoveToPreviousQuestion()
        {
            if (CurrentQuestionIndex > 0)
            {
                CurrentQuestionIndex--;
            }
        }

        public void MoveToQuestion(int index)
        {
            if (Questions != null && index >= 0 && index < Questions.Count)
            {
                CurrentQuestionIndex = index;
            }
        }

        public static string GetQuestionId(Question question)
        {
            if (question == null) return string.Empty;
            return !string.IsNullOrEmpty(question.globalId)
                ? question.globalId
                : $"{question.questionDatabankName}_{question.questionNumber:D3}";
        }

        public int GetSelectedAnswer(int questionIndex)
        {
            if (Questions == null || questionIndex < 0 || questionIndex >= Questions.Count)
                return -1;

            return GetSelectedAnswer(Questions[questionIndex]);
        }

        public int GetSelectedAnswer(Question question)
        {
            if (question == null || CurrentAttempt?.Answers == null)
                return -1;

            string qId = GetQuestionId(question);
            var item = CurrentAttempt.Answers.Find(a => a.QuestionId == qId);
            return item != null ? item.SelectedIndex : -1;
        }

        public void RecordAnswer(Question question, int selectedIndex)
        {
            if (question == null) return;

            bool isCorrect = (selectedIndex == question.correctIndex);
            string qId = GetQuestionId(question);

            if (CurrentAttempt != null)
            {
                var existingItem = CurrentAttempt.Answers.Find(a => a.QuestionId == qId);
                if (existingItem != null)
                {
                    existingItem.SelectedIndex = selectedIndex;
                    existingItem.CorrectIndex = question.correctIndex;
                    existingItem.IsCorrect = isCorrect;
                    existingItem.QuestionLevel = question.questionLevel;
                    existingItem.QuestionDatabankName = question.questionDatabankName ?? "";
                }
                else
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
                }

                if (!CurrentAttempt.QuestionIds.Contains(qId))
                {
                    CurrentAttempt.QuestionIds.Add(qId);
                }

                CurrentAttempt.CorrectQuestionIds.Remove(qId);
                CurrentAttempt.WrongQuestionIds.Remove(qId);

                if (isCorrect)
                {
                    CurrentAttempt.CorrectQuestionIds.Add(qId);
                }
                else
                {
                    CurrentAttempt.WrongQuestionIds.Add(qId);
                }

                CurrentAttempt.Score.Total = CurrentAttempt.Answers.Count;
                CurrentAttempt.Score.Correct = CurrentAttempt.CorrectQuestionIds.Count;
                CurrentAttempt.Score.Wrong = CurrentAttempt.WrongQuestionIds.Count;
            }

            if (CurrentResult != null)
            {
                string userAns = (question.answers != null && question.answers.Length > selectedIndex) ? question.answers[selectedIndex] : selectedIndex.ToString();
                string correctAns = (question.answers != null && question.answers.Length > question.correctIndex) ? question.answers[question.correctIndex] : question.correctIndex.ToString();

                var existingDetail = CurrentResult.QuestionsAnswered.Find(d => d.QuestionId == qId);
                if (existingDetail != null)
                {
                    existingDetail.Difficulty = question.questionLevel.ToString();
                    existingDetail.IsCorrect = isCorrect;
                    existingDetail.UserAnswer = userAns;
                    existingDetail.CorrectAnswer = correctAns;
                }
                else
                {
                    CurrentResult.QuestionsAnswered.Add(new AssessmentAnswerDetail
                    {
                        QuestionId = qId,
                        Difficulty = question.questionLevel.ToString(),
                        IsCorrect = isCorrect,
                        UserAnswer = userAns,
                        CorrectAnswer = correctAns
                    });
                }

                CurrentResult.Score.Total = CurrentResult.QuestionsAnswered.Count;
                int correctCount = 0;
                for (int i = 0; i < CurrentResult.QuestionsAnswered.Count; i++)
                {
                    if (CurrentResult.QuestionsAnswered[i].IsCorrect) correctCount++;
                }
                CurrentResult.Score.Correct = correctCount;
            }
        }

        public void RecordAnswer(string questionId, string difficulty, bool isCorrect, string userAnswer, string correctAnswer)
        {
            if (CurrentResult != null)
            {
                var existingDetail = CurrentResult.QuestionsAnswered.Find(d => d.QuestionId == questionId);
                if (existingDetail != null)
                {
                    existingDetail.Difficulty = difficulty;
                    existingDetail.IsCorrect = isCorrect;
                    existingDetail.UserAnswer = userAnswer;
                    existingDetail.CorrectAnswer = correctAnswer;
                }
                else
                {
                    CurrentResult.QuestionsAnswered.Add(new AssessmentAnswerDetail
                    {
                        QuestionId = questionId,
                        Difficulty = difficulty,
                        IsCorrect = isCorrect,
                        UserAnswer = userAnswer,
                        CorrectAnswer = correctAnswer
                    });
                }

                CurrentResult.Score.Total = CurrentResult.QuestionsAnswered.Count;
                int correctCount = 0;
                for (int i = 0; i < CurrentResult.QuestionsAnswered.Count; i++)
                {
                    if (CurrentResult.QuestionsAnswered[i].IsCorrect) correctCount++;
                }
                CurrentResult.Score.Correct = correctCount;
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
