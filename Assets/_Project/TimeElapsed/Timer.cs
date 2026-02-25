using UnityEngine;
using TMPro;
using Brawler.Core;
using NaughtyAttributes;
using System.Xml.Serialization;

public class ElapsedTime: MonoBehaviour
{
    [Header("References")]
    public TMP_Text timerText; //inspector
    private float _remainingTime;

    [Header("Config")]
    [Expandable]
    [SerializeField] private MatchConfig matchConfig;

    private void Awake()
    {
        _remainingTime = matchConfig.matchTimeLimit;
    }

    void Update()
    {
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Game Over");
        }

        timerText.text = _remainingTime.ToString("00");
    }
}