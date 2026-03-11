using UnityEngine;
using Brawler.Core;
using TMPro;

public class MatchResultsPanel : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI matchResults;

    private bool _wasPreviouslyActive;

    private void OnEnable()
    {
        FighterGameEvents.OnMatchStart += UpdateMatchResultsPanel;
        FighterGameEvents.OnMatchEnd += UpdateMatchResultsPanel;

        FighterGameEvents.OnGameStateChange += DisableIfPaused;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnMatchStart -= UpdateMatchResultsPanel;
        FighterGameEvents.OnMatchEnd -= UpdateMatchResultsPanel;

        FighterGameEvents.OnGameStateChange -= DisableIfPaused;
    }

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void UpdateMatchResultsPanel(MatchEvent matchEndEvent)
    {
        if (matchEndEvent.Result == RoundResult.None) return;

        if (matchEndEvent.Result == RoundResult.Tie)
        {
            matchResults.text = "DRAW";
        }
        else
        {
            matchResults.text = $"Player {(int)matchEndEvent.Result + 1} WINS!";
        }

        panel.SetActive(matchEndEvent.IsMatchEnd);
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
