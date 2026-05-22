using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Storage;

namespace BioBlocks.Tests
{
    public class TestImagePreloader : MonoBehaviour
    {
        private Dictionary<string, Texture2D> _imageCache = new Dictionary<string, Texture2D>();
        private FirebaseStorage _storage = FirebaseStorage.DefaultInstance;

        public async Task PreloadImagesAsync(List<TestQuestion> questions)
        {
            List<Task> downloadTasks = new List<Task>();
            foreach (var q in questions)
            {
                if (!string.IsNullOrEmpty(q.ImageUrl) && !_imageCache.ContainsKey(q.ImageUrl))
                {
                    downloadTasks.Add(DownloadImageFromStorageAsync(q.ImageUrl));
                }
            }
            await Task.WhenAll(downloadTasks);
        }

        private async Task DownloadImageFromStorageAsync(string storagePath)
        {
            try
            {
                StorageReference reference = _storage.GetReferenceFromUrl(storagePath);
                System.Uri downloadUri = await reference.GetDownloadUrlAsync();

                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(downloadUri))
                {
                    var operation = uwr.SendWebRequest();
                    while (!operation.isDone) await Task.Yield();

                    if (uwr.result == UnityWebRequest.Result.Success)
                    {
                        _imageCache[storagePath] = DownloadHandlerTexture.GetContent(uwr);
                    }
                    else
                    {
                        Debug.LogError($"[TestImagePreloader] Erro: {uwr.error}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[TestImagePreloader] Falha ao resolver imagem do Storage: {ex.Message}");
            }
        }

        public Texture2D GetCachedImage(string path)
        {
            return _imageCache.TryGetValue(path, out var tex) ? tex : null;
        }

        private void OnDestroy()
        {
            foreach (var texture in _imageCache.Values)
            {
                if (texture != null)
                {
                    Destroy(texture);
                }
            }
            _imageCache.Clear();
        }
    }
}
