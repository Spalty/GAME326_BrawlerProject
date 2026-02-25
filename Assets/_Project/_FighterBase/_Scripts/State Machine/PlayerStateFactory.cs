using System.Collections;
using System.Collections.Generic;

public class PlayerStateFactory
{
    enum PlayerStates
    {
        //Root States
        Airborne,
        Ground,
        
        //Ground Sub States
        Standing,
        Crouch,
        
        //Standing Sub States
        Idle,
        ForwardWalk,
        ForwardDash,
        BackWalk,
        BackDash,
        StandBlock,
        LightAttack,
        MediumAttack,
        HeavyAttack,
        WasHitStanding,
        
        //Crouch Sub States
        CrouchBlock,
        CRLightAttack,
        CRMediumAttack,
        CRHeavyAttack,
        WasHitCrouching,
       
        //Air Sub States
        AirBlock,
        JLightAttack,
        JMediumAttack,
        JHeavyAttack,
        WasHitAirborne
    }

    PlayerStateMachine _context;
    Dictionary<PlayerStates, PlayerBaseState> _stateCache = new Dictionary<PlayerStates, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
        //root states
        _stateCache[PlayerStates.Airborne] = new PlayerAirborneState(_context, this);
        _stateCache[PlayerStates.Ground] = new PlayerGroundState(_context, this);
        
        //Ground sub states
        _stateCache[PlayerStates.Standing] = new PlayerStandingState(_context, this);
        _stateCache[PlayerStates.Crouch] = new PlayerCrouchState(_context, this);
        
        //Standing sub states
        _stateCache[PlayerStates.Idle] = new PlayerIdleState(_context, this);
        _stateCache[PlayerStates.ForwardWalk] = new PlayerForwardWalkState(_context, this);
        _stateCache[PlayerStates.ForwardDash] = new PlayerForwardDashState(_context, this);
        _stateCache[PlayerStates.BackWalk] = new PlayerBackWalkState(_context, this);
        _stateCache[PlayerStates.BackDash] = new PlayerBackDashState(_context, this);
        _stateCache[PlayerStates.StandBlock] = new PlayerStandBlockState(_context, this);
        _stateCache[PlayerStates.LightAttack] = new PlayerLightAttackState(_context, this);
        _stateCache[PlayerStates.MediumAttack] = new PlayerMediumAttackState(_context, this);
        _stateCache[PlayerStates.HeavyAttack] = new PlayerHeavyAttackState(_context, this);
        _stateCache[PlayerStates.WasHitStanding] = new PlayerWasHitStandingState(_context, this);
        
        //Crouch sub states
        _stateCache[PlayerStates.CrouchBlock] = new PlayerCrouchBlockState(_context, this);
        _stateCache[PlayerStates.CRLightAttack] = new PlayerCRLightAttackState(_context, this);
        _stateCache[PlayerStates.CRMediumAttack] = new PlayerCRMediumAttackState(_context, this);
        _stateCache[PlayerStates.CRHeavyAttack] = new PlayerCRHeavyAttackState(_context, this);
        _stateCache[PlayerStates.WasHitCrouching] = new PlayerWasHitCrouchingState(_context, this);
        
        //Airborne sub states
        _stateCache[PlayerStates.AirBlock] = new PlayerAirBlockState(_context, this);
        _stateCache[PlayerStates.JLightAttack] = new PlayerJLightAttackState(_context, this);
        _stateCache[PlayerStates.JMediumAttack] = new PlayerJMediumAttackState(_context, this);
        _stateCache[PlayerStates.JHeavyAttack] = new PlayerJHeavyAttackState(_context, this);
        _stateCache[PlayerStates.WasHitAirborne] = new PlayerWasHitAirborneState(_context, this);
    }

    public PlayerBaseState Ground()
    {
        return _stateCache[PlayerStates.Ground];
    }
    public PlayerBaseState Standing()
    {
        return _stateCache[PlayerStates.Standing];
    }
    #region ---Standing Sub States---
    public PlayerBaseState Idle()//Idle is the default sub state of Ground, so it will be the first state to enter when we switch to Ground
    {
        return _stateCache[PlayerStates.Idle];
    }
    public PlayerBaseState ForwardWalk()
    {
        return _stateCache[PlayerStates.ForwardWalk];
    }
    public PlayerBaseState ForwardDash()
    {
        return _stateCache[PlayerStates.ForwardDash];
    }
    public PlayerBaseState BackWalk()
    {
        return _stateCache[PlayerStates.BackWalk];
    }
    public PlayerBaseState BackDash()
    {
        return _stateCache[PlayerStates.BackDash];
    }
    public PlayerBaseState StandBlock()
    {
        return _stateCache[PlayerStates.StandBlock];
    }
    public PlayerBaseState LightAttack()
    {
        return _stateCache[PlayerStates.LightAttack];
    }
    public PlayerBaseState MediumAttack()
    {
        return _stateCache[PlayerStates.MediumAttack];
    }
    public PlayerBaseState HeavyAttack()
    {
        return _stateCache[PlayerStates.HeavyAttack];
    }
    public PlayerBaseState WasHitStanding()
    {
        return _stateCache[PlayerStates.WasHitStanding];
    }

    
    #endregion
    public PlayerBaseState Crouch()//Crouch is the default sub state of Ground when the player is holding down, so it will be the first state to enter when we switch to Ground while holding down
    {
        return _stateCache[PlayerStates.Crouch];
    }
    #region ---Crouch Sub States---
    public PlayerBaseState CrouchBlock()
    {
        return _stateCache[PlayerStates.CrouchBlock];
    }
    public PlayerBaseState CRLightAttack()
    {
        return _stateCache[PlayerStates.CRLightAttack];
    }
    public PlayerBaseState CRMediumAttack()
    {
        return _stateCache[PlayerStates.CRMediumAttack];
    }
    public PlayerBaseState CRHeavyAttack()
    {
        return _stateCache[PlayerStates.CRHeavyAttack];
    }
    public PlayerBaseState WasHitCrouching()
    {
        return _stateCache[PlayerStates.WasHitCrouching];
    }
    #endregion
    public PlayerBaseState Airborne()
    {
        return _stateCache[PlayerStates.Airborne];
    }
    #region ---Airborne Sub States---
    public PlayerBaseState AirBlock()
    {
        return _stateCache[PlayerStates.AirBlock];
    }
    public PlayerBaseState JLightAttack()
    {
        return _stateCache[PlayerStates.JLightAttack];
    }
    public PlayerBaseState JMediumAttack()
    {
        return _stateCache[PlayerStates.JMediumAttack];
    }
    public PlayerBaseState JHeavyAttack()
    {
        return _stateCache[PlayerStates.JHeavyAttack];
    }
    public PlayerBaseState WasHitAirborne()
    {
        return _stateCache[PlayerStates.WasHitAirborne];
    }
    #endregion
}