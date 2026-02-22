using UnityEngine;

public class PlayerCrouchBlockState : PlayerBaseState
{
    public PlayerCrouchBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        // Implementation for entering crouch block state
        Debug.Log("Entering Crouch Block State");
    }

    public override void UpdateState()
    {
        // Implementation for updating crouch block state
    }

    public override void ExitState()
    {
        // Implementation for exiting crouch block state
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
