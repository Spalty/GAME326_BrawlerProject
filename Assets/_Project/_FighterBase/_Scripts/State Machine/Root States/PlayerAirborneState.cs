using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    InputHandler InputHandler => Context.InputHandler;
    private int Jumpcount => Context.JumpCount;
   public override void EnterState()
    {
        InitializeSubState();
        Debug.Log("Entering Airborne State");
        Context.CurrentRootState = RootStates.Airborne;

        Context.AnimController.SetGroundedBool(false);
    }

    public override void InitializeSubState()
    {
        SetSubState(Factory.Falling()); //MAKE FALLING SUBSTATE LATER
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
        else if (InputHandler.WasJumpPressed && Jumpcount > 0)
        {
            SwitchState(Factory.Jump());
        }
    }

    public override void ExitState() { }
}
