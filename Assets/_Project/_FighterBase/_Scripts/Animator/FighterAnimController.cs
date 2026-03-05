using UnityEngine;

public enum MoveDirection
{
    Left,
    Right,
}

public enum MovementType
{
    Idle,
    Walking,
    Dashing,
}

public class FighterAnimController : MonoBehaviour
{
    private Animator _animator;

    [Header("---Animator Header---")]
    //Conditional Hashes
    public int IsGroundedHash => Animator.StringToHash("IsGrounded");
    public int IsCrouchingHash => Animator.StringToHash("IsCrouching");
    public int IsBlockingHash => Animator.StringToHash("IsBlocking");
    public int IsHitHash => Animator.StringToHash("IsHit");

    //Movement Hashes
    public int MoveDirectionX => Animator.StringToHash("MoveDirX");
    public int MoveValue => Animator.StringToHash("HorizontalSpeed");
    public int VerticalSpeed => Animator.StringToHash("VerticalSpeed");
    public int JumpingHash => Animator.StringToHash("Jumping");

    //Attack Hashes
    public int LightAtkHash => Animator.StringToHash("LightAtk");
    public int MediumAtkHash => Animator.StringToHash("MediumAtk");
    public int HeavyAtkHash => Animator.StringToHash("HeavyAtk");

    public int JLightAtkHash => Animator.StringToHash("JLightAtk");
    public int JMediumAtkHash => Animator.StringToHash("JMediumAtk");
    public int JHeavyAtkHash => Animator.StringToHash("JHeavyAtk");

    public int CRLightAtkHash => Animator.StringToHash("CRLightAtk");
    public int CRMediumAtkHash => Animator.StringToHash("CRMediumAtk");
    public int CRHeavyAtkHash => Animator.StringToHash("CRHeavyAtk");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    #region Conditional Methods
    public void SetGroundedBool(bool isGrounded)
    {
        _animator.SetBool(IsGroundedHash, isGrounded);
    }

    public void SetCrouchingBool(bool isCrouching)
    {
        _animator.SetBool(IsCrouchingHash, isCrouching);
    }

    public void SetBlockingBool(bool isBlocking)
    {
        _animator.SetBool(IsBlockingHash, isBlocking);
    }

    public void SetHitBool(bool isHit)
    {
        _animator.SetBool(IsHitHash, isHit);
    }
    #endregion

    #region Movement Methods
    public void SetMoveDirection(MoveDirection moveDirection)
    {
        float directionValue = moveDirection == MoveDirection.Left ? -1f : 1f;
        _animator.SetFloat(MoveDirectionX, directionValue);  
    }
    
    public void SetMoveType(MovementType movementType)
    {
        //The values are for the Movement Blend Tree in the Animator
        var moveValue = movementType switch
        {
            MovementType.Idle => 0f,
            MovementType.Walking => 0.5f,
            MovementType.Dashing => 1f,
            _ => 0f,
        };

        _animator.SetFloat(MoveValue, moveValue);
    }
    #endregion

    #region Attack Methods
    public void TriggerAttack(int attackHash)
    {
        _animator.SetTrigger(attackHash);
    }
    #endregion
}
