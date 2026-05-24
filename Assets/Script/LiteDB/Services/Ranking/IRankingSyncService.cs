using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Serviço de sincronização do ranking.
///
/// Fonte verdade:
///   - Firestore
///
/// Cache:
///   - LiteDB, apenas para fallback offline e para evitar leituras remotas
///     repetidas em intervalos curtos.
/// </summary>
public interface IRankingSyncService
{
    bool IsSyncing { get; }

    /// <summary>
    /// Retorna imediatamente o ranking salvo no LiteDB.
    /// Não acessa o Firestore.
    /// Útil para popular a tela sem flash enquanto o refresh remoto acontece.
    /// </summary>
    List<Ranking> GetCachedRankings(int limit = 20);

    /// <summary>
    /// Retorna o ranking geral com fallback offline.
    ///
    /// Online:
    ///   - se o cache local estiver válido, retorna LiteDB e atualiza em background;
    ///   - se o cache estiver vazio ou stale, busca do Firestore e atualiza LiteDB.
    ///
    /// Offline:
    ///   - retorna LiteDB.
    /// </summary>
    Task<List<Ranking>> GetRankingsWithFallback(int limit = 20);

    /// <summary>
    /// Força atualização do ranking geral a partir do Firestore.
    /// </summary>
    Task<bool> ForceRefresh(int limit = 20);

    /// <summary>
    /// Retorna o timestamp do último sync remoto bem-sucedido.
    /// </summary>
    DateTime GetLastSyncedAt();
}
