# Lesson 03: Wiring UI

This lesson shows how to connect the UI scaffolds to the game events system.

## Overview

The UI system uses events to stay decoupled from game logic:

```
FighterHealth ──OnHealthChanged──► HealthBarUI
GameManager ───OnRoundStart─────► MatchUI
GameEvents ────OnRoundScoreChanged──► RoundDisplayUI
```

You subscribe to events, update visuals when they fire.

---

## HealthBarUI

Open `_Shared/Scripts/UI/HealthBarUI.cs`.

### Step 1: Subscribe to Health Events

```csharp
private void Start()
{
    if (fighter != null)
    {
        health = fighter.GetComponent<FighterHealth>();

        // Subscribe to health changes
        if (health != null)
        {
            health.OnHealthChanged += OnHealthChanged;
            UpdateHealthBar(health.HealthPercent);
        }
    }
}

private void OnDestroy()
{
    // Always unsubscribe!
    if (health != null)
    {
        health.OnHealthChanged -= OnHealthChanged;
    }
}
```

### Step 2: Handle Health Changes

The `OnHealthChanged` method is already there, just needs to be called:

```csharp
private void OnHealthChanged(float oldHealth, float newHealth)
{
    if (health == null) return;

    float percent = health.HealthPercent;
    UpdateHealthBar(percent);

    // Optional: Add damage feedback
    if (newHealth < oldHealth)
    {
        StartCoroutine(DamageFlash());
    }
}

private IEnumerator DamageFlash()
{
    // Flash white briefly
    if (fillImage != null)
    {
        Color original = fillImage.color;
        fillImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        fillImage.color = original;
    }
}
```

### Step 3: Set Up in Scene

1. Create a Canvas (UI > Canvas)
2. Add an Image for the health bar background
3. Add a child Image for the fill (set Image Type to "Filled", Fill Method to "Horizontal")
4. Add `HealthBarUI` component
5. Assign references in Inspector

---

## RoundDisplayUI

Open `_Shared/Scripts/UI/RoundDisplayUI.cs`.

### Step 1: Subscribe to Score Events

```csharp
private void Start()
{
    // Subscribe to round score changes
    GameEvents.OnRoundScoreChanged += OnRoundScoreChanged;

    // Initialize display
    UpdateDisplay(0, 0);
    UpdateDisplay(1, 0);
}

private void OnDestroy()
{
    GameEvents.OnRoundScoreChanged -= OnRoundScoreChanged;
}
```

### Step 2: Handle Score Changes

The method already exists:

```csharp
private void OnRoundScoreChanged(int playerIndex, int newScore)
{
    UpdateDisplay(playerIndex, newScore);
}
```

### Step 3: Set Up in Scene

1. Create text elements for P1 and P2 scores
2. Optionally create round win icons (small circles/stars)
3. Add `RoundDisplayUI` component
4. Assign text and icon references

---

## MatchUI

Open `_Shared/Scripts/UI/MatchUI.cs`.

### Step 1: Subscribe to Game Events

```csharp
private void Start()
{
    // Hide announcements initially
    if (announcementText != null)
        announcementText.gameObject.SetActive(false);
    if (winnerPanel != null)
        winnerPanel.SetActive(false);

    // Subscribe to events
    GameEvents.OnRoundStart += OnRoundStart;
    GameEvents.OnGameStateChanged += OnGameStateChanged;
    GameEvents.OnMatchEnd += OnMatchEnd;
}

private void OnDestroy()
{
    GameEvents.OnRoundStart -= OnRoundStart;
    GameEvents.OnGameStateChanged -= OnGameStateChanged;
    GameEvents.OnMatchEnd -= OnMatchEnd;
}
```

### Step 2: Implement Countdown

```csharp
private void OnRoundStart(int roundNumber)
{
    if (roundText != null)
    {
        roundText.text = $"Round {roundNumber}";
    }

    StartCoroutine(CountdownCoroutine());
}

private IEnumerator CountdownCoroutine()
{
    if (announcementText == null) yield break;

    announcementText.gameObject.SetActive(true);

    // 3
    announcementText.text = "3";
    yield return new WaitForSecondsRealtime(countdownDelay);

    // 2
    announcementText.text = "2";
    yield return new WaitForSecondsRealtime(countdownDelay);

    // 1
    announcementText.text = "1";
    yield return new WaitForSecondsRealtime(countdownDelay);

    // GO!
    announcementText.text = "GO!";
    yield return new WaitForSecondsRealtime(announcementDuration);

    announcementText.gameObject.SetActive(false);
}
```

### Step 3: Handle State Changes

```csharp
private void OnGameStateChanged(GameState newState)
{
    switch (newState)
    {
        case GameState.RoundEnd:
            ShowAnnouncement("KO!", 1f);
            break;

        case GameState.Paused:
            // Show pause menu if you have one
            break;
    }
}
```

### Step 4: Handle Match End

```csharp
private void OnMatchEnd(int winnerIndex)
{
    if (winnerPanel != null)
        winnerPanel.SetActive(true);

    if (winnerText != null)
        winnerText.text = $"Player {winnerIndex + 1} Wins!";

    if (announcementText != null)
    {
        announcementText.gameObject.SetActive(true);
        announcementText.text = "GAME!";
    }
}
```

---

## UI Hierarchy Example

```
Canvas
├── MatchUI (MatchUI component)
│   ├── AnnouncementText (large, centered)
│   ├── RoundText (top center, smaller)
│   └── WinnerPanel (hidden initially)
│       └── WinnerText
│
├── Player1Panel
│   ├── HealthBar (HealthBarUI component)
│   │   ├── Background
│   │   └── Fill
│   └── RoundIcons
│       ├── Icon1
│       └── Icon2
│
├── Player2Panel
│   ├── HealthBar (HealthBarUI component)
│   │   ├── Background
│   │   └── Fill
│   └── RoundIcons
│       ├── Icon1
│       └── Icon2
│
└── RoundDisplay (RoundDisplayUI component)
    ├── P1Score
    └── P2Score
```

---

## Testing Checklist

- [ ] Health bars update when fighters take damage
- [ ] Health bar color changes at thresholds
- [ ] Countdown shows "3, 2, 1, GO!"
- [ ] Round text updates ("Round 1", "Round 2")
- [ ] Score updates when rounds are won
- [ ] "GAME!" shows when match ends
- [ ] Winner panel displays correct winner

---

## Tips

### Using TextMeshPro
The UI scripts use `TextMeshProUGUI`. Make sure you have TextMeshPro imported:
- Window > TextMeshPro > Import TMP Essential Resources

### Event Timing
Events fire immediately. If you need delayed reactions:
```csharp
private void OnMatchEnd(int winner)
{
    StartCoroutine(DelayedWinnerDisplay(winner));
}

private IEnumerator DelayedWinnerDisplay(int winner)
{
    yield return new WaitForSeconds(0.5f);
    // Show winner UI
}
```

### Multiple Health Bars
Each `HealthBarUI` needs its own fighter reference. Assign P1's fighter to P1's health bar, P2's fighter to P2's health bar.

---

## Next Steps

Continue to **Lesson 05: Setting Up Attacks** to learn how to create attack data.
