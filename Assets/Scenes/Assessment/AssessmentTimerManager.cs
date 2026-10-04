using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class AssessmentTimerManager : MonoBehaviour
{
    public const float DefaultDurationMinutes = 15f;
    public const float DefaultDurationSeconds = DefaultDurationMinutes * 60f; // 900s

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject timePanel;

    [Header("Color Feedback")]
    [SerializeField] private Color normalColor = new Color(0.31f, 0.09f, 0.76f, 1f); // Roxo do design
    [SerializeField] private Color warningColor = new Color(0.85f, 0.2f, 0.2f, 1f); // Vermelho de alerta nos últimos 60s

    private float _currentTime;
    private bool _isRunning;
    private Coroutine _timerCoroutine;

    public event Action OnTimerComplete;
    public float CurrentTime => _currentTime;
    public bool IsRunning => _isRunning;

    private void Awake()
    {
        if (timerText == null)
        {
            timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
        }

        if (timePanel == null)
        {
            timePanel = GameObject.Find("TimePanel");
        }
    }

    public void StartTimer(float durationSeconds)
    {
        if (timePanel != null)
        {
            timePanel.SetActive(true);
        }

        StopTimer();

        _currentTime = durationSeconds > 0 ? durationSeconds : DefaultDurationSeconds;
        _isRunning = true;

        UpdateDisplay();
        _timerCoroutine = StartCoroutine(TimerRoutine());
    }

    public void StopTimer()
    {
        _isRunning = false;
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

    public void PauseTimer()
    {
        _isRunning = false;
    }

    public void ResumeTimer()
    {
        if (_currentTime > 0 && !_isRunning)
        {
            _isRunning = true;
            if (_timerCoroutine == null)
            {
                _timerCoroutine = StartCoroutine(TimerRoutine());
            }
        }
    }

    private IEnumerator TimerRoutine()
    {
        while (_isRunning && _currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            if (!_isRunning) yield break;

            _currentTime -= 1f;
            if (_currentTime < 0) _currentTime = 0;

            UpdateDisplay();
        }

        if (_currentTime <= 0)
        {
            _isRunning = false;
            OnTimerComplete?.Invoke();
        }
    }

    private void UpdateDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(_currentTime);
            timerText.color = (_currentTime <= 60f) ? warningColor : normalColor;
        }
    }

    /// <summary>
    /// Formata o tempo restante:
    /// - Se > 60s: Formato MM:SS (ex: "15:00", "14:59")
    /// - Se <= 60s: Formato em segundos (ex: "59s", "58s", ... "0s")
    /// </summary>
    public static string FormatTime(float timeRemainingSeconds)
    {
        if (timeRemainingSeconds <= 0f)
        {
            return "0s";
        }

        if (timeRemainingSeconds > 60f)
        {
            int minutes = Mathf.FloorToInt(timeRemainingSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeRemainingSeconds % 60f);
            return $"{minutes:D2}:{seconds:D2}";
        }

        int remainingSec = Mathf.CeilToInt(timeRemainingSeconds);
        return $"{remainingSec}s";
    }
}
