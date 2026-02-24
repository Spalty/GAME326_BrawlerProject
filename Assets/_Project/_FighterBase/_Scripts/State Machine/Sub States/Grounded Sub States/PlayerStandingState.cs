using UnityEngine;

public class PlayerStandingState : PlayerBaseState
{
    public PlayerStandingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        InitializeSubState();
        // Implementation for entering standing state
        Debug.Log("Entering Standing State");
    }

    public override void UpdateState()
    {
        // Implementation for updating standing state
        Debug.Log("Updating Standing State");
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting standing state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (Context.IsCrouching)
        {
            SwitchState(Factory.Crouch());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
        if (!Context.IsMoving)
        {
            SetSubState(Factory.Idle());
        }
        else if (Context.InputHandler.MoveDirection > 0)
        {
            SetSubState(Factory.ForwardWalk());
        }
        else if (Context.InputHandler.MoveDirection < 0)
        {
            SetSubState(Factory.BackWalk());
        }
        else if(Context.InputHandler.IsLightAttackPressed)
        {
            SetSubState(Factory.LightAttack());
        }
        else if(Context.InputHandler.IsMediumAttackPressed)
        {
            SetSubState(Factory.MediumAttack());
        }
        else if(Context.InputHandler.IsHeavyAttackPressed)
        {
            SetSubState(Factory.HeavyAttack());
        }
        else 
        {
            SetSubState(Factory.Idle());
        }
    }
}
