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
/// Editor Script para fazer upload e limpeza de imagens no Firebase Storage.
///
/// Uso — Upload:
///   1. Tools > Upload Question Images to Storage
///   2. Selecione o ambiente (Dev ou Prod)
///   3. Clique "Verificar imagens" → "Enviar para Storage"
///
/// Uso — Limpeza de órfãos:
///   1. Selecione o ambiente
///   2. Clique "Verificar órfãos" para listar arquivos no bucket sem referência
///   3. Clique "Apagar órfãos" para removê-los do Storage
///
/// Pré-requisito: gsutil instalado e autenticado (gcloud auth login).
///
/// Layout no Storage:
///   Question/<topic>/<filename>.png
/// </summary>
public class UploadQuestionImagesToStorageEditor : EditorWindow
{
    // ── Configuração dos buckets ───────────────────────────────────────────────

    private const string BucketDev   = "microlearning-dev-79c0c.firebasestorage.app";
    private const string BucketProd  = "microlearning-33132.firebasestorage.app";
    private const string StorageRoot = "Question";

    // ── Estado da janela ───────────────────────────────────────────────────────

    private FirebaseEnvironment targetEnvironment = FirebaseEnvironment.Dev;
    private bool skipExisting = true;
    private bool isRunning    = false;
    private string statusLog  = "";
    private Vector2 scrollPosition;

    // Upload
    private List<ImageUploadJob> pendingJobs = new List<ImageUploadJob>();
    private bool jobsScanned = false;

    // Limpeza de órfãos
    private List<string> orphanKeys = new List<string>(); // storage keys órfãs
    private bool orphansScanned = false;

    // ── Abertura ───────────────────────────────────────────────────────────────

    [MenuItem("Tools/Upload Question Images to Storage")]
    public static void ShowWindow()
    {
        var window = GetWindow<UploadQuestionImagesToStorageEditor>("Upload Images to Storage");
        window.minSize = new Vector2(620, 580);
    }

    private void OnEnable()
    {
        statusLog = "Clique em \"Verificar imagens\" para escanear uploads pendentes.\n";
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

        GUI.enabled = !isRunning;

        // ── Seção: Upload ──────────────────────────────────────────────────────
        GUILayout.Label("📤 Upload de imagens novas", EditorStyles.boldLabel);

        skipExisting = EditorGUILayout.ToggleLeft(
            "Pular imagens já existentes no Storage (recomendado)", skipExisting);
        GUILayout.Space(4);

        if (GUILayout.Button("🔍 Verificar imagens", GUILayout.Height(32)))
        {
            orphansScanned = false;
            ScanImages();
        }

        if (jobsScanned && pendingJobs.Count > 0)
        {
            EditorGUILayout.HelpBox($"{pendingJobs.Count} imagem(ns) para enviar.", MessageType.None);
            if (GUILayout.Button("🚀 Enviar para Storage", GUILayout.Height(40)))
                EditorApplication.delayCall += RunUpload;
        }
        else if (jobsScanned && pendingJobs.Count == 0)
        {
            EditorGUILayout.HelpBox("✅ Nenhuma imagem nova para enviar.", MessageType.Info);
        }

        GUILayout.Space(12);

        // ── Seção: Limpeza de órfãos ───────────────────────────────────────────
        GUILayout.Label("🗑  Limpeza de imagens órfãs", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Lista arquivos que existem no Storage mas não são mais referenciados " +
            "por nenhuma questão nos databases C#.",
            MessageType.None);
        GUILayout.Space(4);

        if (GUILayout.Button("🔍 Verificar órfãos", GUILayout.Height(32)))
        {
            jobsScanned = false;
            ScanOrphans();
        }

        if (orphansScanned && orphanKeys.Count > 0)
        {
            EditorGUILayout.HelpBox(
                $"{orphanKeys.Count} arquivo(s) órfão(s) encontrado(s) no Storage.",
                MessageType.Warning);

            if (GUILayout.Button($"🗑  Apagar {orphanKeys.Count} arquivo(s) órfão(s)", GUILayout.Height(40)))
            {
                bool confirm = EditorUtility.DisplayDialog(
                    "Confirmar exclusão",
                    $"Isso vai apagar {orphanKeys.Count} arquivo(s) de " +
                    $"gs://{bucket}/{StorageRoot}/\n\nEssa ação não pode ser desfeita.",
                    "Apagar", "Cancelar");

                if (confirm)
                    EditorApplication.delayCall += DeleteOrphans;
            }
        }
        else if (orphansScanned && orphanKeys.Count == 0)
        {
            EditorGUILayout.HelpBox("✅ Nenhum órfão encontrado no Storage.", MessageType.Info);
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

    // ── Escaneamento de uploads ────────────────────────────────────────────────

    private void ScanImages()
    {
        statusLog = "Escaneando imagens...\n";
        pendingJobs.Clear();
        jobsScanned = false;

        var allJobs = new Dictionary<string, ImageUploadJob>();

        foreach (var db in GetAllDatabases())
        {
            foreach (var q in db.GetQuestions())
            {
                foreach (string storageKey in QuestionStorageKeys.AllForQuestion(q))
                {
                    if (allJobs.ContainsKey(storageKey)) continue;
                    allJobs[storageKey] = new ImageUploadJob
                    {
                        StorageKey = storageKey,
                        LocalPath  = FindLocalFile(storageKey),
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
                statusLog += $"⚠️  Arquivo não encontrado em Resources: {job.StorageKey}\n";
                missing++;
            }
            else
            {
                pendingJobs.Add(job);
            }
        }

        jobsScanned = true;
        statusLog += $"\n✓ {pendingJobs.Count} arquivo(s) localizado(s)";
        if (missing > 0) statusLog += $" | ⚠️  {missing} não localizado(s)";
        statusLog += "\n";
        Repaint();
    }

    // ── Escaneamento de órfãos ─────────────────────────────────────────────────

    private void ScanOrphans()
    {
        orphanKeys.Clear();
        orphansScanned = false;
        statusLog = "Listando arquivos no Storage...\n";
        Repaint();

        string bucket = targetEnvironment == FirebaseEnvironment.Dev ? BucketDev : BucketProd;
        string gsPath = $"gs://{bucket}/{StorageRoot}/";

        // Coleta todas as storage keys referenciadas nos databases
        var referencedKeys = new HashSet<string>();
        foreach (var db in GetAllDatabases())
            foreach (var q in db.GetQuestions())
                foreach (string k in QuestionStorageKeys.AllForQuestion(q))
                    referencedKeys.Add(k);

        statusLog += $"  {referencedKeys.Count} storage keys referenciadas nos databases.\n";

        // Lista todos os arquivos no bucket sob Question/
        var result = RunProcess("gsutil", $"ls -r \"{gsPath}\"");
        if (result.ExitCode != 0)
        {
            statusLog += $"❌ Erro ao listar o Storage:\n{result.StdErr}\n";
            Repaint();
            return;
        }

        // Cada linha é algo como:
        //   gs://bucket/Question/aminoacids/isoleucina.png
        int total = 0;
        foreach (string line in result.StdOut.Split('\n'))
        {
            string trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed.EndsWith("/")) continue; // pula diretórios

            // Extrai a storage key: remove o prefixo "gs://bucket/Question/" e a extensão
            string prefix = $"gs://{bucket}/{StorageRoot}/";
            if (!trimmed.StartsWith(prefix)) continue;

            string relativePath = trimmed.Substring(prefix.Length); // ex: "aminoacids/isoleucina.png"
            string storageKey   = Path.ChangeExtension(relativePath, null); // remove .png
            total++;

            if (!referencedKeys.Contains(storageKey))
            {
                orphanKeys.Add(storageKey);
                statusLog += $"👻 {storageKey}\n";
            }
        }

        orphansScanned = true;
        statusLog += $"\n📦 Total no Storage: {total} | 👻 Órfãos: {orphanKeys.Count}\n";
        Repaint();
    }

    // ── Exclusão de órfãos ─────────────────────────────────────────────────────

    private void DeleteOrphans()
    {
        if (orphanKeys.Count == 0) return;

        isRunning = true;
        string bucket = targetEnvironment == FirebaseEnvironment.Dev ? BucketDev : BucketProd;
        statusLog += $"\nApagando {orphanKeys.Count} arquivo(s) órfão(s)...\n";

        int deleted = 0;
        int errors  = 0;

        foreach (string storageKey in orphanKeys)
        {
            string gsPath = $"gs://{bucket}/{StorageRoot}/{storageKey}.png";
            var result = RunProcess("gsutil", $"rm \"{gsPath}\"");

            if (result.ExitCode == 0)
            {
                deleted++;
                statusLog += $"🗑  {storageKey}\n";
            }
            else
            {
                errors++;
                statusLog += $"❌ {storageKey} — {result.StdErr.Trim()}\n";
            }

            Repaint();
        }

        statusLog += $"\n─────────────────────────────────────\n";
        statusLog += $"🗑  Apagados: {deleted} | ❌ Erros: {errors}\n";

        orphanKeys.Clear();
        orphansScanned = false;
        isRunning = false;
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

        int success = 0, skipped = 0, errors = 0;

        foreach (var job in pendingJobs)
        {
            if (job.LocalPath == null) continue;

            string destPath = $"gs://{bucket}/{StorageRoot}/{job.StorageKey}.png";

            try
            {
                string skipFlag = skipExisting ? " -n" : "";
                var result = RunProcess("gsutil", $"cp{skipFlag} -a public-read \"{job.LocalPath}\" \"{destPath}\"");

                if (result.ExitCode == 0)
                {
                    success++;
                    statusLog += $"✅ {job.StorageKey}\n";
                }
                else if (skipExisting && result.StdOut.Contains("Skipping"))
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
    /// </summary>
    private static string FindLocalFile(string storageKey)
    {
        string filename      = Path.GetFileName(storageKey);
        string resourcesPath = Path.Combine(Application.dataPath, "Resources");

        foreach (string searchRoot in new[] { "AnswerImages", "QuestionImages" })
        {
            string dir = Path.Combine(resourcesPath, searchRoot);
            if (!Directory.Exists(dir)) continue;

            string[] candidates = Directory.GetFiles(dir, $"{filename}.png", SearchOption.AllDirectories);
            if (candidates.Length > 0) return candidates[0];
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
                statusLog = $"✓ gsutil: {result.StdOut.Trim().Split('\n')[0]}\n\n" + statusLog;
            else
                AppendGsutilWarning();
        }
        catch { AppendGsutilWarning(); }
    }

    private void AppendGsutilWarning()
    {
        statusLog = "⚠️  gsutil não encontrado.\n\n" +
                    "Para instalar:\n" +
                    "  brew install --cask google-cloud-sdk\n" +
                    "  gcloud auth login\n\n" + statusLog;
    }

    private static ProcessResult RunProcess(string command, string args)
    {
        string fullCommand = $"{command} {args}";
        var psi = new ProcessStartInfo("/bin/bash", $"-l -c \"{EscapeForShell(fullCommand)}\"")
        {
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
            UseShellExecute        = false,
            CreateNoWindow         = true
        };

        using var p = Process.Start(psi);
        string stdout = p.StandardOutput.ReadToEnd();
        string stderr = p.StandardError.ReadToEnd();
        p.WaitForExit();
        return new ProcessResult { ExitCode = p.ExitCode, StdOut = stdout, StdErr = stderr };
    }

    private static string EscapeForShell(string cmd) => cmd.Replace("\"", "\\\"");

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
        public string StorageKey;
        public string LocalPath;
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
