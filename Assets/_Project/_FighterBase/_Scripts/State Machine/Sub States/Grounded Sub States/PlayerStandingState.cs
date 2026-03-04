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
        if (Context.InputHandler.moveDirection > 0)
        {
            SetSubState(Factory.ForwardWalk());
        }
        else if (Context.InputHandler.moveDirection < 0)
        {
            SetSubState(Factory.BackWalk());
        }
        else if (Context.InputHandler.IsLightAttackPressed)
        {
            SetSubState(Factory.LightAttack());
        }
        else if (Context.InputHandler.IsMediumAttackPressed)
        {
            SetSubState(Factory.MediumAttack());
        }
        else if (Context.InputHandler.IsHeavyAttackPressed)
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
        if (Context.InputHandler.verticalInput < 0)
        {
            SwitchState(Factory.Crouch());
        }
    }

    public override void ExitState() { }
}
