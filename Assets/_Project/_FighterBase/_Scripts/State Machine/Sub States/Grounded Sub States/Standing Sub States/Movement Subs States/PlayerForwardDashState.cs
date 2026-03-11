using UnityEngine;

public class PlayerForwardDashState : PlayerBaseState
{
    public PlayerForwardDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_ForwardDash;

        //Logic

        //Animation
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
