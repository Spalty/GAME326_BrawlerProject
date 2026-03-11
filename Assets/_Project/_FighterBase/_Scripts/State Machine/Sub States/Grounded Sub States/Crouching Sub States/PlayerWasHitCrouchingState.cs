using UnityEngine;

public class PlayerWasHitCrouchingState : PlayerBaseState
{
    public PlayerWasHitCrouchingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Crouch_Hit;

        //Logic

        //Animation
    }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void CheckSwitchState() { }

    public override void InitializeSubState() { }
}
