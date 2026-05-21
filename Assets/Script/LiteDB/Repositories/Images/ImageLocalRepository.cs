using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class ImageLocalRepository : MonoBehaviour, IImageLocalRepository
{
    private ILiteDBManager _liteDb;
    private IImageCacheService _imageCache;

    public void InjectDependencies(ILiteDBManager liteDb, IImageCacheService imageCache)
    {
        _liteDb     = liteDb;
        _imageCache = imageCache;
    }

    // ── Leitura ────────────────────────────────────────────────────────────────

    public bool TryGetCachedTexture(string storageKey, out Texture2D texture)
    {
        texture = null;

        try
        {
            string cachedPath = _imageCache.GetCachedImagePath(storageKey);

            if (string.IsNullOrEmpty(cachedPath))
                return false;

            texture = _imageCache.LoadImageFromCache(cachedPath);
            if (texture != null)
            {
                Debug.Log($"[ImageLocalRepository] Cache hit '{storageKey}'.");
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao obter texture em cache para '{storageKey}': {e.Message}");
            return false;
        }
    }

    // ── Escrita ────────────────────────────────────────────────────────────────

    public void Save(string storageKey, byte[] pngBytes, string topic)
    {
        if (string.IsNullOrEmpty(storageKey) || pngBytes == null || pngBytes.Length == 0)
        {
            Debug.LogWarning("[ImageLocalRepository] storageKey ou pngBytes vazio — nada salvo.");
            return;
        }

        try
        {
            // Salva os bytes PNG direto no cache, sem decodificar pra Texture2D.
            // Isso permite que o Save seja chamado de qualquer thread (background
            // downloads do prewarm). A conversão pra Texture2D acontece só no
            // TryGetCachedTexture, que é sempre chamado da main thread.
            string sha256 = ComputeSha256(pngBytes);
            _imageCache.SaveImageBytesToCache(storageKey, pngBytes, topic, sha256);

            Debug.Log($"[ImageLocalRepository] Imagem '{storageKey}' salva (topic='{topic ?? "-"}', {pngBytes.Length} bytes).");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao salvar imagem '{storageKey}': {e.Message}");
            throw;
        }
    }

    // ── Verificação ────────────────────────────────────────────────────────────

    public bool Has(string storageKey)
    {
        if (string.IsNullOrEmpty(storageKey)) return false;
        try
        {
            return _liteDb.CachedImages.Exists(x => x.ImageUrl == storageKey);
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao verificar existência de '{storageKey}': {e.Message}");
            return false;
        }
    }

    // ── Metadados de cache ─────────────────────────────────────────────────────

    public DateTime? GetLatestCacheTimestamp(string topic = null)
    {
        try
        {
            var query = _liteDb.CachedImages.FindAll();
            if (!string.IsNullOrEmpty(topic))
                query = query.Where(x => x.Topic == topic);

            var latest = query.OrderByDescending(x => x.CachedAt).FirstOrDefault();
            return latest?.CachedAt;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao obter timestamp de cache para '{topic}': {e.Message}");
            return null;
        }
    }

    // ── Limpeza ────────────────────────────────────────────────────────────────

    public void EvictByTopic(string topic)
    {
        if (string.IsNullOrEmpty(topic))
        {
            Debug.LogWarning("[ImageLocalRepository] EvictByTopic chamado sem topic — ignorado.");
            return;
        }

        try
        {
            var toDelete = _liteDb.CachedImages.FindAll()
                                  .Where(x => x.Topic == topic)
                                  .ToList();

            foreach (var cached in toDelete)
            {
                try
                {
                    if (System.IO.File.Exists(cached.LocalPath))
                        System.IO.File.Delete(cached.LocalPath);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"[ImageLocalRepository] Erro ao deletar arquivo físico '{cached.LocalPath}': {e.Message}");
                }

                _liteDb.CachedImages.Delete(cached.ImageUrl);
            }

            Debug.Log($"[ImageLocalRepository] {toDelete.Count} imagens removidas para topic '{topic}'.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao fazer evict por topic '{topic}': {e.Message}");
            throw;
        }
    }

    public void ClearAll()
    {
        try
        {
            _imageCache.ClearAllCache();
            Debug.Log("[ImageLocalRepository] Cache de imagens totalmente limpo.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao limpar todo o cache: {e.Message}");
            throw;
        }
    }

    // ── Utilitários ────────────────────────────────────────────────────────────

    private static string ComputeSha256(byte[] data)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(data);
            var sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
