using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using UnityEngine;
using QuestionSystem;

/// <summary>
/// Repositório LiteDB para questões.
/// 
/// Regra importante:
/// este repositório NÃO acessa diretamente _db.Database, _db.Questions ou qualquer
/// coleção exposta pelo LiteDBManager. Todo acesso ao LiteDB passa por
/// ILiteDBManager.ExecuteRead/ExecuteWrite, garantindo que o SemaphoreSlim do
/// LiteDBManager seja o único ponto de serialização das operações.
/// </summary>
public class QuestionLocalRepository : MonoBehaviour, IQuestionLocalRepository
{
    private const string QUESTIONS_COLLECTION = "questions";
    private const string VERSION_PREFS_KEY   = "QuestionCache_Version";

    private ILiteDBManager _db;

    public void InjectDependencies(ILiteDBManager db)
    {
        _db = db;
    }

    // ── Escrita ────────────────────────────────────────────────────────────────

    public void SaveQuestions(List<Question> questions)
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogWarning("[QuestionLocalRepository] Lista de questões vazia — nada salvo.");
            return;
        }

        EnsureInjected();

        try
        {
            List<QuestionDB> docs = questions
                .Select(QuestionDB.FromDomain)
                .ToList();

            _db.ExecuteWrite(database =>
            {
                ILiteCollection<QuestionDB> collection =
                    database.GetCollection<QuestionDB>(QUESTIONS_COLLECTION);

                database.BeginTrans();

                try
                {
                    int saved = collection.Upsert(docs);
                    database.Commit();

                    Debug.Log($"[QuestionLocalRepository] {saved} questões salvas/atualizadas no LiteDB.");
                }
                catch
                {
                    database.Rollback();
                    throw;
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLocalRepository] Erro ao salvar questões: {e.Message}");
            throw;
        }
    }

    /// <summary>
    /// Substitui todo o cache de questões de forma atômica.
    /// 
    /// Este método é útil para refresh completo do Firestore, porque evita a janela
    /// em que ClearAll() e SaveQuestions() seriam duas operações separadas. Assim,
    /// nenhuma leitura consegue enxergar o cache vazio entre a limpeza e a nova escrita.
    /// </summary>
    public void ReplaceAllQuestions(List<Question> questions)
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogWarning("[QuestionLocalRepository] Lista de questões vazia — cache não substituído.");
            return;
        }

        EnsureInjected();

        try
        {
            List<QuestionDB> docs = questions
                .Select(QuestionDB.FromDomain)
                .ToList();

            _db.ExecuteWrite(database =>
            {
                ILiteCollection<QuestionDB> collection =
                    database.GetCollection<QuestionDB>(QUESTIONS_COLLECTION);

                database.BeginTrans();

                try
                {
                    int deleted = collection.DeleteAll();
                    int saved   = collection.Upsert(docs);

                    database.Commit();

                    Debug.Log($"[QuestionLocalRepository] Cache substituído: {deleted} antigas removidas; {saved} novas salvas.");
                }
                catch
                {
                    database.Rollback();
                    throw;
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLocalRepository] Erro ao substituir cache de questões: {e.Message}");
            throw;
        }
    }

    // ── Leitura ────────────────────────────────────────────────────────────────

    public List<Question> GetQuestionsByDatabankName(string databankName)
    {
        if (string.IsNullOrWhiteSpace(databankName))
        {
            Debug.LogWarning("[QuestionLocalRepository] databankName vazio em GetQuestionsByDatabankName.");
            return new List<Question>();
        }

        EnsureInjected();

        try
        {
            return _db.ExecuteRead(database =>
            {
                ILiteCollection<QuestionDB> collection =
                    database.GetCollection<QuestionDB>(QUESTIONS_COLLECTION);

                List<QuestionDB> docs = collection
                    .Find(q => q.QuestionDatabankName == databankName)
                    .ToList();

                return docs
                    .Select(d => d.ToDomain())
                    .ToList();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLocalRepository] Erro ao buscar questões de '{databankName}': {e.Message}");
            return new List<Question>();
        }
    }

    public List<Question> GetAllQuestions()
    {
        EnsureInjected();

        try
        {
            return _db.ExecuteRead(database =>
            {
                ILiteCollection<QuestionDB> collection =
                    database.GetCollection<QuestionDB>(QUESTIONS_COLLECTION);

                return collection
                    .FindAll()
                    .Select(d => d.ToDomain())
                    .ToList();
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLocalRepository] Erro em GetAllQuestions: {e.Message}");
            return new List<Question>();
        }
    }

    // ── Metadados de cache ─────────────────────────────────────────────────────

    public bool HasAnyQuestions()
    {
        EnsureInjected();

        try
        {
            return _db.ExecuteRead(database =>
            {
                ILiteCollection<QuestionDB> collection =
                    database.GetCollection<QuestionDB>(QUESTIONS_COLLECTION);

                return collection.Count() > 0;
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLocalRepository] Erro em HasAnyQuestions: {e.Message}");
            return false;
        }
    }

    public DateTime GetLatestCacheTimestamp()
    {
        EnsureInjected();

        try
        {
            return _db.ExecuteRead(database =>
            {
                ILiteCollection<QuestionDB> collection =
                    database.GetCollection<QuestionDB>(QUESTIONS_COLLECTION);

                QuestionDB latest = collection
                    .FindAll()
                    .OrderByDescending(q => q.CachedAt)
                    .FirstOrDefault();

                return latest?.CachedAt ?? DateTime.MinValue;
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLocalRepository] Erro em GetLatestCacheTimestamp: {e.Message}");
            return DateTime.MinValue;
        }
    }

    // ── Limpeza ────────────────────────────────────────────────────────────────

    public void ClearAll()
    {
        EnsureInjected();

        try
        {
            _db.ExecuteWrite(database =>
            {
                ILiteCollection<QuestionDB> collection =
                    database.GetCollection<QuestionDB>(QUESTIONS_COLLECTION);

                int deleted = collection.DeleteAll();
                Debug.Log($"[QuestionLocalRepository] {deleted} questões removidas do cache local.");
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[QuestionLocalRepository] Erro em ClearAll: {e.Message}");
            throw;
        }
    }

    // ── Versão do cache (PlayerPrefs) ─────────────────────────────────────────

    public long GetCachedVersion()
    {
        string raw = PlayerPrefs.GetString(VERSION_PREFS_KEY, "-1");
        return long.TryParse(raw, out long v) ? v : -1L;
    }

    public void SaveCachedVersion(long version)
    {
        PlayerPrefs.SetString(VERSION_PREFS_KEY, version.ToString());
        PlayerPrefs.Save();

        Debug.Log($"[QuestionLocalRepository] Versão do cache salva: {version}.");
    }

    // ── Utilitários ────────────────────────────────────────────────────────────

    private void EnsureInjected()
    {
        if (_db == null)
            throw new InvalidOperationException("[QuestionLocalRepository] ILiteDBManager não foi injetado.");
    }
}
