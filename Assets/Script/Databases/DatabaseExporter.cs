using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using QuestionSystem;

/// <summary>
/// Exporta dados de todas as databases de questões para JSON.
/// Usado para gerar arquivo de mapeamento de imagens para reorganização do Firebase Storage.
/// </summary>
public class DatabaseExporter
{
    [System.Serializable]
    private class QuestionImageData
    {
        public int questionNumber;
        public string questionImagePath;
        public string[] answerImages;
    }

    [System.Serializable]
    private class DatabaseExportData
    {
        public string databankName;
        public string displayName;
        public QuestionImageData[] questions;
    }

    /// <summary>
    /// Exporta todas as databases para arquivos JSON em um diretório especificado.
    /// </summary>
    public static void ExportAllDatabases(string outputDirectory)
    {
        Debug.Log($"[DatabaseExporter] ===== INICIANDO EXPORTAÇÃO =====");
        Debug.Log($"[DatabaseExporter] Diretório de saída: {outputDirectory}");

        try
        {
            // Criar diretório se não existir
            if (!Directory.Exists(outputDirectory))
            {
                Debug.Log($"[DatabaseExporter] Diretório não existe. Criando...");
                Directory.CreateDirectory(outputDirectory);
                Debug.Log($"[DatabaseExporter] Diretório criado com sucesso.");
            }
            else
            {
                Debug.Log($"[DatabaseExporter] Diretório já existe.");
            }

            // Lista de todas as databases
            var databases = new IQuestionDatabase[]
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
                new WaterQuestionDatabase()
            };

            Debug.Log($"[DatabaseExporter] Exportando {databases.Length} databases...");

            int successCount = 0;
            foreach (var database in databases)
            {
                try
                {
                    ExportSingleDatabase(database, outputDirectory);
                    successCount++;
                }
                catch (Exception e)
                {
                    Debug.LogError($"[DatabaseExporter] Erro ao exportar {database.GetDatabankName()}: {e.Message}");
                }
            }

            Debug.Log($"[DatabaseExporter] ===== EXPORTAÇÃO CONCLUÍDA =====");
            Debug.Log($"[DatabaseExporter] Sucesso: {successCount}/{databases.Length} databases");
            Debug.Log($"[DatabaseExporter] Arquivos salvos em: {outputDirectory}");
            Debug.Log($"[DatabaseExporter] Copie este caminho no Finder/Explorador para verificar:");
            Debug.Log($"<color=yellow>{outputDirectory}</color>");
        }
        catch (Exception e)
        {
            Debug.LogError($"[DatabaseExporter] ===== ERRO GERAL =====");
            Debug.LogError($"[DatabaseExporter] Mensagem: {e.Message}");
            Debug.LogError($"[DatabaseExporter] Stack: {e.StackTrace}");
        }
    }

    /// <summary>
    /// Exporta uma única database para JSON.
    /// </summary>
    private static void ExportSingleDatabase(IQuestionDatabase database, string outputDirectory)
    {
        var questions = database.GetQuestions();

        // Converter questões para formato de exportação
        var questionsData = questions
            .Where(q => q.isImageQuestion || q.isImageAnswer)  // Apenas questões com imagens
            .OrderBy(q => q.questionNumber)
            .Select(q => new QuestionImageData
            {
                questionNumber = q.questionNumber,
                questionImagePath = string.IsNullOrEmpty(q.questionImagePath) ? null : q.questionImagePath,
                answerImages = ExtractImageAnswers(q.answers)
            })
            .ToArray();

        // Criar estrutura de exportação
        var exportData = new DatabaseExportData
        {
            databankName = database.GetDatabankName(),
            displayName = database.GetDisplayName(),
            questions = questionsData
        };

        // Serializar para JSON
        string json = JsonUtility.ToJson(exportData, true);

        // Salvar arquivo
        string filename = $"{database.GetDatabankName()}.json";
        string filePath = Path.Combine(outputDirectory, filename);

        File.WriteAllText(filePath, json);

        Debug.Log($"[DatabaseExporter] Exportado: {filename} ({questionsData.Length} questões com imagens)");
    }

    /// <summary>
    /// Extrai apenas os paths de imagens da array de respostas.
    /// Ignora respostas que são texto puro.
    /// </summary>
    private static string[] ExtractImageAnswers(string[] answers)
    {
        if (answers == null || answers.Length == 0)
            return new string[0];

        return answers
            .Where(answer => answer.StartsWith("AnswerImages/") || answer.StartsWith("QuestionImages/"))
            .ToArray();
    }
}
