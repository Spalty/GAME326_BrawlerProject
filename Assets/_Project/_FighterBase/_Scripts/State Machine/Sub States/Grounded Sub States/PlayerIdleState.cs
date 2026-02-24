using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        InitializeSubState();
        // Implementation for entering idle state
        Debug.Log("Entering Idle State");
    }

    public override void UpdateState()
    {
        // Implementation for updating idle state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting idle state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (Context.isCrouching)
        {
            SwitchState(Factory.Crouch());
        }
        else if (Context.isMoving && Context.MoveDirection > 0)
        {
            SwitchState(Factory.ForwardWalk());
        }
        else if (Context.isMoving && Context.MoveDirection < 0)
        {
            SwitchState(Factory.BackWalk());
        }
        else if (Context.isLightAttackPressed)
        {
            SwitchState(Factory.LightAttack());
        }
        else if (Context.isMediumAttackPressed)
        {
            SwitchState(Factory.MediumAttack());
        }
        else if (Context.isHeavyAttackPressed)
        {
            SwitchState(Factory.HeavyAttack());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
        
        if(Context.isMoving && Context.MoveDirection > 0)
        {
            SetSubState(Factory.ForwardWalk());
        }
        else if(Context.isMoving && Context.MoveDirection < 0)
        {
            SetSubState(Factory.BackWalk());
        }
        else if(Context.isLightAttackPressed)
        {
            SetSubState(Factory.LightAttack());
        }
        else if(Context.isMediumAttackPressed)
        {
            SetSubState(Factory.MediumAttack());
        }
        else if(Context.isHeavyAttackPressed)
        {
            SetSubState(Factory.HeavyAttack());
        }
    }
}
