using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class ImageCacheService : MonoBehaviour, IImageCacheService
{
    private string _cacheDirectory;

    private const long MAX_CACHE_SIZE_BYTES = 50 * 1024 * 1024;
    private const int MAX_IMAGE_DIMENSION = 512;
    private const int MAX_IMAGE_BYTES = 5 * 1024 * 1024;
    private const int CACHE_EXPIRY_DAYS = 7;
    private const float CLEANUP_FRACTION = 0.25f;
    private const string CACHE_FILE_PREFIX = "cache_";

    public bool IsInitialized { get; private set; }

    public void InjectDependencies()
    {
        Initialize();
    }

    public void InjectDependencies(ILiteDBManager dbManager)
    {
        _ = dbManager;
        InjectDependencies();
    }

    private void Initialize()
    {
        if (IsInitialized) return;

        try
        {
            _cacheDirectory = Path.Combine(Application.persistentDataPath, "ImageCache");

            if (!Directory.Exists(_cacheDirectory))
                Directory.CreateDirectory(_cacheDirectory);

            IsInitialized = true;
            Debug.Log("[ImageCacheService] Inicializado com sucesso.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro ao inicializar: {e.Message}");
            IsInitialized = false;
        }
    }

    public string GetCachedImagePath(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl) || !IsInitialized) return null;

        try
        {
            string localPath = Path.Combine(_cacheDirectory, GetHashedFileName(imageUrl));

            if (!File.Exists(localPath))
                return null;

            DateTime lastWriteUtc = File.GetLastWriteTimeUtc(localPath);

            if (DateTime.UtcNow >= lastWriteUtc.AddDays(CACHE_EXPIRY_DAYS))
            {
                File.Delete(localPath);
                return null;
            }

            Debug.Log($"[ImageCacheService] Cache hit: {imageUrl}");
            return localPath;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro ao buscar cache: {e.Message}");
            return null;
        }
    }

    public void SaveImageToCache(string imageUrl, Texture2D texture,
                                 string topic = null, string sha256 = null)
    {
        if (string.IsNullOrEmpty(imageUrl) || texture == null || !IsInitialized) return;

        try
        {
            bool needsResize = texture.width > MAX_IMAGE_DIMENSION || texture.height > MAX_IMAGE_DIMENSION;
            Texture2D toSave = needsResize ? ResizeTexture(texture, MAX_IMAGE_DIMENSION, MAX_IMAGE_DIMENSION) : texture;
            byte[] imageBytes = toSave.EncodeToPNG();

            if (needsResize && toSave != texture)
                Destroy(toSave);

            SaveImageBytesToCache(imageUrl, imageBytes, topic, sha256);
        }
        catch (OutOfMemoryException)
        {
            Debug.LogError("[ImageCacheService] OutOfMemory ao salvar imagem.");
            CleanupOldCacheIfNeeded();
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro ao salvar cache: {e.Message}");
        }
    }

    public void SaveImageBytesToCache(string imageUrl, byte[] pngBytes,
                                      string topic = null, string sha256 = null)
    {
        if (string.IsNullOrEmpty(imageUrl) || pngBytes == null || pngBytes.Length == 0) return;
        if (!IsInitialized) return;

        try
        {
            if (pngBytes.Length > MAX_IMAGE_BYTES)
            {
                Debug.LogWarning($"[ImageCacheService] Imagem muito grande ({pngBytes.Length} bytes), não cacheada: {imageUrl}");
                return;
            }

            if (!IsValidPng(pngBytes))
            {
                Debug.LogWarning($"[ImageCacheService] Bytes não parecem PNG válido: {imageUrl}");
                return;
            }

            string localPath = Path.Combine(_cacheDirectory, GetHashedFileName(imageUrl));

            File.WriteAllBytes(localPath, pngBytes);
            File.SetLastWriteTimeUtc(localPath, DateTime.UtcNow);

            Debug.Log($"[ImageCacheService] Imagem cacheada (raw bytes): {imageUrl} ({pngBytes.Length} bytes, topic='{topic ?? "-"}')");
            CleanupOldCacheIfNeeded();
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro ao salvar bytes no cache para '{imageUrl}': {e.Message}");
        }
    }

    public Texture2D LoadImageFromCache(string localPath)
    {
        try
        {
            if (!File.Exists(localPath)) return null;

            byte[] imageBytes = File.ReadAllBytes(localPath);
            Texture2D texture = new Texture2D(2, 2);

            if (texture.LoadImage(imageBytes))
                return texture;

            Destroy(texture);
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro ao carregar cache: {e.Message}");
            return null;
        }
    }

    public void ClearAllCache()
    {
        if (!IsInitialized) return;

        try
        {
            FileInfo[] files = GetCacheFiles();

            foreach (var file in files)
                file.Delete();

            Debug.Log($"[ImageCacheService] Cache limpo ({files.Length} imagens removidas).");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro ao limpar cache: {e.Message}");
        }
    }

    public long GetTotalCacheSize()
    {
        if (!IsInitialized) return 0;

        try
        {
            return GetCacheFiles().Sum(x => x.Length);
        }
        catch
        {
            return 0;
        }
    }

    public int GetCachedImagesCount()
    {
        if (!IsInitialized) return 0;

        try
        {
            return GetCacheFiles().Length;
        }
        catch
        {
            return 0;
        }
    }

    private void CleanupOldCacheIfNeeded()
    {
        if (!IsInitialized) return;

        try
        {
            FileInfo[] files = GetCacheFiles();
            long totalSize = files.Sum(x => x.Length);

            if (totalSize > MAX_CACHE_SIZE_BYTES)
            {
                var toDelete = files
                    .OrderBy(x => x.LastWriteTimeUtc)
                    .Take(Mathf.Max(1, Mathf.RoundToInt(files.Length * CLEANUP_FRACTION)))
                    .ToList();

                foreach (var file in toDelete)
                    file.Delete();

                Debug.Log($"[ImageCacheService] Cleanup: {toDelete.Count} imagens removidas.");
            }

            DateTime expiryLimit = DateTime.UtcNow.AddDays(-CACHE_EXPIRY_DAYS);

            foreach (var expired in files.Where(x => x.LastWriteTimeUtc <= expiryLimit))
                expired.Delete();
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro no cleanup: {e.Message}");
        }
    }

    private FileInfo[] GetCacheFiles()
    {
        if (!Directory.Exists(_cacheDirectory))
            return Array.Empty<FileInfo>();

        var directory = new DirectoryInfo(_cacheDirectory);
        return directory.GetFiles($"{CACHE_FILE_PREFIX}*.png");
    }

    private Texture2D ResizeTexture(Texture2D source, int maxWidth, int maxHeight)
    {
        float ratio = Mathf.Min((float)maxWidth / source.width, (float)maxHeight / source.height);
        if (ratio >= 1f) return source;

        int newWidth = Mathf.RoundToInt(source.width * ratio);
        int newHeight = Mathf.RoundToInt(source.height * ratio);

        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
        rt.filterMode = FilterMode.Bilinear;
        RenderTexture.active = rt;
        Graphics.Blit(source, rt);

        Texture2D result = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);
        result.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
        result.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        return result;
    }

    private static bool IsValidPng(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 8) return false;

        return bytes[0] == 0x89 &&
               bytes[1] == 0x50 &&
               bytes[2] == 0x4E &&
               bytes[3] == 0x47 &&
               bytes[4] == 0x0D &&
               bytes[5] == 0x0A &&
               bytes[6] == 0x1A &&
               bytes[7] == 0x0A;
    }

    private static string GetHashedFileName(string value)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            var sb = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return $"{CACHE_FILE_PREFIX}{sb}.png";
        }
    }
}
