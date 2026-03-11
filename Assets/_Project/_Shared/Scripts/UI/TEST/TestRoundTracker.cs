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
        FighterGameEvents.OnPlayerKO += UpdateRoundTracker;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnPlayerKO -= UpdateRoundTracker;
    }

    private void Awake()
    {
        SetAllTrackers(inactiveColor);
    }

    private void UpdateRoundTracker(PlayerKOEvent playerKOEvent)
    {
        SetAllTrackers(inactiveColor);

        for (int i = 0; i < roundTrackers.Count; i++)
        {
            if (roundTrackers[i] == null || roundTrackers[i].enabled == false) return;

            Color displayColor = i < playerKOEvent.PlayerWinCounts[playerIndex] ? activeColor : inactiveColor;
            roundTrackers[i].color = displayColor;
        }
    }

    private void SetAllTrackers(Color color)
    {
        foreach (var tracker in roundTrackers)
        {
            if (tracker == null || tracker.enabled == false) return;

            tracker.color = color;
        }
    }
}