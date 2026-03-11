using UnityEngine;
using Brawler.Core;
using NaughtyAttributes;
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

    [Header("---Timer---")]
    private float _remainingTime;

    [Header("---Debug---")]
    public bool useDebug;
    [ShowIf("useDebug")]
    [Button] public void ResetMatch()
    {
        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();

        _isRoundActive = true;
    }


    protected override void Awake()
    {
        base.Awake();

        InitializePlayers();
        _remainingTime = matchConfig.matchTimeLimit;

        _isRoundActive = true;
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

        _playerHealths[playerIndex] = maxHealth;

        return playerSM;
    }
    #endregion

    private void Update()
    {
        UpdateTimer();
    }

    public void UpdateTimer()
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

            _roundResults = RoundResults.Tie;
        }
        else
        {
            winnerIndex = _playerHealths[0] > _playerHealths[1] ? 0 : 1;
            _playerWinCounts[winnerIndex] += 1;

            _roundResults = winnerIndex == 0 ? RoundResults.Player1Wins : RoundResults.Player2Wins;
        }

        _isRoundActive = false;
        FighterGameEvents.OnPlayerKO.Invoke(new PlayerKOEvent(_roundResults, _playerWinCounts));

        if (!IsMatchOver)
        {
            //Start next round after seconds
        }
        else
        {
            FighterGameEvents.OnMatchStart.Invoke(new MatchEvent(_roundResults));
        }
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
                FighterGameEvents.OnMatchStart?.Invoke(new MatchEvent(_roundResults));
            }
        }
    }

    public void ResetRoundWins()
    {
        _remainingTime = matchConfig.matchTimeLimit;
        FighterGameEvents.OnTimerChanged?.Invoke(new TimerChangedEvent(_remainingTime));
    }

    public void ResetPlayerHealth()
    {
        _playerWinCounts[0] = 0;
        _playerWinCounts[1] = 0;
    }

    public void ResetTimer()
    {
        _playerHealths[0] = maxHealth;
        _playerHealths[1] = maxHealth;

        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(0, 1));
        FighterGameEvents.OnPlayerHit?.Invoke(new PlayerHitEvent(1, 1));
    }
}
