using UnityEngine;
using TMPro;

public class RoundTimer: MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        FighterGameEvents.OnTimerUpdate += UpdateTimerDisplay;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnTimerUpdate -= UpdateTimerDisplay;
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