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
        Debug.Log("Entering Airborne State");

        Context.AnimController.SetGroundedBool(false);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void ExitState() { }
}
