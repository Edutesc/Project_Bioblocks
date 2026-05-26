using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Fake puramente em memória do IImageLocalRepository.
/// 
/// Não toca o disco.
/// Não decodifica PNG.
/// SaveAsync apenas registra metadados.
/// GetCachedTextureAsync retorna null por padrão, pois testes normalmente focam
/// em ordem, cacheamento e efeitos colaterais, não em textura real.
/// </summary>
public class FakeImageLocalRepository : IImageLocalRepository
{
    public class Entry
    {
        public string Topic;
        public DateTime CachedAt;
        public byte[] Bytes;
    }

    private readonly object _gate = new object();

    public Dictionary<string, Entry> Saved { get; } = new Dictionary<string, Entry>();
    public List<string> SaveOrder { get; } = new List<string>();

    public bool ClearAllCalled { get; private set; }
    public bool CleanupCalled { get; private set; }

    // ── Leitura ────────────────────────────────────────────────────────────────

    public Task<Texture2D> GetCachedTextureAsync(
        string storageKey,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        // Este fake não cria Texture2D.
        // Mesmo que exista entrada em Saved, retornamos null para simular que
        // não há textura real carregável em cache.
        return Task.FromResult<Texture2D>(null);
    }

    public Task<bool> HasAsync(
        string storageKey,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(storageKey))
            return Task.FromResult(false);

        lock (_gate)
        {
            return Task.FromResult(Saved.ContainsKey(storageKey));
        }
    }

    public Task<DateTime?> GetLatestCacheTimestampAsync(
        string topic = null,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        lock (_gate)
        {
            DateTime? latest = null;

            foreach (var kvp in Saved)
            {
                if (!string.IsNullOrEmpty(topic) && kvp.Value.Topic != topic)
                    continue;

                if (latest == null || kvp.Value.CachedAt > latest.Value)
                    latest = kvp.Value.CachedAt;
            }

            return Task.FromResult(latest);
        }
    }

    // ── Escrita ────────────────────────────────────────────────────────────────

    public Task SaveAsync(
        string storageKey,
        byte[] pngBytes,
        string topic = null,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(storageKey) || pngBytes == null || pngBytes.Length == 0)
            return Task.CompletedTask;

        lock (_gate)
        {
            SaveOrder.Add(storageKey);

            Saved[storageKey] = new Entry
            {
                Topic = topic,
                CachedAt = DateTime.UtcNow,
                Bytes = pngBytes.ToArray()
            };
        }

        return Task.CompletedTask;
    }

    // ── Limpeza ────────────────────────────────────────────────────────────────

    public Task EvictByTopicAsync(
        string topic,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(topic))
            return Task.CompletedTask;

        lock (_gate)
        {
            var keys = Saved
                .Where(kvp => kvp.Value.Topic == topic)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (string key in keys)
                Saved.Remove(key);
        }

        return Task.CompletedTask;
    }

    public Task ClearAllAsync(CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        lock (_gate)
        {
            Saved.Clear();
            SaveOrder.Clear();
            ClearAllCalled = true;
        }

        return Task.CompletedTask;
    }

    public Task CleanupOldCacheIfNeededAsync(CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        lock (_gate)
        {
            CleanupCalled = true;
        }

        return Task.CompletedTask;
    }

    // ── Utilitário para testes ─────────────────────────────────────────────────

    public void Reset()
    {
        lock (_gate)
        {
            Saved.Clear();
            SaveOrder.Clear();
            ClearAllCalled = false;
            CleanupCalled = false;
        }
    }
}