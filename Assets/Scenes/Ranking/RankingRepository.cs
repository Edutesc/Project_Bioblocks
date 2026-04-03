using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

/// <summary>
/// Repositório de Rankings com suporte offline-first.
///
/// Online  → busca do Firestore → salva cache no LiteDB → retorna dados
/// Offline → lê cache do LiteDB → retorna dados (pode estar desatualizado)
/// </summary>
public class RankingRepository : IRankingRepository
{
    private FirebaseFirestore _db   => FirebaseFirestore.DefaultInstance;
    private IAuthRepository   _auth => AppContext.Auth;
    private ILiteDBService    _localDB => AppContext.LocalDB;

    // ─────────────────────────────────────────────────────
    // IRankingRepository
    // ─────────────────────────────────────────────────────

    public async Task<Ranking> GetCurrentUserRankingAsync()
    {
        if (!_auth.IsUserLoggedIn())
        {
            Debug.LogError("[RankingRepository] Usuário não autenticado.");
            return null;
        }

        string userId = _auth.CurrentUserId;

        if (ConnectivityMonitor.Instance.IsOnline)
        {
            try
            {
                DocumentSnapshot snap = await _db
                    .Collection("Rankings")
                    .Document(userId)
                    .GetSnapshotAsync();

                if (!snap.Exists)
                {
                    Debug.LogWarning($"[RankingRepository] Rankings/{userId} não encontrado no Firestore.");
                    return GetCachedUserRanking(userId);
                }

                Ranking ranking = snap.ConvertTo<Ranking>();
                ranking.UserId  = userId;
                return ranking;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[RankingRepository] Firestore falhou, usando cache: {e.Message}");
                return GetCachedUserRanking(userId);
            }
        }
        else
        {
            Debug.Log("[RankingRepository] Offline — carregando ranking do usuário do cache.");
            return GetCachedUserRanking(userId);
        }
    }

    public async Task<List<Ranking>> GetRankingsAsync(int limit = 50)
    {
        if (ConnectivityMonitor.Instance.IsOnline)
        {
            try
            {
                QuerySnapshot snap = await _db
                    .Collection("Rankings")
                    .OrderByDescending("score")
                    .Limit(limit)
                    .GetSnapshotAsync();

                var rankings = ToRankingList(snap);
                SaveToCache(rankings);
                return rankings;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[RankingRepository] Firestore falhou, usando cache: {e.Message}");
                return GetCachedRankings();
            }
        }
        else
        {
            Debug.Log("[RankingRepository] Offline — carregando rankings do cache.");
            return GetCachedRankings();
        }
    }

    public async Task<List<Ranking>> GetWeekRankingsAsync(int limit = 50)
    {
        if (ConnectivityMonitor.Instance.IsOnline)
        {
            try
            {
                QuerySnapshot snap = await _db
                    .Collection("Rankings")
                    .OrderByDescending("weekScore")
                    .Limit(limit)
                    .GetSnapshotAsync();

                var rankings = ToRankingList(snap);
                SaveToCache(rankings);
                return rankings;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[RankingRepository] Firestore falhou, usando cache semanal: {e.Message}");
                return GetCachedRankings(weekScore: true);
            }
        }
        else
        {
            Debug.Log("[RankingRepository] Offline — carregando week rankings do cache.");
            return GetCachedRankings(weekScore: true);
        }
    }

    // ─────────────────────────────────────────────────────
    // Cache helpers
    // ─────────────────────────────────────────────────────

    private void SaveToCache(List<Ranking> rankings)
    {
        var rankingDBs = rankings.Select(r => new RankingDB
        {
            UserId        = r.UserId,
            UserName      = r.userName,
            ProfileImageUrl = r.profileImageUrl,
            Score         = r.userScore,
            WeekScore     = r.userWeekScore,
        }).ToList();

        _localDB.SaveRankings(rankingDBs);
    }

    private List<Ranking> GetCachedRankings(bool weekScore = false)
    {
        var cached = _localDB.GetCachedRankings();

        if (cached == null || cached.Count == 0)
        {
            Debug.LogWarning("[RankingRepository] Cache de rankings vazio.");
            return new List<Ranking>();
        }

        var rankings = cached.Select(r => new Ranking
        {
            UserId          = r.UserId,
            userName        = r.UserName,
            profileImageUrl = r.ProfileImageUrl,
            userScore       = r.Score,
            userWeekScore   = r.WeekScore,
        }).ToList();

        return weekScore
            ? rankings.OrderByDescending(r => r.userWeekScore).ToList()
            : rankings.OrderByDescending(r => r.userScore).ToList();
    }

    private Ranking GetCachedUserRanking(string userId)
    {
        var cached = _localDB.GetCachedUserRanking(userId);
        if (cached == null) return null;

        return new Ranking
        {
            UserId          = cached.UserId,
            userName        = cached.UserName,
            profileImageUrl = cached.ProfileImageUrl,
            userScore       = cached.Score,
            userWeekScore   = cached.WeekScore,
        };
    }

    // ─────────────────────────────────────────────────────
    // Helper
    // ─────────────────────────────────────────────────────

    private List<Ranking> ToRankingList(QuerySnapshot snap)
    {
        var result = new List<Ranking>(snap.Count);
        foreach (DocumentSnapshot doc in snap.Documents)
        {
            try
            {
                Ranking ranking = doc.ConvertTo<Ranking>();
                ranking.UserId  = doc.Id;
                result.Add(ranking);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[RankingRepository] Doc {doc.Id} inválido, ignorando: {e.Message}");
            }
        }
        return result;
    }
}