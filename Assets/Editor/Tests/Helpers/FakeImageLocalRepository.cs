using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fake puramente em memória do IImageLocalRepository — não toca o disco e não
/// decodifica PNG. Save apenas registra metadados; TryGetCachedTexture retorna
/// false (nunca há textura em cache "real" — testes focam em ordem/efeitos).
/// </summary>
public class FakeImageLocalRepository : IImageLocalRepository
{
    public class Entry
    {
        public string  Topic;
        public DateTime CachedAt;
        public byte[]   Bytes;
    }

    public Dictionary<string, Entry> Saved { get; } = new Dictionary<string, Entry>();
    public List<string> SaveOrder { get; } = new List<string>();
    public bool ClearAllCalled { get; private set; }

    public bool TryGetCachedTexture(string storageKey, out Texture2D texture)
    {
        texture = null;
        return false;
    }

    public void Save(string storageKey, byte[] pngBytes, string topic)
    {
        lock (SaveOrder)
        {
            SaveOrder.Add(storageKey);
            Saved[storageKey] = new Entry
            {
                Topic    = topic,
                CachedAt = DateTime.UtcNow,
                Bytes    = pngBytes
            };
        }
    }

    public bool Has(string storageKey) => Saved.ContainsKey(storageKey);

    public DateTime? GetLatestCacheTimestamp(string topic = null)
    {
        DateTime? latest = null;
        foreach (var kvp in Saved)
        {
            if (!string.IsNullOrEmpty(topic) && kvp.Value.Topic != topic) continue;
            if (latest == null || kvp.Value.CachedAt > latest.Value)
                latest = kvp.Value.CachedAt;
        }
        return latest;
    }

    public void EvictByTopic(string topic)
    {
        var keys = new List<string>();
        foreach (var kvp in Saved)
            if (kvp.Value.Topic == topic) keys.Add(kvp.Key);
        foreach (var k in keys) Saved.Remove(k);
    }

    public void ClearAll()
    {
        Saved.Clear();
        SaveOrder.Clear();
        ClearAllCalled = true;
    }

    public void Reset()
    {
        Saved.Clear();
        SaveOrder.Clear();
        ClearAllCalled = false;
    }
}
