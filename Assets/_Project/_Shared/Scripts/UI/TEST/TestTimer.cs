using UnityEngine;
using TMPro;

public class TestTimer: MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        TestGameManager.OnTimeChanged += UpdateTimerDisplay;
    }

    private void OnDisable()
    {
        TestGameManager.OnTimeChanged -= UpdateTimerDisplay;
    }

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateTimerDisplay(TimerChangedEvent timeChangedEvent)
    {
        timerText.text = timeChangedEvent.RemainingTime.ToString("00");
    }
}