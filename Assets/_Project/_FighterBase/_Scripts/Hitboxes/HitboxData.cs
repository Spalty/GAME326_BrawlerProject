using UnityEngine;

[CreateAssetMenu(fileName = "HitboxData", menuName = "Game Data/HitboxData")]
public class HitboxData : ScriptableObject
{
        [Header("Damage")]
        [Tooltip("Damage dealt to the opponent's health.")]
        [Range(1f, 1000f)]
        public float damage = 10f;

        [Header("Pushback")]
        [Tooltip("Base pushback force before health multiplier. Higher = launches farther.")]
        [Range(1f, 30f)]
        public float baseKnockback = 5f;
        [Tooltip("Direction of knockback. X is horizontal (1=forward, -1=backward), Y is vertical. " + "Will be flipped based on attacker facing direction.")]
        public Vector2 knockbackAngle = new(1f, 0.5f);

        [Header("Hitstun")]
        [Tooltip("How long the opponent can't act after being hit. 0 = auto-calculate from knockback.")]
        [Range(0, 30)]
        public int hitstunDuration = 5;

        [Header("Hitstun")]
        [Tooltip("How long the opponent can't act after blocking a hit. 0 = auto-calculate from knockback.")]
        [Range(0, 30)]
        public int blockstunDuration = 5;

        [Header("Hitstop")]
        [Tooltip("Freeze frame duration on hit. Creates impact feel.")]
        [Range(0f, 1f)]
        public float hitstopDuration = 0.1f;

        [Header("Attack Type")]
        [Tooltip("What type of attack this is. Controls how opponent must block the attack.")]
        public AttackType attackContext = AttackType.Mid;

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
    High,
    Mid,
    Low,
}
