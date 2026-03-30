using System;
using System.Threading.Tasks;

public class FakePlayerLevelService : IPlayerLevelService
{
    public int CurrentLevel  { get; set; } = 1;
    public int TotalAnswered { get; set; } = 0;

    public event Action<int, int> OnLevelChanged;
    public event Action<int> OnLevelProgressUpdated;

    public Task IncrementTotalAnswered()
    {
        TotalAnswered++;
        OnLevelProgressUpdated?.Invoke(TotalAnswered);
        return Task.CompletedTask;
    }

    public Task CheckAndHandleLevelUp()
    {
        int newLevel = (TotalAnswered / 10) + 1;
        if (newLevel > CurrentLevel)
        {
            int old = CurrentLevel;
            CurrentLevel = newLevel;
            OnLevelChanged?.Invoke(old, CurrentLevel);
        }
        return Task.CompletedTask;
    }

    public Task RecalculateTotalAnswered()       => Task.CompletedTask;
    public void OnUserDataLoaded(UserData userData) { }

    public int GetCurrentLevel()                => CurrentLevel;
    public int GetTotalValidAnswered()           => TotalAnswered;
    public int GetTotalQuestionsInAllDatabanks() => 100;
    public float GetProgressInCurrentLevel()     => (float)(TotalAnswered % 10) / 10f;
    public int GetQuestionsUntilNextLevel()      => 10 - (TotalAnswered % 10);
}