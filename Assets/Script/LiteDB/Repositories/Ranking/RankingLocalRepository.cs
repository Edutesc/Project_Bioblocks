using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Cache LiteDB para rankings.
///
/// O LiteDB aqui não é fonte verdade. Ele armazena uma cópia local da união entre:
///   - top usuários por Score
///   - top usuários por WeekScore
///
/// Isso é necessário porque o top 20 geral e o top 20 semanal podem conter
/// usuários diferentes.
/// </summary>
public class RankingLocalRepository : IRankingLocalRepository
{
    private const string COLLECTION_NAME = "rankings";
    private const string LAST_SYNC_TICKS_KEY = "RankingCache_LastSyncUtcTicks";

    private ILiteDBManager _db;

    public RankingLocalRepository(ILiteDBManager db)
    {
        InjectDependencies(db);
    }

    public void InjectDependencies(ILiteDBManager db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public List<Ranking> GetRankings(int limit = 20)
    {
        try
        {
            int safeLimit = NormalizeLimit(limit);

            return _db.ExecuteRead(db =>
            {
                var collection = GetCollection(db);

                return collection
                    .FindAll()
                    .OrderByDescending(r => r.Score)
                    .Take(safeLimit)
                    .Select(r => r.ToDomain())
                    .ToList();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingLocalRepository] Erro ao ler ranking geral do cache: {e.Message}");
            return new List<Ranking>();
        }
    }

    public List<Ranking> GetWeekRankings(int limit = 20)
    {
        try
        {
            int safeLimit = NormalizeLimit(limit);

            return _db.ExecuteRead(db =>
            {
                var collection = GetCollection(db);

                return collection
                    .FindAll()
                    .OrderByDescending(r => r.WeekScore)
                    .Take(safeLimit)
                    .Select(r => r.ToDomain())
                    .ToList();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingLocalRepository] Erro ao ler ranking semanal do cache: {e.Message}");
            return new List<Ranking>();
        }
    }

    /// <summary>
    /// Substitui o cache local de ranking de forma transacional.
    /// </summary>
    public void ReplaceRankings(List<Ranking> rankings)
    {
        if (rankings == null || rankings.Count == 0)
        {
            Debug.LogWarning("[RankingLocalRepository] Lista de rankings vazia — cache local não foi substituído.");
            return;
        }

        try
        {
            var docs = rankings
                .Where(r => r != null)
                .Select(RankingDB.FromDomain)
                .ToList();

            if (docs.Count == 0)
            {
                Debug.LogWarning("[RankingLocalRepository] Nenhum ranking válido para salvar.");
                return;
            }

            _db.ExecuteWrite(db =>
            {
                var collection = GetCollection(db);

                db.BeginTrans();

                try
                {
                    collection.DeleteAll();
                    collection.Upsert(docs);
                    db.Commit();
                }
                catch
                {
                    db.Rollback();
                    throw;
                }
            });

            Debug.Log($"[RankingLocalRepository] {docs.Count} registros de ranking salvos no cache local.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingLocalRepository] Erro ao substituir cache de ranking: {e.Message}");
            throw;
        }
    }

    public bool HasAnyRankings()
    {
        try
        {
            return _db.ExecuteRead(db => GetCollection(db).Count() > 0);
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingLocalRepository] Erro em HasAnyRankings: {e.Message}");
            return false;
        }
    }

    public void ClearAll()
    {
        try
        {
            _db.ExecuteWrite(db =>
            {
                int deleted = GetCollection(db).DeleteAll();
                Debug.Log($"[RankingLocalRepository] {deleted} rankings removidos do cache local.");
            });

            PlayerPrefs.DeleteKey(LAST_SYNC_TICKS_KEY);
            PlayerPrefs.Save();
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingLocalRepository] Erro em ClearAll: {e.Message}");
            throw;
        }
    }

    public DateTime GetLastSyncedAt()
    {
        string raw = PlayerPrefs.GetString(LAST_SYNC_TICKS_KEY, string.Empty);

        if (!long.TryParse(raw, out long ticks) || ticks <= 0)
            return DateTime.MinValue;

        return new DateTime(ticks, DateTimeKind.Utc);
    }

    public void SaveLastSyncedAt(DateTime syncedAtUtc)
    {
        DateTime utc = syncedAtUtc.Kind == DateTimeKind.Utc
            ? syncedAtUtc
            : syncedAtUtc.ToUniversalTime();

        PlayerPrefs.SetString(LAST_SYNC_TICKS_KEY, utc.Ticks.ToString());
        PlayerPrefs.Save();

        Debug.Log($"[RankingLocalRepository] Timestamp do cache de ranking salvo: {utc:O}.");
    }

    private static int NormalizeLimit(int limit)
    {
        return limit <= 0 ? 20 : limit;
    }

    private static LiteDB.ILiteCollection<RankingDB> GetCollection(LiteDB.LiteDatabase db)
    {
        var collection = db.GetCollection<RankingDB>(COLLECTION_NAME);

        collection.EnsureIndex(x => x.Score);
        collection.EnsureIndex(x => x.WeekScore);

        return collection;
    }
}
