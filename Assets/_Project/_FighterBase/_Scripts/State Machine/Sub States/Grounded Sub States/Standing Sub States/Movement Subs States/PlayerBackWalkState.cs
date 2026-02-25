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
        HandleWalkingBackwards();

        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting back walk state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (Context.InputHandler.moveDirection == 0)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.InputHandler.verticalInput < 0)
        {
            SwitchState(Factory.Crouch());
        }
        else if (Context.InputHandler.IsLightAttackPressed)
        {
            SwitchState(Factory.LightAttack());
        }
        else if (Context.InputHandler.IsMediumAttackPressed)
        {
            SwitchState(Factory.MediumAttack());
        }
        else if (Context.InputHandler.IsHeavyAttackPressed)
        {
            SwitchState(Factory.HeavyAttack());
        }
        else if (Context.InputHandler.WasDashPressed)
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

    private void HandleWalkingBackwards()
    {
        // Implementation for handling backward walking movement
        Context.PlayerRB.linearVelocity = new Vector2(Context.InputHandler.moveDirection * Context.WalkSpeed, Context.PlayerRB.linearVelocity.y);
    }
}
