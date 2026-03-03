using UnityEngine;
using Brawler.Core;
using TMPro;
using NaughtyAttributes;

public class TestTimer: MonoBehaviour
{
    private TextMeshProUGUI timerText;

    [Header("---Config---")]
    [Expandable]
    [SerializeField] private MatchConfig matchConfig;
    private float _remainingTime;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        _remainingTime = matchConfig.matchTimeLimit;
    }

    private void Update()
    {
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
        }

        timerText.text = _remainingTime.ToString("00");
    }
}