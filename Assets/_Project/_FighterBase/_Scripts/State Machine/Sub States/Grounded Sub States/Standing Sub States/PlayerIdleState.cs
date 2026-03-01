using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
   
    public override void EnterState()
    {
        InitializeSubState();
        Context.CurrentSubSubState = SubSubStates.Stand_Idle;

        Context.AnimController.SetMoveType(MovementType.Idle);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.InputHandler.moveDirection > 0)
        {
            SwitchState(Factory.ForwardWalk());
        }
        else if (Context.InputHandler.moveDirection < 0)
        {
            SwitchState(Factory.BackWalk());
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
    }

    public override void ExitState() { }
}
