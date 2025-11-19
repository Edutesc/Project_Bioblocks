using UnityEngine;
using System;
using System.Collections.Generic;
using Firebase.Firestore;

[System.Serializable]
[FirestoreData]
public class UserData
{
    [FirestoreProperty]
    public string UserId { get; set; }

    [FirestoreProperty]
    public string NickName { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Email { get; set; }

    [FirestoreProperty]
    public string ProfileImageUrl { get; set; }

    [FirestoreProperty]
    public int Score { get; set; }

    [FirestoreProperty]
    public int WeekScore { get; set; }

    [FirestoreProperty]
    public int QuestionTypeProgress { get; set; }

    [FirestoreProperty]
    public Timestamp CreatedTime { get; set; }

    [FirestoreProperty]
    public Dictionary<string, List<int>> AnsweredQuestions { get; set; }

    [FirestoreProperty]
    public bool IsUserRegistered { get; set; }

    [FirestoreProperty]
    public int PlayerLevel { get; set; } = 1;

    [FirestoreProperty]
    public int TotalValidQuestionsAnswered { get; set; } = 0;

    [FirestoreProperty]
    public int TotalQuestionsInAllDatabanks { get; set; } = 0;

    [FirestoreProperty]
    public Dictionary<string, bool> ResetDatabankFlags { get; set; } = new Dictionary<string, bool>();

    public UserData()
    {
        AnsweredQuestions = new Dictionary<string, List<int>>();
        IsUserRegistered = false;
    }

    public UserData(string userId, string nickName, string name, string email,
           string profileImageUrl = null, int score = 0, int weekScore = 0, int questionTypeProgress = 0,
           bool isRegistered = false)
    {
        UserId = userId;
        NickName = nickName;
        Name = name;
        Email = email;
        ProfileImageUrl = profileImageUrl;
        Score = score;
        WeekScore = weekScore;
        QuestionTypeProgress = questionTypeProgress;
        CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow);
        IsUserRegistered = isRegistered;
        AnsweredQuestions = new Dictionary<string, List<int>>();
        PlayerLevel = 1;
        TotalValidQuestionsAnswered = 0;
        TotalQuestionsInAllDatabanks = 0;
        ResetDatabankFlags = new Dictionary<string, bool>();
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "AnsweredQuestions", AnsweredQuestions },
            { "UserId", UserId },
            { "NickName", NickName },
            { "Name", Name },
            { "Email", Email },
            { "ProfileImageUrl", ProfileImageUrl },
            { "Score", Score },
            { "WeekScore", WeekScore },
            { "QuestionTypeProgress", QuestionTypeProgress },
            { "CreatedTime", CreatedTime },
            { "IsUserRegistered", IsUserRegistered },
            { "PlayerLevel", PlayerLevel },
            { "TotalValidQuestionsAnswered", TotalValidQuestionsAnswered },
            { "TotalQuestionsInAllDatabanks", TotalQuestionsInAllDatabanks },
            { "ResetDatabankFlags", ResetDatabankFlags }
        };
    }

    public void SetUserRegistered(bool registered)
    {
        IsUserRegistered = registered;
    }

    public DateTime GetCreatedDateTime()
    {
        return CreatedTime.ToDateTime();
    }

    public string GetFormattedCreatedTime()
    {
        return GetCreatedDateTime().ToLocalTime().ToString("dd/MM/yyyy");
    }

    public string GetFormattedCreatedTimeWithHour()
    {
        return GetCreatedDateTime().ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
    }

}

public static class UserDataStore
{
    private static UserData _currentUserData;
    public static event Action<UserData> OnUserDataChanged;

    public static UserData CurrentUserData
    {
        get => _currentUserData;
        set
        {
            _currentUserData = value;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"UserDataStore atualizado para usuário: {_currentUserData?.UserId}, Score: {_currentUserData?.Score}, WeekScore: {_currentUserData?.WeekScore}");
        }
    }

    public static void UpdateScore(int newScore)
    {
        if (_currentUserData != null)
        {
            _currentUserData.Score = newScore;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"Score atualizado para: {newScore}");
        }
    }

    public static void UpdateWeekScore(int newWeekScore)
    {
        if (_currentUserData != null)
        {
            _currentUserData.WeekScore = newWeekScore;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"WeekScore atualizado para: {newWeekScore}");
        }
    }

    public static void AddScore(int additionalScore)
    {
        if (_currentUserData != null)
        {
            _currentUserData.Score += additionalScore;
            _currentUserData.WeekScore += additionalScore; // Also update week score when adding points
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"Score incrementado em {additionalScore}. Novo Score: {_currentUserData.Score}, Novo WeekScore: {_currentUserData.WeekScore}");
        }
    }

    public static void UpdateUserData(UserData userData)
    {
        if (userData != null)
        {
            _currentUserData = userData;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"UserDataStore atualizado para usuário: {_currentUserData?.UserId}, Score: {_currentUserData?.Score}, WeekScore: {_currentUserData?.WeekScore}");
        }
    }

    public static void UpdatePlayerLevel(int newLevel)
    {
        if (_currentUserData != null)
        {
            _currentUserData.PlayerLevel = newLevel;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"PlayerLevel atualizado para: {newLevel}");
        }
    }

    public static void UpdateTotalValidQuestionsAnswered(int newTotal)
    {
        if (_currentUserData != null)
        {
            _currentUserData.TotalValidQuestionsAnswered = newTotal;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"TotalValidQuestionsAnswered atualizado para: {newTotal}");
        }
    }

    public static void UpdateTotalQuestionsInAllDatabanks(int newTotal)
    {
        if (_currentUserData != null)
        {
            _currentUserData.TotalQuestionsInAllDatabanks = newTotal;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"TotalQuestionsInAllDatabanks atualizado para: {newTotal}");
        }
    }

    public static void MarkDatabankAsReset(string databankName, bool isReset)
    {
        if (_currentUserData != null)
        {
            if (_currentUserData.ResetDatabankFlags == null)
            {
                _currentUserData.ResetDatabankFlags = new Dictionary<string, bool>();
            }
            
            _currentUserData.ResetDatabankFlags[databankName] = isReset;
            OnUserDataChanged?.Invoke(_currentUserData);
            Debug.Log($"Databank {databankName} marcado como resetado: {isReset}");
        }
    }

    public static bool IsDatabankReset(string databankName)
    {
        if (_currentUserData?.ResetDatabankFlags == null) return false;
        return _currentUserData.ResetDatabankFlags.ContainsKey(databankName) 
            && _currentUserData.ResetDatabankFlags[databankName];
    }

}
