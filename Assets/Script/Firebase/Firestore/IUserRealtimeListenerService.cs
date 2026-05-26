using System;
using System.Collections.Generic;

/// <summary>
/// Listeners em tempo real do documento Users/{userId}.
/// </summary>
public interface IUserRealtimeListenerService
{
    bool IsListening { get; }

    void ListenToUserData(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null);

    IDisposable ListenToScore(
        string userId,
        Action<int> onScoreChanged,
        Action<int> onWeekScoreChanged);

    IDisposable ListenToAnsweredQuestions(
        string userId,
        Action<Dictionary<string, List<int>>> onChanged);

    void StopListening();

    void ResumeListening(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null);
}
