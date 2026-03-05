using System;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using Brawler.Core;

public class TestGameManager : Singleton<TestGameManager>
{
    // Events
    public static event Action<TimerChangedEvent> OnTimeChanged;
    public static event Action<PlayerHitEvent> OnHealthChanged;
    public static event Action<PlayerKOEvent> OnPlayerKO;
    public static event Action<MatchEvent> OnMatchStart;
    public static event Action<MatchEvent> OnMatchEnd;

    [Header("---Config---")]
    [Expandable]
    [SerializeField] private MatchConfig matchConfig;

    [Header("---Health Settings---")]
    [SerializeField] private float maxHealth = 100f;
    private readonly float[] _playerHealths = new float[2];

    //Timer Properties
    private float _remainingTime;

    //Round Tracking Properties
    private readonly int[] _playerWinCounts = new int[2];
    private bool _isRoundActive;
    public bool IsRoundActive { get { return _isRoundActive; } set { _isRoundActive = value; } }
    private RoundResults _roundResult;

    //Match State Properties
    private bool IsMatchOver => _playerWinCounts[0] >= matchConfig.roundsToWin 
                                || _playerWinCounts[1] >= matchConfig.roundsToWin;

    #region Debug Properties
    [Header("---Debug---")]
    public bool useDebug;
    [Space(20)]
    [ShowIf("useDebug")]
    [SerializeField] private float damageAmount = 10f;

    [ShowIf("useDebug")]
    [Button]
    public void HitPlayer1()
    {
        if (!_isRoundActive || IsMatchOver) return;
        NotifyHealthChanged(0, damageAmount);
    }

    [ShowIf("useDebug")]
    [Button]
    public void HitPlayer2()
    {
        if (!_isRoundActive || IsMatchOver) return;
        NotifyHealthChanged(1, damageAmount);
    }

    [ShowIf("useDebug")]
    [Button]
    public void ResetHealth()
    {
        if (!_isRoundActive || IsMatchOver) return;
        ResetPlayerHealth();
    }

    [ShowIf("IsMatchOver")]
    [Button]
    public void StartNewMatch()
    {
        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();
        _isRoundActive = true;

        OnMatchStart?.Invoke(new MatchEvent(RoundResults.None));
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();

        _isRoundActive = true;
    }

    private void Update()
    {
        if (!_isRoundActive || IsMatchOver) return;

        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            OnTimeChanged?.Invoke(new TimerChangedEvent(_remainingTime));
        }
        else
        {
            HandleTimeOut();
        }
    }

    #region Timer Methods
    private void HandleTimeOut()
    {
        int winnerIndex;

        if (_playerHealths[0] == _playerHealths[1])
        {
            _playerWinCounts[0] += 1;
            _playerWinCounts[1] += 1;

            _roundResult = RoundResults.Tie;
        }
        else
        {
            winnerIndex = _playerHealths[0] > _playerHealths[1] ? 0 : 1;
            _playerWinCounts[winnerIndex] += 1;

            _roundResult = winnerIndex == 0 ? RoundResults.Player1Wins : RoundResults.Player2Wins;
        }

        _isRoundActive = false;
        OnPlayerKO?.Invoke(new PlayerKOEvent(_roundResult, _playerWinCounts));

        if (!IsMatchOver)
        {
            StartCoroutine(StartNextRoundAfterDelay());
        }
        else
        {
            OnMatchEnd?.Invoke(new MatchEvent(_roundResult));
        }
    }

    private IEnumerator StartNextRoundAfterDelay()
    {
        yield return new WaitForSeconds(matchConfig.roundStartDelay);

        ResetTimer();
        ResetPlayerHealth();
        _isRoundActive = true;

        OnMatchStart?.Invoke(new MatchEvent(RoundResults.None));
    }
    #endregion

    #region Player Hit and KO Handling
    public void NotifyHealthChanged(int playerIndex, float damageAmount)
    {
        if (_playerHealths[playerIndex] <= 0) return;

        _playerHealths[playerIndex] -= damageAmount;

        float damagePercent = _playerHealths[playerIndex] / maxHealth;
        OnHealthChanged?.Invoke(new PlayerHitEvent(playerIndex, damagePercent));

        if (_playerHealths[playerIndex] <= 0)
        {
            int winnerIndex = 1 - playerIndex;
            _playerWinCounts[winnerIndex] += 1;

            _roundResult = winnerIndex == 0 ? RoundResults.Player1Wins : RoundResults.Player2Wins;
            OnPlayerKO?.Invoke(new PlayerKOEvent(_roundResult, _playerWinCounts));

            _isRoundActive = false;

            if (!IsMatchOver)
            {
                StartCoroutine(StartNextRoundAfterDelay());
            }
            else
            {
                OnMatchEnd?.Invoke(new MatchEvent(_roundResult));
            }
        }
    }
    #endregion

    #region Reset Methods
    public void ResetMatch()
    {
        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();

        _isRoundActive = true;
    }

    private void ResetTimer()
    {
        _remainingTime = matchConfig.matchTimeLimit;
        OnTimeChanged?.Invoke(new TimerChangedEvent(_remainingTime));
    }

    private void ResetRoundWins()
    {
        _playerWinCounts[0] = 0;
        _playerWinCounts[1] = 0;
    }

    private void ResetPlayerHealth()
    {
        _playerHealths[0] = maxHealth;
        _playerHealths[1] = maxHealth;

        OnHealthChanged?.Invoke(new PlayerHitEvent(0, 1));
        OnHealthChanged?.Invoke(new PlayerHitEvent(1, 1));
    }
    #endregion
}