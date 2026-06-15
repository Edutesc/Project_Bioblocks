using System;
using UnityEngine;
using System.Collections.Generic;

public class SubjectProgress : MonoBehaviour
{
    private Dictionary<string, SubjectData> userProgress = new();

    public class SubjectData
    {
        public string subjectid;
        public int questionCounter;
        public DateTime? lastCompliteDate;
        public DateTime? nextRevisionDate;
    }
    // private void CreateDictionary()
    // {
    //     foreach (var subject in grafo.nodes)
    //     {
    //         userProgress[subject.id] = new SubjectData
    //         {
    //             subjectid = subject.id,
    //             questionCounter = 0,
    //             lastCompliteDate = null,
    //             nextRevisionDate = null
    //         };
     private void CreateDictionary()
{
    userProgress["aminoacidos"] = new SubjectData
    {
        subjectid = "aminoacidos",
        questionCounter = 0,
        lastCompliteDate = null,
        nextRevisionDate = null
    };

    userProgress["enzimas"] = new SubjectData
    {
        subjectid = "enzimas",
        questionCounter = 0,
        lastCompliteDate = null,
        nextRevisionDate = null
    };

    Debug.Log($"Matérias carregadas: {userProgress.Count}");
}

    public void ScheduleRevision(string subjectid, bool iscorrect)
    {
        if (!iscorrect)
            return;

        SubjectData data = userProgress[subjectid];

        data.questionCounter++;

        if (data.questionCounter == 10)
        {
            data.questionCounter = 0;
            data.lastCompliteDate = DateTime.Now;
            data.nextRevisionDate = DateTime.Now.AddDays(7);

            Debug.Log($"Revisão criada para {subjectid}");
            Debug.Log($"Data: {data.nextRevisionDate}");
        
        }

    }


    public void SynctoFirestorm()
    {

    }
    
private void Start()
    {
        CreateDictionary();

        Debug.Log("Dicionário criado");
    }
}

