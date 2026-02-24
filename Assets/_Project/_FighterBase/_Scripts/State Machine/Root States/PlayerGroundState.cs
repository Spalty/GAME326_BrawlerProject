using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        
    }
    public override void EnterState()
    {
        InitializeSubState();
        
        // Implementation for entering ground state
        Debug.Log("Entering Ground State");
    }

    public override void UpdateState()
    {
        // Implementation for updating ground state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting ground state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (!Context.isGrounded)
        {
            SwitchState(Factory.Airborne());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states

        if (Context.isCrouching)
        {
            SetSubState(Factory.Crouch());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }
}
