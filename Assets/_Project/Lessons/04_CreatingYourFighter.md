# Lesson 04: Creating Your Fighter

This lesson shows programmers how to create their own fighter by extending `FighterBase`.

## Quick Start

1. Create a new script in `Fighter1/Scripts/` (or `Fighter2/Scripts/`)
2. Extend `FighterBase`
3. Override the `FighterName` property
4. Done! You have a working fighter.

```csharp
using UnityEngine;
using Brawler.Fighter;

public class MyFighter : FighterBase
{
    public override string FighterName => "My Fighter";
}
```

That's the minimum. Now let's make it interesting.

---

## Understanding FighterBase

`FighterBase` provides:

| Property/Method | What It Does |
|-----------------|--------------|
| `PlayerIndex` | Which player (0 or 1) |
| `FacingDirection` | Which way you're facing (1 or -1) |
| `IsGrounded` | Are you on the ground? |
| `CanAct` | Can you move/attack right now? |
| `IsDead` | Is health zero? |
| `Health` | Reference to FighterHealth component |
| `Knockback` | Reference to KnockbackHandler |
| `Input` | Reference to PlayerInputHandler |

---

## Optional Methods to Override

### OnFighterInitialized()
Called after setup is complete. Use for custom initialization.

```csharp
protected override void OnFighterInitialized()
{
    Debug.Log($"Ready to fight as Player {PlayerIndex + 1}!");
}
```

### OnTakeDamage(float damage)
Called when you take damage. Add effects, sounds, etc.

```csharp
protected override void OnTakeDamage(float damage)
{
    // Screen shake
    Camera.main.GetComponent<ScreenShake>()?.Shake(0.1f);

    // Flash sprite
    StartCoroutine(DamageFlash());
}
```

### OnKO()
Called when you're knocked out.

```csharp
protected override void OnKO()
{
    // Play death sound
    AudioSource.PlayClipAtPoint(koSound, transform.position);
}
```

### OnRespawn(Vector2 position)
Called when you respawn.

```csharp
protected override void OnRespawn(Vector2 position)
{
    // Start invincibility flashing
    StartCoroutine(InvincibilityFlash());
}
```

---

## Using Your Movement from Platformer

Want to use your own movement code? Here's how:

### Option A: Replace FighterMovement

1. Remove `FighterMovement` component from your prefab
2. Add your movement script
3. Make sure it respects `CanAct` (don't move during hitstun!)

```csharp
// In your movement script's Update/FixedUpdate:
if (fighter.CanAct)
{
    // Your movement code here
}
```

### Option B: Modify FighterMovement

Copy `FighterMovement.cs` to your fighter folder and modify it. Then use your version instead.

### Option C: Override in FighterBase

Override `Update()` and handle movement yourself:

```csharp
protected override void Update()
{
    base.Update(); // Keep facing direction update

    if (!CanAct) return;

    // Your movement logic here
    float moveX = Input.MoveInput.x;
    // ...
}
```

---

## Creating Attacks

### Using AttackController (Easy Way)

1. Add `AttackController` to your fighter
2. Create `AttackData` ScriptableObjects (Create > Brawler > Attack Data)
3. Assign them to the AttackController slots:
   - Neutral Attack (no direction)
   - Forward Attack (holding left/right)
   - Up Attack (holding up)
   - Down Attack (holding down)
   - Aerial Attack (in the air)

### Custom Attack System (Advanced)

Override `OnAttackInput` to handle attacks yourself:

```csharp
public override void OnAttackInput(AttackContext context)
{
    // Your custom attack logic
    switch (context)
    {
        case AttackContext.Neutral:
            StartCoroutine(MyJabAttack());
            break;
        case AttackContext.Forward:
            StartCoroutine(MySidePunch());
            break;
        // etc.
    }
}
```

---

## Setting Up Your Prefab

Your fighter prefab needs these components:

### Required Components
- **Rigidbody2D** - Physics
- **Collider2D** - Collision (BoxCollider2D or CapsuleCollider2D)
- **Your Fighter Script** - Extends FighterBase
- **FighterHealth** - Auto-added by RequireComponent
- **KnockbackHandler** - Auto-added by RequireComponent
- **PlayerInputHandler** - For receiving input
- **Hurtbox** - For receiving hits (can be on child object)

### Recommended Components
- **FighterMovement** - Default movement (or your own)
- **AttackController** - Default attacks (or your own)
- **FighterAnimator** - For animations
- **Animator** - Unity's animation system
- **SpriteRenderer** - For visuals

### Hierarchy Example
```
MyFighter
├── Sprite (SpriteRenderer, Animator, FighterAnimator)
├── GroundCheck (empty, at feet)
├── Hurtbox (Collider2D set as trigger)
└── Hitbox (created by AttackController, or add your own)
```

---

## Prefab Setup Checklist

- [ ] Rigidbody2D: Freeze Rotation = true
- [ ] Rigidbody2D: Collision Detection = Continuous
- [ ] Fighter script assigned
- [ ] PlayerInputHandler with InputActions asset
- [ ] MovementConfig assigned (if using FighterMovement)
- [ ] GroundCheck child at feet
- [ ] Hurtbox collider is trigger
- [ ] Layer set correctly for ground detection

---

## Example: Complete Fighter

See `_FighterBase/Scripts/ExampleFighter.cs` for a complete working example.

Key things it demonstrates:
- Overriding `FighterName`
- Custom `OnTakeDamage` with damage flash
- Custom `OnRespawn` with invincibility flash
- Color tinting for visual distinction

---

## Next Steps

- **Lesson 05**: Setting Up Attacks (creating AttackData)
- **Lesson 06**: Artist Guide - Prefabs
- **Lesson 07**: Artist Guide - Animators
