using Unity.VisualScripting;
using UnityEngine;



[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    Hitbox hitbox; // Reference to the hitbox that can damage this hurtbox

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hitbox") && !hitbox.ownerHurtboxCollider)
        {
            Debug.Log("Hurtbox collided with a hitbox!");
            // You can add logic here to determine damage, knockback, etc. based on the hitbox's properties
        }
    }


    #region ---Methods---
    public void TakeDamage(float damage, float hitstunDuration, float baseKnockback, Vector2 knockbackAngle, AttackType attackContext)
    {
        // Implement damage application logic here
        Debug.Log($"Took {damage} damage with knockback {baseKnockback} at angle {knockbackAngle}. Hitstun: {hitstunDuration}f Attack Type {attackContext},");
    }
    #endregion
    
    #region ---Debug---
    [Header("Debug")]
    [Tooltip("Visualize the hurtbox in the editor.")]
    [SerializeField] private Color hurtboxColor = new Color(0f, 1f, 0f, 0.5f); // Semi-transparent green
    private void OnDrawGizmos()
    {
        
        Gizmos.color = hurtboxColor;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            Gizmos.DrawCube(col.bounds.center, col.bounds.size); //color fill for hurtbox
        }
    }
    #endregion
}
