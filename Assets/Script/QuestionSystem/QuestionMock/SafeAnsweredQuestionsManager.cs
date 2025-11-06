using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using QuestionSystem;

public class SafeAnsweredQuestionsManager
{
    private static SafeAnsweredQuestionsManager instance;
    private Dictionary<string, HashSet<int>> devModeAnsweredQuestions = new Dictionary<string, HashSet<int>>();

    public static SafeAnsweredQuestionsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SafeAnsweredQuestionsManager();
            }
            return instance;
        }
    }

    public async Task<List<string>> FetchUserAnsweredQuestionsInTargetDatabase(IQuestionDatabase database)
    {
        string databankName = database.GetDatabankName();

        if (database.IsDatabaseInDevelopment())
        {
            Debug.Log($"[SafeAnsweredQuestionsManager] Database '{databankName}' em modo DEV - usando cache local");
            
            if (devModeAnsweredQuestions.ContainsKey(databankName))
            {
                var answeredList = new List<string>();
                foreach (int questionNumber in devModeAnsweredQuestions[databankName])
                {
                    answeredList.Add(questionNumber.ToString());
                }
                return answeredList;
            }
            
            return new List<string>();
        }

        if (AnsweredQuestionsManager.Instance != null)
        {
            return await AnsweredQuestionsManager.Instance.FetchUserAnsweredQuestionsInTargetDatabase(databankName);
        }

        Debug.LogWarning("[SafeAnsweredQuestionsManager] AnsweredQuestionsManager.Instance is null");
        return new List<string>();
    }

    public async Task MarkQuestionAsAnswered(int questionNumber, IQuestionDatabase database)
    {
        string databankName = database.GetDatabankName();

        if (database.IsDatabaseInDevelopment())
        {
            Debug.LogWarning("=================================================");
            Debug.LogWarning($"[DEV MODE] Questão {questionNumber} marcada como respondida");
            Debug.LogWarning($"Database: {databankName}");
            Debug.LogWarning($"Firebase: NÃO SALVO (modo desenvolvimento)");
            Debug.LogWarning("=================================================");

            if (!devModeAnsweredQuestions.ContainsKey(databankName))
            {
                devModeAnsweredQuestions[databankName] = new HashSet<int>();
            }

            devModeAnsweredQuestions[databankName].Add(questionNumber);
            
            return;
        }

        if (AnsweredQuestionsManager.Instance != null)
        {
            await AnsweredQuestionsManager.Instance.MarkQuestionAsAnswered(databankName, questionNumber);
            Debug.Log($"[SafeAnsweredQuestionsManager] Questão {questionNumber} salva no Firebase (modo produção)");
        }
        else
        {
            Debug.LogError("[SafeAnsweredQuestionsManager] AnsweredQuestionsManager.Instance is null");
        }
    }

    public async Task<bool> HasRemainingQuestions(List<string> currentQuestionList, IQuestionDatabase database)
    {
        string databankName = database.GetDatabankName();

        if (database.IsDatabaseInDevelopment())
        {
            Debug.Log($"[SafeAnsweredQuestionsManager] Verificando questões restantes em modo DEV para '{databankName}'");
            
            var answeredQuestions = await FetchUserAnsweredQuestionsInTargetDatabase(database);
            
            bool hasRemaining = currentQuestionList.Count > answeredQuestions.Count;
            
            Debug.Log($"[SafeAnsweredQuestionsManager] Total: {currentQuestionList.Count}, Respondidas: {answeredQuestions.Count}, Restantes: {hasRemaining}");
            
            return hasRemaining;
        }

        if (AnsweredQuestionsManager.Instance != null)
        {
            return await AnsweredQuestionsManager.Instance.HasRemainingQuestions(databankName, currentQuestionList);
        }

        Debug.LogWarning("[SafeAnsweredQuestionsManager] AnsweredQuestionsManager.Instance is null, retornando true");
        return true;
    }

    public void ResetDevModeAnsweredQuestions(string databankName)
    {
        if (devModeAnsweredQuestions.ContainsKey(databankName))
        {
            devModeAnsweredQuestions[databankName].Clear();
            Debug.Log($"[SafeAnsweredQuestionsManager] Cache local de '{databankName}' foi resetado");
        }
    }

    public void ResetAllDevModeAnsweredQuestions()
    {
        devModeAnsweredQuestions.Clear();
        Debug.Log("[SafeAnsweredQuestionsManager] Todo cache local foi resetado");
    }

    public int GetDevModeAnsweredCount(string databankName)
    {
        if (devModeAnsweredQuestions.ContainsKey(databankName))
        {
            return devModeAnsweredQuestions[databankName].Count;
        }
        return 0;
    }

    public List<int> GetDevModeAnsweredQuestions(string databankName)
    {
        if (devModeAnsweredQuestions.ContainsKey(databankName))
        {
            return new List<int>(devModeAnsweredQuestions[databankName]);
        }
        return new List<int>();
    }
}