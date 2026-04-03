using System.Collections.Generic;

/// <summary>
/// Interface do serviço de banco de dados local (LiteDB).
///
/// Users  → fonte da verdade local. Leitura + escrita. Sync para Firestore quando online.
/// Rankings → cache de leitura. Firestore é a fonte da verdade.
/// </summary>
public interface ILiteDBService
{
    // ─── Users ────────────────────────────────────────────
    void        SaveUser(UserDataDB user);
    UserDataDB  GetUser(string userId);
    void        MarkUserAsSynced(string userId);
    List<UserDataDB> GetAllDirtyUsers();

    // ─── Rankings (cache) ────────────────────────────────
    void             SaveRankings(List<RankingDB> rankings);
    List<RankingDB>  GetCachedRankings();
    RankingDB        GetCachedUserRanking(string userId);
    void             ClearRankingCache();
}