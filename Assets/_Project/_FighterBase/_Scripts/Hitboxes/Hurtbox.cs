using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    //Player Index
    private int _playerIndex;
    public int PlayerIndex { get { return _playerIndex; } set { _playerIndex = value; } }

    #region ---Methods---
    public void TakeDamage(float damage, float hitstunDuration, float baseKnockback, Vector2 knockbackAngle, AttackType attackContext)
    {
        // Implement damage application logic here
        FighterGM.Instance.FireHitEvent(_playerIndex, damage);
    }
    #endregion
    
    #region ---Debug---
    [Header("Debug")]
    [Tooltip("Visualize the hurtbox in the editor.")]
    [SerializeField] private Color hurtboxColor = new(0f, 1f, 0f, 0.5f); // Semi-transparent green
    private void OnDrawGizmos()
    {
        Gizmos.color = hurtboxColor;
        if (TryGetComponent<Collider2D>(out var col))
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            Gizmos.DrawCube(col.bounds.center, col.bounds.size); //color fill for hurtbox
        }
    }
    #endregion
}
