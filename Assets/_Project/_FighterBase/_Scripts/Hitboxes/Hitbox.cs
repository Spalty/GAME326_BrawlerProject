using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    private Collider2D _hitboxCollider;

    //Player Index
    private int _playerIndex;
    public int PlayerIndex { get { return _playerIndex; } set { _playerIndex = value; } }

    [Header("---HitBox Data---")]
    [Expandable][SerializeField] private HitboxData data;
    private readonly HashSet<Hurtbox> _cachedHurtboxes = new();
    public HitboxData Data { get { return data; } set { data = value; } }


    void OnDisable()
    {
        _cachedHurtboxes.Clear();
    }

    
   
    private HashSet<Hurtbox> alreadyHit = new HashSet<Hurtbox>();
    private Hurtbox hurtbox;
    [SerializeField] private Collider2D ownerHurtboxCollider;
    private int hitstunFrames;

    public int HitstunFrames { get { return hitstunFrames; }  set { hitstunFrames = value; } }
   
    void Awake()
    {
        _hitboxCollider = GetComponent<Collider2D>();
        _hitboxCollider.isTrigger = true; // Ensure it's a trigger
    }


    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Hurtbox>(out var opponentHurtbox)) // Ensure the collider has a Hurtbox component
        {
            // Check if we've already hit this target with this hitbox instance
            
                
            // Apply damage and knockback to the target
            opponentHurtbox.TakeDamage(data.damage, data.hitstunDuration, data.baseKnockback, data.knockbackAngle, data.attackContext);
            _cachedHurtboxes.Add(opponentHurtbox); // Mark this target as hit to prevent multiple hits from the same hitbox instance
            hitstunFrames = data.hitstunDuration;
        }
    }
    

    [Header("Debug")]
    [SerializeField] private Color HitboxColor = new Color(1f, 0f, 0f, 0.5f); // Semi-transparent red
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