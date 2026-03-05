using System;

public enum RoundResults
{
    None = -1,
    Player1Wins = 0,
    Player2Wins = 1,
    Tie = 2
}

public static class FighterGameEvents
{
    //Game State Events
    public static Action OnGameStateChange;

    //Match Events
    public static Action<MatchEvent> OnMatchEvent;

    //Round Events
    public static Action<PlayerHitEvent> OnPlayerHit;
    public static Action<PlayerKOEvent> OnPlayerKO;

    //UI
    public static Action<TimerChangedEvent> OnTimerChanged;
   
    public static void ClearAll()
    {
        OnGameStateChange = null;
        OnMatchEvent = null;
        OnPlayerHit = null;
        OnPlayerKO = null;
        OnTimerChanged = null;
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
    public RoundResults Result;
    public int[] PlayerWinCounts;
    public PlayerKOEvent(RoundResults result, int[] winCounts)
    {
        Result = result;
        PlayerWinCounts = winCounts;
    }
}

public struct MatchEvent
{
    public RoundResults Result;
    public readonly bool IsMatchEnd => Result != RoundResults.None;
    public MatchEvent(RoundResults result)
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

