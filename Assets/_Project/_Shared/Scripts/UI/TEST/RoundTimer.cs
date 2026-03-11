using UnityEngine;
using TMPro;

public class RoundTimer: MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        FighterGameEvents.OnTimerChanged += UpdateTimerDisplay;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnTimerChanged -= UpdateTimerDisplay;
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