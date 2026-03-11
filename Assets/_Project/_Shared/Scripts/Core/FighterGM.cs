using UnityEngine;
using Brawler.Core;
using NaughtyAttributes;

public class FighterGM : Singleton<FighterGM>
{
    [Expandable]
    [Header("---Match Configs---")]
    [SerializeField] private MatchConfig matchConfig;

    [Header("---Game State---")]
    private bool _isGamePaused;

    [Header("---Player Initialization---")]
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [Space(10)]
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;

    [Header("---Player Healths---")]
    private readonly float[] _playerHealths = new float[2];

    [Header("---Player Round Tracker---")]
    private readonly int[] _playerWinCounts = new int[2];
    private RoundResult _roundResult;
    private bool _isRoundActive;
    private bool IsMatchOver => _playerWinCounts[0] >= matchConfig.roundsToWin
                                || _playerWinCounts[1] >= matchConfig.roundsToWin;

    [Header("---Timer---")]
    private float _remainingTime;

    [Header("---Debug---")]
    public bool useDebug;
    [ShowIf("useDebug")]

    protected override void Awake()
    {
        base.Awake();

        InitializePlayers();
         
        _remainingTime = matchConfig.matchTimeLimit;
        _isRoundActive = true;
    }

    private void Update()
    {
        UpdateTimer();
    }

    #region Player Initialization Methods
    private void InitializePlayers()
    {
        PlayerStateMachine player1SM = InitializePlayer(0, player1Prefab, spawnPoint1);
        PlayerStateMachine player2SM = InitializePlayer(1, player2Prefab, spawnPoint2);

        player1SM.Opponent = player2SM.transform;
        player2SM.Opponent = player1SM.transform;
    }

    private PlayerStateMachine InitializePlayer(int playerIndex, GameObject playerPrefab, Transform spawnPoint)
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        PlayerStateMachine playerSM = player.GetComponent<PlayerStateMachine>();

        playerSM.PlayerIndex = playerIndex;
        playerSM.InitializePlayerHitbox();

        _playerHealths[playerIndex] = matchConfig.StartingHealth;

        return playerSM;
    }
    #endregion

    #region Game State Methods
    public void PauseGame()
    {
        _isGamePaused = !_isGamePaused;
        Time.timeScale = _isGamePaused ? 0 : 1;

        FighterGameEvents.OnGameStateChange?.Invoke(new GameStateChangeEvent(GameState.Paused));
    }
    #endregion

    #region Timer Methods
    private void UpdateTimer()
    {
        if (!_isRoundActive || IsMatchOver) return;

        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            FighterGameEvents.OnTimerChanged?.Invoke(new TimerChangedEvent(_remainingTime));
        }
        else
        {
            HandleTimeOut();
        }
    }

    private void HandleTimeOut()
    {
        int winnerIndex;

        if (_playerHealths[0] == _playerHealths[1])
        {
            _playerWinCounts[0]++;
            _playerWinCounts[1]++;

            _roundResult = RoundResult.Tie;
        }
        else
        {
            winnerIndex = _playerHealths[0] > _playerHealths[1] ? 0 : 1;
            _playerWinCounts[winnerIndex] += 1;

            _roundResult = winnerIndex == 0 ? RoundResult.Player1Wins : RoundResult.Player2Wins;
        }

        _isRoundActive = false;
        FighterGameEvents.OnPlayerKO.Invoke(new PlayerKOEvent(_roundResult, _playerWinCounts));

        if (!IsMatchOver)
        {
            //Start next round after seconds
        }
        else
        {
            FighterGameEvents.OnMatchStart.Invoke(new MatchEvent(_roundResult));
        }
    }
    #endregion

    #region Player Hit Methods
    public void HitPlayer(int playerIndex, float damageAmount)
    {
        if (!_isRoundActive) return;
        if (_playerHealths[playerIndex] <= 0) return;

        _playerHealths[playerIndex] -= damageAmount;

        float damagePercent = _playerHealths[playerIndex] / matchConfig.StartingHealth;
        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(playerIndex, damagePercent));

        if (_playerHealths[playerIndex] <= 0)
        {
            int winnerIndex = playerIndex == 0 ? 1 : 0;
            _playerWinCounts[winnerIndex]++;

            _roundResult = winnerIndex == 0 ? RoundResult.Player1Wins : RoundResult.Player2Wins;
            FighterGameEvents.OnPlayerKO?.Invoke(new PlayerKOEvent(_roundResult, _playerWinCounts));

            _isRoundActive = false;

            if (!IsMatchOver)
            {
                
            }
            else
            {
                FighterGameEvents.OnMatchStart?.Invoke(new MatchEvent(_roundResult));
            }
        }
    }
    #endregion

    #region Reset Methods
    private void ResetMatch()
    {
        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();

        _isRoundActive = true;
    }

    private void ResetRoundWins()
    {
        _remainingTime = matchConfig.matchTimeLimit;
        FighterGameEvents.OnTimerChanged?.Invoke(new TimerChangedEvent(_remainingTime));
    }

    private void ResetPlayerHealth()
    {
        _playerWinCounts[0] = 0;
        _playerWinCounts[1] = 0;
    }

    private void ResetTimer()
    {
        _playerHealths[0] = matchConfig.StartingHealth;
        _playerHealths[1] = matchConfig.StartingHealth;

        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(0, 1));
        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(1, 1));
    }
    #endregion
}
