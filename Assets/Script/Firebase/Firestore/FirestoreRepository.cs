using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

/// <summary>
/// Fachada temporária para preservar compatibilidade com o código que ainda usa
/// IFirestoreRepository.
///
/// Novas classes devem preferir depender das interfaces específicas:
///   - IFirestoreUserRepository
///   - INicknameRepository
///   - IQuestionStatsRepository
///   - IUserBonusRepository
///   - IUserRealtimeListenerService
///   - IRankingRepository
///   - IFirestoreAdminRepository
/// </summary>
public class FirestoreRepository : MonoBehaviour, IFirestoreRepository
{
    private FirebaseFirestore _db;
    private bool _isInitialized;

    private IFirestoreUserRepository      _users;
    private INicknameRepository           _nicknames;
    private IFirestoreQuestionStatsRepository      _questionStats;
    private IUserBonusRepository          _userBonus;
    private IUserRealtimeListenerService  _listeners;
    private IFirestoreRankingRepository            _rankings;
    private IFirestoreAdminRepository     _admin;

    public bool IsInitialized => _isInitialized;

    public bool IsListening => _listeners != null && _listeners.IsListening;

    public void Initialize()
    {
        if (_isInitialized)
            return;

        try
        {
            _db = FirebaseFirestore.DefaultInstance;

            _users        = new FirestoreUserRepository(_db);
            _nicknames    = new FirestoreNicknameRepository(_db);
            _questionStats= new FirestoreQuestionStatsRepository(_db);
            _userBonus    = new FirestoreUserBonusRepository(_db);
            _listeners    = new UserRealtimeListenerService(_db);
            _rankings     = new FirestoreRankingRepository(_db);
            _admin        = new FirestoreAdminRepository(_db);

            _isInitialized = true;

            Debug.Log("[FirestoreRepository] Firestore initialized successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreRepository] Firestore initialization failed: {e.Message}");
            throw;
        }
    }

    private void EnsureInitialized()
    {
        if (!_isInitialized || _db == null)
            throw new InvalidOperationException("[FirestoreRepository] Firestore não inicializado.");
    }

    // -------------------------------------------------------------------------
    // Users
    // -------------------------------------------------------------------------

    public Task<UserData> GetUserData(string userId)
    {
        EnsureInitialized();
        return _users.GetUserData(userId);
    }

    public Task<List<UserData>> GetAllUsersData()
    {
        EnsureInitialized();
        return _users.GetAllUsersData();
    }

    public async Task CreateUserDocument(UserData userData)
    {
        EnsureInitialized();

        await _users.CreateUserDocument(userData);

        // Mantém o comportamento antigo: criar usuário também reserva nickname.
        if (!string.IsNullOrEmpty(userData.NickName))
            await _nicknames.ReserveNickname(userData.NickName, userData.UserId);
    }

    public Task UpdateUserData(UserData userData)
    {
        EnsureInitialized();
        return _users.UpdateUserData(userData);
    }

    public Task UpdateUserScore(
        string userId,
        int newScore,
        int questionNumber,
        string databankName,
        bool isCorrect)
    {
        EnsureInitialized();
        return _users.UpdateUserScore(userId, newScore, questionNumber, databankName, isCorrect);
    }

    public Task UpdateUserScores(
        string userId,
        int additionalScore,
        int questionNumber,
        string databankName,
        bool isCorrect,
        UserData capturedUserData)
    {
        EnsureInitialized();
        return _users.UpdateUserScores(
            userId,
            additionalScore,
            questionNumber,
            databankName,
            isCorrect,
            capturedUserData
        );
    }

    public Task UpdateUserWeekScore(string userId, int additionalScore)
    {
        EnsureInitialized();

        // Mantém a assinatura antiga, embora o parâmetro represente o novo WeekScore.
        return _users.UpdateUserWeekScore(userId, additionalScore);
    }

    public Task UpdateUserField(string userId, string fieldName, object value)
    {
        EnsureInitialized();
        return _users.UpdateUserField(userId, fieldName, value);
    }

    public Task UpdateUserProgress(string userId, int progress)
    {
        EnsureInitialized();
        return _users.UpdateUserProgress(userId, progress);
    }

    public Task UpdateUserProfileImageUrl(string userId, string imageUrl)
    {
        EnsureInitialized();
        return _users.UpdateUserProfileImageUrl(userId, imageUrl);
    }

    public Task ResetAnsweredQuestions(string userId, string databankName)
    {
        EnsureInitialized();
        return _users.ResetAnsweredQuestions(userId, databankName);
    }

    // -------------------------------------------------------------------------
    // Nicknames
    // -------------------------------------------------------------------------

    public Task<bool> AreNicknameTaken(string nickName)
    {
        EnsureInitialized();
        return _nicknames.AreNicknameTaken(nickName);
    }

    // -------------------------------------------------------------------------
    // QuestionStats
    // -------------------------------------------------------------------------

    public Task<QuestionStats> GetQuestionStats()
    {
        EnsureInitialized();
        return _questionStats.GetQuestionStats();
    }

    // -------------------------------------------------------------------------
    // UserBonus
    // -------------------------------------------------------------------------

    public Task<bool> IsDatabankEligibleForBonus(string userId, string databankName)
    {
        EnsureInitialized();
        return _userBonus.IsDatabankEligibleForBonus(userId, databankName);
    }

    public Task MarkDatabankAsCompleted(string userId, string databankName)
    {
        EnsureInitialized();
        return _userBonus.MarkDatabankAsCompleted(userId, databankName);
    }

    // -------------------------------------------------------------------------
    // Listeners
    // -------------------------------------------------------------------------

    public void ListenToUserData(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null)
    {
        EnsureInitialized();
        _listeners.ListenToUserData(userId, onScoreChanged, onWeekScoreChanged, onAnsweredQuestionsChanged);
    }

    public IDisposable ListenToScore(
        string userId,
        Action<int> onScoreChanged,
        Action<int> onWeekScoreChanged)
    {
        EnsureInitialized();
        return _listeners.ListenToScore(userId, onScoreChanged, onWeekScoreChanged);
    }

    public IDisposable ListenToAnsweredQuestions(
        string userId,
        Action<Dictionary<string, List<int>>> onChanged)
    {
        EnsureInitialized();
        return _listeners.ListenToAnsweredQuestions(userId, onChanged);
    }

    public void StopListening()
    {
        _listeners?.StopListening();
    }

    public void ResumeListening(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null)
    {
        EnsureInitialized();
        _listeners.ResumeListening(userId, onScoreChanged, onWeekScoreChanged, onAnsweredQuestionsChanged);
    }

    // -------------------------------------------------------------------------
    // Admin
    // -------------------------------------------------------------------------

    public Task EnsureWeekScoreField()
    {
        EnsureInitialized();
        return _admin.EnsureWeekScoreField();
    }

    public Task DeleteDocument(string collection, string documentId)
    {
        EnsureInitialized();
        return _admin.DeleteDocument(collection, documentId);
    }

    // -------------------------------------------------------------------------
    // Rankings
    // -------------------------------------------------------------------------

    public Task<List<Ranking>> GetRankingsAsync(int limit = 50)
    {
        EnsureInitialized();
        return _rankings.GetRankingsAsync(limit);
    }

    public Task<List<Ranking>> GetWeekRankingsAsync(int limit = 50)
    {
        EnsureInitialized();

        // Compatibilidade com a interface antiga.
        // Como o app atualmente não usa ranking semanal, retornamos o ranking geral.
        // Quando o ranking semanal for reativado, crie IFirestoreWeekRankingRepository
        // ou recoloque GetWeekRankingsAsync em IRankingRepository.
        return _rankings.GetRankingsAsync(limit);
    }

    private void OnDestroy()
    {
        _listeners?.StopListening();
    }
}
