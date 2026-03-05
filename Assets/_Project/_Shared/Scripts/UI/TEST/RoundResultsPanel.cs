using UnityEngine;
using TMPro;

public class RoundResultsPanel : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI roundResults;

    private void OnEnable()
    {
        TestGameManager.OnPlayerKO += UpdateRoundResultsPanel;
        TestGameManager.OnMatchStart += DisableRoundResultsPanel;
        TestGameManager.OnMatchEnd += DisableRoundResultsPanel;
    }

    private void OnDisable()
    {
        TestGameManager.OnPlayerKO -= UpdateRoundResultsPanel;
        TestGameManager.OnMatchStart -= DisableRoundResultsPanel;
        TestGameManager.OnMatchEnd -= DisableRoundResultsPanel;
    }

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void UpdateRoundResultsPanel(PlayerKOEvent playerKOEvent)
    {
        if (playerKOEvent.Result == RoundResults.Tie)
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
}
