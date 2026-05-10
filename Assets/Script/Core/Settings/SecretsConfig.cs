using UnityEngine;

/// <summary>
/// Configuração de secrets do app (chaves de API externas).
///
/// Asset esperado em Assets/Resources/SecretsConfig.asset.
/// IMPORTANTE: Este arquivo .asset está no .gitignore — nunca commitar.
///
/// Como configurar localmente:
///   1. Menu: Assets > Create > BioBlocks > Secrets Config
///   2. Salve o asset em Assets/Resources/SecretsConfig.asset
///   3. Preencha o campo "Gemini Api Key" no Inspector com a chave da API.
///      Obtenha em: aistudio.google.com/apikey
/// </summary>
[CreateAssetMenu(
    fileName = "SecretsConfig",
    menuName  = "BioBlocks/Secrets Config",
    order     = 1
)]
public class SecretsConfig : ScriptableObject
{
    [Header("Google Gemini API")]
    [Tooltip("Chave de API do Google Gemini. Obtenha gratuitamente em aistudio.google.com/apikey")]
    [SerializeField] private string geminiApiKey = "";

    public string GeminiApiKey => geminiApiKey;

    private static SecretsConfig _cached;

    /// <summary>
    /// Carrega o asset de Resources. Cacheia em static para evitar
    /// múltiplos Resources.Load.
    /// </summary>
    public static SecretsConfig Load()
    {
        if (_cached == null)
        {
            _cached = Resources.Load<SecretsConfig>("SecretsConfig");
            if (_cached == null)
            {
                Debug.LogError(
                    "[SecretsConfig] Asset não encontrado em Resources/SecretsConfig. " +
                    "Crie via menu Assets > Create > BioBlocks > Secrets Config " +
                    "e salve em Assets/Resources/SecretsConfig.asset"
                );
            }
        }
        return _cached;
    }

    // ---------- Override para testes ----------
    private static string _geminiKeyOverride;

    public static void OverrideGeminiKeyForTests(string apiKey)
        => _geminiKeyOverride = apiKey;

    public static void ClearTestOverride()
        => _geminiKeyOverride = null;

    public string ResolvedGeminiApiKey =>
        !string.IsNullOrEmpty(_geminiKeyOverride) ? _geminiKeyOverride : geminiApiKey;
}
