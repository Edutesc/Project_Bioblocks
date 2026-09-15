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
    /// Retorna quando houver usuário e um ID token utilizável. O token em cache
    /// pode ser usado; o SDK o renova quando estiver expirado. Cancela a espera
    /// por CurrentUser via CancellationToken.
    /// </summary>
    Task WaitForAuthenticatedAsync(CancellationToken ct = default);
}
