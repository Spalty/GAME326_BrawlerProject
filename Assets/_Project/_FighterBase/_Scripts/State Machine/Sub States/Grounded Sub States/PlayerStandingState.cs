using UnityEngine;

public class PlayerStandingState : PlayerBaseState
{
    public PlayerStandingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        InitializeSubState();
        Context.CurrentSubState = SubStates.Standing;
    }

    public override void InitializeSubState()
    {
        if (Context.InputHandler.HorizontalInput > 0)
        {
            SetSubState(Factory.ForwardWalk());
        }
        else if (Context.InputHandler.HorizontalInput < 0)
        {
            SetSubState(Factory.BackWalk());
        }
        else if (Context.InputHandler.WasLightAttackPressed)
        {
            SetSubState(Factory.LightAttack());
        }
        else if (Context.InputHandler.WasMediumAttackPressed)
        {
            SetSubState(Factory.MediumAttack());
        }
        else if (Context.InputHandler.WasHeavyAttackPressed)
        {
            SetSubState(Factory.HeavyAttack());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.InputHandler.VerticalInput < 0)
        {
            SwitchState(Factory.Crouch());
        }
    }

    public override void ExitState() { }
}
