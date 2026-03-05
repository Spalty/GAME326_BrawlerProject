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
        if (Context.InputHandler.WasLightAttackPressed && Context.InputHandler.VerticalInput < 0)
        {
            SetSubState(Factory.CRLightAttack());
        }
        else if (Context.InputHandler.WasMediumAttackPressed && Context.InputHandler.VerticalInput < 0)
        {
            SetSubState(Factory.CRMediumAttack());
        }
        else if (Context.InputHandler.WasHeavyAttackPressed && Context.InputHandler.VerticalInput < 0)
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
        if (Context.InputHandler.VerticalInput >= 0)
        {
            SwitchState(Factory.Standing());
        }
    }

    public override void ExitState() { }
}
