using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ImageLocalRepository : MonoBehaviour, IImageLocalRepository
{
    private IImageCacheService _imageCache;

    private readonly SemaphoreSlim _manifestGate = new SemaphoreSlim(1, 1);

    private string _cacheDirectory;
    private string _manifestPath;
    private bool _missingManifestLogged;

    private const long MAX_CACHE_SIZE_BYTES = 50 * 1024 * 1024;
    private const int MAX_IMAGE_BYTES = 5 * 1024 * 1024;
    private const int CACHE_EXPIRY_DAYS = 7;
    private const float CLEANUP_FRACTION = 0.25f;

    public void InjectDependencies(IImageCacheService imageCache)
    {
        _imageCache = imageCache;

        _cacheDirectory = Path.Combine(Application.persistentDataPath, "ImageCache");
        _manifestPath = Path.Combine(_cacheDirectory, "manifest.json");

        if (!Directory.Exists(_cacheDirectory))
            Directory.CreateDirectory(_cacheDirectory);
    }

    // ── Leitura ────────────────────────────────────────────────────────────────

    public async Task<Texture2D> GetCachedTextureAsync(
        string storageKey,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(storageKey))
            return null;

        try
        {
            ImageCacheManifestEntry cached = await GetEntryAsync(storageKey, ct);

            if (cached == null)
                return null;

            if (!IsEntryValid(cached))
            {
                await DeleteCachedImageAsync(cached, ct);
                return null;
            }

            Texture2D texture = _imageCache.LoadImageFromCache(cached.localPath);

            if (texture != null)
            {
                Debug.Log($"[ImageLocalRepository] Cache hit '{storageKey}'.");
                return texture;
            }

            await DeleteCachedImageAsync(cached, ct);
            return null;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao obter texture em cache para '{storageKey}': {e.Message}");
            return null;
        }
    }

    // ── Escrita ────────────────────────────────────────────────────────────────

    public async Task SaveAsync(
        string storageKey,
        byte[] pngBytes,
        string topic = null,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(storageKey) || pngBytes == null || pngBytes.Length == 0)
        {
            Debug.LogWarning("[ImageLocalRepository] storageKey ou pngBytes vazio — nada salvo.");
            return;
        }

        if (pngBytes.Length > MAX_IMAGE_BYTES)
        {
            Debug.LogWarning($"[ImageLocalRepository] Imagem muito grande ({pngBytes.Length} bytes), não cacheada: {storageKey}");
            return;
        }

        if (!IsValidPng(pngBytes))
        {
            Debug.LogWarning($"[ImageLocalRepository] Bytes não parecem PNG válido: {storageKey}");
            return;
        }

        try
        {
            ct.ThrowIfCancellationRequested();

            string sha256 = ComputeSha256(pngBytes);
            string fileName = GetStableHashedFileName(storageKey);
            string localPath = Path.Combine(_cacheDirectory, fileName);

            File.WriteAllBytes(localPath, pngBytes);

            DateTime cachedAt = DateTime.UtcNow;

            var cached = new ImageCacheManifestEntry
            {
                storageKey = storageKey,
                localPath = localPath,
                cachedAtUtc = ToUtcString(cachedAt),
                expiresAtUtc = ToUtcString(cachedAt.AddDays(CACHE_EXPIRY_DAYS)),
                fileSizeBytes = pngBytes.Length,
                topic = topic,
                sha256 = sha256
            };

            await UpsertEntryAsync(cached, ct);

            Debug.Log($"[ImageLocalRepository] Imagem '{storageKey}' salva (topic='{topic ?? "-"}', {pngBytes.Length} bytes).");
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao salvar imagem '{storageKey}': {e.Message}");
            throw;
        }
    }

    // ── Verificação ────────────────────────────────────────────────────────────

    public async Task<bool> HasAsync(
        string storageKey,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(storageKey))
            return false;

        try
        {
            ImageCacheManifestEntry cached = await GetEntryAsync(storageKey, ct);

            if (cached == null)
                return false;

            if (!IsEntryValid(cached))
            {
                await DeleteCachedImageAsync(cached, ct);
                return false;
            }

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao verificar existência de '{storageKey}': {e.Message}");
            return false;
        }
    }

    // ── Metadados de cache ─────────────────────────────────────────────────────

    public async Task<DateTime?> GetLatestCacheTimestampAsync(
        string topic = null,
        CancellationToken ct = default)
    {
        try
        {
            List<ImageCacheManifestEntry> entries = await GetEntriesSnapshotAsync(ct);

            var query = entries.Where(IsEntryValid);

            if (!string.IsNullOrEmpty(topic))
                query = query.Where(x => x.topic == topic);

            return query
                .Select(x => TryParseUtc(x.cachedAtUtc, out DateTime cachedAt) ? (DateTime?)cachedAt : null)
                .Where(x => x != null)
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao obter timestamp de cache para '{topic}': {e.Message}");
            return null;
        }
    }

    // ── Limpeza ────────────────────────────────────────────────────────────────

    public async Task EvictByTopicAsync(
        string topic,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(topic))
        {
            Debug.LogWarning("[ImageLocalRepository] EvictByTopicAsync chamado sem topic — ignorado.");
            return;
        }

        try
        {
            List<ImageCacheManifestEntry> toDelete = (await GetEntriesSnapshotAsync(ct))
                .Where(x => x.topic == topic)
                .ToList();

            foreach (var cached in toDelete)
            {
                ct.ThrowIfCancellationRequested();
                await DeleteCachedImageAsync(cached, ct);
            }

            Debug.Log($"[ImageLocalRepository] {toDelete.Count} imagens removidas para topic '{topic}'.");
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao fazer evict por topic '{topic}': {e.Message}");
            throw;
        }
    }

    public async Task ClearAllAsync(CancellationToken ct = default)
    {
        try
        {
            List<ImageCacheManifestEntry> all = await GetEntriesSnapshotAsync(ct);

            foreach (var cached in all)
            {
                ct.ThrowIfCancellationRequested();
                DeleteFileIfPresent(cached.localPath);
            }

            await SaveManifestAsync(new ImageCacheManifest(), ct);

            Debug.Log($"[ImageLocalRepository] Cache de imagens limpo ({all.Count} imagens removidas).");
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao limpar todo o cache: {e.Message}");
            throw;
        }
    }

    public async Task CleanupOldCacheIfNeededAsync(CancellationToken ct = default)
    {
        try
        {
            List<ImageCacheManifestEntry> all = await GetEntriesSnapshotAsync(ct);

            long totalSize = all.Sum(x => x.fileSizeBytes);

            if (totalSize > MAX_CACHE_SIZE_BYTES)
            {
                var toDelete = all
                    .OrderBy(x => TryParseUtc(x.cachedAtUtc, out DateTime cachedAt) ? cachedAt : DateTime.MinValue)
                    .Take(Mathf.Max(1, Mathf.RoundToInt(all.Count * CLEANUP_FRACTION)))
                    .ToList();

                foreach (var cached in toDelete)
                {
                    ct.ThrowIfCancellationRequested();
                    await DeleteCachedImageAsync(cached, ct);
                }

                Debug.Log($"[ImageLocalRepository] Cleanup por tamanho: {toDelete.Count} imagens removidas.");
            }

            var expired = all
                .Where(IsExpired)
                .ToList();

            foreach (var cached in expired)
            {
                ct.ThrowIfCancellationRequested();
                await DeleteCachedImageAsync(cached, ct);
            }

            if (expired.Count > 0)
                Debug.Log($"[ImageLocalRepository] Cleanup por expiração: {expired.Count} imagens removidas.");
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro no cleanup: {e.Message}");
        }
    }

    private async Task DeleteCachedImageAsync(
        ImageCacheManifestEntry cached,
        CancellationToken ct = default)
    {
        if (cached == null)
            return;

        try
        {
            ct.ThrowIfCancellationRequested();

            DeleteFileIfPresent(cached.localPath);
            await RemoveEntryAsync(cached.storageKey, ct);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            Debug.LogError($"[ImageLocalRepository] Erro ao deletar imagem cacheada '{cached.storageKey}': {e.Message}");
        }
    }

    // ── Manifesto ──────────────────────────────────────────────────────────────

    private async Task<ImageCacheManifestEntry> GetEntryAsync(
        string storageKey,
        CancellationToken ct)
    {
        await _manifestGate.WaitAsync(ct);

        try
        {
            ImageCacheManifest manifest = LoadManifestUnlocked();
            return manifest.images.FirstOrDefault(x => x.storageKey == storageKey);
        }
        finally
        {
            _manifestGate.Release();
        }
    }

    private async Task<List<ImageCacheManifestEntry>> GetEntriesSnapshotAsync(CancellationToken ct)
    {
        await _manifestGate.WaitAsync(ct);

        try
        {
            ImageCacheManifest manifest = LoadManifestUnlocked();
            return manifest.images.ToList();
        }
        finally
        {
            _manifestGate.Release();
        }
    }

    private async Task UpsertEntryAsync(
        ImageCacheManifestEntry entry,
        CancellationToken ct)
    {
        await _manifestGate.WaitAsync(ct);

        try
        {
            ImageCacheManifest manifest = LoadManifestUnlocked();

            manifest.images.RemoveAll(x => x.storageKey == entry.storageKey);
            manifest.images.Add(entry);

            SaveManifestUnlocked(manifest);
        }
        finally
        {
            _manifestGate.Release();
        }
    }

    private async Task RemoveEntryAsync(
        string storageKey,
        CancellationToken ct)
    {
        if (string.IsNullOrEmpty(storageKey))
            return;

        await _manifestGate.WaitAsync(ct);

        try
        {
            ImageCacheManifest manifest = LoadManifestUnlocked();

            if (manifest.images.RemoveAll(x => x.storageKey == storageKey) > 0)
                SaveManifestUnlocked(manifest);
        }
        finally
        {
            _manifestGate.Release();
        }
    }

    private async Task SaveManifestAsync(
        ImageCacheManifest manifest,
        CancellationToken ct)
    {
        await _manifestGate.WaitAsync(ct);

        try
        {
            SaveManifestUnlocked(manifest);
        }
        finally
        {
            _manifestGate.Release();
        }
    }

    private ImageCacheManifest LoadManifestUnlocked()
    {
        if (string.IsNullOrEmpty(_manifestPath) || !File.Exists(_manifestPath))
        {
            LogMissingManifestOnce();
            return new ImageCacheManifest();
        }

        try
        {
            string json = File.ReadAllText(_manifestPath);

            if (string.IsNullOrWhiteSpace(json))
                return new ImageCacheManifest();

            ImageCacheManifest manifest = JsonUtility.FromJson<ImageCacheManifest>(json);

            if (manifest == null)
                return new ImageCacheManifest();

            if (manifest.images == null)
                manifest.images = new List<ImageCacheManifestEntry>();

            return manifest;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[ImageLocalRepository] Manifesto de imagens inválido; recriando. Erro: {e.Message}");
            return new ImageCacheManifest();
        }
    }

    private void LogMissingManifestOnce()
    {
        if (_missingManifestLogged)
            return;

        _missingManifestLogged = true;

        Debug.Log(
            "[ImageLocalRepository] Manifesto de imagens ausente. " +
            "Cache legado não será migrado; imagens serão baixadas novamente sob demanda/prewarm."
        );
    }

    private void SaveManifestUnlocked(ImageCacheManifest manifest)
    {
        if (manifest == null)
            manifest = new ImageCacheManifest();

        if (manifest.images == null)
            manifest.images = new List<ImageCacheManifestEntry>();

        if (!Directory.Exists(_cacheDirectory))
            Directory.CreateDirectory(_cacheDirectory);

        string json = JsonUtility.ToJson(manifest, prettyPrint: true);
        string tempPath = _manifestPath + ".tmp";

        File.WriteAllText(tempPath, json);

        if (File.Exists(_manifestPath))
            File.Delete(_manifestPath);

        File.Move(tempPath, _manifestPath);
    }

    // ── Utilitários ────────────────────────────────────────────────────────────

    private static bool IsEntryValid(ImageCacheManifestEntry entry)
    {
        return entry != null &&
               !IsExpired(entry) &&
               !string.IsNullOrEmpty(entry.localPath) &&
               File.Exists(entry.localPath);
    }

    private static bool IsExpired(ImageCacheManifestEntry entry)
    {
        if (entry == null)
            return true;

        if (!TryParseUtc(entry.expiresAtUtc, out DateTime expiresAt))
            return true;

        return DateTime.UtcNow >= expiresAt;
    }

    private static bool TryParseUtc(string value, out DateTime utc)
    {
        if (DateTime.TryParse(
                value,
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind,
                out DateTime parsed))
        {
            utc = parsed.Kind == DateTimeKind.Utc
                ? parsed
                : parsed.ToUniversalTime();

            return true;
        }

        utc = DateTime.MinValue;
        return false;
    }

    private static string ToUtcString(DateTime value)
    {
        DateTime utc = value.Kind == DateTimeKind.Utc
            ? value
            : value.ToUniversalTime();

        return utc.ToString("O", CultureInfo.InvariantCulture);
    }

    private static void DeleteFileIfPresent(string path)
    {
        if (!string.IsNullOrEmpty(path) && File.Exists(path))
            File.Delete(path);
    }

    private static bool IsValidPng(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 8)
            return false;

        return bytes[0] == 0x89 &&
               bytes[1] == 0x50 &&
               bytes[2] == 0x4E &&
               bytes[3] == 0x47 &&
               bytes[4] == 0x0D &&
               bytes[5] == 0x0A &&
               bytes[6] == 0x1A &&
               bytes[7] == 0x0A;
    }

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

    private static string GetStableHashedFileName(string value)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            var sb = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return $"img_{sb}.png";
        }
    }
}
