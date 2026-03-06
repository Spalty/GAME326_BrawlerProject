using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    FighterData FighterData => Context.FightData;

    public override void EnterState()
    {
        InitializeSubState();
        Context.CurrentRootState = RootStates.Grounded;
        
        Context.PlayerRB.linearVelocity = Vector2.zero; // Stop player movement when entering grounded state

        Context.AnimController.SetGroundedBool(Context.IsGrounded());
    }

    public override void InitializeSubState()
    {
        if (Context.InputHandler.VerticalInput < 0)
        {
            SetSubState(Factory.Crouch());
        }
        else
        {
            SetSubState(Factory.Standing());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.InputHandler.WasJumpPressed)
        {
            SwitchState(Factory.Jump());
        }
    }

    public override void ExitState()
    {
        Context.AnimController.SetGroundedBool(false);
        Context.IsGrounded();

        Context.JumpCount = FighterData.MaxJumpCount; //Reset jump count when exiting grounded state
        Context.AirDashCount = FighterData.MaxAirDashCount; //Reset air dash count when exiting grounded state
    }
}
