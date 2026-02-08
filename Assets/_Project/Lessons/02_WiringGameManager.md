# Lesson 02: Wiring GameManager

The GameManager controls the match flow: rounds, KOs, respawns, and determining a winner.

## What You're Building

```
Match Start
    │
    ▼
┌─────────┐
│Countdown│ "3... 2... 1... GO!"
└────┬────┘
     │
     ▼
┌─────────┐
│Fighting │◄────────────────┐
└────┬────┘                 │
     │ (KO happens)         │
     ▼                      │
┌─────────┐                 │
│RoundEnd │ Award point     │
└────┬────┘                 │
     │                      │
     ▼                      │
  Winner?──No──────────────►┘
     │
    Yes
     │
     ▼
┌─────────┐
│MatchEnd │ "GAME!"
└─────────┘
```

---

## Step-by-Step Implementation

Open `_Shared/Scripts/Core/GameManager.cs` and follow these steps.

### Step 1: Subscribe to KO Events

In `Start()`, subscribe to the KO event:

```csharp
private void Start()
{
    // Subscribe to fighter KO events
    GameEvents.OnFighterKO += OnFighterKO;

    // ... rest of Start
}

private void OnDestroy()
{
    // Always unsubscribe!
    GameEvents.OnFighterKO -= OnFighterKO;

    // ... rest of OnDestroy
}
```

### Step 2: Position Fighters at Spawn Points

In `StartRound()`, position fighters:

```csharp
private void StartRound()
{
    CurrentRound++;
    Log($"Round {CurrentRound} starting!");

    // Position fighters at spawn points
    for (int i = 0; i < fighters.Length; i++)
    {
        if (fighters[i] != null && i < spawnPoints.Length && spawnPoints[i] != null)
        {
            fighters[i].Respawn(spawnPoints[i].Position);
        }
    }

    // Start countdown
    SetState(GameState.Countdown);
    StartCoroutine(CountdownCoroutine());
}
```

### Step 3: Implement Countdown

Add the countdown coroutine:

```csharp
private IEnumerator CountdownCoroutine()
{
    // Notify UI to show countdown
    GameEvents.OnRoundStart?.Invoke(CurrentRound);

    // Wait for countdown duration
    yield return new WaitForSeconds(matchConfig.roundStartDelay);

    // Fight!
    SetState(GameState.Fighting);
}
```

### Step 4: Handle KO and End Round

Implement the KO handler and round end:

```csharp
private void OnFighterKO(FighterKOEventArgs args)
{
    // Only process KOs during fighting
    if (CurrentState != GameState.Fighting) return;

    Log($"Player {args.PlayerIndex} KO'd via {args.ZoneType}!");

    // The OTHER player wins this round
    int winnerIndex = args.PlayerIndex == 0 ? 1 : 0;

    StartCoroutine(EndRoundCoroutine(winnerIndex));
}

private IEnumerator EndRoundCoroutine(int roundWinner)
{
    SetState(GameState.RoundEnd);

    // Award the round win
    RoundWins[roundWinner]++;

    // Notify listeners
    GameEvents.OnRoundEnd?.Invoke(roundWinner);
    GameEvents.OnRoundScoreChanged?.Invoke(roundWinner, RoundWins[roundWinner]);

    Log($"Player {roundWinner + 1} wins round {CurrentRound}! " +
        $"Score: P1={RoundWins[0]} P2={RoundWins[1]}");

    // Pause before next round
    yield return new WaitForSeconds(matchConfig.roundEndDelay);

    // Check if match is over
    CheckMatchEnd(roundWinner);
}
```

### Step 5: Check Win Condition

Implement the match end check:

```csharp
private void CheckMatchEnd(int lastRoundWinner)
{
    // Has someone won enough rounds?
    if (RoundWins[lastRoundWinner] >= matchConfig.roundsToWin)
    {
        // Match over!
        EndMatch(lastRoundWinner);
    }
    else
    {
        // Next round!
        StartRound();
    }
}
```

---

## Testing Your Implementation

1. Add a `GameManager` to your scene
2. Assign a `MatchConfig` ScriptableObject
3. Add two `SpawnPoint` objects (set playerIndex 0 and 1)
4. Add blast zones around the arena
5. Add two fighters to the scene
6. Hit Play!

### Debug Checklist

- [ ] Fighters spawn at correct positions
- [ ] Countdown happens before fighting
- [ ] KO triggers round end
- [ ] Round wins are tracked
- [ ] Match ends when someone wins enough rounds

---

## Bonus: Platform Drop-Through

While you're here, implement platform drop-through in `_Shared/Scripts/Arena/Platform.cs`:

```csharp
public void DropThrough(Collider2D fighterCollider)
{
    StartCoroutine(DropThroughCoroutine(fighterCollider));
}

private IEnumerator DropThroughCoroutine(Collider2D fighterCollider)
{
    // Disable collision between fighter and platform
    Physics2D.IgnoreCollision(fighterCollider, platformCollider, true);

    // Wait
    yield return new WaitForSeconds(dropThroughDuration);

    // Re-enable collision
    Physics2D.IgnoreCollision(fighterCollider, platformCollider, false);
}
```

Then in your fighter's movement code, call `platform.DropThrough(myCollider)` when pressing down on a platform.

---

## Next Lesson

Continue to **Lesson 03: Wiring UI** to connect the health bars and match UI.
