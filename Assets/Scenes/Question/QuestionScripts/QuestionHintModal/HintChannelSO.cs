using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

[CreateAssetMenu(fileName = "HintChannel", menuName = "BioBlocks/HintChannel")]
public class HintChannelSO : ScriptableObject
{
    public string panelTitle;
    public List<Hint> hints = new List<Hint>();
    public bool isReady;

    public void Publish(string title, List<Hint> hintList)
    {
        panelTitle = title;
        hints = hintList;
        isReady = true;
    }

    public void Clear()
    {
        panelTitle = string.Empty;
        hints = null;
        isReady = false;
    }
}