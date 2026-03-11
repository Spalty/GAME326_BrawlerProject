using UnityEngine;
using Brawler.Core;
using TMPro;

public class RoundResultsPanel : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI roundResults;

    private bool _wasPreviouslyActive;

    private void OnEnable()
    {
        FighterGameEvents.OnPlayerKO += UpdateRoundResultsPanel;
        FighterGameEvents.OnMatchStart += DisableRoundResultsPanel;
        FighterGameEvents.OnMatchEnd += DisableRoundResultsPanel;

        FighterGameEvents.OnGameStateChange += DisableIfPaused;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnPlayerKO -= UpdateRoundResultsPanel;
        FighterGameEvents.OnMatchStart -= DisableRoundResultsPanel;
        FighterGameEvents.OnMatchEnd -= DisableRoundResultsPanel;

        FighterGameEvents.OnGameStateChange -= DisableIfPaused;
    }

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void UpdateRoundResultsPanel(PlayerKOEvent playerKOEvent)
    {
        if (playerKOEvent.Result == RoundResult.Tie)
        {
            roundResults.text = "Time's Up!";
        }
        else
        {
            roundResults.text = "KO!!!";
        }

        panel.SetActive(true);
    }

    private void DisableRoundResultsPanel(MatchEvent matchEvent)
    {
        panel.SetActive(false);
    }

    private void DisableIfPaused(GameStateChangeEvent gameStateEvent)
    {
        if (_wasPreviouslyActive)
        {
            panel.SetActive(true);
            _wasPreviouslyActive = false;
            return;
        }

        if (panel.activeSelf == true)
        {
            panel.SetActive(gameStateEvent.NewState != GameState.Paused);
            _wasPreviouslyActive = true;
        }
    }
}
