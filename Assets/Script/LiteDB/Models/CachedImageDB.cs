using System;
using LiteDB;

/// <summary>
/// Documento de cache local para uma imagem baixada do Firebase Storage.
///
/// Layout no Storage: Question/&lt;topic&gt;/&lt;filename&gt;.png
/// ImageUrl é a storage key relativa à raiz "Question" — ex.: "biochem/benzeno".
/// </summary>
public class CachedImageDB
{
    [BsonId]
    public string ImageUrl { get; set; }

    public string LocalPath { get; set; }
    public DateTime CachedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public long FileSizeBytes { get; set; }

    /// <summary>
    /// Topic (= pasta no Storage). Ex.: "acidsBase", "biochem", "water".
    /// Unidade natural de eviction e prewarm.
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// Hash do conteúdo PNG, usado para detectar mudança de versão de uma imagem
    /// que mantém a mesma chave.
    /// </summary>
    public string Sha256 { get; set; }
}
