using System.Collections.Generic;

public interface ILocalRankingRepository
{
    void SaveRankings(List<RankingEntity> rankings);
    List<RankingEntity> GetAllRankings();
    List<RankingEntity> GetTop20Rankings();
    RankingEntity GetRankingByUserId(string userId);
    int GetUserRankPosition(string userId);
    void UpsertRanking(RankingEntity ranking);
    void DeleteAllRankings();
    int GetRankingsCount();
}