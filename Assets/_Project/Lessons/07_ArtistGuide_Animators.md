# Lesson 07: Artist Guide - Animators

This lesson covers setting up Animator Controllers for fighters.

## Overview

The animation system flow:

```
Game State → FighterAnimator → Animator Controller → Animation Clips
   (code)      (sets params)      (state machine)      (visuals)
```

FighterAnimator reads game state and sets parameters.
The Animator Controller uses parameters to transition between states.
Animation clips play the actual visuals.

---

## Required Animator Parameters

Create these parameters in your Animator Controller:

| Parameter | Type | Set By | Used For |
|-----------|------|--------|----------|
| `HorizontalSpeed` | Float | Movement speed | Idle/Run blend |
| `VerticalSpeed` | Float | Vertical velocity | Jump direction |
| `IsGrounded` | Bool | Ground contact | Ground vs air states |
| `IsJumping` | Bool | Moving upward | Jump animation |
| `IsFalling` | Bool | Moving downward | Fall animation |
| `IsAttacking` | Bool | During attack | Attack state |
| `IsHitstun` | Bool | Being hit | Hit reaction |
| `Attack` | Trigger | Attack starts | Trigger attack anim |

---

## Creating the Animator Controller

1. Right-click in Fighter1/Art/Animations (or Fighter2)
2. Create > Animator Controller
3. Name it (e.g., "Fighter1Controller")
4. Double-click to open Animator window

---

## Basic State Machine

### Minimal Setup

```
                    ┌─────────┐
                    │  Idle   │◄──────────────────┐
                    └────┬────┘                   │
                         │ HorizontalSpeed > 0.1  │
                         ▼                        │
                    ┌─────────┐                   │
                    │   Run   │───────────────────┘
                    └─────────┘   HorizontalSpeed < 0.1
```

### Full Setup

```
                         Entry
                           │
                           ▼
         ┌─────────────────────────────────┐
         │                                 │
         │         GROUND LAYER            │
         │  ┌──────┐      ┌──────┐        │
         │  │ Idle │◄────►│ Run  │        │
         │  └──┬───┘      └──┬───┘        │
         │     │             │             │
         └─────┼─────────────┼─────────────┘
               │             │
               │ !IsGrounded │
               ▼             ▼
         ┌─────────────────────────────────┐
         │                                 │
         │          AIR LAYER              │
         │  ┌──────┐      ┌──────┐        │
         │  │ Jump │─────►│ Fall │        │
         │  └──────┘      └──────┘        │
         │                                 │
         └─────────────────────────────────┘
               │
               │ IsGrounded
               ▼
            (back to Ground Layer)
```

---

## Setting Up States

### Idle State
1. Right-click in Animator > Create State > Empty
2. Name it "Idle"
3. In Inspector, set Motion to your Idle animation clip
4. Right-click > Set as Layer Default State (orange)

### Run State
1. Create another state, name it "Run"
2. Set Motion to your Run animation clip
3. Run animation should loop

### Transitions: Idle ↔ Run

**Idle to Run:**
1. Right-click Idle > Make Transition > click Run
2. Select the transition arrow
3. In Inspector:
   - Uncheck "Has Exit Time"
   - Set Transition Duration to 0 (instant)
   - Add Condition: `HorizontalSpeed` Greater than `0.1`

**Run to Idle:**
1. Right-click Run > Make Transition > click Idle
2. Settings:
   - Uncheck "Has Exit Time"
   - Transition Duration: 0
   - Condition: `HorizontalSpeed` Less than `0.1`

### Jump State
1. Create state "Jump"
2. Set Motion to Jump animation
3. Transition from Any State:
   - Right-click "Any State" > Make Transition > Jump
   - Condition: `IsJumping` is true AND `IsGrounded` is false

### Fall State
1. Create state "Fall"
2. Set Motion to Fall animation (or falling part of jump)
3. Transition from Jump:
   - Condition: `IsFalling` is true

### Landing (back to ground)
1. Transition from Fall to Idle
2. Condition: `IsGrounded` is true

---

## Attack Animations

### Option A: Single Attack State

Simple approach - one attack animation:

1. Create state "Attack"
2. Transition from Any State:
   - Condition: `Attack` trigger
3. Transition back to Idle:
   - Has Exit Time: ✓
   - Exit Time: 1.0 (plays full animation)

### Option B: Multiple Attack States

For different attacks:

1. Create states: "NeutralAttack", "ForwardAttack", "UpAttack", etc.
2. Create parameters: `AttackNeutral`, `AttackForward`, `AttackUp` (triggers)
3. Each transitions from Any State with its trigger
4. Set different animations per state

In your AttackData, set `Animation Trigger` to match:
- Neutral attack: "AttackNeutral"
- Forward attack: "AttackForward"
- etc.

---

## Animation Clips

### Required Animations

| Name | Frames | Loop | Notes |
|------|--------|------|-------|
| Idle | 4-8 | Yes | Subtle breathing/movement |
| Run | 6-12 | Yes | Full run cycle |
| Jump | 3-6 | No | Anticipation + rise |
| Fall | 2-4 | Yes | Falling pose, can loop |
| Attack_Neutral | 4-8 | No | Quick jab |
| Attack_Forward | 6-10 | No | Punch/kick |
| Attack_Up | 5-8 | No | Upward strike |
| Attack_Down | 5-8 | No | Low attack |
| Attack_Aerial | 4-8 | No | Air attack |
| Hitstun | 2-4 | No | Getting hit reaction |

### Animation Settings

For each animation clip:
1. Select the clip in Project
2. In Inspector:
   - Loop Time: ✓ for Idle, Run, Fall
   - Loop Time: ✗ for attacks, jump

### Sprite Sheet Import

1. Import sprite sheet image
2. Select in Project
3. Inspector settings:
   - Texture Type: Sprite (2D and UI)
   - Sprite Mode: Multiple
   - Pixels Per Unit: Match your game scale (16, 32, 100, etc.)
4. Click "Sprite Editor"
5. Slice: Grid By Cell Size (or manually)
6. Apply

### Creating Animation from Sprites

1. Select all frames for an animation in Project
2. Drag onto Scene or Hierarchy
3. Unity creates:
   - Animation clip
   - Animator Controller (if none exists)
4. Rename the clip appropriately

---

## Animation Events (Advanced)

For precise attack timing, use Animation Events:

1. Open Animation window (Window > Animation > Animation)
2. Select your attack animation
3. Scrub to the frame where hitbox should activate
4. Click "Add Event" (below the timeline)
5. In Inspector, set function name (e.g., "ActivateHitbox")

Then in your fighter script:
```csharp
public void ActivateHitbox()
{
    // Called by animation event
    // Custom hitbox activation if not using AttackController
}
```

---

## Testing Animations

1. Enter Play mode
2. Select the fighter
3. Open Animator window
4. Watch states highlight as they play
5. Use the Parameters section to test transitions

### Common Issues

**Animation doesn't play:**
- Check Animator component has controller assigned
- Check parameter names match exactly (case-sensitive)

**Stuck in state:**
- Check transition conditions
- Make sure "Has Exit Time" is unchecked for parameter-based transitions

**Animation too fast/slow:**
- Adjust animation clip Sample Rate
- Or adjust animation Speed in state settings

**Flickering between states:**
- Transition conditions might be fighting
- Add "IsGrounded" checks to prevent air/ground conflicts

---

## Blend Trees (Optional)

For smooth idle-to-run blending:

1. Right-click in Animator > Create State > From New Blend Tree
2. Double-click to enter Blend Tree
3. Set Parameter to `HorizontalSpeed`
4. Add motions:
   - Threshold 0: Idle
   - Threshold 8 (or max speed): Run
5. Unity blends between them based on speed

---

## Checklist

Before testing:

- [ ] Animator Controller created
- [ ] All required parameters added (correct types!)
- [ ] Idle state set as default
- [ ] Transitions have conditions (not just exit time)
- [ ] Attack animations don't loop
- [ ] Idle/Run/Fall loop
- [ ] FighterAnimator component on Sprite object
- [ ] Animator component has controller assigned

---

## Reference: FighterAnimator Parameters

From `FighterAnimator.cs`:

```csharp
// Parameters that FighterAnimator sets automatically:
animator.SetFloat("HorizontalSpeed", speed);    // 0 to max
animator.SetFloat("VerticalSpeed", velocity);   // negative to positive
animator.SetBool("IsGrounded", grounded);       // true/false
animator.SetBool("IsJumping", rising);          // true when going up
animator.SetBool("IsFalling", falling);         // true when going down in air
animator.SetBool("IsAttacking", attacking);     // true during attack
animator.SetBool("IsHitstun", stunned);         // true when hit
animator.SetTrigger("Attack");                  // triggered on attack start
```

Make sure your Animator Controller has parameters with these exact names!
