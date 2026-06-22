using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Firestore;
using UnityEngine;

/// <summary>
/// Serviço responsável apenas por listeners em tempo real do usuário.
/// Não faz escrita no Firestore.
/// </summary>
public class UserRealtimeListenerService : IUserRealtimeListenerService
{
    private readonly FirebaseFirestore _db;

    private ListenerRegistration _userDataListener;
    private ListenerRegistration _answeredQuestionsListener;

    public bool IsListening => _userDataListener != null;

    public UserRealtimeListenerService(FirebaseFirestore db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public void ListenToUserData(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null)
    {
        _userDataListener?.Stop();

        _userDataListener = _db.Collection("Users").Document(userId)
            .Listen(snapshot =>
            {
                try
                {
                    if (snapshot == null || !snapshot.Exists)
                        return;

                    Dictionary<string, object> data;

                    try
                    {
                        data = snapshot.ToDictionary();
                    }
                    catch
                    {
                        // Snapshot inválido por perda temporária de conexão.
                        return;
                    }

                    UserData currentUserData = UserDataStore.CurrentUserData;

                    HandleScore(data, currentUserData, onScoreChanged);
                    HandleWeekScore(data, currentUserData, onWeekScoreChanged);
                    HandleTotalValidQuestionsAnswered(data, currentUserData);
                    HandlePlayerLevel(data, currentUserData);
                    HandleProfileImage(data, currentUserData);
                    HandleAnsweredQuestions(data, userId, onAnsweredQuestionsChanged);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning(
                        $"[UserRealtimeListenerService] Erro no listener de usuário: {ex.Message}"
                    );
                }
            });
    }

    public IDisposable ListenToScore(
        string userId,
        Action<int> onScoreChanged,
        Action<int> onWeekScoreChanged)
    {
        _userDataListener?.Stop();

        _userDataListener = _db.Collection("Users").Document(userId)
            .Listen(snapshot =>
            {
                try
                {
                    if (snapshot == null || !snapshot.Exists)
                        return;

                    var data = snapshot.ToDictionary();

                    if (data.ContainsKey("Score"))
                    {
                        int newScore = Convert.ToInt32(data["Score"]);

                        MainThreadDispatcher.Enqueue(() =>
                        {
                            UserDataStore.UpdateScore(newScore);
                            onScoreChanged?.Invoke(newScore);
                        });
                    }

                    if (data.ContainsKey("WeekScore"))
                    {
                        int newWeekScore = Convert.ToInt32(data["WeekScore"]);

                        MainThreadDispatcher.Enqueue(() =>
                        {
                            UserDataStore.UpdateWeekScore(newWeekScore);
                            onWeekScoreChanged?.Invoke(newWeekScore);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning(
                        $"[UserRealtimeListenerService] Erro no listener de score: {ex.Message}"
                    );
                }
            });

        return _userDataListener;
    }

    public IDisposable ListenToAnsweredQuestions(
        string userId,
        Action<Dictionary<string, List<int>>> onChanged)
    {
        _answeredQuestionsListener?.Stop();

        _answeredQuestionsListener = _db.Collection("Users").Document(userId)
            .Listen(snapshot =>
            {
                try
                {
                    if (snapshot == null || !snapshot.Exists)
                        return;

                    var data = snapshot.ToDictionary();

                    if (!data.ContainsKey("AnsweredQuestions"))
                        return;

                    if (data["AnsweredQuestions"] is not Dictionary<string, object> raw)
                        return;

                    var answeredQuestions = ConvertAnsweredQuestions(raw);

                    MainThreadDispatcher.Enqueue(() =>
                    {
                        var local = UserDataStore.CurrentUserData;

                        if (local != null)
                        {
                            local.AnsweredQuestions = answeredQuestions;
                            UserDataStore.CurrentUserData = local;
                        }

                        onChanged?.Invoke(answeredQuestions);
                    });
                }
                catch (Exception ex)
                {
                    Debug.LogError(
                        $"[UserRealtimeListenerService] Erro no listener AnsweredQuestions: {ex.Message}"
                    );
                }
            });

        return _answeredQuestionsListener;
    }

    public void StopListening()
    {
        if (_userDataListener != null)
        {
            _userDataListener.Stop();
            _userDataListener = null;
        }

        if (_answeredQuestionsListener != null)
        {
            _answeredQuestionsListener.Stop();
            _answeredQuestionsListener = null;
        }

        Debug.Log("[UserRealtimeListenerService] Listeners parados.");
    }

    public void ResumeListening(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null)
    {
        ListenToUserData(userId, onScoreChanged, onWeekScoreChanged, onAnsweredQuestionsChanged);
        Debug.Log("[UserRealtimeListenerService] Listener retomado.");
    }

    private static void HandleScore(
        Dictionary<string, object> data,
        UserData currentUserData,
        Action<int> onScoreChanged)
    {
        if (!data.ContainsKey("Score"))
            return;

        int incomingScore = Convert.ToInt32(data["Score"]);

        if (currentUserData == null || incomingScore >= currentUserData.Score)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                UserDataStore.UpdateScore(incomingScore);
                onScoreChanged?.Invoke(incomingScore);
            });
        }
    }

    private static void HandleWeekScore(
        Dictionary<string, object> data,
        UserData currentUserData,
        Action<int> onWeekScoreChanged)
    {
        if (!data.ContainsKey("WeekScore"))
            return;

        int incomingWeekScore = Convert.ToInt32(data["WeekScore"]);

        if (currentUserData == null || incomingWeekScore >= currentUserData.WeekScore)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                if (UserDataStore.CurrentUserData != null)
                    UserDataStore.UpdateWeekScore(incomingWeekScore);

                onWeekScoreChanged?.Invoke(incomingWeekScore);
            });
        }
    }

    private static void HandleTotalValidQuestionsAnswered(
        Dictionary<string, object> data,
        UserData currentUserData)
    {
        if (!data.ContainsKey("TotalValidQuestionsAnswered") || currentUserData == null)
            return;

        int total = Convert.ToInt32(data["TotalValidQuestionsAnswered"]);

        if (currentUserData.TotalValidQuestionsAnswered != total)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                var local = UserDataStore.CurrentUserData;

                if (local != null)
                {
                    local.TotalValidQuestionsAnswered = total;
                    UserDataStore.UpdateTotalValidQuestionsAnswered(total);
                }
            });
        }
    }

    private static void HandlePlayerLevel(
        Dictionary<string, object> data,
        UserData currentUserData)
    {
        if (!data.ContainsKey("PlayerLevel") || currentUserData == null)
            return;

        int level = Convert.ToInt32(data["PlayerLevel"]);

        if (currentUserData.PlayerLevel != level)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                var local = UserDataStore.CurrentUserData;

                if (local != null)
                {
                    local.PlayerLevel = level;
                    UserDataStore.UpdatePlayerLevel(level);
                }
            });
        }
    }

    private static void HandleProfileImage(
        Dictionary<string, object> data,
        UserData currentUserData)
    {
        if (!data.ContainsKey("ProfileImageUrl") || currentUserData == null)
            return;

        string incomingUrl = data["ProfileImageUrl"] as string ?? "";

        if (string.IsNullOrEmpty(incomingUrl))
            return;

        if (currentUserData.ProfileImageUrl == incomingUrl)
            return;

        var capturedUrl = incomingUrl;

        MainThreadDispatcher.Enqueue(() =>
        {
            var local = UserDataStore.CurrentUserData;

            if (local == null)
                return;

            local.ProfileImageUrl = capturedUrl;
            UserDataStore.CurrentUserData = local;
            UserAvatarSyncHelper.NotifyAvatarChanged(capturedUrl);

            Debug.Log(
                $"[UserRealtimeListenerService] ProfileImageUrl atualizado via listener: {capturedUrl}"
            );
        });
    }

    private static void HandleAnsweredQuestions(
        Dictionary<string, object> data,
        string userId,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged)
    {
        if (onAnsweredQuestionsChanged == null)
            return;

        if (!data.ContainsKey("AnsweredQuestions"))
            return;

        if (data["AnsweredQuestions"] is not Dictionary<string, object> raw)
            return;

        try
        {
            var answeredQuestions = ConvertAnsweredQuestions(raw);

            MainThreadDispatcher.Enqueue(() =>
            {
                var local = UserDataStore.CurrentUserData;

                if (local != null)
                    local.AnsweredQuestions = answeredQuestions;

                foreach (var kvp in answeredQuestions)
                {
                    AnsweredQuestionsListStore.UpdateAnsweredQuestionsCount(
                        userId,
                        kvp.Key,
                        kvp.Value.Count
                    );
                }

                onAnsweredQuestionsChanged.Invoke(answeredQuestions);
            });
        }
        catch (Exception ex)
        {
            Debug.LogError(
                $"[UserRealtimeListenerService] Erro ao processar AnsweredQuestions: {ex.Message}"
            );
        }
    }

    private static Dictionary<string, List<int>> ConvertAnsweredQuestions(
        Dictionary<string, object> raw)
    {
        var answeredQuestions = new Dictionary<string, List<int>>();

        foreach (var kvp in raw)
        {
            if (kvp.Value is IEnumerable<object> list)
            {
                answeredQuestions[kvp.Key] = list
                    .Select(q => Convert.ToInt32(q))
                    .ToList();
            }
        }

        return answeredQuestions;
    }
}
