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
/// 

using System;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;

public sealed class FirebaseAuthGate : IAuthGate
{
    public async Task WaitForAuthenticatedAsync(CancellationToken ct = default)
    {
        var auth = FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser == null)
            await WaitForCurrentUserAsync(auth, ct);

        var user = auth.CurrentUser;
        if (user == null)
            throw new InvalidOperationException("Usuário não autenticado após StateChanged.");

        try
        {
            string token = await user.TokenAsync(forceRefresh: true);

            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("Token vazio retornado pelo Firebase Auth.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirebaseAuthGate] Falha ao refrescar token: {e.Message}");
            throw;
        }
    }

    private static Task WaitForCurrentUserAsync(FirebaseAuth auth, CancellationToken ct)
    {
        if (auth.CurrentUser != null)
            return Task.CompletedTask;

        if (ct.IsCancellationRequested)
            return Task.FromCanceled(ct);

        var tcs = new TaskCompletionSource<bool>(
            TaskCreationOptions.RunContinuationsAsynchronously
        );

        EventHandler handler = null;
        CancellationTokenRegistration ctr = default;

        void Cleanup()
        {
            auth.StateChanged -= handler;
            ctr.Dispose();
        }

        handler = (_, __) =>
        {
            if (auth.CurrentUser == null)
                return;

            Cleanup();
            tcs.TrySetResult(true);
        };

        if (ct.CanBeCanceled)
        {
            ctr = ct.Register(() =>
            {
                Cleanup();
                tcs.TrySetCanceled(ct);
            });
        }

        auth.StateChanged += handler;

        // Evita race condition: o usuário pode ter sido definido
        // entre a checagem inicial e a inscrição no evento.
        if (auth.CurrentUser != null)
        {
            Cleanup();
            tcs.TrySetResult(true);
        }

        return tcs.Task;
    }
}