using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// Componente genérico e reutilizável para carregar e exibir imagens de perfil
/// Usado em: RankingRowUI, UserTopBar, ProfileScene, e qualquer outro lugar que precise exibir avatares
/// </summary>
public class ProfileImageLoader : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RawImage imageContent;
    [SerializeField] private Sprite standardProfileImage;

    [Header("Mask Configuration")]
    [SerializeField] private bool autoConfigureMask = true;
    [SerializeField] private int maskResolution = 256;

    private bool isInitialized = false;

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Inicializa o loader. Chamado automaticamente no Awake ou manualmente se necessário
    /// </summary>
    public void Initialize()
    {
        if (isInitialized) return;

        if (imageContent == null)
        {
            // Tenta encontrar RawImage automaticamente
            imageContent = GetComponentInChildren<RawImage>();
            if (imageContent == null)
            {
                Debug.LogWarning($"[ProfileImageLoader] ImageContent não encontrado em {gameObject.name}");
                return;
            }
        }

        if (autoConfigureMask)
        {
            ConfigureImageMask();
        }

        isInitialized = true;
    }

    /// <summary>
    /// Define o RawImage a ser usado (útil para configuração dinâmica)
    /// </summary>
    public void SetImageContent(RawImage rawImage)
    {
        imageContent = rawImage;

        if (autoConfigureMask)
        {
            ConfigureImageMask();
        }

        isInitialized = true;
    }

    /// <summary>
    /// Configura máscara circular automaticamente
    /// </summary>
    private void ConfigureImageMask()
    {
        if (imageContent == null)
        {
            Debug.LogWarning("[ProfileImageLoader] ImageContent é null, não é possível configurar máscara");
            return;
        }

        // Encontrar ou criar o MaskObject (pai do RawImage)
        var maskObject = imageContent.transform.parent?.gameObject;
        if (maskObject == null)
        {
            Debug.LogWarning($"[ProfileImageLoader] MaskObject (pai do RawImage) não encontrado em {gameObject.name}");
            return;
        }

        // Configurar componente Mask
        var mask = maskObject.GetComponent<Mask>();
        if (mask == null)
        {
            mask = maskObject.AddComponent<Mask>();
        }
        mask.showMaskGraphic = true;

        // Configurar Image da máscara
        var maskImage = maskObject.GetComponent<Image>();
        if (maskImage == null)
        {
            maskImage = maskObject.AddComponent<Image>();
        }

        maskImage.sprite = CreateCircleSprite(maskResolution);
        maskImage.type = Image.Type.Simple;
        maskImage.color = Color.white;

        // Configurar RawImage para preencher o espaço da máscara
        var imageRect = imageContent.GetComponent<RectTransform>();
        if (imageRect != null)
        {
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
            imageRect.sizeDelta = Vector2.zero;
            imageRect.anchoredPosition = Vector2.zero;
        }

        Debug.Log($"[ProfileImageLoader] Máscara circular configurada em {gameObject.name}");
    }

    /// <summary>
    /// Cria sprite circular para a máscara
    /// </summary>
    private Sprite CreateCircleSprite(int resolution)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        float radius = resolution / 2f;
        Vector2 center = new Vector2(radius, radius);

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                Color color = distance < radius ? Color.white : Color.clear;
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return Sprite.Create(
            texture,
            new Rect(0, 0, resolution, resolution),
            Vector2.one * 0.5f,
            100f,
            0,
            SpriteMeshType.Tight
        );
    }

    /// <summary>
    /// Carrega imagem de perfil a partir de uma URL ou usa imagem padrão
    /// </summary>
    public void LoadProfileImage(string imageUrl)
    {
        if (!isInitialized)
        {
            Initialize();
        }

        if (imageContent == null)
        {
            Debug.LogError($"[ProfileImageLoader] ImageContent é null em {gameObject.name}");
            return;
        }

        if (string.IsNullOrEmpty(imageUrl))
        {
            LoadStandardProfileImage();
            return;
        }

        StartCoroutine(LoadImageFromUrl(imageUrl));
    }

    /// <summary>
    /// Carrega imagem de perfil do UserData atual
    /// </summary>
    public void LoadFromCurrentUser()
    {
        UserData currentUserData = UserDataStore.CurrentUserData;
        if (currentUserData != null)
        {
            LoadProfileImage(currentUserData.ProfileImageUrl);
        }
        else
        {
            LoadStandardProfileImage();
        }
    }

    /// <summary>
    /// Coroutine que faz o download da imagem
    /// </summary>
    private IEnumerator LoadImageFromUrl(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                SetTexture(texture);
                Debug.Log($"[ProfileImageLoader] Imagem carregada com sucesso de {url}");
            }
            else
            {
                Debug.LogWarning($"[ProfileImageLoader] Erro ao carregar imagem: {www.error}. Usando imagem padrão.");
                LoadStandardProfileImage();
            }
        }
    }

    /// <summary>
    /// Carrega a imagem padrão
    /// </summary>
    public void LoadStandardProfileImage()
    {
        if (!isInitialized)
        {
            Initialize();
        }

        if (imageContent == null)
        {
            Debug.LogError($"[ProfileImageLoader] ImageContent é null em {gameObject.name}");
            return;
        }

        if (standardProfileImage == null)
        {
            Debug.LogWarning($"[ProfileImageLoader] StandardProfileImage não atribuída em {gameObject.name}. Criando placeholder...");
            CreateAndSetPlaceholderTexture();
            return;
        }

        if (standardProfileImage.texture == null)
        {
            Debug.LogWarning($"[ProfileImageLoader] StandardProfileImage não tem textura em {gameObject.name}");
            CreateAndSetPlaceholderTexture();
            return;
        }

        imageContent.texture = standardProfileImage.texture;
        imageContent.color = Color.white;
        Debug.Log($"[ProfileImageLoader] Imagem padrão configurada em {gameObject.name}");
    }

    /// <summary>
    /// Cria uma textura placeholder cinza quando não há imagem disponível
    /// </summary>
    private void CreateAndSetPlaceholderTexture()
    {
        Texture2D texture = new Texture2D(128, 128);
        Color[] colors = new Color[128 * 128];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.gray;
        }
        texture.SetPixels(colors);
        texture.Apply();

        SetTexture(texture);
        Debug.Log($"[ProfileImageLoader] Textura placeholder criada em {gameObject.name}");
    }

    /// <summary>
    /// Define a textura no RawImage, fazendo cleanup da textura anterior
    /// </summary>
    public void SetTexture(Texture2D texture)
    {
        if (imageContent == null)
        {
            Debug.LogError($"[ProfileImageLoader] ImageContent é null em {gameObject.name}");
            return;
        }

        // Limpar textura anterior se não for a padrão
        if (imageContent.texture != null &&
            imageContent.texture != standardProfileImage?.texture &&
            imageContent.texture != texture)
        {
            Destroy(imageContent.texture);
        }

        imageContent.texture = texture;
        imageContent.color = Color.white;
    }

    /// <summary>
    /// Limpa os recursos ao destruir o componente
    /// </summary>
    private void OnDestroy()
    {
        if (imageContent != null &&
            imageContent.texture != null &&
            imageContent.texture != standardProfileImage?.texture)
        {
            Destroy(imageContent.texture);
        }
    }

    // ========== GETTERS/SETTERS PÚBLICOS ==========

    public RawImage ImageContent => imageContent;
    public Sprite StandardProfileImage
    {
        get => standardProfileImage;
        set => standardProfileImage = value;
    }
    public bool IsInitialized => isInitialized;
}