using System;
using System.Collections.Generic;
using System.Text;
using QuestionSystem;
using UnityEngine;
using UnityEngine.Networking;

#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// Script Editor para fazer upload direto dos bancos de dados para Firestore.
/// 
/// Uso:
/// 1. Copie FirebaseSecrets-template.json para Assets/Editor/FirebaseSecrets.json
/// 2. Edite FirebaseSecrets.json com suas secret keys (Dev e Prod)
/// 3. Adicione FirebaseSecrets.json ao .gitignore
/// 4. Adicione os 10 bancos em GetAllDatabases()
/// 5. Tools > Upload Question Banks to Firestore
/// 6. Cole a URL da Cloud Function
/// https://us-central1-microlearning-dev-79c0c.cloudfunctions.net/uploadQuestionBanks
/// 7. Click "Upload" — pronto!
/// 
/// Não precisa digitar secret key — é carregada do arquivo local.
/// </summary>

public class UploadQuestionBanksEditor : EditorWindow
{
    // URLs das Cloud Functions por ambiente
    private const string CloudFunctionUrlDev  = "https://us-central1-microlearning-dev-79c0c.cloudfunctions.net/uploadQuestionBanks";
    private const string CloudFunctionUrlProd = "https://us-central1-microlearning-33132.cloudfunctions.net/uploadQuestionBanks";

    private FirebaseEnvironment targetEnvironment = FirebaseEnvironment.Dev;
    private SecretsData secrets;
    private bool isUploading = false;
    private string uploadStatus = "";
    private Vector2 scrollPosition;

    [MenuItem("Tools/Upload Question Banks to Firestore")]
    public static void ShowWindow()
    {
        var window = GetWindow<UploadQuestionBanksEditor>("Upload Question Banks");
        window.minSize = new Vector2(550, 450);
    }

    private void OnEnable()
    {
        LoadSecrets();
    }

    private void LoadSecrets()
    {
        secrets = null;
        string secretsPath = "Assets/Editor/FirebaseSecrets.json";
        if (System.IO.File.Exists(secretsPath))
        {
            try
            {
                string json = System.IO.File.ReadAllText(secretsPath);
                secrets = JsonUtility.FromJson<SecretsData>(json);
                uploadStatus = "✓ FirebaseSecrets.json carregado.\n";
            }
            catch (System.Exception ex)
            {
                uploadStatus = $"❌ Erro ao carregar FirebaseSecrets.json: {ex.Message}\n";
            }
        }
        else
        {
            uploadStatus = "❌ Arquivo não encontrado: Assets/Editor/FirebaseSecrets.json\n\n" +
                           "Crie o arquivo com o conteúdo:\n" +
                           "{\n  \"devSecretKey\": \"SUA_KEY_DEV\",\n  \"prodSecretKey\": \"SUA_KEY_PROD\"\n}\n";
        }
    }

    private string GetActiveSecretKey()
    {
        if (secrets == null) return null;
        return targetEnvironment == FirebaseEnvironment.Dev
            ? secrets.devSecretKey
            : secrets.prodSecretKey;
    }

    private string GetCloudFunctionUrl()
    {
        return targetEnvironment == FirebaseEnvironment.Dev
            ? CloudFunctionUrlDev
            : CloudFunctionUrlProd;
    }

    private void OnGUI()
    {
        GUILayout.Label("Upload Question Banks to Firestore", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // Secrets não carregadas
        if (secrets == null)
        {
            EditorGUILayout.HelpBox(
                "❌ FirebaseSecrets.json não carregado.\n\n" +
                "Crie Assets/Editor/FirebaseSecrets.json com suas secret keys.",
                MessageType.Error);

            if (GUILayout.Button("Tentar carregar novamente", GUILayout.Height(30)))
                LoadSecrets();

            GUILayout.Space(10);
            GUILayout.Label("Status:", EditorStyles.boldLabel);
            GUILayout.TextArea(uploadStatus, GUILayout.Height(120));
            return;
        }

        // Seleção de ambiente
        EditorGUILayout.LabelField("Ambiente de destino:", EditorStyles.label);
        var newEnv = (FirebaseEnvironment)EditorGUILayout.EnumPopup(targetEnvironment);
        if (newEnv != targetEnvironment)
        {
            targetEnvironment = newEnv;
            uploadStatus = $"Ambiente alterado para {targetEnvironment}.\n";
        }

        GUILayout.Space(6);

        // Validação da secret key do ambiente selecionado
        string activeKey = GetActiveSecretKey();
        bool keyOk = !string.IsNullOrEmpty(activeKey) && activeKey != "SUA_KEY_DEV" && activeKey != "SUA_KEY_PROD";

        if (!keyOk)
        {
            EditorGUILayout.HelpBox(
                $"⚠️  Secret key de {targetEnvironment} não configurada em FirebaseSecrets.json.",
                MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox(
                $"✓ Secret key {targetEnvironment} carregada.\n" +
                $"Cloud Function: {GetCloudFunctionUrl()}",
                MessageType.Info);
        }

        GUILayout.Space(15);

        // Confirmação extra para Prod
        if (targetEnvironment == FirebaseEnvironment.Prod)
        {
            EditorGUILayout.HelpBox(
                "⚠️  ATENÇÃO: você está enviando para PRODUÇÃO.\n" +
                "Isso vai sobrescrever os dados reais dos usuários.",
                MessageType.Warning);
            GUILayout.Space(8);
        }

        // Botão de upload
        GUI.enabled = !isUploading && keyOk;
        if (GUILayout.Button($"🚀 Upload para {targetEnvironment}", GUILayout.Height(50)))
        {
            if (targetEnvironment == FirebaseEnvironment.Prod)
            {
                bool confirm = EditorUtility.DisplayDialog(
                    "Confirmar upload para PRODUÇÃO",
                    "Isso vai sobrescrever as questões no Firestore de PRODUÇÃO.\n\nTem certeza?",
                    "Enviar", "Cancelar");
                if (confirm)
                    EditorApplication.delayCall += UploadDatabases;
            }
            else
            {
                EditorApplication.delayCall += UploadDatabases;
            }
        }
        GUI.enabled = true;

        GUILayout.Space(10);

        // Status
        GUILayout.Label("Status:", EditorStyles.boldLabel);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));
        GUILayout.TextArea(uploadStatus, GUILayout.ExpandHeight(true));
        GUILayout.EndScrollView();

        if (isUploading)
            GUILayout.Label("⏳ Uploading...", EditorStyles.miniLabel);
    }

    private void UploadDatabases()
    {
        isUploading = true;
        uploadStatus = "Coletando bancos de dados...\n";

        try
        {
            // Coleta todos os bancos
            var databases = GetAllDatabases();
            uploadStatus += $"✓ {databases.Count} bancos encontrados\n";

            // Constrói payload
            var payload = new QuestionBanksPayload
            {
                questionBanks = new List<QuestionBankData>()
            };

            int totalQuestions = 0;
            foreach (var db in databases)
            {
                var bankName = db.GetDatabankName();
                var questions = db.GetQuestions();
                totalQuestions += questions.Count;

                var bankData = new QuestionBankData
                {
                    bankName = bankName,
                    questions = new List<QuestionData>()
                };

                foreach (var q in questions)
                {
                    // Traduz paths legados (ex: "AnswerImages/AminoacidsDB/.../isoleucina") para
                    // storage keys (ex: "aminoacids/isoleucina") antes de gravar no Firestore.
                    // Assim o app pode usar o path diretamente, sem precisar de Resolve() em runtime.
                    string resolvedImagePath = q.isImageQuestion && !string.IsNullOrEmpty(q.questionImagePath)
                        ? QuestionSystem.QuestionStorageKeys.Resolve(q.questionImagePath, q.topic) ?? q.questionImagePath
                        : q.questionImagePath;

                    string[] resolvedAnswers = q.isImageAnswer && q.answers != null
                        ? System.Array.ConvertAll(q.answers, a =>
                            QuestionSystem.QuestionStorageKeys.LooksLikeImagePath(a)
                                ? QuestionSystem.QuestionStorageKeys.Resolve(a, q.topic) ?? a
                                : a)
                        : q.answers;

                    var questionData = new QuestionData
                    {
                        globalId = string.IsNullOrEmpty(q.globalId)
                            ? $"{q.topic}_{q.questionNumber:D3}"
                            : q.globalId,
                        questionDatabankName = q.questionDatabankName,
                        questionNumber = q.questionNumber,
                        questionText = q.questionText,
                        answers = resolvedAnswers,
                        correctIndex = q.correctIndex,
                        isImageQuestion = q.isImageQuestion,
                        isImageAnswer = q.isImageAnswer,
                        questionImagePath = resolvedImagePath,
                        questionLevel = q.questionLevel,
                        topic = q.topic,
                        subtopic = q.subtopic,
                        displayName = q.displayName,
                        bloomLevel = q.bloomLevel ?? "unclassified",
                        conceptTags = q.conceptTags ?? new List<string>(),
                        prerequisites = q.prerequisites ?? new List<string>(),
                        questionInDevelopment = q.questionInDevelopment,
                        questionHint = q.questionHint != null ? new QuestionHintData
                        {
                            imagePath = q.questionHint.imagePath ?? "",
                            link = q.questionHint.link ?? "",
                            text = q.questionHint.text ?? "",
                            videoUrl = q.questionHint.videoUrl ?? ""
                        } : new QuestionHintData()
                    };

                    bankData.questions.Add(questionData);
                }

                payload.questionBanks.Add(bankData);
                uploadStatus += $"  • {bankName}: {bankData.questions.Count} questões\n";
            }

            uploadStatus += $"\n✓ Total: {totalQuestions} questões\n";
            uploadStatus += $"Enviando para Firestore {targetEnvironment}...\n";

            // Serializa e envia
            string json = JsonUtility.ToJson(payload);
            SendToCloudFunction(json, GetCloudFunctionUrl(), GetActiveSecretKey());
        }
        catch (Exception ex)
        {
            uploadStatus += $"\n❌ ERRO: {ex.Message}\n{ex.StackTrace}";
            isUploading = false;
        }
    }

    private void SendToCloudFunction(string jsonPayload, string url, string secretKey)
    {
        // Constrói URL com secret key
        string urlWithKey = $"{url}?key={UnityWebRequest.EscapeURL(secretKey)}";

        // Cria request
        var www = new UnityWebRequest(urlWithKey, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Envia e aguarda resposta (blocking)
        var operation = www.SendWebRequest();

        int timeout = 0;
        while (!operation.isDone && timeout < 300) // 5 minutos timeout
        {
            System.Threading.Thread.Sleep(100);
            timeout++;
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            try
            {
                string responseText = www.downloadHandler.text;
                uploadStatus += $"\n✅ SUCESSO!\n\n";
                uploadStatus += "Resposta da Cloud Function:\n";
                uploadStatus += responseText;
            }
            catch (Exception ex)
            {
                uploadStatus += $"\n⚠️  Resposta recebida mas erro ao parsear:\n{ex.Message}\n";
                uploadStatus += www.downloadHandler.text;
            }
        }
        else
        {
            uploadStatus += $"\n❌ ERRO na requisição:\n";
            uploadStatus += $"Status Code: {www.responseCode}\n";
            uploadStatus += $"Error: {www.error}\n";
            uploadStatus += $"Response: {www.downloadHandler.text}\n";
        }

        www.Dispose();
        isUploading = false;
    }

    /// <summary>
    /// ⚠️ ADICIONE AQUI OS 10 BANCOS DE DADOS
    /// </summary>
    private List<IQuestionDatabase> GetAllDatabases()
    {
        var databases = new List<IQuestionDatabase>
        {
            // Substitua pela lista completa dos 10 bancos:
            new AcidBaseBufferQuestionDatabase(),
            new AminoacidQuestionDatabase(),
            new BiochemistryIntroductionQuestionDatabase(),
            new CarbohydratesQuestionDatabase(),
            new EnzymeQuestionDatabase(),
            new LipidsQuestionDatabase(),
            new MembranesQuestionDatabase(),
            new NucleicAcidsQuestionDatabase(),
            new ProteinQuestionDatabase(),
            new WaterQuestionDatabase(),
        };

        return databases;
    }

    // ──────────────────────────────────────────────────────────────
    // Classes serializáveis
    // ──────────────────────────────────────────────────────────────

    [System.Serializable]
    public class QuestionBanksPayload
    {
        public List<QuestionBankData> questionBanks;
    }

    [System.Serializable]
    public class QuestionBankData
    {
        public string bankName;
        public List<QuestionData> questions;
    }

    [System.Serializable]
    public class QuestionData
    {
        public string globalId;
        public string questionDatabankName;
        public int questionNumber;
        public string questionText;
        public string[] answers;
        public int correctIndex;
        public bool isImageQuestion;
        public bool isImageAnswer;
        public string questionImagePath;
        public int questionLevel;
        public string topic;
        public string subtopic;
        public string displayName;
        public string bloomLevel;
        public List<string> conceptTags;
        public List<string> prerequisites;
        public bool questionInDevelopment;
        public QuestionHintData questionHint;
    }

    [System.Serializable]
    public class QuestionHintData
    {
        public string imagePath;
        public string link;
        public string text;
        public string videoUrl;
    }

    [System.Serializable]
    public class SecretsData
    {
        public string devSecretKey;
        public string prodSecretKey;
    }
}

#endif