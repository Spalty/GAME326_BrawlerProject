using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        // Implementation for entering crouch state
        Debug.Log("Entering Crouch State");
    }

    public override void UpdateState()
    {
        // Implementation for updating crouch state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting crouch state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (Context.InputHandler.verticalInput >= 0)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.InputHandler.IsLightAttackPressed && Context.InputHandler.verticalInput < 0)
        {
            SwitchState(Factory.CRLightAttack());
        }
        else if (Context.InputHandler.IsMediumAttackPressed && Context.InputHandler.verticalInput < 0)
        {
            SwitchState(Factory.CRMediumAttack());
        }
        else if (Context.InputHandler.IsHeavyAttackPressed && Context.InputHandler.verticalInput < 0)
        {
            SwitchState(Factory.CRHeavyAttack());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
        if (Context.InputHandler.IsLightAttackPressed && Context.InputHandler.verticalInput < 0)
        {
            SetSubState(Factory.CRLightAttack());
        }
        else if (Context.InputHandler.IsMediumAttackPressed && Context.InputHandler.verticalInput < 0)
        {
            SetSubState(Factory.CRMediumAttack());
        }
        else if (Context.InputHandler.IsHeavyAttackPressed && Context.InputHandler.verticalInput < 0)
        {
            SetSubState(Factory.CRHeavyAttack());
        }
        /*else if (Context.MoveDirection < 0 && Context.isCrouching)
        {
            SetSubState(Factory.CrouchBlock());
        }
        */
    }
}
