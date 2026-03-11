using UnityEngine;
using TMPro;

public class MatchResultsPanel : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI matchResults;

    private void OnEnable()
    {
        FighterGameEvents.OnMatchStart += UpdateMatchResultsPanel;
        FighterGameEvents.OnMatchEnd += UpdateMatchResultsPanel;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnMatchStart -= UpdateMatchResultsPanel;
        FighterGameEvents.OnMatchEnd -= UpdateMatchResultsPanel;
    }

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void UpdateMatchResultsPanel(MatchEvent matchEndEvent)
    {
        if (matchEndEvent.Result == RoundResults.None) return;

        if (matchEndEvent.Result == RoundResults.Tie)
        {
            matchResults.text = "DRAW";
        }
        else
        {
            matchResults.text = $"Player {(int)matchEndEvent.Result + 1} WINS!";
        }

        panel.SetActive(matchEndEvent.IsMatchEnd);
    }
}
