using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    private Collider2D _hitboxCollider;

    //Player Index
    private int _playerIndex;
    public int PlayerIndex { get { return _playerIndex; } set { _playerIndex = value; } }

    [Header("---Debug---")]
    public bool useDebugData = false;
    [ShowIf("useDebugData")]
    [Expandable][SerializeField] private HitboxData debugData;

    private HitboxData _data;
    public HitboxData Data { get { return _data; } set { _data = value; } }

    void Awake()
    {
        _hitboxCollider = GetComponent<Collider2D>();
        _hitboxCollider.isTrigger = true; // Ensure it's a trigger
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Hurtbox>(out var opponentHurtbox)) // Ensure the collider has a Hurtbox component
        {
            // Apply damage and knockback to the target
            HitboxData data = useDebugData ? debugData : _data;

            opponentHurtbox.HitstunFrames = data.hitstunDuration;
            opponentHurtbox.BlockStunFrames = data.blockstunDuration;
            opponentHurtbox.TryTakeDamage(data.damage, data.hitstunDuration, data.baseKnockback, data.knockbackAngle, data.attackContext);
        }
    }
    
    [Header("Debug")]
    [SerializeField] private Color HitboxColor = new(1f, 0f, 0f, 0.5f); // Semi-transparent red
    private void OnDrawGizmos()
    {
        _hitboxCollider = GetComponent<Collider2D>();
        _hitboxCollider.isTrigger = true; // Ensure it's a trigger
        
        Gizmos.color = HitboxColor;
        if (TryGetComponent<Collider2D>(out var collider))
        {
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
    }
}