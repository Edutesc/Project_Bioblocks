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
        _liteDb = liteDb;
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
            {
                Debug.Log($"[ImageLocalRepository] Cache não encontrado para '{storageKey}'.");
                return false;
            }

            texture = _imageCache.LoadImageFromCache(cachedPath);

            if (texture != null)
            {
                Debug.Log($"[ImageLocalRepository] Imagem '{storageKey}' carregada do cache.");
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

    public void Save(string storageKey, byte[] pngBytes, string databankName)
    {
        if (string.IsNullOrEmpty(storageKey) || pngBytes == null || pngBytes.Length == 0)
        {
            Debug.LogWarning("[ImageLocalRepository] storageKey ou pngBytes vazio — nada salvo.");
            return;
        }

        try
        {
            // Decodifica bytes para Texture2D
            Texture2D texture = new Texture2D(2, 2);
            if (!texture.LoadImage(pngBytes))
            {
                Debug.LogError($"[ImageLocalRepository] Falha ao decodificar PNG para '{storageKey}'.");
                Destroy(texture);
                return;
            }

            // Salva no cache físico
            _imageCache.SaveImageToCache(storageKey, texture);
            Destroy(texture);

            // Computa SHA256 dos bytes
            string sha256 = ComputeSha256(pngBytes);

            // Atualiza documento CachedImageDB com DatabankName e Sha256
            var cached = _liteDb.CachedImages.FindById(storageKey);
            if (cached != null)
            {
                cached.DatabankName = databankName;
                cached.Sha256 = sha256;
                _liteDb.CachedImages.Update(cached);
                Debug.Log($"[ImageLocalRepository] Imagem '{storageKey}' atualizada com DatabankName='{databankName}' e Sha256.");
            }
            else
            {
                Debug.LogWarning($"[ImageLocalRepository] Documento CachedImageDB não encontrado para '{storageKey}' após SaveImageToCache.");
            }
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

    public DateTime? GetLatestCacheTimestamp(string databankName)
    {
        try
        {
            var latest = _liteDb.CachedImages.FindAll()
                                .Where(x => x.DatabankName == databankName)
                                .OrderByDescending(x => x.CachedAt)
                                .FirstOrDefault();

            return latest?.CachedAt;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao obter timestamp de cache para '{databankName}': {e.Message}");
            return null;
        }
    }

    // ── Limpeza ────────────────────────────────────────────────────────────────

    public void EvictByDatabank(string databankName)
    {
        try
        {
            var toDelete = _liteDb.CachedImages.FindAll()
                                  .Where(x => x.DatabankName == databankName)
                                  .ToList();

            foreach (var cached in toDelete)
            {
                // Remove arquivo físico
                try
                {
                    if (System.IO.File.Exists(cached.LocalPath))
                        System.IO.File.Delete(cached.LocalPath);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"[ImageLocalRepository] Erro ao deletar arquivo físico '{cached.LocalPath}': {e.Message}");
                }

                // Remove documento LiteDB
                _liteDb.CachedImages.Delete(cached.ImageUrl);
            }

            Debug.Log($"[ImageLocalRepository] {toDelete.Count} imagens removidas para databank '{databankName}'.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao fazer evict por databank '{databankName}': {e.Message}");
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

    private string ComputeSha256(byte[] data)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(data);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }
    }
}
