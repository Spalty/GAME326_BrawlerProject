using UnityEngine;

public class PlayerForwardWalkState : PlayerBaseState
{
    public PlayerForwardWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    private Rigidbody2D PlayerRB => Context.PlayerRB;
    private FighterData FighterData => Context.FightData;
    private InputHandler InputHandler => Context.InputHandler;

    public override void EnterState()
    {
        Context.CurrentSubSubState = SubSubStates.Stand_ForwardWalk;

        Context.AnimController.SetMoveType(MovementType.Walking);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        HandleWalkingForward();
        
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.InputHandler.HorizontalInput == 0)
        {
            SwitchState(Factory.Idle());
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
        else if (Context.InputHandler.WasDashPressed)
        {
            SwitchState(Factory.ForwardDash());
        }
    }

    public override void ExitState() { }

    private void HandleWalkingForward()
    {
        float xVelocity = InputHandler.HorizontalInput * FighterData.WalkSpeed;
        Vector2 moveVelocity = new(xVelocity, PlayerRB.linearVelocity.y);
        PlayerRB.linearVelocity = moveVelocity;
    }
}
