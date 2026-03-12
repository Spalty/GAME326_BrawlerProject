# Design Document

---

## Game Identity

- **Game Name:** *Magepunk Fighters*
- **Tagline** (one sentence pitch): *Simple classic fighting game*
- **Art Style / Visual Direction:** *2D pixel art*

---

## Game Rules

The template ships with default rules. Keep them, modify them, or replace them entirely.

### Win Condition
> Template default: Ring-out via blast zones (knock opponent off the arena).

Your game: *Reduce the opponent's health to 0 and win 2 rounds*

### Health / Damage Model
> Template default: HP bars with knockback scaling — lower health means more knockback taken. Formula: `knockback = base * (2 - healthPercent)`.

*Your game: Traditional HP bars* 

### Match Format
> Template default: Best of 3 rounds. Single KO ends a round.

*Your game: First to 2, KO the opponent by reducing their health to 0 to end the round*

### Time Limit
> Template default: No time limit.

*Your game: 99 Seconds*

### Unique Mechanics
What makes your game different? (Examples: meter/super system, environmental hazards, status effects, stance switching, items)
*We have multiple attacks, blocking and beutiful animations*
---

## Fighters

### Fighter 1
- **Name:** *Ice Skater*
- **Archetype** *Rushdown (speed)*
- **Unique Mechanic:** *Multi-Hitting attack*
- **Visual Description:** *Woman with sky blue hair, and deadly Ice skates on her feet*
- **Key Attacks:** *Multi-Hitting kicking attack*

### Fighter 2
- **Name:** *Definetly not Sol Badguy*
- **Archetype:** *Shoto (all Rounder)*
- **Unique Mechanic:** *Long range attacks using a sword*
- **Visual Description:** *long ponytail and spiky heair, Sleveless jacket, white pants, A white sword.*
- **Key Attacks:** *long sword slash*

---

## Team Plan

There are no role restrictions. Organize however works best for your team.

- **Tech Lead** (repo owner): Ryan Truong
- **How are you dividing the work?**
- *Ryan - Lead programmer, and UI Designer*
- *Diego - Programming state machine and game feel*
- *Alex - Art and animations*
- *Memphis - Particles*

---

## Technical Decisions

- Using default `FighterMovement` or bringing your own?

- Modifying any shared systems (`_Shared/`)? If so, who owns that work?
    Ryan & Diego- Hitbox & Hurtbox Script
    Ryan- FighterGM & Fighter GameEvents
    Mempsis- Particle pooler
- Any custom mechanics that need new scripts beyond the template?
    > *we need many scripts for the hierarchical state machine (one for every state)*

---

## Milestones

Week 1:
- Goals: Finish state machine and project et up

Week 2: 
- Goals: player movement, playing prototype animations and finish UI

Week 3:
- Goals: finish working on hitbox logic and prepare for tying hitboxes to animations

Final Week: 
- Goals: have all art assets finished and ready to be put the game, also fix any bugs we come across, and add juice

---

## Notes

Use this space for anything else: reference links, inspiration, playtest feedback, etc.
