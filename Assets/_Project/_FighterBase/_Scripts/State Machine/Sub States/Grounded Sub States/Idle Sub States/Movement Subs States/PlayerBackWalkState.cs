using UnityEngine;

public class PlayerBackWalkState : PlayerBaseState
{
    public PlayerBackWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering back walk state
        Debug.Log("Entering Back Walk State");
    }

    public override void UpdateState()
    {
        // Implementation for updating back walk state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting back walk state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (!Context.isMoving || Context.MoveDirection == 0)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.isCrouching)
        {
            SwitchState(Factory.Crouch());
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
        else if (Context.WasDashPressed)
        {
            SwitchState(Factory.BackDash());
        }
        else if (Context.TouchingBlockBox)
        {
            SwitchState(Factory.StandBlock());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
    }
}
