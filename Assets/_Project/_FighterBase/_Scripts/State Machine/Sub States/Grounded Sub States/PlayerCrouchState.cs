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
        if (!Context.isCrouching)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.isLightAttackPressed && Context.isCrouching)
        {
            SwitchState(Factory.CRLightAttack());
        }
        else if (Context.isMediumAttackPressed && Context.isCrouching)
        {
            SwitchState(Factory.CRMediumAttack());
        }
        else if (Context.isHeavyAttackPressed && Context.isCrouching)
        {
            SwitchState(Factory.CRHeavyAttack());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
        if (Context.isLightAttackPressed && Context.isCrouching)
        {
            SetSubState(Factory.CRLightAttack());
        }
        else if (Context.isMediumAttackPressed && Context.isCrouching)
        {
            SetSubState(Factory.CRMediumAttack());
        }
        else if (Context.isHeavyAttackPressed && Context.isCrouching)
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
