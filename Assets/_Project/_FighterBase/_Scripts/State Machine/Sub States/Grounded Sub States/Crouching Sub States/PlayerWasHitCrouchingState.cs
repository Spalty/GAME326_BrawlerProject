using UnityEngine;

public class PlayerWasHitCrouchingState : PlayerBaseState
{
    public PlayerWasHitCrouchingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        // Implementation for entering was hit crouching state
        Debug.Log("Entering Was Hit Crouching State");
    }

    public override void UpdateState()
    {
        // Implementation for updating was hit crouching state
    }

    public override void ExitState()
    {
        // Implementation for exiting was hit crouching state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
    }
}
