using UnityEngine;

public interface IImageCacheService
{
    /// <summary>
    /// Retorna o caminho local de uma imagem cacheada, ou null se não estiver
    /// cacheada/expirada.
    /// </summary>
    string GetCachedImagePath(string url);

    /// <summary>
    /// Carrega uma Texture2D a partir do caminho local.
    /// </summary>
    Texture2D LoadImageFromCache(string path);

    /// <summary>
    /// Salva a textura no cache em disco e atualiza o LiteDB.
    /// `topic` (opcional) identifica o agrupamento por tema do Storage; usado pelo
    /// ImageLocalRepository das Question. Para imagens sem topic (avatar/profile),
    /// passe null.
    ///
    /// IMPORTANTE: este método decodifica/encodifica via Texture2D — só pode ser
    /// chamado da main thread. Para fluxos de download em background, use a
    /// sobrecarga que recebe `byte[]` diretamente.
    /// </summary>
    void SaveImageToCache(string imageUrl, Texture2D texture,
                          string topic = null, string sha256 = null);

    /// <summary>
    /// Salva bytes PNG diretamente no cache em disco e LiteDB, sem passar por
    /// Texture2D. Thread-safe — pode ser chamado de qualquer thread (background
    /// downloads). Não redimensiona — os bytes vão pro disco como vieram.
    /// </summary>
    void SaveImageBytesToCache(string imageUrl, byte[] pngBytes,
                               string topic = null, string sha256 = null);

    /// <summary>Remove todas as imagens do cache (disco + LiteDB).</summary>
    void ClearAllCache();

    long GetTotalCacheSize();
    int  GetCachedImagesCount();
}
