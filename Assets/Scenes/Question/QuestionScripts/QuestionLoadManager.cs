using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;
using System;
using System.Linq;

public class QuestionLoadManager : MonoBehaviour
{
    private List<Question> questions;
    public string databankName;
    private bool isInitialized = false;
    public string DatabankName => databankName;

    private async void Start()
    {
        await Initialize();
    }

    private async Task Initialize()
    {
        if (isInitialized) return;

        try
        {
            await WaitForAnsweredQuestionsManager();
            isInitialized = true;
            Debug.Log("QuestionLoadManager inicializado com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao inicializar QuestionLoadManager: {e.Message}");
        }
    }

    private async Task WaitForAnsweredQuestionsManager()
    {
        int maxAttempts = 10;
        int currentAttempt = 0;

        while (currentAttempt < maxAttempts)
        {
            if (AnsweredQuestionsManager.Instance != null &&
                AnsweredQuestionsManager.Instance.IsManagerInitialized)
            {
                Debug.Log("AnsweredQuestionsManager encontrado e inicializado");
                return;
            }

            Debug.Log($"Tentativa {currentAttempt + 1} de {maxAttempts} para encontrar AnsweredQuestionsManager inicializado");
            await Task.Delay(500);
            currentAttempt++;
        }

        throw new Exception("AnsweredQuestionsManager não foi inicializado após várias tentativas");
    }

    public async Task<List<Question>> LoadQuestionsForSet(QuestionSet targetSet)
    {
        try
        {
            if (!isInitialized)
            {
                await Initialize();
            }

            IQuestionDatabase database = FindQuestionDatabase(targetSet);

            if (database == null)
            {
                Debug.LogError($"❌ Nenhum database encontrado para o QuestionSet: {targetSet}");
                return new List<Question>();
            }

            return await LoadQuestionsFromDatabase(database);
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ Erro em LoadQuestionsForSet: {e.Message}\n{e.StackTrace}");
            return new List<Question>();
        }
    }

    private IQuestionDatabase FindQuestionDatabase(QuestionSet targetSet)
    {
        try
        {
            MonoBehaviour[] allBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

            foreach (MonoBehaviour behaviour in allBehaviours)
            {
                if (behaviour is IQuestionDatabase database)
                {
                    if (database.GetQuestionSetType() == targetSet)
                    {
                        return database;
                    }
                }
            }

            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao procurar database: {e.Message}");
            return null;
        }
    }

    private async Task<List<Question>> LoadQuestionsFromDatabase(IQuestionDatabase database)
    {
        if (database == null)
        {
            Debug.LogError("Database é null em LoadQuestionsFromDatabase");
            return new List<Question>();
        }

        try
        {
            if (!AnsweredQuestionsManager.Instance.IsManagerInitialized)
            {
                Debug.LogError("AnsweredQuestionsManager não está inicializado");
                return new List<Question>();
            }

            // ═══════════════════════════════════════════════════════════
            // PASSO 1: CARREGAR TODAS AS QUESTÕES DO BANCO LOCAL
            // ═══════════════════════════════════════════════════════════
            List<Question> allQuestions = database.GetQuestions();

            if (allQuestions == null || allQuestions.Count == 0)
            {
                Debug.LogError("❌ Database retornou lista nula ou vazia de questões");
                return new List<Question>();
            }

            Debug.Log($"\n📚 PASSO 1: BANCO LOCAL");
            Debug.Log($"  Total de questões: {allQuestions.Count}");

            if (string.IsNullOrEmpty(databankName))
            {
                databankName = database.GetDatabankName();
                Debug.Log($"  Nome do banco: {databankName}");
            }

            // Registrar estatísticas
            int totalQuestions = allQuestions.Count;
            QuestionBankStatistics.SetTotalQuestions(databankName, totalQuestions);

            var questionsByLevel = GetQuestionCountByLevel(allQuestions);
            QuestionBankStatistics.SetQuestionsPerLevel(databankName, questionsByLevel);

            // Mostrar distribuição por nível
            foreach (var kvp in questionsByLevel.OrderBy(x => x.Key))
            {
                Debug.Log($"    Nível {kvp.Key}: {kvp.Value} questões");
            }

            // ═══════════════════════════════════════════════════════════
            // PASSO 2: OBTER QUESTÕES RESPONDIDAS DO FIREBASE
            // ═══════════════════════════════════════════════════════════
            string userId = UserDataStore.CurrentUserData?.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                Debug.LogWarning("⚠️ UserId não disponível, carregando apenas questões de nível 1");
                allQuestions = allQuestions.Where(q => GetQuestionLevel(q) == 1).ToList();
                questions = allQuestions;
                return questions;
            }

            List<string> answeredQuestionsFromFirebase = await AnsweredQuestionsManager.Instance
                .FetchUserAnsweredQuestionsInTargetDatabase(databankName);

            Debug.Log($"\n🔥 PASSO 2: FIREBASE (AnsweredQuestions)");
            Debug.Log($"  Questões respondidas corretamente: {answeredQuestionsFromFirebase.Count}");
            if (answeredQuestionsFromFirebase.Count > 0 && answeredQuestionsFromFirebase.Count <= 20)
            {
                Debug.Log($"  IDs: [{string.Join(", ", answeredQuestionsFromFirebase)}]");
            }

            // ═══════════════════════════════════════════════════════════
            // PASSO 3: CALCULAR NÍVEL ATUAL DINAMICAMENTE
            // ═══════════════════════════════════════════════════════════
            Debug.Log($"\n🔢 PASSO 3: CÁLCULO DO NÍVEL ATUAL");

            int currentLevel = LevelCalculator.CalculateCurrentLevel(
                allQuestions,
                answeredQuestionsFromFirebase
            );

            // ═══════════════════════════════════════════════════════════
            // PASSO 4: REMOVER QUESTÕES JÁ RESPONDIDAS
            // ═══════════════════════════════════════════════════════════
            HashSet<string> answeredSet = new HashSet<string>(answeredQuestionsFromFirebase);

            List<Question> questionsNotAnswered = allQuestions
                .Where(q => !answeredSet.Contains(q.questionNumber.ToString()))
                .ToList();

            Debug.Log($"\n🗑️ PASSO 4: REMOVER QUESTÕES RESPONDIDAS");
            Debug.Log($"  Questões restantes: {questionsNotAnswered.Count}");

            // ═══════════════════════════════════════════════════════════
            // PASSO 5: FILTRAR APENAS QUESTÕES DO NÍVEL ATUAL
            // ═══════════════════════════════════════════════════════════
            List<Question> questionsForCurrentLevel = questionsNotAnswered
                .Where(q => GetQuestionLevel(q) == currentLevel)
                .ToList();

            Debug.Log($"\n✅ PASSO 5: FILTRAR POR NÍVEL {currentLevel}");
            Debug.Log($"  Questões disponíveis: {questionsForCurrentLevel.Count}");

            if (questionsForCurrentLevel.Count > 0)
            {
                var questionNumbers = questionsForCurrentLevel
                    .Select(q => q.questionNumber)
                    .OrderBy(n => n)
                    .ToList();

                if (questionNumbers.Count <= 20)
                {
                    Debug.Log($"  IDs que serão mostradas: [{string.Join(", ", questionNumbers)}]");
                }
                else
                {
                    Debug.Log($"  IDs que serão mostradas: [{string.Join(", ", questionNumbers.Take(10))}... +{questionNumbers.Count - 10} mais]");
                }
            }
            else
            {
                Debug.Log($"  ⚠️ NENHUMA questão disponível no nível {currentLevel}!");

                // Mostrar estatísticas para debug
                var stats = LevelCalculator.GetLevelStats(allQuestions, answeredQuestionsFromFirebase);
                Debug.Log($"\n📊 ESTATÍSTICAS:");
                foreach (var stat in stats.Values.OrderBy(s => s.Level))
                {
                    Debug.Log($"  {stat}");
                }
            }

            Debug.Log($"╚══════════════════════════════════════════════════════╝\n");

            questions = questionsForCurrentLevel;
            return questions;
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ Erro em LoadQuestionsFromDatabase: {e.Message}\n{e.StackTrace}");
            return new List<Question>();
        }
    }

    private int GetQuestionLevel(Question question)
    {
        if (question.questionLevel <= 0)
        {
            return 1;
        }
        return question.questionLevel;
    }

    private Dictionary<int, int> GetQuestionCountByLevel(List<Question> allQuestions)
    {
        var stats = new Dictionary<int, int>();

        if (allQuestions == null || allQuestions.Count == 0)
        {
            return stats;
        }

        foreach (var question in allQuestions)
        {
            int level = GetQuestionLevel(question);

            if (!stats.ContainsKey(level))
            {
                stats[level] = 0;
            }
            stats[level]++;
        }

        return stats;
    }
}