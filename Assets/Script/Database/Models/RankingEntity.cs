using System;
using SQLite4Unity3d;

[Table("Rankings")]
public class RankingEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public string UserId { get; set; }

    public string UserName { get; set; }

    [Indexed]
    public int TotalScore { get; set; }

    [Indexed]
    public int WeekScore { get; set; }

    public string ProfileImageUrl { get; set; }

    public DateTime LastUpdated { get; set; }

    public bool IsSynced { get; set; }

    public RankingEntity()
    {
        LastUpdated = DateTime.UtcNow;
        IsSynced = false;
    }
}