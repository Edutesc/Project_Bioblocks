using QuestionSystem;
using TMPro;
using System.Collections;
using UnityEngine;

public class QuestionTimerManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject timePanel;

    [Header("Duração por Bloom Level")]
    [SerializeField] private BloomLevelDuration[] bloomLevelDurations;
    [SerializeField] private float defaultDuration = 30f;

    [System.Serializable]
    public struct BloomLevelDuration
    {
        public BloomLevel bloomLevel;
        public float duration;
    }

    private float currentTime;
    private bool isRunning;

    public event System.Action OnTimerComplete;

    private void Start()
    {
        if (timerText == null)
        {
            timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
            if (timerText == null)
                Debug.LogError("TimerText não encontrado!");
        }

        if (timePanel == null)
        {
            timePanel = GameObject.Find("TimePanel");
            if (timePanel == null)
                Debug.LogError("TimePanel não encontrado!");
        }
    }

    public void StartTimer(BloomLevel bloomLevel)
    {
        if (timePanel != null)
            timePanel.SetActive(true);
        else
        {
            Debug.LogError("TimePanel é null ao tentar iniciar o timer");
            return;
        }

        currentTime = GetDurationFor(bloomLevel);
        isRunning = true;
        UpdateTimerDisplay();
        StartCoroutine(TimerCoroutine());
        Debug.Log($"Timer iniciado: {currentTime}s (BloomLevel: {bloomLevel})");
    }

    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }

    private float GetDurationFor(BloomLevel bloomLevel)
    {
        foreach (var entry in bloomLevelDurations)
            if (entry.bloomLevel == bloomLevel)
                return entry.duration;

        return defaultDuration;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
            timerText.text = $"{Mathf.Ceil(currentTime)}";
        else
            Debug.LogError("TimerText está null!");
    }

    private IEnumerator TimerCoroutine()
    {
        while (isRunning && currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            UpdateTimerDisplay();
        }

        if (currentTime <= 0)
            OnTimerComplete?.Invoke();
    }
}
