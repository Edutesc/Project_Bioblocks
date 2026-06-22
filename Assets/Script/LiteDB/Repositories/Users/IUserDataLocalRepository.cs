using System;

public interface IUserDataLocalRepository
{
    UserData GetUser(string userId);

    void SaveUser(UserData userData);

    void UpdateUser(UserData userData);

    void MarkAsDirty(string userId);

    void MarkAsSynced(string userId);

    /// <summary>
    /// Marca o usuário como sincronizado apenas se o SavedAt local ainda for igual
    /// ao SavedAt do snapshot que foi enviado ao Firestore.
    ///
    /// Isso evita limpar IsDirty quando uma alteração local mais nova ocorreu
    /// enquanto um sync em background ainda estava em andamento.
    /// </summary>
    bool MarkAsSyncedIfSavedAtMatches(string userId, DateTime expectedSavedAt);

    bool HasUser(string userId);

    bool IsDirty(string userId);

    void DeleteUser(string userId);

    DateTime GetLastSyncedAt(string userId);

    void UpdateScore(string userId, int newScore, int newWeekScore);

    void AddAnsweredQuestion(string userId, string databankName, int questionNumber);
}