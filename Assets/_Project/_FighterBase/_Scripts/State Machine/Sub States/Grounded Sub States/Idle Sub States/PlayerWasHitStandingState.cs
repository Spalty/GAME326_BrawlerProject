using UnityEngine;

public class PlayerWasHitStandingState : PlayerBaseState
{
    public PlayerWasHitStandingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering was hit standing state
        Debug.Log("Entering Was Hit Standing State");
    }

    public override void UpdateState()
    {
        // Implementation for updating was hit standing state
    }

    public override void ExitState()
    {
        // Implementation for exiting was hit standing state
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
