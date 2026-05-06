using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Fake do IFirebaseStorageImageRepository para testes unitários.
///
/// Registra a ordem em que DownloadImageAsync foi chamado (DownloadOrder),
/// permitindo asserts sobre a ordem de prewarm por topic.
/// Por padrão devolve um PNG mínimo (1x1 px) para qualquer key.
/// </summary>
public class FakeFirebaseStorageImageRepository : IFirebaseStorageImageRepository
{
    public List<string> DownloadOrder { get; } = new List<string>();
    public bool ShouldFail { get; set; } = false;
    public ConcurrentDictionary<string, int> DownloadCount { get; } = new ConcurrentDictionary<string, int>();

    // PNG mínimo (1x1 transparente) — apenas o header é o suficiente para que o
    // ImageLocalRepository considere bytes válidos no fluxo de teste, mas em
    // testes que não dependem de decodificação Texture2D ele basta.
    private static readonly byte[] DummyPng = new byte[]
    {
        0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A,
        0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
        0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
        0x08, 0x06, 0x00, 0x00, 0x00, 0x1F, 0x15, 0xC4,
        0x89, 0x00, 0x00, 0x00, 0x0A, 0x49, 0x44, 0x41,
        0x54, 0x78, 0x9C, 0x63, 0x00, 0x01, 0x00, 0x00,
        0x05, 0x00, 0x01, 0x0D, 0x0A, 0x2D, 0xB4, 0x00,
        0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE,
        0x42, 0x60, 0x82
    };

    public Task<byte[]> DownloadImageAsync(string storageKey)
    {
        lock (DownloadOrder) { DownloadOrder.Add(storageKey); }
        DownloadCount.AddOrUpdate(storageKey, 1, (_, c) => c + 1);

        if (ShouldFail) return Task.FromResult<byte[]>(null);
        return Task.FromResult(DummyPng);
    }

    public Task<bool> ExistsAsync(string storageKey) => Task.FromResult(true);

    public void Reset()
    {
        DownloadOrder.Clear();
        DownloadCount.Clear();
        ShouldFail = false;
    }
}
