using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Acesso remoto à coleção Users.
/// </summary>
public interface IFirestoreUserRepository
{
    Task<UserData> GetUserData(string userId);

    Task<List<UserData>> GetAllUsersData();

    Task CreateUserDocument(UserData userData);

    Task UpdateUserData(UserData userData);

    Task UpdateUserScore(
        string userId,
        int newScore,
        int questionNumber,
        string databankName,
        bool isCorrect);

    Task UpdateUserScores(
        string userId,
        int additionalScore,
        int questionNumber,
        string databankName,
        bool isCorrect,
        UserData capturedUserData);

    Task UpdateUserWeekScore(string userId, int weekScore);

    Task UpdateUserField(string userId, string fieldName, object value);

    Task UpdateUserProgress(string userId, int progress);

    Task UpdateUserProfileImageUrl(string userId, string imageUrl);

    Task ResetAnsweredQuestions(string userId, string databankName);
}
