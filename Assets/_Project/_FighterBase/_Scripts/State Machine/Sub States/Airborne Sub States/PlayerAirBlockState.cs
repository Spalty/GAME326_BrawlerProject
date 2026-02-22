using UnityEngine;

public class PlayerAirBlockState : PlayerBaseState
{
    public PlayerAirBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        // Implementation for entering air block state
        Debug.Log("Entering Air Block State");
    }

    public override void UpdateState()
    {
        // Implementation for updating air block state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting air block state
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
