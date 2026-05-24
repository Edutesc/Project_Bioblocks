using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Firestore;

/// <summary>
/// Conversões entre UserData do domínio e documentos Firestore.
/// Mantido separado para evitar duplicação entre repositórios e listeners.
/// </summary>
public static class FirestoreUserDataMapper
{
    public static UserData FromDictionary(Dictionary<string, object> data)
    {
        UserData userData = new UserData();

        userData.UserId   = data.ContainsKey("UserId")   ? (string)data["UserId"]   : "";
        userData.NickName = data.ContainsKey("NickName") ? (string)data["NickName"] : "";
        userData.Name     = data.ContainsKey("Name")     ? (string)data["Name"]     : "";
        userData.Email    = data.ContainsKey("Email")    ? (string)data["Email"]    : "";

        userData.ProfileImageUrl = data.ContainsKey("ProfileImageUrl")
            ? (string)data["ProfileImageUrl"] ?? ""
            : "";

        userData.Score = data.ContainsKey("Score")
            ? Convert.ToInt32(data["Score"])
            : 0;

        userData.WeekScore = data.ContainsKey("WeekScore")
            ? Convert.ToInt32(data["WeekScore"])
            : 0;

        userData.QuestionTypeProgress = data.ContainsKey("QuestionTypeProgress")
            ? Convert.ToInt32(data["QuestionTypeProgress"])
            : (data.ContainsKey("Progress") ? Convert.ToInt32(data["Progress"]) : 0);

        userData.PlayerLevel = data.ContainsKey("PlayerLevel")
            ? Convert.ToInt32(data["PlayerLevel"])
            : 1;

        userData.TotalValidQuestionsAnswered = data.ContainsKey("TotalValidQuestionsAnswered")
            ? Convert.ToInt32(data["TotalValidQuestionsAnswered"])
            : 0;

        userData.TotalQuestionsInAllDatabanks = data.ContainsKey("TotalQuestionsInAllDatabanks")
            ? Convert.ToInt32(data["TotalQuestionsInAllDatabanks"])
            : 0;

        userData.LevelSnapshotDenominator = data.ContainsKey("LevelSnapshotDenominator")
            ? Convert.ToInt32(data["LevelSnapshotDenominator"])
            : 0;

        userData.IsUserRegistered = data.ContainsKey("IsUserRegistered")
            ? Convert.ToBoolean(data["IsUserRegistered"])
            : false;

        userData.SavedAt = data.ContainsKey("SavedAt") && data["SavedAt"] is Timestamp savedAt
            ? savedAt.ToDateTime()
            : DateTime.MinValue;

        if (data.ContainsKey("CreatedTime") && data["CreatedTime"] is Timestamp timestamp)
            userData.CreatedTime = timestamp.ToDateTime();
        else
            userData.CreatedTime = DateTime.UtcNow;

        if (data.ContainsKey("ResetDatabankFlags") &&
            data["ResetDatabankFlags"] is Dictionary<string, object> resetFlagsData)
        {
            userData.ResetDatabankFlags = new Dictionary<string, bool>();

            foreach (var kvp in resetFlagsData)
                userData.ResetDatabankFlags[kvp.Key] = Convert.ToBoolean(kvp.Value);
        }

        userData.AnsweredQuestions = new Dictionary<string, List<int>>();

        if (data.ContainsKey("AnsweredQuestions") &&
            data["AnsweredQuestions"] is Dictionary<string, object> answeredQuestionsData)
        {
            foreach (var kvp in answeredQuestionsData)
            {
                if (kvp.Value is IEnumerable<object> list)
                    userData.AnsweredQuestions[kvp.Key] = list.Select(x => Convert.ToInt32(x)).ToList();
            }
        }

        return userData;
    }

    public static Dictionary<string, object> ToDictionary(UserData userData)
    {
        return new Dictionary<string, object>
        {
            { "UserId",                       userData.UserId },
            { "NickName",                     userData.NickName },
            { "Name",                         userData.Name },
            { "Email",                        userData.Email },
            { "ProfileImageUrl",              userData.ProfileImageUrl ?? "" },
            { "Score",                        userData.Score },
            { "WeekScore",                    userData.WeekScore },
            { "QuestionTypeProgress",         userData.QuestionTypeProgress },
            { "IsUserRegistered",             userData.IsUserRegistered },
            { "SavedAt",                      FieldValue.ServerTimestamp },
            { "PlayerLevel",                  userData.PlayerLevel },
            { "TotalValidQuestionsAnswered",  userData.TotalValidQuestionsAnswered },
            { "TotalQuestionsInAllDatabanks", userData.TotalQuestionsInAllDatabanks },
            { "LevelSnapshotDenominator",     userData.LevelSnapshotDenominator },
            { "AnsweredQuestions",            userData.AnsweredQuestions
                                                ?? new Dictionary<string, List<int>>() },
            { "ResetDatabankFlags",           userData.ResetDatabankFlags
                                                ?? new Dictionary<string, bool>() },
            { "CreatedTime",                  Timestamp.FromDateTime(
                DateTime.SpecifyKind(userData.CreatedTime, DateTimeKind.Utc)) }
        };
    }

    public static Dictionary<string, object> ToNewUserDocument(UserData userData)
    {
        return new Dictionary<string, object>
        {
            { "UserId",               userData.UserId },
            { "NickName",             userData.NickName },
            { "Name",                 userData.Name },
            { "Email",                userData.Email },
            { "Score",                userData.Score },
            { "WeekScore",            userData.WeekScore },
            { "QuestionTypeProgress", userData.QuestionTypeProgress },
            { "IsUserRegistered",     userData.IsUserRegistered },
            { "CreatedTime",          Timestamp.FromDateTime(
                DateTime.SpecifyKind(userData.CreatedTime, DateTimeKind.Utc)) },
            { "ProfileImageUrl",      userData.ProfileImageUrl },
            { "AnsweredQuestions",    new Dictionary<string, object>() }
        };
    }
}
