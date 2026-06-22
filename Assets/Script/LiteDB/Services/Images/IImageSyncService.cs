using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QuestionSystem;
using UnityEngine;

public interface IImageSyncService
{
    /// <summary>
    /// Obtém uma imagem assincronamente: cache local primeiro; depois Firebase
    /// Storage; senão null. A storage key é relativa à raiz "Question"
    /// (ex.: "biochem/benzeno").
    /// O caller é responsável por destruir a Texture2D quando terminar de usá-la.
    /// </summary>
    Task<Texture2D> GetImageAsync(string storageKey, CancellationToken ct = default);

    /// <summary>
    /// Pré-aquece o cache para uma coleção de Question, agrupando por topic e
    /// baixando os temas em ordem do enum QuestionSystem.QuestionSet (acidsBase →
    /// aminoacids → biochem → ... → water). Dentro de cada topic, faz downloads
    /// em paralelo com limite. O callback `onTopicReady` é invocado assim que
    /// todas as imagens de um tema foram cacheadas (ou tentadas).
    /// </summary>
    Task PrewarmAsync(
        IEnumerable<Question> questions,
        IProgress<float> progress = null,
        Action<string> onTopicReady = null,
        CancellationToken ct = default);

    /// <summary>Sincronização (download em massa) em andamento?</summary>
    bool IsSyncing { get; }

    /// <summary>Cache contém alguma imagem (qualquer topic)?</summary>
    bool IsCacheReady { get; }

    /// <summary>Última mensagem de erro. null se sem erro.</summary>
    string LastError { get; }

    /// <summary>Topic já totalmente cacheado (todas as imagens conhecidas presentes).</summary>
    bool IsTopicReady(string topic);
}
