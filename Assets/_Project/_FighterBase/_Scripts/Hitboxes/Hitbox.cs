using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;


[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    [Tooltip("Data asset defining this hitbox's properties.")]
    [Expandable]
    public HitboxData data;

    [Tooltip("Fighter that spawned this hitbox. Used to prevent hitting self and for knockback direction.")]
    public FighterController owner;

    private Collider2D hitboxCollider;
    private HashSet<Hurtbox> alreadyHit = new HashSet<Hurtbox>();
    private Hurtbox hurtbox;
    public Collider2D ownerHurtboxCollider;


    void Awake()
    {
        hitboxCollider = GetComponent<Collider2D>();
        hitboxCollider.isTrigger = true; // Ensure it's a trigger
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit a fighter and it's not the owner


        Hurtbox opponentHurtbox = other.GetComponent<Hurtbox>();


        if (opponentHurtbox != null) // Ensure the collider has a Hurtbox component
        {
            // Check if we've already hit this target with this hitbox instance
            if (alreadyHit.Contains(opponentHurtbox)) return;
                

            // Apply damage and knockback to the target
            opponentHurtbox.TakeDamage(data.damage, data.hitstunDuration, data.baseKnockback, data.knockbackAngle, data.attackContext);
            alreadyHit.Add(opponentHurtbox); // Mark this target as hit to prevent multiple hits from the same hitbox instance
        }
    }

    void OnDisable()
    {
        alreadyHit.Clear(); //
    }

    [Header("Debug")]
    [SerializeField] private Color HitboxColor = new Color(1f, 0f, 0f, 0.5f); // Semi-transparent red
    private void OnDrawGizmos()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            Gizmos.color = HitboxColor;
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            Gizmos.DrawCube(col.bounds.center, col.bounds.size);
        }
    }
}