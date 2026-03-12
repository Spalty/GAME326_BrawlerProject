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
        //Debug
        Context.CurrentRootState = RootStates.Grounded;

        //Logic
        Context.JumpCount = FighterData.MaxJumpCount; //Reset jump count when entering grounded state

        //Animations & Effects
        Context.AnimController.SetGroundedBool(true);
        Context.AnimController.SetJumpingBool(false);
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
        if (Context.WasHit)
        {
            SwitchState(Factory.WasHit());
        }
        else if (Context.InputHandler.WasJumpPressed)
        {
            SwitchState(Factory.Jump());
        }
        else if (!Context.IsGrounded())
        {
            SwitchState(Factory.Airborne());
        }
    }

    public override void ExitState()
    {
        Context.AnimController.SetGroundedBool(false);
        //Context.IsGrounded();

        
        Context.AirDashCount = FighterData.MaxAirDashCount; //Reset air dash count when exiting grounded state
    }
}
