using Brawler.Core;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Vector3 _spawnPointPos1;
    private Vector3 _spawnPointPos2;

    [Header("---Player Healths---")]
    private readonly float[] _playerHealths = new float[2];

    [Header("---Timer---")]
    private float _remainingTime;

    [Header("---Player Round Tracker---")]
    private readonly int[] _playerWinCounts = new int[2];
    private int _roundCount = 1;
    private RoundResult _roundResult;
    private bool _isRoundActive;
    private bool IsMatchOver => _playerWinCounts[0] >= matchConfig.roundsToWin
                                || _playerWinCounts[1] >= matchConfig.roundsToWin;
    private Coroutine _roundCountdownRoutine;
    private Coroutine _roundEndDelayRoutine;

    [Header("---Brute Force---")]
    private bool isFirstSpawn = true;

    #region Getters / Setters
    public GameState CurrentGameState { get { return _currentGameState; } }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        _remainingTime = matchConfig.matchTimeLimit;
        _isRoundActive = false;
        _currentGameState = GameState.Fighting;
    }

    protected override void Start()
    {
        //Dont delete this;
        //Some reason players wont spawn on first load, but behaves accordingly on subsequent loads.
        //This is a temporary fix until I can figure out why.
        if (isFirstSpawn)
        {
            SpawnPlayers();
            StartRoundCountdown();
            isFirstSpawn = false;
        }
        //Dont delete this;

        base.Start();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayers();
        StartRoundCountdown();  
    }

    private void Update()
    {
        UpdateTimer();
    }

    #region Player Initialization Methods
    private void SpawnPlayers()
    {
        _spawnPointHandler = ServiceLocator.Get<SpawnPointHandler>();
        Transform spawnPoint1 = _spawnPointHandler.SpawnPoints[0].transform;
        Transform spawnPoint2 = _spawnPointHandler.SpawnPoints[1].transform;

        if (spawnPoint1 != null) _spawnPointPos1 = spawnPoint1.position;
        if (spawnPoint2 != null) _spawnPointPos2 = spawnPoint2.position;

        _player1 = InitializePlayer(0, player1Prefab, _spawnPointPos1);
        _player2 = InitializePlayer(1, player2Prefab, _spawnPointPos2);

        _player1.Opponent = _player2.transform;
        _player2.Opponent = _player1.transform;
    }

    private PlayerStateMachine InitializePlayer(int playerIndex, GameObject playerPrefab, Vector3 spawnPointPos)
    {
        GameObject player = Instantiate(playerPrefab, spawnPointPos, Quaternion.identity);
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

        _currentGameState = _isGamePaused ? GameState.Paused : GameState.Fighting; //Need to cache the previous gameState
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
            FighterGameEvents.OnTimerUpdate?.Invoke(new TimerChangedEvent(_remainingTime));
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
            _roundEndDelayRoutine = StartCoroutine(StartNewRoundAfterDelay(matchConfig.roundEndDelay));
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
        _roundEndDelayRoutine = StartCoroutine(Countdown(matchConfig.roundStartDelay));
    }

    private IEnumerator Countdown(float duration)
    {
        //-1 indicates "Round {roundCount}"
        FighterGameEvents.OnCountdownUpdate?.Invoke(new CountdownUpdateEvent(-1, _roundCount)); 

        yield return new WaitForSeconds(duration);

        //3, 2, 1...
        float countdownTime = matchConfig.countDownDuration + 1; 
        for (int i = 0; i < countdownTime; i++)
        {
            FighterGameEvents.OnCountdownUpdate?.Invoke(new CountdownUpdateEvent(countdownTime - i, _roundCount));
            yield return new WaitForSeconds(1f);
        }

        //0 indicates "Fight"
        FighterGameEvents.OnCountdownUpdate?.Invoke(new CountdownUpdateEvent(0, _roundCount)); 

        _isRoundActive = true;
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

            _roundResult = winnerIndex == 0 ? RoundResult.Player1Wins : RoundResult.Player2Wins;
            FighterGameEvents.OnPlayerKO?.Invoke(new PlayerKOEvent(_roundResult, _playerWinCounts));

            _isRoundActive = false;

            if (!IsMatchOver)
            {
                //Start next round after delay
                _roundEndDelayRoutine = StartCoroutine(StartNewRoundAfterDelay(matchConfig.roundEndDelay));
            }
            else
            {
                FighterGameEvents.OnMatchEnd?.Invoke(new MatchEvent(_roundResult));
            }
        }
    }
    #endregion

    #region Reset Methods
    private IEnumerator StartNewRoundAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        StartNewRound();
        StartRoundCountdown();
    }

    private void StartNewRound()
    {
        _player1.transform.position = _spawnPointPos1;
        _player2.transform.position = _spawnPointPos2;

        ResetPlayerHealth();
        ResetTimer();

        _roundCount++;
        _isRoundActive = false;

        FighterGameEvents.OnMatchStart?.Invoke(new MatchEvent(RoundResult.None));
    }

    public void ResetMatch()
    {
        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();
        ResetCoroutines();

        _roundCount = 1;
        _isRoundActive = false;

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

        FighterGameEvents.OnHealthReset?.Invoke(new HealthResetEvent(0, 1));
        FighterGameEvents.OnHealthReset?.Invoke(new HealthResetEvent(1, 1));
    }

    private void ResetTimer()
    {
        _remainingTime = matchConfig.matchTimeLimit;

        FighterGameEvents.OnTimerUpdate?.Invoke(new TimerChangedEvent(_remainingTime));
    }

    private void ResetCoroutines()
    {
        if (_roundCountdownRoutine != null)
        {
            StopCoroutine(_roundCountdownRoutine);
            _roundCountdownRoutine = null;
        }

        if (_roundEndDelayRoutine != null)
        {
            StopCoroutine(_roundEndDelayRoutine);
            _roundEndDelayRoutine = null;
        }
    }
    #endregion
}
