using System;
using UnityEngine;

/// <summary>
/// Cache local de imagens das Question (LiteDB + arquivo em disco).
///
/// As chaves passadas aqui são storage keys relativas à raiz "Question" do
/// Firebase Storage — ex.: "biochem/benzeno", "water/molecula_h2o".
/// </summary>
public interface IImageLocalRepository
{
    /// <summary>
    /// Tenta carregar a textura cacheada. Retorna true em cache hit.
    /// O caller é responsável por destruir a Texture2D quando não for mais usada.
    /// </summary>
    bool TryGetCachedTexture(string storageKey, out Texture2D texture);

    /// <summary>
    /// Salva os bytes PNG no cache, marcando o documento com o topic informado.
    /// </summary>
    void Save(string storageKey, byte[] pngBytes, string topic);

    /// <summary>Existe documento de cache para esta key?</summary>
    bool Has(string storageKey);

    /// <summary>
    /// Timestamp da imagem mais recente cacheada para o topic. Passe null para
    /// considerar todas as imagens cacheadas.
    /// </summary>
    DateTime? GetLatestCacheTimestamp(string topic = null);

    /// <summary>Remove arquivos físicos e documentos LiteDB do topic informado.</summary>
    void EvictByTopic(string topic);

    /// <summary>Remove tudo do cache.</summary>
    void ClearAll();
}
