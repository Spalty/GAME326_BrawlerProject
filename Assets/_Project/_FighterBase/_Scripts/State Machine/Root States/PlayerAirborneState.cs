using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

   public override void EnterState()
    {
        InitializeSubState();

        //Debug
        Context.CurrentRootState = RootStates.Airborne;

        //Logic

        //Animation
        Context.AnimController.SetGroundedBool(false);
    }

    public override void InitializeSubState()
    {
        SwitchState(Factory.Falling());
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.IsGrounded())
        {
            SwitchState(Factory.Grounded());
        }
        else if (Context.InputHandler.WasJumpPressed)
        {
            SwitchState(Factory.Jump());
        }
    }

    public override void ExitState() { }
}
