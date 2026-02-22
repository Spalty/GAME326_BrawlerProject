using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {

    }
    public override void EnterState()
    {
        // Implementation for entering crouch state
        Debug.Log("Entering Crouch State");
    }

    public override void UpdateState()
    {
        // Implementation for updating crouch state
    }

    public override void ExitState()
    {
        // Implementation for exiting crouch state
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
