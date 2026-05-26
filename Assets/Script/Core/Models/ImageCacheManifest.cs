using System;
using System.Collections.Generic;

/// <summary>
/// Manifesto local do cache de imagens em disco.
/// Mantido como lista para compatibilidade com UnityEngine.JsonUtility.
/// </summary>
[Serializable]
public class ImageCacheManifest
{
    public int schemaVersion = 1;
    public List<ImageCacheManifestEntry> images = new List<ImageCacheManifestEntry>();
}

[Serializable]
public class ImageCacheManifestEntry
{
    /// <summary>
    /// Storage key relativa à raiz "Question". Ex.: "proteins/alanina".
    /// </summary>
    public string storageKey;

    public string localPath;
    public string topic;

    /// <summary>
    /// Timestamps UTC em formato round-trip ("O"), para evitar serialização
    /// inconsistente de DateTime via JsonUtility.
    /// </summary>
    public string cachedAtUtc;
    public string expiresAtUtc;

    public long fileSizeBytes;
    public string sha256;
}
