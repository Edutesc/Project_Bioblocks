using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Avalia respostas dissertativas usando a Google Gemini API (gemini-2.0-flash).
///
/// Free tier: sem custo, sem cartão de crédito.
/// Limites: 15 RPM e 1 milhão de tokens/dia — suficiente para milhares de alunos.
///
/// Fluxo:
///   1. Monta prompt de rubric com a lista de prerequisites da questão.
///   2. Solicita resposta em JSON puro via responseMimeType = "application/json".
///   3. Parseia {"found_count": N, "total_count": N} e devolve OpenAnswerResult.
/// </summary>
public class GeminiEvaluator : IOpenAnswerEvaluator
{
    private const string Model     = "gemini-2.5-flash";
    private const int    MaxTokens = 1024;

    private readonly string _apiKey;

    // URL montada no construtor para não recalcular a cada chamada
    private readonly string _apiUrl;

    public GeminiEvaluator(string apiKey)
    {
        _apiKey = apiKey;
        _apiUrl = $"https://generativelanguage.googleapis.com/v1/models/{Model}:generateContent?key={_apiKey}";
    }

    // ── Interface ──────────────────────────────────────────────────────────────

    public async Task<OpenAnswerResult> EvaluateAsync(string answerText, List<string> prerequisites)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
            Debug.LogError("[GeminiEvaluator] API key não configurada em SecretsConfig.");
            return OpenAnswerResult.Failure("API key ausente.");
        }

        if (prerequisites == null || prerequisites.Count == 0)
        {
            Debug.LogWarning("[GeminiEvaluator] Questão sem prerequisites — score 0/0.");
            return OpenAnswerResult.Success(0, 0);
        }

        try
        {
            string requestBody  = BuildRequestBody(answerText, prerequisites);
            string responseJson = await PostAsync(requestBody);
            return ParseResponse(responseJson, prerequisites.Count);
        }
        catch (Exception e)
        {
            Debug.LogError($"[GeminiEvaluator] Erro ao chamar API: {e.Message}");
            return OpenAnswerResult.Failure(e.Message);
        }
    }

    // ── Request Builder ────────────────────────────────────────────────────────

    private string BuildRequestBody(string answerText, List<string> prerequisites)
    {
        // Lista numerada de prerequisites para o prompt
        var prereqList = new StringBuilder();
        for (int i = 0; i < prerequisites.Count; i++)
            prereqList.AppendLine($"{i + 1}. {prerequisites[i]}");

        // v1 não suporta system_instruction nem responseMimeType —
        // incorporamos o system prompt diretamente na mensagem do usuário.
        string fullPrompt =
            "You are an educational answer evaluator. " +
            "Your task is to check which prerequisite concepts appear in a student's answer. " +
            "Be lenient: accept paraphrases, synonyms, and partial descriptions as valid.\n\n" +
            $"Prerequisites (concepts that should be present in the answer):\n{prereqList}\n" +
            $"Student's answer:\n\"{answerText}\"\n\n" +
            "Respond ONLY with this JSON, no explanation, no markdown:\n" +
            "{\"found_count\": <int>, \"total_count\": <int>}";

        string escapedPrompt = EscapeJson(fullPrompt);

        return
            "{" +
                "\"contents\":[{" +
                    "\"role\":\"user\"," +
                    "\"parts\":[{\"text\":\"" + escapedPrompt + "\"}]" +
                "}]," +
                "\"generationConfig\":{" +
                    $"\"maxOutputTokens\":{MaxTokens}" +
                "}" +
            "}";
    }

    // ── HTTP ───────────────────────────────────────────────────────────────────

    private Task<string> PostAsync(string jsonBody)
    {
        var tcs     = new TaskCompletionSource<string>();
        var request = new UnityWebRequest(_apiUrl, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler   = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

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
    /// Extrai found_count e total_count do JSON retornado pelo Gemini.
    ///
    /// A Gemini API retorna:
    /// {
    ///   "candidates": [{
    ///     "content": {
    ///       "parts": [{ "text": "{\"found_count\":2,\"total_count\":3}" }]
    ///     }
    ///   }]
    /// }
    ///
    /// Com responseMimeType=application/json, o campo "text" já é JSON puro.
    /// </summary>
    private OpenAnswerResult ParseResponse(string responseJson, int expectedTotal)
    {
        try
        {
            // Extrai o conteúdo do campo "text" dentro de candidates[0].content.parts[0]
            string textContent = ExtractTextField(responseJson, "\"text\":");
            if (string.IsNullOrEmpty(textContent))
            {
                Debug.LogError($"[GeminiEvaluator] Campo 'text' não encontrado na resposta: {responseJson}");
                return OpenAnswerResult.Failure("Resposta inesperada da API.");
            }

            // O campo "text" vem com aspas escapadas (\" → ") dentro do JSON externo.
            // Precisamos desescapar antes de parsear o JSON interno.
            string unescapedText = textContent.Replace("\\\"", "\"").Replace("\\n", "\n");

            int foundCount = ExtractIntField(unescapedText, "found_count");
            int totalCount = ExtractIntField(unescapedText, "total_count");

            // Sanity check: o total da LLM deve bater com o total local
            if (totalCount != expectedTotal)
            {
                Debug.LogWarning(
                    $"[GeminiEvaluator] total_count da LLM ({totalCount}) " +
                    $"difere do esperado ({expectedTotal}). Usando valor local.");
                totalCount = expectedTotal;
            }

            Debug.Log($"[GeminiEvaluator] Avaliação: {foundCount}/{totalCount}");
            return OpenAnswerResult.Success(foundCount, totalCount);
        }
        catch (Exception e)
        {
            Debug.LogError($"[GeminiEvaluator] Erro ao parsear resposta: {e.Message}\nResposta: {responseJson}");
            return OpenAnswerResult.Failure("Erro ao interpretar resposta da API.");
        }
    }

    // ── Mini JSON helpers ──────────────────────────────────────────────────────

    /// <summary>Extrai o valor string de uma chave JSON como "key":"value".</summary>
    private static string ExtractTextField(string json, string keyWithColon)
    {
        int keyIdx = json.IndexOf(keyWithColon, StringComparison.Ordinal);
        if (keyIdx < 0) return null;

        int start = json.IndexOf('"', keyIdx + keyWithColon.Length);
        if (start < 0) return null;
        start++; // pula aspas de abertura

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
