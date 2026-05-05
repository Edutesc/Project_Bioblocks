using System.Threading;
using System.Threading.Tasks;

public interface IFirebaseStorageImageRepository
{
    Task<byte[]> DownloadImageAsync(string storageKey, CancellationToken ct);
    Task<string> GetDownloadUrlAsync(string storageKey);
    Task<bool> ExistsAsync(string storageKey);
}
