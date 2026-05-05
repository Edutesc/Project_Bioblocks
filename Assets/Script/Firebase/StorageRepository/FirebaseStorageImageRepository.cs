using System;
using System.Threading.Tasks;
using Firebase.Storage;
using UnityEngine;

public class FirebaseStorageImageRepository : MonoBehaviour, IFirebaseStorageImageRepository
{
    private FirebaseStorage _storage;
    private StorageReference _root;
    private bool isInitialized;

    private const int MaxBytes = 1024 * 1024; // 1 MB
    private const string ROOT_PATH = "questions";

    // ── Inicialização ──────────────────────────────────────────────────────────

    public void Initialize()
    {
        if (isInitialized) return;

        try
        {
            _storage = FirebaseStorage.DefaultInstance;
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
            Debug.Log($"[FirebaseStorageImageRepository] Baixando imagem '{storageKey}'...");

            StorageReference imageRef = _root.Child(storageKey + ".png");
            byte[] data = await imageRef.GetBytesAsync(MaxBytes);

            Debug.Log($"[FirebaseStorageImageRepository] Imagem '{storageKey}' baixada com sucesso ({data.Length} bytes).");
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirebaseStorageImageRepository] DownloadImage falhou para {storageKey}: {e.Message}");
            return null;
        }
    }

    public async Task<string> GetDownloadUrlAsync(string storageKey)
    {
        EnsureInitialized();

        try
        {
            Debug.Log($"[FirebaseStorageImageRepository] Obtendo URL de download para '{storageKey}'...");

            StorageReference imageRef = _root.Child(storageKey + ".png");

            var result = await imageRef.GetDownloadUrlAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError($"[FirebaseStorageImageRepository] Erro ao obter URL para '{storageKey}': {task.Exception?.Message}");
                    return (string)null;
                }

                string url = task.Result.ToString();
                Debug.Log($"[FirebaseStorageImageRepository] URL obtida para '{storageKey}'.");
                return url;
            });

            return result;
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirebaseStorageImageRepository] GetDownloadUrl falhou para {storageKey}: {e.Message}");
            return null;
        }
    }

    public async Task<bool> ExistsAsync(string storageKey)
    {
        EnsureInitialized();

        try
        {
            Debug.Log($"[FirebaseStorageImageRepository] Verificando existência de '{storageKey}'...");

            StorageReference imageRef = _root.Child(storageKey + ".png");
            await imageRef.GetMetadataAsync();

            Debug.Log($"[FirebaseStorageImageRepository] Imagem '{storageKey}' existe.");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"[FirebaseStorageImageRepository] Imagem '{storageKey}' não existe ou erro ao verificar: {e.Message}");
            return false;
        }
    }

    // ── Utilitário ─────────────────────────────────────────────────────────────

    private void EnsureInitialized()
    {
        if (!isInitialized)
            throw new InvalidOperationException("[FirebaseStorageImageRepository] Não inicializado. Chame Initialize() antes de usar.");
    }
}
