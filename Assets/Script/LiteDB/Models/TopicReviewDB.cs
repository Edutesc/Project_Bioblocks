using System;
using System.Collections.Generic;
using LiteDB;

public class TopicReviewDB
{
    [BsonId]
    public string GlobalId { get; set; }

    public string UserId       { get; set; }
    public string DatabankName { get; set; }

    public DateTime LastInteractionAt { get; set; }
    public DateTime NextReviewAt      { get; set; }

    public int TotalSessionsCompleted { get; set; }
    public DateTime UpdatedAt         { get; set; }

    // Guarda o próprio tipo de domínio direto — LiteDB serializa como subdocumento
    public List<TopicReviewSessionHistoryItem> SessionHistory { get; set; }

    // ── Controle de sincronização (igual UserDataDB) ──────────────────────────
    public DateTime LastSyncedAt { get; set; }
    public bool     IsDirty      { get; set; }

    public DateTime CachedAt { get; set; }

    public TopicReviewDB()
    {
        SessionHistory = new List<TopicReviewSessionHistoryItem>();
        IsDirty = false;
    }

    public static TopicReviewDB FromDomain(TopicReviewData domain)
    {
        return new TopicReviewDB
        {
            GlobalId               = BuildId(domain.userId, domain.databankName),
            UserId                 = domain.userId       ?? "",
            DatabankName           = domain.databankName ?? "",
            LastInteractionAt      = domain.lastInteractionAt,
            NextReviewAt           = domain.nextReviewAt,
            TotalSessionsCompleted = domain.totalSessionsCompleted,
            UpdatedAt              = domain.updatedAt,
            SessionHistory         = domain.sessionHistory ?? new List<TopicReviewSessionHistoryItem>(),
            CachedAt = DateTime.Now
        };
    }

    public TopicReviewData ToDomain()
    {
        return new TopicReviewData
        {
            userId                 = UserId,
            databankName           = DatabankName,
            lastInteractionAt      = DateTime.SpecifyKind(LastInteractionAt, DateTimeKind.Utc),
            nextReviewAt           = DateTime.SpecifyKind(NextReviewAt, DateTimeKind.Utc),
            totalSessionsCompleted = TotalSessionsCompleted,
            updatedAt              = DateTime.SpecifyKind(UpdatedAt, DateTimeKind.Utc),
            sessionHistory         = SessionHistory ?? new List<TopicReviewSessionHistoryItem>()
        };
    }

    public static string BuildId(string userId, string databankName)
        => $"{userId}_{databankName}";
}