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
        if (!Context.IsCrouching)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.InputHandler.IsLightAttackPressed && Context.IsCrouching)
        {
            SwitchState(Factory.CRLightAttack());
        }
        else if (Context.InputHandler.IsMediumAttackPressed && Context.IsCrouching)
        {
            SwitchState(Factory.CRMediumAttack());
        }
        else if (Context.InputHandler.IsHeavyAttackPressed && Context.IsCrouching)
        {
            SwitchState(Factory.CRHeavyAttack());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
        if (Context.InputHandler.IsLightAttackPressed && Context.IsCrouching)
        {
            SetSubState(Factory.CRLightAttack());
        }
        else if (Context.InputHandler.IsMediumAttackPressed && Context.IsCrouching)
        {
            SetSubState(Factory.CRMediumAttack());
        }
        else if (Context.InputHandler.IsHeavyAttackPressed && Context.IsCrouching)
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
