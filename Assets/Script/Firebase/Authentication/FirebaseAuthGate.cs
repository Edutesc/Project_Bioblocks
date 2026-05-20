using System;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;

/// <summary>
/// Implementação de IAuthGate baseada em Firebase Auth.
///
/// Garante duas coisas antes de resolver:
///   1. FirebaseAuth.CurrentUser != null
///   2. Existe um ID token válido (TokenAsync retorna não-vazio)
///
/// Sem (2) é possível ter um race entre sign-in e Storage SDK em que o
/// CurrentUser é populado mas o token ainda não foi propagado, e o Storage
/// retorna "User is not authenticated".
/// </summary>
public sealed class FirebaseAuthGate : IAuthGate
{
    public async Task WaitForAuthenticatedAsync(CancellationToken ct = default)
    {
        var auth = FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser == null)
            await WaitForCurrentUserAsync(ct).ConfigureAwait(false);

        try
        {
            string token = await auth.CurrentUser.TokenAsync(forceRefresh: true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("Token vazio retornado pelo Firebase Auth.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirebaseAuthGate] Falha ao refrescar token: {e.Message}");
            throw;
        }
    }

    private static Task WaitForCurrentUserAsync(CancellationToken ct)
    {
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        EventHandler handler = null;

        handler = (sender, _) =>
        {
            var current = (sender as FirebaseAuth) ?? FirebaseAuth.DefaultInstance;
            if (current.CurrentUser != null)
            {
                FirebaseAuth.DefaultInstance.StateChanged -= handler;
                tcs.TrySetResult(true);
            }
        };

        FirebaseAuth.DefaultInstance.StateChanged += handler;

        if (ct.CanBeCanceled)
        {
            ct.Register(() =>
            {
                FirebaseAuth.DefaultInstance.StateChanged -= handler;
                tcs.TrySetCanceled(ct);
            });
        }

        return tcs.Task;
    }
}
