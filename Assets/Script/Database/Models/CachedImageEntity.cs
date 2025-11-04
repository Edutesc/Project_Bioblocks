using System;
using SQLite4Unity3d;

[Table("CachedImages")]
public class CachedImageEntity
{
    [PrimaryKey]
    public string ImageUrl { get; set; }

    public string LocalPath { get; set; }

    public DateTime CachedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public long FileSizeBytes { get; set; }

    public CachedImageEntity()
    {
        CachedAt = DateTime.UtcNow;
        ExpiresAt = DateTime.UtcNow.AddDays(7);
    }
}
