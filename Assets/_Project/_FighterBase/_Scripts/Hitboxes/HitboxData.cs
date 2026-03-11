using UnityEngine;

[CreateAssetMenu(fileName = "HitboxData", menuName = "Scriptable Objects/HitboxData")]
public class HitboxData : ScriptableObject
{
     [Header("Identity")]
        [Tooltip("Display name for this attack (e.g., 'Jab', 'Forward Smash').")]
        public string attackName = "New Attack";

        [Header("Damage")]
        [Tooltip("Damage dealt to the opponent's health.")]
        [Range(1f, 1000f)]
        public float damage = 10f;

        [Header("Pushback")]
        [Tooltip("Base pushback force before health multiplier. Higher = launches farther.")]
        [Range(1f, 30f)]
        public float baseKnockback = 5f;

        [Tooltip("Direction of knockback. X is horizontal (1=forward, -1=backward), Y is vertical. " + "Will be flipped based on attacker facing direction.")]
        public Vector2 knockbackAngle = new Vector2(1f, 0.5f);

        

        [Header("Hitstun")]
        [Tooltip("How long the opponent can't act after being hit. 0 = auto-calculate from knockback.")]
        [Range(0f, 1f)]
        public float hitstunDuration = 5f;

        [Header("Hitstun")]
        [Tooltip("How long the opponent can't act after blocking a hit. 0 = auto-calculate from knockback.")]
        [Range(0f, 1f)]
        public float blockstunDuration = 5f;


        [Header("Hitstop")]
        [Tooltip("Freeze frame duration on hit. Creates impact feel.")]
        [Range(0f, 0.2f)]
        public float hitstopDuration = 0.05f;

        [Header("Hitbox Size")]
        [Tooltip("Size of the hitbox.")]
        public Vector2 hitboxSize = new Vector2(1f, 1f);

        [Header("Hitbox Offset")]
        [Tooltip("Offset of the hitbox from the fighter's position. X is horizontal (positive = forward), Y is vertical.")]
        public Vector2 hitboxOffset = new Vector2(0.5f, 0f);

        [Header("Attack Type")]
        [Tooltip("What type of attack this is. Controls how opponent must block the attack.")]
        public AttackType attackContext = AttackType.Mid;

        [Header("Audio/Visual (Optional)")]
        [Tooltip("Sound effect to play on attack start.")]
        public AudioClip attackSound;

        [Tooltip("Sound effect to play on hit.")]
        public AudioClip hitSound;


        
        // Convenience properties for timing in seconds (at 60fps)
        /*
        public float StartupTime => startupFrames / 60f;
        public float ActiveTime => activeFrames / 60f;
        public float RecoveryTime => recoveryFrames / 60f;
        public float TotalTime => (startupFrames + activeFrames + recoveryFrames) / 60f;
        */
    }

public enum AttackType
{
    Overhead,
    Mid,
    Low,
}
