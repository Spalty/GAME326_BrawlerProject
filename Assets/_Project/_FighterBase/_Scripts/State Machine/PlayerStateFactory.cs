public class PlayerStateFactory
{
   PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerGroundState Ground()
    {
        return new PlayerGroundState(_context, this);
    }
    public PlayerIdleState Idle()//Idle is the default sub state of Ground, so it will be the first state to enter when we switch to Ground
    {
        return new PlayerIdleState(_context, this);
    }
    #region ---Idle Sub States---
    public PlayerForwardWalkState ForwardWalk()
    {
        return new PlayerForwardWalkState(_context, this);
    }
    public PlayerForwardDashState ForwardDash()
    {
        return new PlayerForwardDashState(_context, this);
    }
    public PlayerBackWalkState BackWalk()
    {
        return new PlayerBackWalkState(_context, this);
    }
    public PlayerBackDashState BackDash()
    {
        return new PlayerBackDashState(_context, this);
    }
    public PlayerStandBlockState StandBlock()
    {
        return new PlayerStandBlockState(_context, this);
    }
    public PlayerLightAttackState LightAttack()
    {
        return new PlayerLightAttackState(_context, this);
    }
    public PlayerMediumAttackState MediumAttack()
    {
        return new PlayerMediumAttackState(_context, this);
    }
    public PlayerHeavyAttackState HeavyAttack()
    {
        return new PlayerHeavyAttackState(_context, this);
    }
    public PlayerWasHitStandingState WasHitStanding()
    {
        return new PlayerWasHitStandingState(_context, this);
    }

    
    #endregion
    public PlayerCrouchState Crouch()//Crouch is the default sub state of Ground when the player is holding down, so it will be the first state to enter when we switch to Ground while holding down
    {
        return new PlayerCrouchState(_context, this);
    }
    #region ---Crouch Sub States---
    public PlayerCrouchBlockState CrouchBlock()
    {
        return new PlayerCrouchBlockState(_context, this);
    }
    public PlayerCRLightAttackState CRLightAttack()
    {
        return new PlayerCRLightAttackState(_context, this);
    }
    public PlayerCRMediumAttackState CRMediumAttack()
    {
        return new PlayerCRMediumAttackState(_context, this);
    }
    public PlayerCRHeavyAttackState CRHeavyAttack()
    {
        return new PlayerCRHeavyAttackState(_context, this);
    }
    public PlayerWasHitCrouchingState WasHitCrouching()
    {
        return new PlayerWasHitCrouchingState(_context, this);
    }
    #endregion
    public PlayerAirborneState Airborne()
    {
        return new PlayerAirborneState(_context, this);
    }
    #region ---Airborne Sub States---
    public PlayerAirBlockState AirBlock()
    {
        return new PlayerAirBlockState(_context, this);
    }
    public PlayerJLightAttackState JLightAttack()
    {
        return new PlayerJLightAttackState(_context, this);
    }
    public PlayerJMediumAttackState JMediumAttack()
    {
        return new PlayerJMediumAttackState(_context, this);
    }
    public PlayerJHeavyAttackState JHeavyAttack()
    {
        return new PlayerJHeavyAttackState(_context, this);
    }
    public PlayerWasHitAirborneState WasHitAirborne()
    {
        return new PlayerWasHitAirborneState(_context, this);
    }
    #endregion
}