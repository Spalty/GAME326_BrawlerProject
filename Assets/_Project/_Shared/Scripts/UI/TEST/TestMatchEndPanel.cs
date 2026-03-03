using UnityEngine;
using TMPro;

public class TestMatchEndPanel : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private GameObject matchEndPanel;
    [SerializeField] private TextMeshProUGUI winnerText;

    private void OnEnable()
    {
        TestGameManager.OnMatchStart += UpdateMatchEndPanel;
        TestGameManager.OnMatchEnd += UpdateMatchEndPanel;
    }

    private void OnDisable()
    {
        TestGameManager.OnMatchStart -= UpdateMatchEndPanel;
        TestGameManager.OnMatchEnd -= UpdateMatchEndPanel;
    }

    private void Awake()
    {
        matchEndPanel.SetActive(false);
    }

    private void UpdateMatchEndPanel(MatchEvent matchEndEvent)
    {
        winnerText.text = $"Player {matchEndEvent.WinnerIndex + 1} Wins!";

        matchEndPanel.SetActive(matchEndEvent.IsMatchEnd);
    }
}
