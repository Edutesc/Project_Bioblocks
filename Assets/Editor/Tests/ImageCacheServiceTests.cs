// Assets/Editor/Tests/ImageCacheServiceTests.cs
// Testes unitários para ImageCacheService — cobrindo apenas a lógica pura.
//
// O que É testado aqui (sem Texture2D):
//   - GetCachedImagePath: URL nula/vazia, cache não inicializado, cache expirado,
//     entrada inexistente em disco
//   - GetCachedImagesCount e GetTotalCacheSize: com arquivos reais no cache isolado
//   - ClearAllCache: remove os arquivos do cache isolado
//   - IsInitialized: estado após InjectDependencies
//
// O que NÃO é testado aqui (requer Play Mode):
//   - SaveImageToCache (depende de Texture2D.EncodeToPNG e File.WriteAllBytes)
//   - LoadImageFromCache (depende de File.ReadAllBytes e Texture2D.LoadImage)
//   - ResizeTexture (depende de RenderTexture e Graphics.Blit)

using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

[TestFixture]
public class ImageCacheServiceTests
{
    // -------------------------------------------------------
    // Fixtures
    // -------------------------------------------------------

    private ImageCacheService  _service;
    private GameObject         _serviceGO;
    private string             _testCacheDirectory;

    private static readonly byte[] PngSignature =
    {
        0x89, 0x50, 0x4E, 0x47,
        0x0D, 0x0A, 0x1A, 0x0A
    };

    [SetUp]
    public void Setup()
    {
        _serviceGO = new GameObject("ImageCacheService");
        _service   = _serviceGO.AddComponent<ImageCacheService>();
        _service.InjectDependencies();

        _testCacheDirectory = Path.Combine(
            Path.GetTempPath(),
            "BioBlocks_ImageCacheServiceTests_" + Guid.NewGuid().ToString("N")
        );

        Directory.CreateDirectory(_testCacheDirectory);
        SetCacheDirectory(_service, _testCacheDirectory);
    }

    [TearDown]
    public void TearDown()
    {
        if (_serviceGO != null)
            UnityEngine.Object.DestroyImmediate(_serviceGO);

        if (!string.IsNullOrEmpty(_testCacheDirectory) && Directory.Exists(_testCacheDirectory))
            Directory.Delete(_testCacheDirectory, recursive: true);
    }

    // -------------------------------------------------------
    // Helper: salva bytes PNG diretamente no cache isolado do teste.
    // -------------------------------------------------------

    private string SaveCacheEntry(
        string url,
        long   sizeBytes,
        bool   expired     = false)
    {
        byte[] bytes = MakePngLikeBytes(sizeBytes);
        _service.SaveImageBytesToCache(url, bytes);

        string localPath = _service.GetCachedImagePath(url);

        if (expired && localPath != null)
        {
            File.SetLastWriteTimeUtc(localPath, DateTime.UtcNow.AddDays(-8));
        }

        return localPath;
    }

    private static byte[] MakePngLikeBytes(long sizeBytes)
    {
        int size = (int)Math.Max(PngSignature.Length, sizeBytes);
        byte[] bytes = new byte[size];
        Array.Copy(PngSignature, bytes, PngSignature.Length);
        return bytes;
    }

    private static void SetCacheDirectory(ImageCacheService service, string path)
    {
        typeof(ImageCacheService)
            .GetField("_cacheDirectory", BindingFlags.Instance | BindingFlags.NonPublic)
            .SetValue(service, path);
    }

    // =======================================================
    // IsInitialized
    // =======================================================

    [Test]
    public void IsInitialized_AposInjectDependencies_ETrue()
    {
        Assert.IsTrue(_service.IsInitialized);
    }

    [Test]
    public void IsInitialized_SemInjectDependencies_EFalse()
    {
        var go      = new GameObject("Uninit");
        var service = go.AddComponent<ImageCacheService>();

        Assert.IsFalse(service.IsInitialized);

        UnityEngine.Object.DestroyImmediate(go);
    }

    // =======================================================
    // GetCachedImagePath — guards de entrada
    // =======================================================

    [Test]
    public void GetCachedImagePath_UrlNula_RetornaNull()
    {
        var result = _service.GetCachedImagePath(null);
        Assert.IsNull(result);
    }

    [Test]
    public void GetCachedImagePath_UrlVazia_RetornaNull()
    {
        var result = _service.GetCachedImagePath(string.Empty);
        Assert.IsNull(result);
    }

    [Test]
    public void GetCachedImagePath_EntradaInexistenteEmDisco_RetornaNull()
    {
        var result = _service.GetCachedImagePath("https://example.com/foto.png");
        Assert.IsNull(result);
    }

    [Test]
    public void GetCachedImagePath_EntradaExpirada_RetornaNull()
    {
        SaveCacheEntry("https://example.com/foto.png", sizeBytes: 1024, expired: true);

        var result = _service.GetCachedImagePath("https://example.com/foto.png");

        Assert.IsNull(result, "Cache expirado deve retornar null");
    }

    [Test]
    public void GetCachedImagePath_EntradaExpirada_RemoveArquivo()
    {
        string localPath = SaveCacheEntry("https://example.com/foto.png", sizeBytes: 1024, expired: true);

        _service.GetCachedImagePath("https://example.com/foto.png");

        Assert.IsFalse(File.Exists(localPath),
            "Arquivo expirado deve ser removido do cache ao ser acessado");
    }

    // =======================================================
    // GetCachedImagesCount
    // =======================================================

    [Test]
    public void GetCachedImagesCount_CacheVazio_RetornaZero()
    {
        Assert.AreEqual(0, _service.GetCachedImagesCount());
    }

    [Test]
    public void GetCachedImagesCount_ComDuasEntradas_RetornaDois()
    {
        SaveCacheEntry("https://example.com/img1.png", sizeBytes: 1024);
        SaveCacheEntry("https://example.com/img2.png", sizeBytes: 2048);

        Assert.AreEqual(2, _service.GetCachedImagesCount());
    }

    [Test]
    public void GetCachedImagesCount_NaoInicializado_RetornaZero()
    {
        var go      = new GameObject("Uninit");
        var service = go.AddComponent<ImageCacheService>();
        // InjectDependencies não foi chamado — IsInitialized = false

        Assert.AreEqual(0, service.GetCachedImagesCount());

        UnityEngine.Object.DestroyImmediate(go);
    }

    // =======================================================
    // GetTotalCacheSize
    // =======================================================

    [Test]
    public void GetTotalCacheSize_CacheVazio_RetornaZero()
    {
        Assert.AreEqual(0L, _service.GetTotalCacheSize());
    }

    [Test]
    public void GetTotalCacheSize_SomaCorretamente()
    {
        SaveCacheEntry("https://example.com/img1.png", sizeBytes: 1_000_000);
        SaveCacheEntry("https://example.com/img2.png", sizeBytes: 2_000_000);
        SaveCacheEntry("https://example.com/img3.png", sizeBytes:   500_000);

        long total = _service.GetTotalCacheSize();

        Assert.AreEqual(3_500_000L, total);
    }

    [Test]
    public void GetTotalCacheSize_NaoInicializado_RetornaZero()
    {
        var go      = new GameObject("Uninit");
        var service = go.AddComponent<ImageCacheService>();

        Assert.AreEqual(0L, service.GetTotalCacheSize());

        UnityEngine.Object.DestroyImmediate(go);
    }

    // =======================================================
    // ClearAllCache
    // =======================================================

    [Test]
    public void ClearAllCache_CacheVazio_NaoLancaExcecao()
    {
        Assert.DoesNotThrow(() => _service.ClearAllCache());
    }

    [Test]
    public void ClearAllCache_ComArquivos_RemoveTodosDoCache()
    {
        SaveCacheEntry("https://example.com/img1.png", sizeBytes: 1024);
        SaveCacheEntry("https://example.com/img2.png", sizeBytes: 2048);
        SaveCacheEntry("https://example.com/img3.png", sizeBytes: 512);

        _service.ClearAllCache();

        Assert.AreEqual(0, _service.GetCachedImagesCount(),
            "ClearAllCache deve remover todos os arquivos do cache");
    }

    [Test]
    public void ClearAllCache_GetCachedImagesCountVoltaAZero()
    {
        SaveCacheEntry("https://example.com/img1.png", sizeBytes: 1024);
        SaveCacheEntry("https://example.com/img2.png", sizeBytes: 2048);

        _service.ClearAllCache();

        Assert.AreEqual(0, _service.GetCachedImagesCount());
    }

    [Test]
    public void ClearAllCache_GetTotalCacheSizeVoltaAZero()
    {
        SaveCacheEntry("https://example.com/img1.png", sizeBytes: 1_000_000);

        _service.ClearAllCache();

        Assert.AreEqual(0L, _service.GetTotalCacheSize());
    }

    [Test]
    public void ClearAllCache_NaoInicializado_NaoLancaExcecao()
    {
        var go      = new GameObject("Uninit");
        var service = go.AddComponent<ImageCacheService>();

        Assert.DoesNotThrow(() => service.ClearAllCache());

        UnityEngine.Object.DestroyImmediate(go);
    }
}
