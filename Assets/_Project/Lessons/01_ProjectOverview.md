# Lesson 01: Project Overview

Welcome to the Brawler Template! This project is a 2-player local fighting game inspired by Super Smash Bros.

## Team Structure

Each game is built by a team of 4:

```
Your Team (4 people)
├── Programmer 1 + Artist 1 → Fighter 1
└── Programmer 2 + Artist 2 → Fighter 2
```

You share a single repository. On Night One, your team decides how to divide the work.

---

## Folder Structure

```
Assets/_Project/
│
├── _Shared/           ← Shared systems (your team manages this together)
│   ├── Scripts/
│   │   ├── Core/      GameManager, GameEvents, ServiceLocator
│   │   ├── Combat/    Hitbox, Hurtbox, KnockbackHandler
│   │   ├── Arena/     BlastZone, SpawnPoint, Platform
│   │   ├── UI/        HealthBarUI, MatchUI, RoundDisplayUI
│   │   └── Input/     PlayerInputHandler, InputConfig
│   ├── Prefabs/
│   ├── Configs/
│   └── Scenes/
│
├── _FighterBase/      ← Reference code - DO NOT MODIFY these scripts
│   ├── Scripts/
│   │   ├── FighterBase.cs      (extend this!)
│   │   ├── FighterHealth.cs    (use as-is)
│   │   ├── FighterMovement.cs  (use or replace)
│   │   ├── AttackController.cs (use or replace)
│   │   └── AttackData.cs       (create your own)
│   └── Prefabs/
│       └── ExampleFighter.prefab
│
├── Fighter1/          ← Programmer 1 + Artist 1 ONLY
│   ├── Scripts/
│   ├── Art/
│   ├── Prefabs/
│   └── Configs/
│
├── Fighter2/          ← Programmer 2 + Artist 2 ONLY
│   ├── Scripts/
│   ├── Art/
│   ├── Prefabs/
│   └── Configs/
│
└── Lessons/           ← You are here!
```

### Golden Rules

1. **Stay in your folder.** Fighter1 team only touches Fighter1/, Fighter2 team only touches Fighter2/.
2. **Don't modify _FighterBase scripts.** Extend them instead.
3. **Coordinate on _Shared/.** Decide who owns what on Night One.

---

## What's Complete vs What You Wire Up

### Complete (Use As-Is)
These systems are finished. You use them, you don't change them:

| System | What It Does |
|--------|--------------|
| `FighterBase` | Base class you extend for your fighter |
| `FighterHealth` | Tracks health, calculates knockback multiplier |
| `KnockbackHandler` | Applies knockback physics |
| `HitstopManager` | Freeze frames on hit |
| `Hitbox` / `Hurtbox` | Combat collision detection |
| `BlastZone` | KOs fighters who touch it |
| `PlayerInputHandler` | 2-player input (keyboard + gamepad) |
| `AttackData` | ScriptableObject for defining attacks |

### Scaffolded (You Wire Up)
These have the structure but need you to connect them:

| System | What You Do | See Lesson |
|--------|-------------|------------|
| `GameManager` | Wire up match flow, KO handling, respawns | Lesson 02 |
| `HealthBarUI` | Connect to FighterHealth events | Lesson 03 |
| `RoundDisplayUI` | Connect to GameEvents | Lesson 03 |
| `MatchUI` | Wire countdown, winner display | Lesson 03 |
| `Platform` | Complete drop-through logic | Lesson 02 |

### You Create
These you build yourself:

- Your fighter script (extends `FighterBase`)
- Your attack data (ScriptableObjects)
- Your animations
- Your sprites

---

## The Game Design

### Health + Knockback
- Each fighter has a health bar
- Lower health = more knockback taken
- Formula: `knockback = baseKnockback * (2 - healthPercent)`
  - At 100% health: 1.0x knockback
  - At 50% health: 1.5x knockback
  - At 0% health: 2.0x knockback

### Win Condition
- Single elimination rounds (one KO = round over)
- Best of 3 rounds
- Win by knocking opponent into blast zones (edges of arena)

### Controls
**Player 1 (Keyboard):**
- Move: WASD
- Jump: Space
- Attack: J
- Special: K
- Dash: Left Shift

**Player 2 (Keyboard):**
- Move: Arrow Keys
- Jump: Right Ctrl / Numpad 0
- Attack: Numpad 1
- Special: Numpad 2
- Dash: Numpad 3

**Gamepad:**
- Move: Left Stick
- Jump: A (South)
- Attack: X (West)
- Special: Y (North)
- Dash: Right Trigger

---

## Night One Checklist

Your team needs to decide:

1. **Who owns _Shared/?** Options:
   - One programmer owns it all
   - Split it (P1 does GameManager, P2 does UI)
   - Work collaboratively (more merge conflicts)

2. **Using existing movement?**
   - Each programmer can bring their Platformer movement code
   - Or use the default `FighterMovement`
   - Or build something new

3. **Fighter themes?**
   - What does each fighter look like?
   - What makes them unique? (Speed vs power, range vs close combat)

4. **Artist deliverables?**
   - Sprite sheet format
   - Animation list (Idle, Run, Jump, Fall, Attack x4)
   - When are assets due?

---

## Next Steps

1. Read **Lesson 02: Wiring GameManager** to understand the match flow
2. Read **Lesson 04: Creating Your Fighter** to start your fighter
3. Artists: Read **Lesson 06: Artist Guide - Prefabs**

Good luck, and may the best fighter win!
