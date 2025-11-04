using System;
using SQLite4Unity3d;

[Table("SyncMetadata")]
public class SyncMetadataEntity
{
    [PrimaryKey]
    public string EntityType { get; set; }

    public DateTime LastSyncTimestamp { get; set; }

    public string SyncStatus { get; set; }

    public string LastError { get; set; }

    public int SyncAttempts { get; set; }

    public SyncMetadataEntity()
    {
        LastSyncTimestamp = DateTime.MinValue;
        SyncStatus = "Never";
        SyncAttempts = 0;
    }
}