using System;
using Firebase.Firestore;
public static class UserDataDBMapper
{
    // UserData (Firestore) → UserDataDB (LiteDB)
    public static UserDataDB FromUserData(UserData source)
    {
        return new UserDataDB
        {
            UserId = source.UserId,
            NickName = source.NickName,
            Name = source.Name,
            Email = source.Email,
            ProfileImageUrl = source.ProfileImageUrl,
            Score = source.Score,
            WeekScore = source.WeekScore,
            QuestionTypeProgress = source.QuestionTypeProgress,
            CreatedTime = source.CreatedTime,
            IsUserRegistered = source.IsUserRegistered,
            PlayerLevel = source.PlayerLevel,
            TotalValidQuestionsAnswered = source.TotalValidQuestionsAnswered,
            TotalQuestionsInAllDatabanks = source.TotalQuestionsInAllDatabanks,
            AnsweredQuestions = source.AnsweredQuestions ?? new(),
            ResetDatabankFlags = source.ResetDatabankFlags ?? new(),
            IsDirty = false,
            SyncStatus = SyncStatus.Synced,
            LastSyncedAt = DateTime.UtcNow,
            LastModifiedLocal = DateTime.UtcNow,
        };
    }
    // UserDataDB (LiteDB) → UserData (Firestore / memória)
    public static UserData ToUserData(UserDataDB source)
    {
        return new UserData
        {
            UserId = source.UserId,
            NickName = source.NickName,
            Name = source.Name,
            Email = source.Email,
            ProfileImageUrl = source.ProfileImageUrl,
            Score = source.Score,
            WeekScore = source.WeekScore,
            QuestionTypeProgress = source.QuestionTypeProgress,
            CreatedTime = Timestamp.FromDateTime(DateTime.SpecifyKind(source.CreatedTime, DateTimeKind.Utc)).ToDateTime(),
            IsUserRegistered = source.IsUserRegistered,
            PlayerLevel = source.PlayerLevel,
            TotalValidQuestionsAnswered = source.TotalValidQuestionsAnswered,
            TotalQuestionsInAllDatabanks = source.TotalQuestionsInAllDatabanks,
            AnsweredQuestions = source.AnsweredQuestions ?? new(),
            ResetDatabankFlags = source.ResetDatabankFlags ?? new(),
        };
    }
}
