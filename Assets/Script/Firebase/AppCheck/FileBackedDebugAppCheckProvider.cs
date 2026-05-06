#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Firebase;
using Firebase.AppCheck;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// IAppCheckProviderFactory custom para o Editor da Unity.
///
/// Por que isto existe: o DebugAppCheckProviderFactory.Instance do SDK depende
/// da variável de ambiente FIREBASE_APPCHECK_DEBUG_TOKEN, que não é vista pelo
/// plugin nativo no Editor macOS quando setada via .NET em runtime. Para
/// contornar, este provider lê o UUID de debug de um arquivo gitignored e
/// chama diretamente o endpoint exchangeDebugToken da App Check API,
/// devolvendo o JWT resultante para o SDK.
///
/// Arquivo esperado: &lt;projectRoot&gt;/firebase_app_check_debug_token.txt
/// Conteúdo: o UUID cadastrado em Firebase Console → App Check → Apps →
/// Manage debug tokens.
///
/// Em build real (Android/iOS), use os providers de produção
/// (Play Integrity / DeviceCheck). Esta classe é só para Editor.
/// </summary>
public sealed class FileBackedDebugAppCheckProviderFactory : IAppCheckProviderFactory
{
    private readonly string _debugUuid;

    public FileBackedDebugAppCheckProviderFactory(string debugUuid)
    {
        if (string.IsNullOrWhiteSpace(debugUuid))
            throw new ArgumentException("Debug UUID não pode ser vazio.", nameof(debugUuid));
        _debugUuid = debugUuid.Trim();
    }

    /// <summary>
    /// Carrega o UUID de &lt;projectRoot&gt;/firebase_app_check_debug_token.txt e
    /// devolve a factory. Retorna null se o arquivo não existir ou estiver vazio
    /// (caller deve cair no fallback de não setar provider).
    /// </summary>
    public static FileBackedDebugAppCheckProviderFactory TryCreateFromFile()
    {
        try
        {
            string projectRoot = Path.GetDirectoryName(Application.dataPath);
            string tokenPath   = Path.Combine(projectRoot, "firebase_app_check_debug_token.txt");

            if (!File.Exists(tokenPath))
            {
                Debug.LogWarning($"[FileBackedDebugAppCheck] {tokenPath} não encontrado.");
                return null;
            }

            string token = File.ReadAllText(tokenPath).Trim();
            if (string.IsNullOrEmpty(token))
            {
                Debug.LogWarning($"[FileBackedDebugAppCheck] {tokenPath} está vazio.");
                return null;
            }

            return new FileBackedDebugAppCheckProviderFactory(token);
        }
        catch (Exception e)
        {
            Debug.LogError($"[FileBackedDebugAppCheck] Erro ao ler token: {e.Message}");
            return null;
        }
    }

    public IAppCheckProvider CreateProvider(FirebaseApp app)
        => new FileBackedDebugAppCheckProvider(_debugUuid, app);
}

internal sealed class FileBackedDebugAppCheckProvider : IAppCheckProvider
{
    private readonly string _debugUuid;
    private readonly string _appId;
    private readonly string _apiKey;

    // Cache simples — o JWT vale ~1h, não precisa pedir um novo a cada request.
    private string _cachedToken;
    private DateTime _cachedExpiresAt;
    private static readonly object _cacheLock = new object();

    public FileBackedDebugAppCheckProvider(string debugUuid, FirebaseApp app)
    {
        _debugUuid = debugUuid;
        _appId     = app.Options.AppId;
        _apiKey    = app.Options.ApiKey;
    }

    public async Task<AppCheckToken> GetTokenAsync()
    {
        // Cache hit — devolve token guardado se ainda tem 5min de validade.
        lock (_cacheLock)
        {
            if (!string.IsNullOrEmpty(_cachedToken) &&
                _cachedExpiresAt > DateTime.UtcNow.AddMinutes(5))
            {
                return BuildAppCheckToken(_cachedToken, _cachedExpiresAt);
            }
        }

        // Exchange UUID por JWT via API REST do App Check.
        var exchanged = await ExchangeDebugTokenAsync().ConfigureAwait(false);

        lock (_cacheLock)
        {
            _cachedToken     = exchanged.token;
            _cachedExpiresAt = exchanged.expiresAt;
        }

        return BuildAppCheckToken(exchanged.token, exchanged.expiresAt);
    }

    private async Task<(string token, DateTime expiresAt)> ExchangeDebugTokenAsync()
    {
        // Endpoint público da App Check API (não requer OAuth — só o API key
        // do projeto Firebase, que já é exposto no google-services.json).
        string url = $"https://firebaseappcheck.googleapis.com/v1/projects/-/apps/{_appId}:exchangeDebugToken?key={_apiKey}";
        string body = $"{{\"debugToken\":\"{_debugUuid}\"}}";

        using (var req = new UnityWebRequest(url, "POST"))
        {
            req.uploadHandler   = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            var op = req.SendWebRequest();
            while (!op.isDone) await Task.Yield();

            if (req.result != UnityWebRequest.Result.Success)
            {
                throw new Exception(
                    $"exchangeDebugToken falhou: status={req.responseCode} " +
                    $"error={req.error} body={req.downloadHandler?.text}");
            }

            return ParseExchangeResponse(req.downloadHandler.text);
        }
    }

    /// <summary>
    /// Resposta do endpoint:
    ///   { "token": "<jwt>", "ttl": "3600s" }
    /// Parse manual (sem JsonUtility, porque "ttl" é string com "s" no fim).
    /// </summary>
    private static (string token, DateTime expiresAt) ParseExchangeResponse(string json)
    {
        string token = ExtractJsonString(json, "token");
        if (string.IsNullOrEmpty(token))
            throw new Exception($"Resposta sem campo 'token': {json}");

        string ttl = ExtractJsonString(json, "ttl");  // ex.: "3600s"
        int ttlSeconds = 3600;
        if (!string.IsNullOrEmpty(ttl) && ttl.EndsWith("s"))
            int.TryParse(ttl.Substring(0, ttl.Length - 1), out ttlSeconds);

        return (token, DateTime.UtcNow.AddSeconds(ttlSeconds));
    }

    private static string ExtractJsonString(string json, string key)
    {
        // Parser super simples — assume JSON bem formado (vindo do Google).
        string marker = $"\"{key}\":\"";
        int start = json.IndexOf(marker, StringComparison.Ordinal);
        if (start < 0) return null;
        start += marker.Length;
        int end = json.IndexOf('"', start);
        if (end < 0) return null;
        return json.Substring(start, end - start);
    }

    /// <summary>
    /// Constrói um AppCheckToken via reflection. O construtor é internal no
    /// Firebase Unity SDK 13.x, então caímos pra reflection. Se a API mudar,
    /// este método é o ponto único a atualizar.
    /// </summary>
    private static AppCheckToken BuildAppCheckToken(string token, DateTime expiresAt)
    {
        var type = typeof(AppCheckToken);
        var ctor = type.GetConstructor(
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Public,
            null,
            new[] { typeof(string), typeof(long) },
            null);

        if (ctor != null)
        {
            long expiresMillis = new DateTimeOffset(expiresAt).ToUnixTimeMilliseconds();
            return (AppCheckToken)ctor.Invoke(new object[] { token, expiresMillis });
        }

        // Fallback: setter de propriedades (algumas versões do SDK expõem set).
        var instance = (AppCheckToken)Activator.CreateInstance(type, nonPublic: true);
        var tokenProp = type.GetProperty("Token");
        tokenProp?.SetValue(instance, token);
        // expiração não é crítica — o SDK refaz se precisar.
        return instance;
    }
}
#endif
