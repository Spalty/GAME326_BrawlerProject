using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        // Implementation for entering idle state
        Debug.Log("Entering Idle State");
    }

    public override void UpdateState()
    {
        // Implementation for updating idle state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting idle state
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
