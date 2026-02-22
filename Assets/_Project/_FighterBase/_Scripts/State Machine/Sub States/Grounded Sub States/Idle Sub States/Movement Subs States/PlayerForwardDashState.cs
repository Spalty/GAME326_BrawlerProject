using UnityEngine;

public class PlayerForwardDashState : PlayerBaseState
{
    public PlayerForwardDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering forward dash state
        Debug.Log("Entering Forward Dash State");
    }

    public override void UpdateState()
    {
        // Implementation for updating forward dash state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting forward dash state
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
