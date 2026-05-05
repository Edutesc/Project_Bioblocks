using System;
using UnityEngine;

public interface IImageLocalRepository
{
    bool TryGetCachedTexture(string storageKey, out Texture2D texture);
    void Save(string storageKey, byte[] pngBytes, string databankName);
    bool Has(string storageKey);
    DateTime? GetLatestCacheTimestamp(string databankName);
    void EvictByDatabank(string databankName);
    void ClearAll();
}
