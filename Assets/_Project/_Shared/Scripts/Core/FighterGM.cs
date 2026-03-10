using UnityEngine;
using Brawler.Core;
using NaughtyAttributes;
using Unity.VisualScripting;

public class FighterGM : Singleton<FighterGM>
{
    [Expandable]
    [Header("---Match Configs---")]
    [SerializeField] private MatchConfig matchConfig;

    [Header("---Test---")]
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [Space(10)]
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;

    [Header("---Player Healths---")]
    [SerializeField] private float maxHealth = 100f;
    private readonly float[] _playerHealths = new float[2];

    [Header("---Player Round Tracker---")]
    private readonly int[] _playerWinCounts = new int[2];

    private RoundResults _roundResults;
    private bool _isRoundActive;

    private bool IsMatchOver => _playerWinCounts[0] >= matchConfig.roundsToWin 
                                || _playerWinCounts[1] >= matchConfig.roundsToWin;

    protected override void Awake()
    {
        base.Awake();

        InitializePlayers();

        _isRoundActive = true;
    }

    private void InitializePlayers()
    {
        GameObject player1 = Instantiate(player1Prefab, spawnPoint1.position, spawnPoint1.rotation);
        GameObject player2 =  Instantiate(player2Prefab, spawnPoint2.position, spawnPoint2.rotation);

        PlayerStateMachine player1SM = player1.GetComponent<PlayerStateMachine>();
        PlayerStateMachine player2SM = player2.GetComponent<PlayerStateMachine>();

        player1SM.PlayerIndex = 0;
        player2SM.PlayerIndex = 1;

        player1SM.Opponent = player2.transform;
        player2SM.Opponent = player1.transform;

        _playerHealths[0] = maxHealth;
        _playerHealths[1] = maxHealth;
    }

    public void FireHitEvent(int playerIndex, float damageAmount)
    {
        if (!_isRoundActive) return;
        if (_playerHealths[playerIndex] <= 0) return;

        _playerHealths[playerIndex] -= damageAmount;

        float damagePercent = _playerHealths[playerIndex] / maxHealth;
        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(playerIndex, damagePercent));

        if (_playerHealths[playerIndex] <= 0)
        {
            int winnerIndex = playerIndex == 0 ? 1 : 0;
            _playerWinCounts[winnerIndex]++;

            _roundResults = winnerIndex == 0 ? RoundResults.Player1Wins : RoundResults.Player2Wins;
            FighterGameEvents.OnPlayerKO?.Invoke(new PlayerKOEvent(_roundResults, _playerWinCounts));

            _isRoundActive = false;

            if (!IsMatchOver)
            {
                
            }
            else
            {
                FighterGameEvents.OnMatchEvent?.Invoke(new MatchEvent(_roundResults));
            }
        }
    }
}
