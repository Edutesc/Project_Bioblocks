using System;
using System.Collections.Generic;

/// <summary>
/// Cache local de ranking.
///
/// Importante: este repositório NÃO é fonte verdade.
/// Ele existe apenas para fallback offline e para evitar consultas remotas
/// repetidas em janelas curtas de tempo.
/// </summary>
public interface IRankingLocalRepository
{
    List<Ranking> GetRankings(int limit = 20);

    List<Ranking> GetWeekRankings(int limit = 20);

    void ReplaceRankings(List<Ranking> rankings);

    bool HasAnyRankings();

    void ClearAll();

    DateTime GetLastSyncedAt();

    void SaveLastSyncedAt(DateTime syncedAtUtc);
}
