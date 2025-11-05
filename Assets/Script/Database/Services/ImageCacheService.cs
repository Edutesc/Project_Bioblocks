using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ImageCacheService : MonoBehaviour
{
    private static ImageCacheService _instance;
    public static ImageCacheService Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ImageCacheService");
                _instance = go.AddComponent<ImageCacheService>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private SQLite4Unity3d.SQLiteConnection _db;
    private string _cacheDirectory;
    private const long MAX_CACHE_SIZE_BYTES = 50 * 1024 * 1024;
    private bool _isInitialized = false;
    private bool _isInitializing = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(InitializeCoroutine());
    }

    private IEnumerator InitializeCoroutine()
    {
        if (_isInitializing || _isInitialized)
            yield break;

        _isInitializing = true;

        int maxRetries = 50;
        int retryCount = 0;

        while (retryCount < maxRetries)
        {
            if (DatabaseManager.Instance != null && DatabaseManager.Instance.IsInitialized)
            {
                _db = DatabaseManager.Instance.GetConnection();

                if (_db != null)
                {
                    Debug.Log($"[ImageCacheService] Banco conectado após {retryCount} tentativas");
                    break;
                }
            }

            retryCount++;
            yield return new WaitForSeconds(0.1f);
        }

        if (_db == null)
        {
            Debug.LogWarning("[ImageCacheService] Banco não disponível após timeout, serviço rodará sem cache");
            _isInitializing = false;
            yield break;
        }

        try
        {
            _cacheDirectory = Path.Combine(Application.persistentDataPath, "ImageCache");

            if (!Directory.Exists(_cacheDirectory))
            {
                Directory.CreateDirectory(_cacheDirectory);
            }

            try
            {
                _db.CreateTable<CachedImageEntity>();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[ImageCacheService] Tabela já existe: {ex.Message}");
            }

            _isInitialized = true;
            Debug.Log("[ImageCacheService] Inicializado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Erro ao inicializar: {e.Message}");
            _isInitialized = false;
        }
        finally
        {
            _isInitializing = false;
        }
    }

    private bool EnsureInitialized()
    {
        return _isInitialized && _db != null;
    }

    public string GetCachedImagePath(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            return null;
        }

        if (!EnsureInitialized())
        {
            return null;
        }

        try
        {
            var cachedImage = _db.Table<CachedImageEntity>()
                                .Where(img => img.ImageUrl == imageUrl)
                                .FirstOrDefault();

            if (cachedImage != null)
            {
                if (DateTime.UtcNow < cachedImage.ExpiresAt)
                {
                    if (File.Exists(cachedImage.LocalPath))
                    {
                        Debug.Log($"[ImageCacheService] Cache hit: {imageUrl}");
                        return cachedImage.LocalPath;
                    }
                    else
                    {
                        _db.Delete(cachedImage);
                    }
                }
                else
                {
                    DeleteCachedImage(cachedImage);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error getting cached image: {e.Message}");
        }

        return null;
    }

    public void SaveImageToCache(string imageUrl, Texture2D texture)
    {
        if (string.IsNullOrEmpty(imageUrl) || texture == null)
        {
            return;
        }

        if (!EnsureInitialized())
        {
            return;
        }

        try
        {
            string fileName = GetHashedFileName(imageUrl);
            string localPath = Path.Combine(_cacheDirectory, fileName);

            Texture2D textureToSave = texture;
            bool needsResize = texture.width > 512 || texture.height > 512;

            if (needsResize)
            {
                textureToSave = ResizeTexture(texture, 512, 512);
            }

            byte[] imageBytes = textureToSave.EncodeToPNG();

            if (needsResize && textureToSave != texture)
            {
                Destroy(textureToSave);
            }

            if (imageBytes.Length > 5 * 1024 * 1024)
            {
                Debug.LogWarning($"[ImageCacheService] Imagem muito grande, não será cacheada: {imageUrl}");
                return;
            }

            File.WriteAllBytes(localPath, imageBytes);

            var cachedImage = new CachedImageEntity
            {
                ImageUrl = imageUrl,
                LocalPath = localPath,
                CachedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                FileSizeBytes = imageBytes.Length
            };

            _db.InsertOrReplace(cachedImage);

            Debug.Log($"[ImageCacheService] Image cached: {imageUrl} ({imageBytes.Length} bytes)");

            CleanupOldCacheIfNeeded();
        }
        catch (OutOfMemoryException)
        {
            Debug.LogError($"[ImageCacheService] Out of memory ao salvar imagem");
            CleanupOldCacheIfNeeded();
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error saving image: {e.Message}");
        }
    }

    private Texture2D ResizeTexture(Texture2D source, int maxWidth, int maxHeight)
    {
        float ratio = Mathf.Min((float)maxWidth / source.width, (float)maxHeight / source.height);

        if (ratio >= 1f)
        {
            return source;
        }

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

    public Texture2D LoadImageFromCache(string localPath)
    {
        try
        {
            if (File.Exists(localPath))
            {
                byte[] imageBytes = File.ReadAllBytes(localPath);
                Texture2D texture = new Texture2D(2, 2);

                if (texture.LoadImage(imageBytes))
                {
                    return texture;
                }
                else
                {
                    Destroy(texture);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error loading from cache: {e.Message}");
        }

        return null;
    }

    private void DeleteCachedImage(CachedImageEntity cachedImage)
    {
        if (!EnsureInitialized())
        {
            return;
        }

        try
        {
            if (File.Exists(cachedImage.LocalPath))
            {
                File.Delete(cachedImage.LocalPath);
            }

            _db.Delete(cachedImage);
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error deleting cached image: {e.Message}");
        }
    }

    private void CleanupOldCacheIfNeeded()
    {
        if (!EnsureInitialized())
        {
            return;
        }

        try
        {
            var allCachedImages = _db.Table<CachedImageEntity>().ToList();
            long totalSize = allCachedImages.Sum(img => img.FileSizeBytes);

            if (totalSize > MAX_CACHE_SIZE_BYTES)
            {
                var imagesToDelete = allCachedImages
                    .OrderBy(img => img.CachedAt)
                    .Take(allCachedImages.Count / 4)
                    .ToList();

                foreach (var image in imagesToDelete)
                {
                    DeleteCachedImage(image);
                }

                Debug.Log($"[ImageCacheService] Deleted {imagesToDelete.Count} old images");
            }

            var expiredImages = allCachedImages
                .Where(img => DateTime.UtcNow >= img.ExpiresAt)
                .ToList();

            if (expiredImages.Count > 0)
            {
                foreach (var image in expiredImages)
                {
                    DeleteCachedImage(image);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error cleaning cache: {e.Message}");
        }
    }

    public void ClearAllCache()
    {
        if (!EnsureInitialized())
        {
            return;
        }

        try
        {
            var allCachedImages = _db.Table<CachedImageEntity>().ToList();

            foreach (var image in allCachedImages)
            {
                DeleteCachedImage(image);
            }

            Debug.Log($"[ImageCacheService] Cleared all cache ({allCachedImages.Count} images)");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error clearing cache: {e.Message}");
        }
    }

    private string GetHashedFileName(string url)
    {
        int hash = url.GetHashCode();
        string hashString = Math.Abs(hash).ToString("X8");
        return $"img_{hashString}.png";
    }

    public long GetTotalCacheSize()
    {
        if (!EnsureInitialized())
        {
            return 0;
        }

        try
        {
            var allCachedImages = _db.Table<CachedImageEntity>().ToList();
            return allCachedImages.Sum(img => img.FileSizeBytes);
        }
        catch
        {
            return 0;
        }
    }

    public int GetCachedImagesCount()
    {
        if (!EnsureInitialized())
        {
            return 0;
        }

        try
        {
            return _db.Table<CachedImageEntity>().Count();
        }
        catch
        {
            return 0;
        }
    }

    public bool IsInitialized => _isInitialized;
}