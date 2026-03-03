using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestRoundTracker : MonoBehaviour
{
    [Header("---Player Index---")]
    [SerializeField] private int playerIndex;

    [Header("---Tracker References---")]
    [SerializeField] private List<Image> roundTrackers;

    [Header("---Tracker Settings---")]
    [SerializeField] private Color inactiveColor = new(40, 40, 40, 255);
    [SerializeField] private Color activeColor;

    private void OnEnable()
    {
        TestGameManager.OnPlayerKO += UpdateRoundTracker;
        TestGameManager.OnMatchStart += ResetTrackers;
    }

    private void OnDisable()
    {
        TestGameManager.OnPlayerKO -= UpdateRoundTracker;
        TestGameManager.OnMatchStart -= ResetTrackers;
    }

    private void Awake()
    {
        SetAllTrackers(inactiveColor);
    }

    private void UpdateRoundTracker(PlayerKOEvent playerKOEvent)
    {
        if (playerKOEvent.WinnerIndex != playerIndex) return;

        SetAllTrackers(inactiveColor);

        for (int i = 0; i < roundTrackers.Count; i++)
        {
            roundTrackers[i].color = i < playerKOEvent.RoundsWon ? activeColor : inactiveColor;
        }
    }

    private void ResetTrackers(MatchEvent matchEvent)
    {
        if (!matchEvent.IsMatchEnd)
        {
            SetAllTrackers(inactiveColor);
        }
    }

    private void SetAllTrackers(Color color)
    {
        foreach (var tracker in roundTrackers)
        {
            tracker.color = color;
        }
    }
}
