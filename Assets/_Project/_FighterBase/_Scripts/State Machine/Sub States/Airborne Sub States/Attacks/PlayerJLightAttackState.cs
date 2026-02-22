using UnityEngine;

public class PlayerJLightAttackState : PlayerBaseState
{
    public PlayerJLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        // Implementation for entering jump light attack state
        Debug.Log("Entering Jump Light Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating jump light attack state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting jump light attack state
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
