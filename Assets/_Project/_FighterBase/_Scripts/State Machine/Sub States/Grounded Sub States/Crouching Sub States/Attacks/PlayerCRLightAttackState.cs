using UnityEngine;

public class PlayerCRLightAttackState : PlayerBaseState
{
    public PlayerCRLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering crouch light attack state
        Debug.Log("Entering Crouch Light Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating crouch light attack state
    }

    public override void ExitState()
    {
        // Implementation for exiting crouch light attack state
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
