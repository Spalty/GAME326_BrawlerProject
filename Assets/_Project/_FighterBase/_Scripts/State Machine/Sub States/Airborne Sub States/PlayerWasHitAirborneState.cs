using UnityEngine;

public class PlayerWasHitAirborneState : PlayerBaseState
{
    public PlayerWasHitAirborneState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        // Implementation for entering was hit airborne state
        Debug.Log("Entering Was Hit Airborne State");
    }

    public override void UpdateState()
    {
        // Implementation for updating was hit airborne state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting was hit airborne state
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
