using UnityEngine;

public class PlayerCRMediumAttackState : PlayerBaseState
{
    public PlayerCRMediumAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering crouch medium attack state
        Debug.Log("Entering Crouch Medium Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating crouch medium attack state
    }

    public override void ExitState()
    {
        // Implementation for exiting crouch medium attack state
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
