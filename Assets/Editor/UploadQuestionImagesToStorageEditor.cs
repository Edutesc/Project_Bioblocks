#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using QuestionSystem;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Editor Script para fazer upload das imagens de questões para o Firebase Storage.
///
/// Uso:
///   1. Tools > Upload Question Images to Storage
///   2. Selecione o ambiente (Dev ou Prod)
///   3. Clique "Verificar imagens" para ver quais precisam ser enviadas
///   4. Clique "Upload" para enviar
///
/// Pré-requisito: gsutil instalado e autenticado (gcloud auth login ou
/// gcloud auth application-default login). Verifique com: gsutil version
///
/// Layout no Storage:
///   Question/<topic>/<filename>.png
///   Ex.: Question/aminoacids/isoleucina.png
/// </summary>
public class UploadQuestionImagesToStorageEditor : EditorWindow
{
    // ── Configuração dos buckets ───────────────────────────────────────────────

    private const string BucketDev  = "microlearning-dev-79c0c.firebasestorage.app";
    private const string BucketProd = "microlearning-33132.firebasestorage.app";
    private const string StorageRoot = "Question";

    // ── Estado da janela ───────────────────────────────────────────────────────

    private FirebaseEnvironment targetEnvironment = FirebaseEnvironment.Dev;
    private bool skipExisting = true;
    private bool isRunning = false;
    private string statusLog = "";
    private Vector2 scrollPosition;

    private List<ImageUploadJob> pendingJobs = new List<ImageUploadJob>();
    private bool jobsScanned = false;

    // ── Abertura ───────────────────────────────────────────────────────────────

    [MenuItem("Tools/Upload Question Images to Storage")]
    public static void ShowWindow()
    {
        var window = GetWindow<UploadQuestionImagesToStorageEditor>("Upload Images to Storage");
        window.minSize = new Vector2(600, 520);
    }

    private void OnEnable()
    {
        statusLog = "Clique em \"Verificar imagens\" para escanear quais precisam ser enviadas.\n";
        CheckGsutil();
    }

    // ── Interface ──────────────────────────────────────────────────────────────

    private void OnGUI()
    {
        GUILayout.Label("Upload Question Images to Firebase Storage", EditorStyles.boldLabel);
        GUILayout.Space(8);

        // Ambiente
        EditorGUILayout.LabelField("Ambiente de destino:", EditorStyles.label);
        targetEnvironment = (FirebaseEnvironment)EditorGUILayout.EnumPopup(targetEnvironment);
        string bucket = targetEnvironment == FirebaseEnvironment.Dev ? BucketDev : BucketProd;
        EditorGUILayout.HelpBox($"Bucket: gs://{bucket}/{StorageRoot}/", MessageType.Info);
        GUILayout.Space(8);

        // Opções
        skipExisting = EditorGUILayout.ToggleLeft(
            "Pular imagens já existentes no Storage (recomendado)", skipExisting);
        GUILayout.Space(8);

        // Botão de escaneamento
        GUI.enabled = !isRunning;
        if (GUILayout.Button("🔍 Verificar imagens", GUILayout.Height(36)))
        {
            ScanImages();
        }

        // Resumo após escaneamento
        if (jobsScanned && pendingJobs.Count > 0)
        {
            GUILayout.Space(6);
            EditorGUILayout.HelpBox(
                $"{pendingJobs.Count} imagem(ns) encontrada(s) para enviar.",
                MessageType.None);

            if (GUILayout.Button("🚀 Enviar para Storage", GUILayout.Height(46)))
            {
                EditorApplication.delayCall += () => RunUpload();
            }
        }
        else if (jobsScanned && pendingJobs.Count == 0)
        {
            EditorGUILayout.HelpBox("✅ Nenhuma imagem nova para enviar.", MessageType.Info);
        }

        GUI.enabled = true;

        // Log
        GUILayout.Space(10);
        GUILayout.Label("Log:", EditorStyles.boldLabel);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));
        GUILayout.TextArea(statusLog, GUILayout.ExpandHeight(true));
        GUILayout.EndScrollView();

        if (isRunning)
            GUILayout.Label("⏳ Executando...", EditorStyles.miniLabel);
    }

    // ── Escaneamento ───────────────────────────────────────────────────────────

    private void ScanImages()
    {
        statusLog = "Escaneando imagens...\n";
        pendingJobs.Clear();
        jobsScanned = false;

        var databases = GetAllDatabases();
        var allJobs = new Dictionary<string, ImageUploadJob>(); // storageKey → job

        foreach (var db in databases)
        {
            foreach (var q in db.GetQuestions())
            {
                foreach (string storageKey in QuestionStorageKeys.AllForQuestion(q))
                {
                    if (allJobs.ContainsKey(storageKey)) continue;

                    string localPath = FindLocalFile(storageKey, q.topic);
                    allJobs[storageKey] = new ImageUploadJob
                    {
                        StorageKey = storageKey,
                        LocalPath  = localPath,
                        Topic      = q.topic
                    };
                }
            }
        }

        int missing = 0;
        foreach (var job in allJobs.Values)
        {
            if (job.LocalPath == null)
            {
                statusLog += $"⚠️  Arquivo não encontrado em Resources para: {job.StorageKey}\n";
                missing++;
            }
            else
            {
                pendingJobs.Add(job);
            }
        }

        jobsScanned = true;
        statusLog += $"\n✓ {pendingJobs.Count} arquivo(s) localizado(s)";
        if (missing > 0)
            statusLog += $" | ⚠️  {missing} não localizado(s) em Resources";
        statusLog += "\n";

        Repaint();
    }

    // ── Upload ─────────────────────────────────────────────────────────────────

    private void RunUpload()
    {
        if (pendingJobs.Count == 0)
        {
            statusLog += "Nenhuma imagem para enviar.\n";
            return;
        }

        isRunning = true;
        string bucket = targetEnvironment == FirebaseEnvironment.Dev ? BucketDev : BucketProd;
        statusLog += $"\nIniciando upload para gs://{bucket}/{StorageRoot}/\n";
        statusLog += $"Total: {pendingJobs.Count} arquivo(s)\n\n";

        int success = 0;
        int skipped = 0;
        int errors  = 0;

        foreach (var job in pendingJobs)
        {
            if (job.LocalPath == null) continue;

            string destPath = $"gs://{bucket}/{StorageRoot}/{job.StorageKey}.png";

            try
            {
                // gsutil -q: modo silencioso; -n: skip se já existe (quando skipExisting=true)
                string skipFlag = skipExisting ? " -n" : "";
                string args = $"cp{skipFlag} -a public-read \"{job.LocalPath}\" \"{destPath}\"";

                var result = RunProcess("gsutil", args);

                if (result.ExitCode == 0)
                {
                    success++;
                    statusLog += $"✅ {job.StorageKey}\n";
                }
                else if (result.ExitCode == 1 && skipExisting && result.StdErr.Contains("Skipping"))
                {
                    skipped++;
                    statusLog += $"⏭  {job.StorageKey} (já existe)\n";
                }
                else
                {
                    errors++;
                    statusLog += $"❌ {job.StorageKey}\n   {result.StdErr.Trim()}\n";
                }
            }
            catch (Exception e)
            {
                errors++;
                statusLog += $"❌ {job.StorageKey} — {e.Message}\n";
            }

            Repaint();
        }

        statusLog += $"\n─────────────────────────────────────\n";
        statusLog += $"✅ Enviados: {success} | ⏭ Pulados: {skipped} | ❌ Erros: {errors}\n";

        isRunning = false;
        Repaint();
    }

    // ── Localização de arquivos em Resources ───────────────────────────────────

    /// <summary>
    /// Dado uma storageKey como "aminoacids/isoleucina", busca o arquivo .png
    /// em Assets/Resources/AnswerImages/** e Assets/Resources/QuestionImages/**
    /// pelo nome do arquivo (sem extensão), retornando o caminho absoluto ou null.
    ///
    /// Isso é necessário porque o Resolve() descarta as subpastas intermediárias
    /// (ex: "aminoacid_images/") e mantém apenas o filename.
    /// </summary>
    private static string FindLocalFile(string storageKey, string topic)
    {
        string filename = Path.GetFileName(storageKey); // ex: "isoleucina"
        string resourcesPath = Path.Combine(Application.dataPath, "Resources");

        // Busca em AnswerImages e QuestionImages recursivamente
        foreach (string searchRoot in new[] { "AnswerImages", "QuestionImages" })
        {
            string dir = Path.Combine(resourcesPath, searchRoot);
            if (!Directory.Exists(dir)) continue;

            string[] candidates = Directory.GetFiles(dir, $"{filename}.png", SearchOption.AllDirectories);
            if (candidates.Length > 0)
                return candidates[0]; // pega o primeiro match (filenames são únicos por topic)
        }

        return null;
    }

    // ── Utilitários ────────────────────────────────────────────────────────────

    private void CheckGsutil()
    {
        try
        {
            var result = RunProcess("gsutil", "version");
            if (result.ExitCode == 0)
                statusLog = $"✓ gsutil disponível: {result.StdOut.Trim().Split('\n')[0]}\n";
            else
                AppendGsutilWarning();
        }
        catch
        {
            AppendGsutilWarning();
        }
    }

    private void AppendGsutilWarning()
    {
        statusLog = "⚠️  gsutil não encontrado.\n\n" +
                    "Para instalar:\n" +
                    "  brew install --cask google-cloud-sdk\n" +
                    "  gcloud auth login\n\n" +
                    "Ou via pip:\n" +
                    "  pip install gsutil\n";
    }

    private static ProcessResult RunProcess(string command, string args)
    {
        var psi = new ProcessStartInfo(command, args)
        {
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
            UseShellExecute        = false,
            CreateNoWindow         = true
        };

        // Garante que o PATH do sistema esteja disponível (necessário no macOS Editor)
        psi.EnvironmentVariables["PATH"] =
            "/usr/local/bin:/usr/bin:/bin:/usr/sbin:/sbin:/opt/homebrew/bin:" +
            (psi.EnvironmentVariables.ContainsKey("PATH") ? psi.EnvironmentVariables["PATH"] : "");

        using var p = Process.Start(psi);
        string stdout = p.StandardOutput.ReadToEnd();
        string stderr = p.StandardError.ReadToEnd();
        p.WaitForExit();
        return new ProcessResult { ExitCode = p.ExitCode, StdOut = stdout, StdErr = stderr };
    }

    private static List<IQuestionDatabase> GetAllDatabases() => new List<IQuestionDatabase>
    {
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

    // ── Tipos internos ─────────────────────────────────────────────────────────

    private class ImageUploadJob
    {
        public string StorageKey; // ex: "aminoacids/isoleucina"
        public string LocalPath;  // caminho absoluto em Assets/Resources/
        public string Topic;
    }

    private struct ProcessResult
    {
        public int    ExitCode;
        public string StdOut;
        public string StdErr;
    }
}
#endif
