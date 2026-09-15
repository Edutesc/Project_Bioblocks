using System;
using System.Threading;
using System.Threading.Tasks;

public sealed class FakeAuthGate : IAuthGate
{
    public int CallCount { get; private set; }
    public bool ShouldThrow { get; set; }
    public Exception ExceptionToThrow { get; set; } =
        new InvalidOperationException("Sessao remota indisponivel.");

    public Task WaitForAuthenticatedAsync(CancellationToken ct = default)
    {
        CallCount++;

        if (ct.IsCancellationRequested)
            return Task.FromCanceled(ct);

        if (ShouldThrow)
            return Task.FromException(ExceptionToThrow);

        return Task.CompletedTask;
    }
}
