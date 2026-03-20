using LiteDB;
using System;
using System.Collections.Generic;
public class UserDataDB
{
 [BsonId]
 public string UserId { get; set; }
 public string NickName { get; set; }
 public string Name { get; set; }
 public string Email { get; set; }
 public string ProfileImageUrl { get; set; }
 public int Score { get; set; }
 public int WeekScore { get; set; }
 public int QuestionTypeProgress { get; set; }
 public DateTime CreatedTime { get; set; }
 public bool IsUserRegistered { get; set; }
 public int PlayerLevel { get; set; } = 1;
 public int TotalValidQuestionsAnswered { get; set; } = 0;
 public int TotalQuestionsInAllDatabanks { get; set; } = 0;
 public Dictionary<string, List<int>> AnsweredQuestions { get; set; } = new Dictionary<string, List<int>>();
    public Dictionary<string, bool> ResetDatabankFlags { get; set; } =  new Dictionary<string, bool>();

    // Campos de controle de sync
    public DateTime LastModifiedLocal { get; set; } = DateTime.UtcNow;
 public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;
 public bool IsDirty { get; set; } = true;
 public SyncStatus SyncStatus { get; set; } = SyncStatus.PendingUpload;
}
public enum SyncStatus
{
 Synced,
 PendingUpload,
 PendingDownload,
 Conflict
}