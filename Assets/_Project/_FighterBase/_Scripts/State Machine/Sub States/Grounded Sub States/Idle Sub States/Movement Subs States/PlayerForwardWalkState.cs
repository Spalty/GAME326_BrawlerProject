using UnityEngine;

public class PlayerForwardWalkState : PlayerBaseState
{
    public PlayerForwardWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering forward walk state
        Debug.Log("Entering Forward Walk State");
    }

    public override void UpdateState()
    {
        // Implementation for updating forward walk state
    }

    public override void ExitState()
    {
        // Implementation for exiting forward walk state
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
