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
        TestGameManager.OnMatchEnd += ShowLastMatchResults;
    }

    private void OnDisable()
    {
        TestGameManager.OnPlayerKO -= UpdateRoundTracker;
        TestGameManager.OnMatchEnd -= ShowLastMatchResults;
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
            roundTrackers[i].color =
                i < playerKOEvent.RoundsWon ? activeColor : inactiveColor;
        }
    }

    private void ShowLastMatchResults(MatchEvent matchEvent)
    {
        int roundsWon =
            playerIndex == 0
                ? TestGameManager.Instance.LastMatchRoundsP1
                : TestGameManager.Instance.LastMatchRoundsP2;

        SetAllTrackers(inactiveColor);

        for (int i = 0; i < roundTrackers.Count; i++)
        {
            roundTrackers[i].color =
                i < roundsWon ? activeColor : inactiveColor;
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