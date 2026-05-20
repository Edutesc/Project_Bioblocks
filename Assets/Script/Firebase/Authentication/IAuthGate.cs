using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Espera até que haja um usuário autenticado no Firebase Auth.
///
/// Usado por serviços que precisam de auth válida antes de tocar APIs
/// protegidas (Storage, Firestore com rules baseadas em auth.uid).
/// Em testes, basta não injetar a implementação real (= sem espera).
/// </summary>
public interface IAuthGate
{
    /// <summary>
    /// Retorna assim que houver usuário autenticado. Se já houver, retorna
    /// imediatamente (Task completed). Cancela via CancellationToken.
    /// </summary>
    Task WaitForAuthenticatedAsync(CancellationToken ct = default);
}
