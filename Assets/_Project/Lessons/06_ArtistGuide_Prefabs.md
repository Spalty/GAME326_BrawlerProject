# Lesson 06: Artist Guide - Prefabs

This lesson helps artists understand how to set up fighter prefabs in Unity.

## What You're Building

A fighter prefab is a reusable template containing:
- Sprites (how the fighter looks)
- Colliders (physical shape)
- Components (scripts that make it work)
- Animations (how it moves)

---

## Prefab Hierarchy

```
MyFighter (root GameObject)
│
├── Sprite                    ← Visual representation
│   └── SpriteRenderer
│   └── Animator
│   └── FighterAnimator
│
├── GroundCheck              ← Empty object at feet
│
├── Hurtbox                  ← Where fighter can be hit
│   └── BoxCollider2D (trigger)
│   └── Hurtbox script
│
└── Hitbox                   ← Created by AttackController
    └── BoxCollider2D (trigger)
    └── Hitbox script
```

---

## Step-by-Step Setup

### Step 1: Create Root GameObject

1. GameObject > Create Empty
2. Name it (e.g., "Fighter1")
3. Position at (0, 0, 0)

### Step 2: Add Required Components to Root

Add these to the root object:

| Component | Settings |
|-----------|----------|
| **Rigidbody2D** | Gravity Scale: 1, Freeze Rotation: ✓, Collision Detection: Continuous |
| **BoxCollider2D** or **CapsuleCollider2D** | Sized to match character body |
| **Your Fighter Script** | (extends FighterBase) |
| **PlayerInputHandler** | Assign Input Actions asset |
| **FighterMovement** | Assign MovementConfig |
| **AttackController** | Assign attack data assets |

### Step 3: Create Sprite Child

1. Right-click root > Create Empty > Name it "Sprite"
2. Add components:
   - **SpriteRenderer**: Assign your idle sprite
   - **Animator**: Assign your Animator Controller
   - **FighterAnimator**: Connects game state to animations

### Step 4: Create GroundCheck

1. Right-click root > Create Empty > Name it "GroundCheck"
2. Position at the bottom of your character (at the feet)
3. No components needed - just a transform reference

### Step 5: Create Hurtbox

1. Right-click root > Create Empty > Name it "Hurtbox"
2. Add components:
   - **BoxCollider2D**: Set "Is Trigger" ✓
   - **Hurtbox** script
3. Size the collider to cover the vulnerable area (usually the body)
4. Can be same as main collider, or separate

### Step 6: Hitbox (Optional)

If using AttackController, it creates the hitbox automatically.

For manual hitbox:
1. Right-click root > Create Empty > Name it "Hitbox"
2. Add components:
   - **BoxCollider2D**: Set "Is Trigger" ✓
   - **Hitbox** script
3. This will be positioned by AttackController during attacks

---

## Rigidbody2D Settings

| Setting | Value | Why |
|---------|-------|-----|
| Body Type | Dynamic | Responds to physics |
| Material | None | Unless you want custom friction |
| Simulated | ✓ | Physics active |
| Use Auto Mass | ✗ | We set mass manually |
| Mass | 1 | Standard mass |
| Linear Drag | 0 | Movement handles deceleration |
| Angular Drag | 0.05 | Default |
| Gravity Scale | 1 | Normal gravity (movement script may override) |
| Collision Detection | Continuous | Prevents tunneling at high speeds |
| Sleeping Mode | Start Awake | Always active |
| Interpolate | Interpolate | Smooth movement between physics steps |
| Freeze Rotation | ✓ Z | Don't rotate from physics |

---

## Collider Setup

### Main Collider (Physics)
- On root object
- NOT a trigger
- Used for platform collision, pushing

For humanoid characters:
- **CapsuleCollider2D**: Natural body shape
- **BoxCollider2D**: Simpler, works fine

Size to match the character's physical presence (not the full sprite if there's empty space).

### Hurtbox Collider (Combat)
- On Hurtbox child
- IS a trigger (Is Trigger ✓)
- Used for receiving hits

Usually similar size to main collider, but can be different:
- Smaller = harder to hit
- Larger = easier to hit

---

## Layer Setup

Layers help control what collides with what.

Recommended layers:
- **Ground**: Platforms and floors
- **Player**: Fighter physics colliders
- **Hitbox**: Attack hitboxes
- **Hurtbox**: Vulnerable areas

Set in Edit > Project Settings > Tags and Layers.

### Physics2D Layer Matrix
Edit > Project Settings > Physics 2D > Layer Collision Matrix

```
             Ground  Player  Hitbox  Hurtbox
Ground         -       ✓       ✗       ✗
Player         ✓       ✗       ✗       ✗
Hitbox         ✗       ✗       ✗       ✓
Hurtbox        ✗       ✗       ✓       ✗
```

This means:
- Players collide with Ground
- Players don't collide with each other (pass through)
- Hitboxes only detect Hurtboxes
- Hurtboxes only detect Hitboxes

---

## Sprite Setup

### Pivot Point
- Set sprite pivot to "Bottom" or "Center" based on preference
- Bottom pivot: Position represents feet (good for ground alignment)
- Center pivot: Position represents center (common for fighting games)

### Sorting
- Set Sorting Layer to "Characters" (create this layer)
- Order in Layer: 0 (or vary for foreground/background)

### Flip Method
FighterAnimator handles flipping by scaling X negative.
Make sure your sprites face RIGHT by default.

---

## Prefab Checklist

Before saving as prefab:

**Root Object:**
- [ ] Rigidbody2D configured
- [ ] Main collider sized correctly
- [ ] Fighter script attached
- [ ] PlayerInputHandler with Input Actions
- [ ] FighterMovement with MovementConfig
- [ ] AttackController with attacks assigned

**Sprite Child:**
- [ ] SpriteRenderer with sprite assigned
- [ ] Animator with controller assigned
- [ ] FighterAnimator attached
- [ ] Facing right by default

**GroundCheck Child:**
- [ ] Positioned at feet
- [ ] Transform assigned in FighterMovement

**Hurtbox Child:**
- [ ] Collider is trigger
- [ ] Hurtbox script attached
- [ ] Sized to vulnerable area

**Save Prefab:**
1. Drag root object to Fighter1/Prefabs/ (or Fighter2/Prefabs/)
2. Name it appropriately

---

## Duplicating ExampleFighter

Fastest way to start:

1. Find `_FighterBase/Prefabs/ExampleFighter`
2. Duplicate it (Ctrl+D)
3. Move to your fighter folder
4. Rename
5. Replace components:
   - Your fighter script instead of ExampleFighter
   - Your sprites
   - Your animator controller
   - Your attack data

---

## Common Issues

### Fighter falls through floor
- Check Ground layer is set on platforms
- Check MovementConfig has correct Ground Layer mask
- Check GroundCheck is positioned at feet

### Fighter doesn't animate
- Check Animator component has controller assigned
- Check FighterAnimator is on Sprite child
- Check animator parameters exist (see Lesson 07)

### Attacks don't hit
- Check Hitbox layer collides with Hurtbox layer
- Check hitbox collider is trigger
- Check AttackController has attacks assigned

### Fighter slides around
- Check Rigidbody2D Freeze Rotation is enabled
- Check there's no physics material with weird friction

---

## Next Steps

Continue to **Lesson 07: Artist Guide - Animators** to set up the Animator Controller.
