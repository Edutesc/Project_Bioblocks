using System;

public class RankingDTO
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int TotalScore { get; set; }
    public int WeekScore { get; set; }
    public string ProfileImageUrl { get; set; }

    public static RankingEntity ToEntity(Ranking ranking, string userId)
    {
        return new RankingEntity
        {
            UserId = userId,
            UserName = ranking.userName,
            TotalScore = ranking.userScore,
            WeekScore = ranking.userWeekScore,
            ProfileImageUrl = ranking.profileImageUrl,
            LastUpdated = DateTime.UtcNow,
            IsSynced = true
        };
    }

    public static Ranking ToRanking(RankingEntity entity)
    {
        return new Ranking(
            entity.UserName,
            entity.TotalScore,
            entity.WeekScore,
            entity.ProfileImageUrl ?? ""
        );
    }

    public static RankingEntity FromUserData(UserData userData)
    {
        return new RankingEntity
        {
            UserId = userData.UserId,
            UserName = userData.NickName,
            TotalScore = userData.Score,
            WeekScore = userData.WeekScore,
            ProfileImageUrl = userData.ProfileImageUrl,
            LastUpdated = DateTime.UtcNow,
            IsSynced = true
        };
    }
}