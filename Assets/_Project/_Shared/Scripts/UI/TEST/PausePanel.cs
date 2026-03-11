using UnityEngine;
using Brawler.Core;

public class PausePanel : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private GameObject panel;

    private void OnEnable()
    {
        FighterGameEvents.OnGameStateChange += ShowPanel;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnGameStateChange -= ShowPanel;
    }

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void ShowPanel(GameStateChangeEvent gameStateEvent)
    {
        panel.SetActive(gameStateEvent.NewState == GameState.Paused); 
    }
}
