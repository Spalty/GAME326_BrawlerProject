using UnityEngine;

public class PlayerLightAttackState : PlayerBaseState
{
    public PlayerLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering light attack state
        Debug.Log("Entering Light Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating light attack state
    }

    public override void ExitState()
    {
        // Implementation for exiting light attack state
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
