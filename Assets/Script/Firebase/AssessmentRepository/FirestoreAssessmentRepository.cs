using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;
using Edutesc.BioBlocks.Core.Models;

public class FirestoreAssessmentRepository : IFirestoreAssessmentRepository
{
    private readonly FirebaseFirestore _db;
    private const string AssessmentsCollection = "Assessments";
    private const string AttemptsSubcollection = "Attempts";

    public FirestoreAssessmentRepository()
    {
        _db = FirebaseFirestore.DefaultInstance;
    }

    public async Task<AssessmentData> GetAssessmentAsync(string assessmentId)
    {
        if (string.IsNullOrEmpty(assessmentId))
        {
            Debug.LogWarning("[FirestoreAssessmentRepository] AssessmentId inválido.");
            return null;
        }

        try
        {
            DocumentReference docRef = _db.Collection(AssessmentsCollection).Document(assessmentId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return ConvertFromFirestore(snapshot);
            }

            Debug.LogWarning($"[FirestoreAssessmentRepository] Avaliação '{assessmentId}' não encontrada no Firestore.");
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreAssessmentRepository] Erro ao buscar avaliação '{assessmentId}': {e.Message}");
            throw;
        }
    }

    public async Task<AssessmentData> GetActiveAssessmentAsync()
    {
        try
        {
            Query query = _db.Collection(AssessmentsCollection)
                .WhereEqualTo("enabled", true)
                .WhereEqualTo("archived", false)
                .Limit(1);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count > 0)
            {
                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    var assessment = ConvertFromFirestore(doc);
                    if (assessment != null)
                    {
                        return assessment;
                    }
                }
            }

            Debug.LogWarning("[FirestoreAssessmentRepository] Nenhuma avaliação ativa encontrada. Tentando fallback padrão.");
            return await GetAssessmentAsync("2026-3-aminoacidos-proteinas-enzimas");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreAssessmentRepository] Erro ao buscar avaliação ativa: {e.Message}");
            return null;
        }
    }

    public async Task SaveAssessmentAttemptAsync(string assessmentId, AssessmentAttempt attempt)
    {
        if (string.IsNullOrEmpty(assessmentId) || attempt == null)
        {
            Debug.LogError("[FirestoreAssessmentRepository] AssessmentId ou Attempt nulo ao tentar salvar tentativa.");
            return;
        }

        try
        {
            if (string.IsNullOrEmpty(attempt.AttemptId))
            {
                attempt.AttemptId = $"attempt-{Guid.NewGuid().ToString("N").Substring(0, 8)}";
            }

            if (attempt.CompletedAt == default)
            {
                attempt.CompletedAt = Timestamp.GetCurrentTimestamp();
            }

            DocumentReference docRef = _db.Collection(AssessmentsCollection)
                .Document(assessmentId)
                .Collection(AttemptsSubcollection)
                .Document(attempt.AttemptId);

            await docRef.SetAsync(attempt);

            Debug.Log($"[FirestoreAssessmentRepository] Tentativa '{attempt.AttemptId}' do usuário '{attempt.UserId}' salva na avaliação '{assessmentId}'.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreAssessmentRepository] Erro ao salvar tentativa de avaliação: {e.Message}");
            throw;
        }
    }

    public async Task SaveAssessmentAsync(AssessmentResult result)
    {
        try
        {
            if (result.CompletedAt == default)
            {
                result.CompletedAt = Timestamp.GetCurrentTimestamp();
            }

            CollectionReference collection = _db.Collection("assessments");
            await collection.AddAsync(result);
            
            Debug.Log($"[FirestoreAssessmentRepository] Assessment result for user {result.UserId} saved successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreAssessmentRepository] Failed to save assessment result: {e.Message}");
            throw;
        }
    }

    private AssessmentData ConvertFromFirestore(DocumentSnapshot doc)
    {
        if (doc == null || !doc.Exists) return null;

        var data = doc.ToDictionary();
        if (data == null) return null;

        return new AssessmentData
        {
            AssessmentId = GetString(data, "assessmentId", doc.Id),
            AssessmentType = GetString(data, "assessmentType", ""),
            AcademicTerm = GetString(data, "academicTerm", ""),
            CourseId = GetString(data, "courseId", ""),
            ClassId = GetString(data, "classId", ""),
            Title = GetString(data, "title", "Avaliação formativa"),
            Description = GetString(data, "description", ""),
            DisplayTopics = GetStringList(data, "displayTopics"),
            AllowedDatabanks = GetStringList(data, "allowedDatabanks"),
            QuestionDistribution = GetDistribution(data, "questionDistribution"),
            TotalQuestions = GetInt(data, "totalQuestions", 10),
            DurationMinutes = GetInt(data, "durationMinutes", 15),
            AllowRetakes = GetBool(data, "allowRetakes", true),
            Enabled = GetBool(data, "enabled", true),
            OpensAt = GetTimestamp(data, "opensAt"),
            LastStartAt = GetTimestamp(data, "lastStartAt"),
            ClosesAt = GetTimestamp(data, "closesAt"),
            DisplayTimeZone = GetString(data, "displayTimeZone", "America/Sao_Paulo"),
            Archived = GetBool(data, "archived", false),
            CreatedAt = GetTimestamp(data, "createdAt"),
            UpdatedAt = GetTimestamp(data, "updatedAt")
        };
    }

    private string GetString(Dictionary<string, object> data, string key, string fallback = "")
    {
        if (data != null && data.TryGetValue(key, out object val) && val != null)
        {
            return val.ToString();
        }
        return fallback;
    }

    private int GetInt(Dictionary<string, object> data, string key, int fallback = 0)
    {
        if (data != null && data.TryGetValue(key, out object val) && val != null)
        {
            if (val is int i) return i;
            if (val is long l) return (int)l;
            if (int.TryParse(val.ToString(), out int parsed)) return parsed;
        }
        return fallback;
    }

    private bool GetBool(Dictionary<string, object> data, string key, bool fallback = false)
    {
        if (data != null && data.TryGetValue(key, out object val) && val != null)
        {
            if (val is bool b) return b;
            if (bool.TryParse(val.ToString(), out bool parsed)) return parsed;
        }
        return fallback;
    }

    private Timestamp GetTimestamp(Dictionary<string, object> data, string key)
    {
        if (data != null && data.TryGetValue(key, out object val) && val != null)
        {
            if (val is Timestamp ts) return ts;
        }
        return default;
    }

    private List<string> GetStringList(Dictionary<string, object> data, string key)
    {
        var result = new List<string>();
        if (data != null && data.TryGetValue(key, out object val) && val != null)
        {
            if (val is IEnumerable enumerable && !(val is string))
            {
                foreach (var item in enumerable)
                {
                    if (item != null) result.Add(item.ToString());
                }
            }
        }
        return result;
    }

    private QuestionDistribution GetDistribution(Dictionary<string, object> data, string key)
    {
        var dist = new QuestionDistribution { Basic = 4, Intermediate = 3, Hard = 3 };
        if (data != null && data.TryGetValue(key, out object val) && val is Dictionary<string, object> dict)
        {
            dist.Basic = GetInt(dict, "basic", dist.Basic);
            dist.Intermediate = GetInt(dict, "intermediate", dist.Intermediate);
            dist.Hard = GetInt(dict, "hard", dist.Hard);
        }
        return dist;
    }
}
