using UnityEngine;

public class PlayerWalkRightState : PlayerBaseState
{
    public PlayerWalkRightState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    private Rigidbody2D PlayerRB => Context.PlayerRB;
    private FighterData FighterData => Context.FightData;
    private InputHandler InputHandler => Context.InputHandler;

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_ForwardWalk;

        //Logic

        //Animation
        Context.AnimController.SetMoveType(MovementType.Walking);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        HandleWalkingForward();

        //Block Logic
        Vector2 directionToOpponent = Context.Opponent.transform.position - Context.transform.position;

        float opponentDirectionSign = Mathf.Sign(directionToOpponent.x);
        float inputDirectionSign = Mathf.Sign(InputHandler.HorizontalInput);

        Context.IsWalkingBack = inputDirectionSign != opponentDirectionSign;

        //Animation
        MoveDirection moveDirection = Context.IsWalkingBack ? MoveDirection.Left : MoveDirection.Right;
        Context.AnimController.SetMoveDirection(moveDirection);

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
            SwitchState(Factory.RightDash());
        }
        else if (Context.IsBlocking)
        {
            SwitchState(Factory.StandBlock());
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
