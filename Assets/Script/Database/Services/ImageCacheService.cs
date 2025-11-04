using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ImageCacheService
{
    private static ImageCacheService _instance;
    public static ImageCacheService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ImageCacheService();
            }
            return _instance;
        }
    }

    private SQLite4Unity3d.SQLiteConnection _db;
    private string _cacheDirectory;
    private const long MAX_CACHE_SIZE_BYTES = 50 * 1024 * 1024;

    private ImageCacheService()
    {
        _db = DatabaseManager.Instance.GetConnection();
        _cacheDirectory = Path.Combine(Application.persistentDataPath, "ImageCache");
        
        if (!Directory.Exists(_cacheDirectory))
        {
            Directory.CreateDirectory(_cacheDirectory);
        }
    }

    public string GetCachedImagePath(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
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
                        Debug.Log($"[ImageCacheService] Cache file missing, removed from DB: {imageUrl}");
                    }
                }
                else
                {
                    DeleteCachedImage(cachedImage);
                    Debug.Log($"[ImageCacheService] Cache expired: {imageUrl}");
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

        try
        {
            string fileName = GetHashedFileName(imageUrl);
            string localPath = Path.Combine(_cacheDirectory, fileName);

            byte[] imageBytes = texture.EncodeToPNG();
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

            Debug.Log($"[ImageCacheService] Image cached: {imageUrl}");

            CleanupOldCacheIfNeeded();
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error saving image to cache: {e.Message}");
        }
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
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error loading image from cache: {e.Message}");
        }

        return null;
    }

    private void DeleteCachedImage(CachedImageEntity cachedImage)
    {
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
        try
        {
            var allCachedImages = _db.Table<CachedImageEntity>().ToList();
            long totalSize = allCachedImages.Sum(img => img.FileSizeBytes);

            if (totalSize > MAX_CACHE_SIZE_BYTES)
            {
                Debug.Log($"[ImageCacheService] Cache size exceeded ({totalSize} bytes), cleaning up...");

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
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageCacheService] Error cleaning up cache: {e.Message}");
        }
    }

    public void ClearAllCache()
    {
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
        string hash = url.GetHashCode().ToString("X");
        return $"img_{hash}.png";
    }

    public long GetTotalCacheSize()
    {
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
        try
        {
            return _db.Table<CachedImageEntity>().Count();
        }
        catch
        {
            return 0;
        }
    }
}