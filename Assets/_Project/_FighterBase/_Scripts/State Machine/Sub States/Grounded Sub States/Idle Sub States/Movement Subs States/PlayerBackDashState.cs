using UnityEngine;

public class PlayerBackDashState : PlayerBaseState
{
    public PlayerBackDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering back dash state
        Debug.Log("Entering Back Dash State");
    }

    public override void UpdateState()
    {
        // Implementation for updating back dash state
    }

    public override void ExitState()
    {
        // Implementation for exiting back dash state
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
