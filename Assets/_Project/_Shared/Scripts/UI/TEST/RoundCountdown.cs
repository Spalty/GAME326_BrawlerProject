using UnityEngine;
using TMPro;

public class RoundCountdown : MonoBehaviour
{
    private TextMeshProUGUI _countdownText;

    private void Awake()
    {
        _countdownText = GetComponent<TextMeshProUGUI>();
        _countdownText.enabled = false;
    }

    private void OnEnable()
    {
        FighterGameEvents.OnCountdownUpdate += UpdateCountdownDisplay;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnCountdownUpdate -= UpdateCountdownDisplay;
    }

    private void UpdateCountdownDisplay(CountdownUpdateEvent countdownEvent)
    {
        if (countdownEvent.CountdownTime == 0)
        {
            _countdownText.enabled = false;
        }
        else
        {
            _countdownText.enabled = true;
        }

        //If the countdownTime is negative, it means the display should show the round number instead of countdown
        if (countdownEvent.CountdownTime < 0)
        {
            int totalRounds = countdownEvent.RoundCount;
            _countdownText.text = $"Round {totalRounds}";
        }
        else if (countdownEvent.CountdownTime > 1)
        {
            _countdownText.text = (countdownEvent.CountdownTime - 1).ToString("F0");
        }
        else if (countdownEvent.CountdownTime == 1)
        {
            _countdownText.text = "FIGHT!";
        }
    }
}
