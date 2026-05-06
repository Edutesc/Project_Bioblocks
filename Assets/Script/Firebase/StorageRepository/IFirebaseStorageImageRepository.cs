using System.Threading.Tasks;

public interface IFirebaseStorageImageRepository
{
    Task<byte[]> DownloadImageAsync(string storageKey);
    Task<bool> ExistsAsync(string storageKey);
}
