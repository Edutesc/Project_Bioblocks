using LiteDB;
using System;

/// <summary>
/// Modelo LiteDB para cache local da coleção Rankings do Firestore.
/// O Firestore é a fonte da verdade — este é apenas um cache de leitura.
/// </summary>
public class RankingDB
{
    [BsonId]
    public string UserId { get; set; }

    public string UserName        { get; set; }
    public string ProfileImageUrl { get; set; }
    public int    Score           { get; set; }
    public int    WeekScore       { get; set; }

    // Controle de cache
    public DateTime CachedAt { get; set; } = DateTime.UtcNow;
}