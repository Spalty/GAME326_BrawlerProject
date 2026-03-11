using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
   
    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_Idle;

        //Logic


        //Animation
        Context.AnimController.SetMoveType(MovementType.Idle);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.InputHandler.HorizontalInput > 0)
        {
            SwitchState(Factory.ForwardWalk());
        }
        else if (Context.InputHandler.HorizontalInput < 0)
        {
            SwitchState(Factory.BackWalk());
        }
        else if (Context.InputHandler.WasLightAttackPressed)
        {
            SwitchState(Factory.LightAttack());
        }
        else if (Context.InputHandler.WasMediumAttackPressed)
        {
            SwitchState(Factory.MediumAttack());
        }
        else if (Context.InputHandler.WasHeavyAttackPressed)
        {
            SwitchState(Factory.HeavyAttack());
        }
    }

    public override void ExitState() { }
}
