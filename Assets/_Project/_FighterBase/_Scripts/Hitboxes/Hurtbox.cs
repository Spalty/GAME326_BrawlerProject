using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    //Player Index
    private int _playerIndex;
    public int PlayerIndex { get { return _playerIndex; } set { _playerIndex = value; } }
    private PlayerStateMachine _hurtboxOwner;
    public PlayerStateMachine HurtBoxOwner { get { return _hurtboxOwner; } set { _hurtboxOwner = value; } }

    //Hitstun Frames
    private int _hitStunFrames;
    public int HitstunFrames { get { return _hitStunFrames; } set { _hitStunFrames = value; } }

    //Blockstun Frames
    private int _blockStunFrames;
    public int BlockStunFrames { get { return _blockStunFrames; } set { _blockStunFrames = value; } }


    #region ---Methods---
    public void TryTakeDamage(float damage, int hitstunDuration, float baseKnockback, Vector2 knockbackAngle, AttackType attackContext)
    {
        if (_hurtboxOwner.IsWalkingBack)
        {
            _hurtboxOwner.IsBlocking = true;
            FighterGameEvents.OnPlayerBlock?.Invoke(new PlayerBlockEvent(PlayerIndex));
            return;
        }

        // Implement damage application logic here
        FighterGM.Instance.HitPlayer(_playerIndex, damage);
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
