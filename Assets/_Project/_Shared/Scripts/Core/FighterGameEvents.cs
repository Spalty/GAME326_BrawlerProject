using Brawler.Core;
using System;

public enum RoundResult
{
    None = -1,
    Player1Wins = 0,
    Player2Wins = 1,
    Tie = 2
}

public static class FighterGameEvents
{
    //Game State Events
    public static Action<GameStateChangeEvent> OnGameStateChange;

    //Match Events
    public static Action<MatchEvent> OnMatchStart;
    public static Action<MatchEvent> OnMatchEnd;

    //Round Events
    public static Action<PlayerHitEvent> OnPlayerHit;
    public static Action<StrongHitEvent> OnStrongHit;
    public static Action<PlayerBlockEvent> OnPlayerBlock;
    public static Action<PlayerKOEvent> OnPlayerKO;

    //UI
    public static Action<TimerChangedEvent> OnTimerUpdate;
    public static Action<CountdownUpdateEvent> OnCountdownUpdate;
    public static Action<HealthResetEvent> OnHealthReset;

    public static void ClearAll()
    {
        OnGameStateChange = null;
        OnMatchStart = null;
        OnMatchEnd = null;
        OnPlayerHit = null;
        OnStrongHit = null;
        OnPlayerBlock = null;
        OnPlayerKO = null;
        OnTimerUpdate = null;
        OnCountdownUpdate = null;
        OnHealthReset = null;
    }
}

public struct GameStateChangeEvent
{
    public GameState NewState;
    public GameStateChangeEvent(GameState newState)
    {
        NewState = newState;    
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

public struct PlayerBlockEvent
{
    public int PlayerIndex;
    public PlayerBlockEvent(int index)
    {
        PlayerIndex = index;
    }
}

public struct PlayerKOEvent
{
    public RoundResult Result;
    public int[] PlayerWinCounts;
    public PlayerKOEvent(RoundResult result, int[] winCounts)
    {
        Result = result;
        PlayerWinCounts = winCounts;
    }
}

public struct MatchEvent
{
    public RoundResult Result;
    public readonly bool IsMatchEnd => Result != RoundResult.None;
    public MatchEvent(RoundResult result)
    {
        Result = result;
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

public struct CountdownUpdateEvent
{
    public string CountdownText;
    public CountdownUpdateEvent(string text)
    {
        CountdownText = text;
    }
}

public struct HealthResetEvent
{
    public int PlayerIndex;
    public float NewHealthPercent;
    public HealthResetEvent(int playerIndex, float newHealthPercent)
    {
        PlayerIndex = playerIndex;
        NewHealthPercent = newHealthPercent;
    }
}

public struct StrongHitEvent
{
    public float DamageAmount;
    public StrongHitEvent(float damage)
    {
        DamageAmount = damage;
    }
}

