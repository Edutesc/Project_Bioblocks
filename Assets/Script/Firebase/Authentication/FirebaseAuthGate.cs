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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public sealed class FirebaseAuthGate : IAuthGate
{
    private readonly SemaphoreSlim _validationGate = new SemaphoreSlim(1, 1);

    public async Task WaitForAuthenticatedAsync(CancellationToken ct = default)
    {
        var auth = FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser == null)
            await WaitForCurrentUserAsync(auth, ct);

        await _validationGate.WaitAsync(ct);

        try
        {
            var user = auth.CurrentUser;
            if (user == null)
                throw new InvalidOperationException("Usuário não autenticado após StateChanged.");

            // O SDK devolve o token em cache quando ele ainda é válido e o
            // renova automaticamente quando necessário. Forçar refresh em toda
            // operação torna o app dependente de rede mesmo com token válido.
            string token = await user.TokenAsync(forceRefresh: false);

            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("Token vazio retornado pelo Firebase Auth.");

            ValidateTokenIdentity(token, user);

        }
        catch (Exception e)
        {
            Debug.LogWarning($"[FirebaseAuthGate] Sessão remota indisponível: {e.Message}");
            throw;
        }
        finally
        {
            _validationGate.Release();
        }
    }

    private static void ValidateTokenIdentity(string token, FirebaseUser user)
    {
        string payload = DecodeJwtPayload(token);
        string audience = ExtractJsonString(payload, "aud");
        string subject = ExtractJsonString(payload, "sub");
        string projectId = FirebaseApp.DefaultInstance.Options.ProjectId;

        if (!string.Equals(audience, projectId, StringComparison.Ordinal))
            throw new FirebaseEnvironmentMismatchException(
                $"ID token pertence ao projeto '{audience}', mas o app usa '{projectId}'."
            );

        if (!string.Equals(subject, user.UserId, StringComparison.Ordinal))
            throw new InvalidOperationException("UID do ID token diverge do FirebaseAuth.CurrentUser.");

        Debug.Log($"[FirebaseAuthGate] Identidade consistente: project={projectId}, uid={user.UserId}.");
    }

    private static string DecodeJwtPayload(string token)
    {
        string[] parts = token.Split('.');
        if (parts.Length < 2)
            throw new InvalidOperationException("ID token não possui formato JWT.");

        string base64 = parts[1].Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
    }

    private static string ExtractJsonString(string json, string key)
    {
        string marker = $"\"{key}\"";
        int keyIndex = json.IndexOf(marker, StringComparison.Ordinal);
        if (keyIndex < 0) return null;

        int colon = json.IndexOf(':', keyIndex + marker.Length);
        int firstQuote = colon < 0 ? -1 : json.IndexOf('"', colon + 1);
        int lastQuote = firstQuote < 0 ? -1 : json.IndexOf('"', firstQuote + 1);

        return firstQuote >= 0 && lastQuote > firstQuote
            ? json.Substring(firstQuote + 1, lastQuote - firstQuote - 1)
            : null;
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

public sealed class FirebaseEnvironmentMismatchException : InvalidOperationException
{
    public FirebaseEnvironmentMismatchException(string message) : base(message) { }
}
