using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public interface IImageLocalRepository
{
    /// <summary>
    /// Tenta carregar uma textura do cache local.
    /// Deve ser chamado da main thread, pois cria Texture2D.
    /// </summary>
    Task<Texture2D> GetCachedTextureAsync(string storageKey, CancellationToken ct = default);

    /// <summary>
    /// Salva os bytes da imagem no disco e registra os metadados no manifesto local.
    /// Seguro para ser chamado a partir de tarefas assíncronas/background.
    /// </summary>
    Task SaveAsync(string storageKey, byte[] pngBytes, string topic = null, CancellationToken ct = default);

    /// <summary>
    /// Verifica se uma imagem existe no cache local e ainda está válida.
    /// </summary>
    Task<bool> HasAsync(string storageKey, CancellationToken ct = default);

    /// <summary>
    /// Retorna o timestamp mais recente do cache, opcionalmente filtrado por topic.
    /// </summary>
    Task<DateTime?> GetLatestCacheTimestampAsync(string topic = null, CancellationToken ct = default);

    /// <summary>
    /// Remove todas as imagens de um determinado topic.
    /// </summary>
    Task EvictByTopicAsync(string topic, CancellationToken ct = default);

    /// <summary>
    /// Limpa todo o cache local de imagens.
    /// </summary>
    Task ClearAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Remove imagens expiradas ou excedentes.
    /// Recomendo chamar ao final de um topic ou ao final do prewarm, não após cada imagem.
    /// </summary>
    Task CleanupOldCacheIfNeededAsync(CancellationToken ct = default);
}
