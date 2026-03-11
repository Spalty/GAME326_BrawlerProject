using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Brawler.Core;
using Brawler.Arena;
using NaughtyAttributes;

public class FighterGM : Singleton<FighterGM>
{
    private SpawnPointHandler _spawnPointHandler;

    [Expandable]
    [Header("---Match Configs---")]
    [SerializeField] private MatchConfig matchConfig;

    [Header("---Game State---")]
    private GameState _currentGameState;
    private bool _isGamePaused;

    [Header("---Player Initialization---")]
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    private PlayerStateMachine _player1;
    private PlayerStateMachine _player2;
    [Space(10)]
    private SpawnPoint[] _spawnPoints;

    [Header("---Player Healths---")]
    private readonly float[] _playerHealths = new float[2];

    [Header("---Timer---")]
    private float _remainingTime;

    [Header("---Player Round Tracker---")]
    private readonly int[] _playerWinCounts = new int[2];
    private RoundResult _roundResult;
    private bool _isRoundActive;
    private bool IsMatchOver => _playerWinCounts[0] >= matchConfig.roundsToWin
                                || _playerWinCounts[1] >= matchConfig.roundsToWin;

    #region Getters / Setters
    public GameState CurrentGameState { get { return _currentGameState; } }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        //Race condition here
        InitializePlayers();

        _remainingTime = matchConfig.matchTimeLimit;
        _isRoundActive = true;
    }

    private void Start()
    {
        //Start the match
        //Start the countdown
    }

    private void Update()
    {
        UpdateTimer();
    }

    #region Player Initialization Methods
    private void InitializePlayers()
    {
        _spawnPointHandler = ServiceLocator.Get<SpawnPointHandler>();
        _spawnPoints = _spawnPointHandler.SpawnPoints; 

        _player1 = InitializePlayer(0, player1Prefab, _spawnPoints[0].transform);
        _player2 = InitializePlayer(1, player2Prefab, _spawnPoints[1].transform);

        _player1.Opponent = _player2.transform;
        _player2.Opponent = _player1.transform;
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

        _currentGameState = _isGamePaused ? GameState.Paused : GameState.Fighting;
        FighterGameEvents.OnGameStateChange?.Invoke(new GameStateChangeEvent(_currentGameState));
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

    #region Countdown Methods
    private void StartRoundCountdown()
    {
        StartCoroutine(Countdown(matchConfig.roundStartDelay));
    }

    private IEnumerator Countdown(float duration)
    {
        //Display Round number

        yield return new WaitForSeconds(duration);

        //Begin Countdown
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
            _playerWinCounts[winnerIndex] += 1;

            Debug.Log($"Player {winnerIndex} won {_playerWinCounts[winnerIndex]} times");

            _roundResult = winnerIndex == 0 ? RoundResult.Player1Wins : RoundResult.Player2Wins;
            FighterGameEvents.OnPlayerKO?.Invoke(new PlayerKOEvent(_roundResult, _playerWinCounts));

            _isRoundActive = false;

            if (!IsMatchOver)
            {
                //Start next round after delay
                StartCoroutine(StartRoundAfterDelay(matchConfig.roundEndDelay));
            }
            else
            {
                FighterGameEvents.OnMatchEnd?.Invoke(new MatchEvent(_roundResult));
            }
        }
    }
    #endregion

    #region Reset Methods
    private IEnumerator StartRoundAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        StartNewRound();
    }

    private void StartNewRound()
    {
        _player1.transform.position = _spawnPoints[0].transform.position;
        _player2.transform.position = _spawnPoints[1].transform.position;

        ResetPlayerHealth();
        ResetTimer();
        _isRoundActive = true;

        FighterGameEvents.OnMatchStart?.Invoke(new MatchEvent(RoundResult.None));
    }

    public void ResetMatch()
    {
        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();

        _isRoundActive = true;

        //Unpause the game
        _isGamePaused = false;
        Time.timeScale = _isGamePaused ? 0 : 1;
    }

    private void ResetRoundWins()
    {
        _playerWinCounts[0] = 0;
        _playerWinCounts[1] = 0;
    }

    private void ResetPlayerHealth()
    {
        _playerHealths[0] = matchConfig.StartingHealth;
        _playerHealths[1] = matchConfig.StartingHealth;

        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(0, 1));
        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(1, 1));
    }

    private void ResetTimer()
    {
        _remainingTime = matchConfig.matchTimeLimit;

        FighterGameEvents.OnTimerChanged?.Invoke(new TimerChangedEvent(_remainingTime));
    }
    #endregion
}
