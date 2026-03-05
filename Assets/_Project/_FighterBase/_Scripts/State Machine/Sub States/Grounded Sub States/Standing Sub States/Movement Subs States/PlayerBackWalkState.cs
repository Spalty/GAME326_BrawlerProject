using UnityEngine;

public class PlayerBackWalkState : PlayerBaseState
{
    public PlayerBackWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    private InputHandler InputHandler => Context.InputHandler;
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    private FighterData FighterData => Context.FightData;

    public override void EnterState()
    {
        Context.CurrentSubSubState = SubSubStates.Stand_BackWalk;

        Context.AnimController.SetMoveType(MovementType.Walking);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        HandleWalkingBackwards();

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
            SwitchState(Factory.BackDash());
        }
        else if (Context.TouchingBlockBox)
        {
            SwitchState(Factory.StandBlock());
        }
    }

    public override void ExitState() { }

    private void HandleWalkingBackwards()
    {
        float xVelocity = InputHandler.HorizontalInput * FighterData.WalkSpeed;
        Vector2 moveVelocity = new(xVelocity, PlayerRB.linearVelocity.y);
        PlayerRB.linearVelocity = moveVelocity;
    }
}
