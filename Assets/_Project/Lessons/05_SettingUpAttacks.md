# Lesson 05: Setting Up Attacks

This lesson covers creating attacks using the `AttackData` ScriptableObject system.

## Creating Attack Data

1. Right-click in your Configs folder
2. Create > Brawler > Attack Data
3. Name it (e.g., "Jab", "ForwardPunch", "UpKick")

## Attack Data Properties

### Damage Section

| Property | Description | Typical Range |
|----------|-------------|---------------|
| **Damage** | Health removed from opponent | 5-25 |

Light attacks: 5-10 damage
Heavy attacks: 15-25 damage

### Knockback Section

| Property | Description |
|----------|-------------|
| **Base Knockback** | Force applied before health multiplier |
| **Knockback Angle** | Direction (X=horizontal, Y=vertical) |

```
Knockback Angle Examples:
(1, 0)    → Pure horizontal (forward)
(1, 0.5)  → Diagonal up-forward (most common)
(0, 1)    → Pure vertical (launchers)
(1, -0.3) → Spike (sends down-forward)
(-1, 0.5) → Backward launch
```

Remember: X is automatically flipped based on facing direction!

### Timing Section (in frames at 60fps)

| Property | Description | Feel |
|----------|-------------|------|
| **Startup Frames** | Before hitbox activates | Lower = faster attack |
| **Active Frames** | Hitbox is active | Higher = easier to land |
| **Recovery Frames** | After hitbox, before can act | Higher = more punishable |

```
Fast Jab:      3 startup, 3 active, 8 recovery  (total: 14 frames)
Medium Punch: 6 startup, 5 active, 15 recovery (total: 26 frames)
Heavy Smash:  12 startup, 4 active, 25 recovery (total: 41 frames)
```

### Hitstun & Hitstop

| Property | Description |
|----------|-------------|
| **Hitstun Duration** | How long opponent can't act after hit |
| **Hitstop Duration** | Freeze frame on hit (game feel) |

Hitstop: 0.03-0.08 seconds for most attacks. Makes hits feel impactful.

### Hitbox Section

| Property | Description |
|----------|-------------|
| **Hitbox Offset** | Position relative to fighter center |
| **Hitbox Size** | Width and height of hitbox |

```
Example for a forward punch:
Offset: (0.8, 0.2)  → In front of fighter, slightly up
Size: (0.6, 0.4)    → Rectangular punch hitbox
```

---

## Attack Archetypes

### Quick Jab
```
Damage: 5
Base Knockback: 3
Knockback Angle: (1, 0.2)
Startup: 3
Active: 3
Recovery: 8
Hitbox Offset: (0.5, 0)
Hitbox Size: (0.4, 0.3)
```

### Forward Tilt
```
Damage: 10
Base Knockback: 6
Knockback Angle: (1, 0.3)
Startup: 6
Active: 4
Recovery: 12
Hitbox Offset: (0.7, 0)
Hitbox Size: (0.6, 0.4)
```

### Up Attack (Launcher)
```
Damage: 12
Base Knockback: 8
Knockback Angle: (0.2, 1)
Startup: 5
Active: 5
Recovery: 15
Hitbox Offset: (0.2, 0.6)
Hitbox Size: (0.5, 0.6)
```

### Down Attack (Spike)
```
Damage: 14
Base Knockback: 7
Knockback Angle: (0.5, -0.8)
Startup: 8
Active: 3
Recovery: 20
Hitbox Offset: (0.4, -0.3)
Hitbox Size: (0.5, 0.4)
```

### Aerial Attack
```
Damage: 8
Base Knockback: 5
Knockback Angle: (1, 0.4)
Startup: 4
Active: 6
Recovery: 10
Hitbox Offset: (0.5, 0)
Hitbox Size: (0.7, 0.5)
```

### Power Smash
```
Damage: 22
Base Knockback: 12
Knockback Angle: (1, 0.4)
Startup: 15
Active: 4
Recovery: 28
Hitbox Offset: (0.9, 0.1)
Hitbox Size: (0.8, 0.6)
```

---

## Assigning Attacks

### Using AttackController

1. Add `AttackController` to your fighter
2. Assign AttackData to each slot:
   - **Neutral Attack**: No directional input
   - **Forward Attack**: Holding left/right
   - **Up Attack**: Holding up
   - **Down Attack**: Holding down
   - **Aerial Attack**: While in the air

### Attack Context Rules

```csharp
// AttackController automatically determines context:

if (!isGrounded)
    → Aerial Attack

else if (input.y > 0.5)
    → Up Attack

else if (input.y < -0.5)
    → Down Attack

else if (Mathf.Abs(input.x) > 0.5)
    → Forward Attack

else
    → Neutral Attack
```

---

## Balancing Tips

### Risk vs Reward
- Fast attacks: Low damage, safe
- Slow attacks: High damage, punishable

### Combo Starters
- Low knockback attacks that lead into follow-ups
- Hitstun should be long enough to combo

### Kill Moves
- High knockback for finishing opponents
- Usually slower with more recovery

### The Knockback Formula
```
finalKnockback = baseKnockback * (2 - healthPercent)
```

At 0% health (full HP): 1.0x knockback
At 50% health: 1.5x knockback
At 100% damage (0 HP): 2.0x knockback

So a move with 10 base knockback does:
- 10 knockback at full health
- 15 knockback at half health
- 20 knockback at zero health

---

## Testing Attacks

1. Create test AttackData assets
2. Assign to AttackController
3. Enter Play mode
4. Press attack button with different directions
5. Watch the hitbox gizmos (red boxes)
6. Adjust values based on feel

### Debug Visualization

Enable "Draw Gizmos" on the Hitbox component to see:
- Red = Active hitbox
- Gray = Inactive hitbox

Enable "Log Attacks" on AttackController to see timing in console.

---

## Audio (Optional)

AttackData includes optional audio fields:
- **Attack Sound**: Plays on attack start (whoosh)
- **Hit Sound**: Plays on successful hit (impact)

Assign AudioClips in the Inspector.

---

## Next Steps

- **Lesson 06**: Artist Guide - Prefabs
- **Lesson 07**: Artist Guide - Animators
