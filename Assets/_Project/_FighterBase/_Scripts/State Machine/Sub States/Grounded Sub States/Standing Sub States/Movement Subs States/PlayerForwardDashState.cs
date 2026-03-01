using UnityEngine;

public class PlayerForwardDashState : PlayerBaseState
{
    public PlayerForwardDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Entering Forward Dash State");

        Context.AnimController.SetMoveType(MovementType.Dashing);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState() { }

    public override void CheckSwitchState() { }
}
