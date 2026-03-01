using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Context.CurrentSubState = SubStates.Crouching;
    }

    public override void InitializeSubState()
    {
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
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.InputHandler.verticalInput >= 0)
        {
            SwitchState(Factory.Standing());
        }
    }

    public override void ExitState() { }
}
