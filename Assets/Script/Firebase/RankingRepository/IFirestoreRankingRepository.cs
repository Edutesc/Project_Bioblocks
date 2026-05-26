using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Repositório remoto de ranking.
///
/// Implementações desta interface devem consultar a fonte verdade do ranking.
/// No fluxo atual do BioBlocks, essa fonte verdade é o Firestore.
/// </summary>
public interface IFirestoreRankingRepository
{
    Task<List<Ranking>> GetRankingsAsync(int limit = 20);

}
