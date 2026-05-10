using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Avalia respostas dissertativas usando a API da Anthropic (claude-haiku).
///
/// O prompt envia a resposta do aluno + a lista de prerequisites e pede à LLM
/// que identifique quais prerequisites estão presentes na resposta, retornando
/// um JSON estruturado com o resultado.
/// </summary>
public class AnthropicEvaluator : IOpenAnswerEvaluator
{
    private const string ApiUrl    = "https://api.anthropic.com/v1/messages";
    private const string Model     = "claude-haiku-4-5-20251001";
    private const string ApiVersion = "2023-06-01";
    private const int    MaxTokens = 256;

    private readonly string _apiKey;

    public AnthropicEvaluator(string apiKey)
    {
        _apiKey = apiKey;
    }

    // ── Interface ──────────────────────────────────────────────────────────────

    public async Task<OpenAnswerResult> EvaluateAsync(string answerText, List<string> prerequisites)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
            Debug.LogError("[AnthropicEvaluator] API key não configurada em SecretsConfig.");
            return OpenAnswerResult.Failure("API key ausente.");
        }

        if (prerequisites == null || prerequisites.Count == 0)
        {
            Debug.LogWarning("[AnthropicEvaluator] Questão sem prerequisites — score 0/0.");
            return OpenAnswerResult.Success(0, 0);
        }

        try
        {
            string requestBody = BuildRequestBody(answerText, prerequisites);
            string responseJson = await PostAsync(requestBody);
            return ParseResponse(responseJson, prerequisites.Count);
        }
        catch (Exception e)
        {
            Debug.LogError($"[AnthropicEvaluator] Erro ao chamar API: {e.Message}");
            return OpenAnswerResult.Failure(e.Message);
        }
    }

    // ── Request Builder ────────────────────────────────────────────────────────

    private string BuildRequestBody(string answerText, List<string> prerequisites)
    {
        // Monta a lista numerada de prerequisites para o prompt
        var prereqList = new StringBuilder();
        for (int i = 0; i < prerequisites.Count; i++)
            prereqList.AppendLine($"{i + 1}. {prerequisites[i]}");

        string systemPrompt =
            "You are an educational answer evaluator. " +
            "Your task is to check which prerequisite concepts appear in a student's answer. " +
            "Be lenient: accept paraphrases, synonyms, and partial descriptions as valid. " +
            "Respond ONLY with a valid JSON object, no explanation, no markdown.";

        string userPrompt =
            $"Prerequisites (concepts that should be present in the answer):\n{prereqList}\n" +
            $"Student's answer:\n\"{answerText}\"\n\n" +
            "Return ONLY this JSON (no markdown, no extra text):\n" +
            "{\"found_count\": <int>, \"total_count\": <int>}";

        // Serialização manual para evitar dependência de Newtonsoft neste arquivo
        string escapedSystem = EscapeJson(systemPrompt);
        string escapedUser   = EscapeJson(userPrompt);

        return
            "{" +
                $"\"model\":\"{Model}\"," +
                $"\"max_tokens\":{MaxTokens}," +
                $"\"system\":\"{escapedSystem}\"," +
                "\"messages\":[" +
                    "{" +
                        "\"role\":\"user\"," +
                        $"\"content\":\"{escapedUser}\"" +
                    "}" +
                "]" +
            "}";
    }

    // ── HTTP ───────────────────────────────────────────────────────────────────

    private Task<string> PostAsync(string jsonBody)
    {
        var tcs     = new TaskCompletionSource<string>();
        var request = new UnityWebRequest(ApiUrl, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler   = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type",       "application/json");
        request.SetRequestHeader("x-api-key",          _apiKey);
        request.SetRequestHeader("anthropic-version",  ApiVersion);

        var operation = request.SendWebRequest();
        operation.completed += _ =>
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                tcs.SetResult(request.downloadHandler.text);
            }
            else
            {
                string errorBody = request.downloadHandler?.text ?? "(sem corpo)";
                tcs.SetException(new Exception(
                    $"HTTP {request.responseCode}: {request.error} | {errorBody}"));
            }
            request.Dispose();
        };

        return tcs.Task;
    }

    // ── Response Parser ────────────────────────────────────────────────────────

    /// <summary>
    /// Extrai found_count e total_count do JSON retornado pela LLM.
    ///
    /// A LLM retorna o payload dentro de choices[0].message.content (formato
    /// Anthropic Messages API):
    ///   {"id":"...", "content":[{"type":"text","text":"{\"found_count\":2,\"total_count\":3}"}], ...}
    /// </summary>
    private OpenAnswerResult ParseResponse(string responseJson, int expectedTotal)
    {
        try
        {
            // Extrai o campo "text" do primeiro content block
            string textContent = ExtractTextField(responseJson, "\"text\":");
            if (string.IsNullOrEmpty(textContent))
            {
                Debug.LogError($"[AnthropicEvaluator] Campo 'text' não encontrado na resposta: {responseJson}");
                return OpenAnswerResult.Failure("Resposta inesperada da API.");
            }

            int foundCount = ExtractIntField(textContent, "found_count");
            int totalCount = ExtractIntField(textContent, "total_count");

            // Sanity check: o total da LLM deve bater com o total local
            if (totalCount != expectedTotal)
            {
                Debug.LogWarning(
                    $"[AnthropicEvaluator] total_count da LLM ({totalCount}) " +
                    $"difere do esperado ({expectedTotal}). Usando valor local.");
                totalCount = expectedTotal;
            }

            Debug.Log($"[AnthropicEvaluator] Avaliação: {foundCount}/{totalCount}");
            return OpenAnswerResult.Success(foundCount, totalCount);
        }
        catch (Exception e)
        {
            Debug.LogError($"[AnthropicEvaluator] Erro ao parsear resposta: {e.Message}\nResposta: {responseJson}");
            return OpenAnswerResult.Failure("Erro ao interpretar resposta da API.");
        }
    }

    // ── Mini JSON helpers (evita dependência de Newtonsoft aqui) ──────────────

    /// <summary>Extrai o valor string de uma chave JSON como "key":"value".</summary>
    private static string ExtractTextField(string json, string keyWithColon)
    {
        int keyIdx = json.IndexOf(keyWithColon, StringComparison.Ordinal);
        if (keyIdx < 0) return null;

        int start = json.IndexOf('"', keyIdx + keyWithColon.Length);
        if (start < 0) return null;
        start++; // pula a aspas de abertura

        int end = start;
        while (end < json.Length)
        {
            if (json[end] == '\\') { end += 2; continue; }
            if (json[end] == '"') break;
            end++;
        }

        return json.Substring(start, end - start);
    }

    /// <summary>Extrai o valor inteiro de uma chave JSON como "key": 3.</summary>
    private static int ExtractIntField(string json, string key)
    {
        string fullKey = $"\"{key}\":";
        int keyIdx = json.IndexOf(fullKey, StringComparison.Ordinal);
        if (keyIdx < 0) throw new Exception($"Campo '{key}' não encontrado no JSON.");

        int valueStart = keyIdx + fullKey.Length;
        // Pula espaços
        while (valueStart < json.Length && json[valueStart] == ' ') valueStart++;

        int valueEnd = valueStart;
        while (valueEnd < json.Length && (char.IsDigit(json[valueEnd]) || json[valueEnd] == '-'))
            valueEnd++;

        return int.Parse(json.Substring(valueStart, valueEnd - valueStart));
    }

    /// <summary>Escapa caracteres especiais para uso dentro de uma string JSON.</summary>
    private static string EscapeJson(string s) =>
        s.Replace("\\", "\\\\")
         .Replace("\"", "\\\"")
         .Replace("\n", "\\n")
         .Replace("\r", "\\r")
         .Replace("\t", "\\t");
}
