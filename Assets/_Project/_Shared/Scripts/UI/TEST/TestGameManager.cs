using System;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class TestGameManager : Singleton<TestGameManager>
{
    //Events
    public static event Action<PlayerHitEvent> OnHealthChanged;
    public static event Action<PlayerKOEvent> OnPlayerKO;
    public static event Action<MatchEvent> OnMatchStart;
    public static event Action<MatchEvent> OnMatchEnd;

    [Header("---Test Settings---")]
    [SerializeField] private float maxHealth = 100f;
    private readonly float[] _playerHealths = new float[2];
    private readonly int[] _playerRoundsWon = new int[2];
    private bool _isRoundActive;

    #region Debug Properties
    [Header("---Debug---")]
    public bool useDebug;
    [Space(20)]
    [ShowIf("useDebug")]
    [SerializeField] private float damageAmount = 10f;
    
    [ShowIf("useDebug")]
    [Button] public void HitPlayer1()
    {
        if (!_isRoundActive || IsMatchOver) return;

        NotifyHealthChanged(0, damageAmount); //0 is Player1
    }
    
    [ShowIf("useDebug")]
    [Button] public void HitPlayer2()
    {
        if (!_isRoundActive || IsMatchOver) return;

        NotifyHealthChanged(1, damageAmount); //1 is Player2
    }
    
    [ShowIf("useDebug")]
    [Button] public void ResetHealth()
    {
        if (!_isRoundActive || IsMatchOver) return;

        ResetPlayerHealth();
    }

    [ShowIf("useDebug")]
    [Button] public void StartNextRound()
    {
        if (_isRoundActive || IsMatchOver)
        {
            Debug.Log("A Player Needs To Die");
            return;
        }

        ResetPlayerHealth();
        _isRoundActive = true;
    }

    //This appears when a match is over
    [HideInInspector] public bool IsMatchOver => _playerRoundsWon[0] >= 3 || _playerRoundsWon[1] >= 3;
    [ShowIf("IsMatchOver")]
    [Button]
    public void ResetMatch()
    {
        _playerRoundsWon[0] = 0;
        _playerRoundsWon[1] = 0;

        ResetPlayerHealth();
        _isRoundActive = true;

        OnMatchStart?.Invoke(new MatchEvent(-1)); //-1 indicates match reset
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        ResetPlayerHealth();
        _isRoundActive = true;
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
        //Prevents hit events from evoking if health is already 0
        if (_playerHealths[playerIndex] <= 0) return;

        _playerHealths[playerIndex] -= damageAmount;

        float damagePercent = _playerHealths[playerIndex] / maxHealth;
        OnHealthChanged?.Invoke(new PlayerHitEvent(playerIndex, damagePercent));

        //Fire PlayerKO event if the player health went to 0
        if (_playerHealths[playerIndex] <= 0)
        {
            //Gets the winner from the loser
            int winnerIndex = 1 - playerIndex; 
            _playerRoundsWon[winnerIndex] += 1;
            OnPlayerKO?.Invoke(new PlayerKOEvent(winnerIndex, _playerRoundsWon[winnerIndex]));

            if (_playerRoundsWon[winnerIndex] >= 3)
            {
                OnMatchEnd?.Invoke(new MatchEvent(winnerIndex));
            }

            _isRoundActive = false;
        }
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

