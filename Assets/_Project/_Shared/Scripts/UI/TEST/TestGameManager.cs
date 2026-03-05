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
    private readonly int[] _playerRoundsWon = new int[2];

    // ✅ LAST MATCH RESULTS
    private int _lastMatchRoundsP1;
    private int _lastMatchRoundsP2;

    public int LastMatchRoundsP1 => _lastMatchRoundsP1;
    public int LastMatchRoundsP2 => _lastMatchRoundsP2;

    private bool _isRoundActive;
    private float _remainingTime;

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

    [ShowIf("useDebug")]
    [Button]
    public void StartNextRound()
    {
        if (_isRoundActive || IsMatchOver)
        {
            Debug.Log("A Player Needs To Die");
            return;
        }

        StartCoroutine(StartNextRoundAfterDelay());
    }

    // when a match is over
    [HideInInspector]
    public bool IsMatchOver =>
        _playerRoundsWon[0] >= matchConfig.roundsToWin ||
        _playerRoundsWon[1] >= matchConfig.roundsToWin;

    [ShowIf("IsMatchOver")]
    [Button]
    public void ResetMatch()
    {
        ResetRoundWins();
        ResetPlayerHealth();
        ResetTimer();
        _isRoundActive = true;

        OnMatchStart?.Invoke(new MatchEvent(-1));
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // load last match (optional persistence)
        _lastMatchRoundsP1 = PlayerPrefs.GetInt("LastP1", 0);
        _lastMatchRoundsP2 = PlayerPrefs.GetInt("LastP2", 0);

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

    private void HandleTimeOut()
    {
        int winnerIndex;

        if (_playerHealths[0] == _playerHealths[1])
        {
            winnerIndex = -1; // tie
        }
        else
        {
            winnerIndex = _playerHealths[0] > _playerHealths[1] ? 0 : 1;
            _playerRoundsWon[winnerIndex] += 1;
        }

        OnPlayerKO?.Invoke(new PlayerKOEvent(
            winnerIndex,
            winnerIndex == -1 ? 0 : _playerRoundsWon[winnerIndex]
        ));

        _isRoundActive = false;

        if (!IsMatchOver)
        {
            StartCoroutine(StartNextRoundAfterDelay());
        }
        else
        {
            SaveLastMatchResults();
            OnMatchEnd?.Invoke(new MatchEvent(winnerIndex));
        }
    }

    private IEnumerator StartNextRoundAfterDelay()
    {
        yield return new WaitForSeconds(matchConfig.roundStartDelay);

        ResetPlayerHealth();
        ResetTimer();
        _isRoundActive = true;

        OnMatchStart?.Invoke(new MatchEvent(-1));
    }

    private void ResetRoundWins()
    {
        _playerRoundsWon[0] = 0;
        _playerRoundsWon[1] = 0;
    }

    private void ResetTimer()
    {
        _remainingTime = matchConfig.matchTimeLimit;
        OnTimeChanged?.Invoke(new TimerChangedEvent(_remainingTime));
    }

    private void ResetPlayerHealth()
    {
        _playerHealths[0] = maxHealth;
        _playerHealths[1] = maxHealth;

        OnHealthChanged?.Invoke(new PlayerHitEvent(0, 1));
        OnHealthChanged?.Invoke(new PlayerHitEvent(1, 1));
    }

    public void NotifyHealthChanged(int playerIndex, float damageAmount)
    {
        if (_playerHealths[playerIndex] <= 0) return;

        _playerHealths[playerIndex] -= damageAmount;

        float damagePercent = _playerHealths[playerIndex] / maxHealth;
        OnHealthChanged?.Invoke(new PlayerHitEvent(playerIndex, damagePercent));

        if (_playerHealths[playerIndex] <= 0)
        {
            int winnerIndex = 1 - playerIndex;
            _playerRoundsWon[winnerIndex] += 1;
            OnPlayerKO?.Invoke(new PlayerKOEvent(winnerIndex, _playerRoundsWon[winnerIndex]));

            _isRoundActive = false;

            if (!IsMatchOver)
            {
                StartCoroutine(StartNextRoundAfterDelay());
            }
            else
            {
                SaveLastMatchResults();
                OnMatchEnd?.Invoke(new MatchEvent(winnerIndex));
            }
        }
    }

    private void SaveLastMatchResults()
    {
        _lastMatchRoundsP1 = _playerRoundsWon[0];
        _lastMatchRoundsP2 = _playerRoundsWon[1];

        PlayerPrefs.SetInt("LastP1", _lastMatchRoundsP1);
        PlayerPrefs.SetInt("LastP2", _lastMatchRoundsP2);
        PlayerPrefs.Save();
    }
}

public struct PlayerHitEvent
{
    public int PlayerIndex;
    public float DamagePercent;
    public PlayerHitEvent(int index, float percent)
    {
        PlayerIndex = index;
        DamagePercent = percent;
    }
}

public struct PlayerKOEvent
{
    public int WinnerIndex;
    public int RoundsWon;
    public PlayerKOEvent(int index, int winCount)
    {
        WinnerIndex = index;
        RoundsWon = winCount;
    }
}

public struct MatchEvent
{
    public int WinnerIndex;
    public readonly bool IsMatchEnd => WinnerIndex != -1;
    public MatchEvent(int index)
    {
        WinnerIndex = index;
    }
}

public struct TimerChangedEvent
{
    public float RemainingTime;
    public TimerChangedEvent(float remainingTime)
    {
        RemainingTime = remainingTime;
    }
}