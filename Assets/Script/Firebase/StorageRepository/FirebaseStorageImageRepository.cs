using System;
using System.Threading.Tasks;
using Firebase.Storage;
using UnityEngine;

public class FirebaseStorageImageRepository : MonoBehaviour, IFirebaseStorageImageRepository
{
    private FirebaseStorage _storage;
    private StorageReference _root;
    private bool isInitialized;

    // 4 MB cobre com folga as imagens do app (média ~50KB, máx esperado <500KB).
    private const int MaxBytes = 4 * 1024 * 1024;

    // Layout: Question/<topic>/<filename>.png
    // Ex.: Question/biochem/benzeno.png, Question/water/agua_polar.png, ...
    private const string ROOT_PATH       = "Question";
    private const string DEFAULT_EXT     = ".png";

    // ── Inicialização ──────────────────────────────────────────────────────────

    public void Initialize()
    {
        if (isInitialized) return;

        try
        {
            _storage = FirebaseStorage.DefaultInstance;

            // Reduz retry times — defaults do SDK são absurdos (download=600s,
            // operation=120s) e fazem o app parecer travado quando uma imagem
            // dá 404 ou a rede falha. 8s/5s é o suficiente pra cobrir um hiccup
            // de rede sem deixar o usuário esperando.
            _storage.MaxDownloadRetryTime  = TimeSpan.FromSeconds(8);
            _storage.MaxOperationRetryTime = TimeSpan.FromSeconds(5);

            _root = _storage.GetReference(ROOT_PATH);
            isInitialized = true;
            Debug.Log("[FirebaseStorageImageRepository] Inicializado com sucesso.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirebaseStorageImageRepository] Falha na inicialização: {e.Message}");
            throw;
        }
    }

    // ── Download e recuperação ─────────────────────────────────────────────────

    public async Task<byte[]> DownloadImageAsync(string storageKey)
    {
        EnsureInitialized();

        try
        {
            string keyWithExt = EnsureExtension(storageKey);
            StorageReference imageRef = _root.Child(keyWithExt);
            byte[] data = await imageRef.GetBytesAsync(MaxBytes);
            return data;
        }
        catch (Firebase.Storage.StorageException se)
        {
            // Log compacto. ErrorCode -13010 = Object Not Found (404) — comum se
            // o questionImagePath aponta pra arquivo que não foi para o Storage.
            // ErrorCode -13020 = Unauthenticated. ErrorCode -13021 = Unauthorized.
            string keyWithExt = EnsureExtension(storageKey);
            Debug.LogWarning($"[FirebaseStorageImageRepository] 'Question/{keyWithExt}' falhou " +
                             $"(code={se.ErrorCode}, http={se.HttpResultCode}).");
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirebaseStorageImageRepository] {e.GetType().Name} para '{storageKey}': {e.Message}");
            return null;
        }
    }

    public async Task<bool> ExistsAsync(string storageKey)
    {
        EnsureInitialized();

        try
        {
            string keyWithExt = EnsureExtension(storageKey);
            Debug.Log($"[FirebaseStorageImageRepository] Verificando existência de '{keyWithExt}'...");

            StorageReference imageRef = _root.Child(keyWithExt);
            await imageRef.GetMetadataAsync();

            Debug.Log($"[FirebaseStorageImageRepository] Imagem '{keyWithExt}' existe.");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"[FirebaseStorageImageRepository] Imagem '{storageKey}' não existe ou erro ao verificar: {e.Message}");
            return false;
        }
    }

    // ── Utilitário ─────────────────────────────────────────────────────────────

    private static string EnsureExtension(string storageKey)
    {
        if (string.IsNullOrEmpty(storageKey)) return storageKey;
        if (HasImageExtension(storageKey))    return storageKey;
        return storageKey + DEFAULT_EXT;
    }

    private static bool HasImageExtension(string path)
    {
        return path.EndsWith(".png",  StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".jpg",  StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".webp", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".gif",  StringComparison.OrdinalIgnoreCase);
    }

    private void EnsureInitialized()
    {
        if (!isInitialized)
            throw new InvalidOperationException("[FirebaseStorageImageRepository] Não inicializado. Chame Initialize() antes de usar.");
    }
}
