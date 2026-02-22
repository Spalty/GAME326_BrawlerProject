using UnityEngine;

public class PlayerMediumAttackState : PlayerBaseState
{
    public PlayerMediumAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering medium attack state
        Debug.Log("Entering Medium Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating medium attack state
    }

    public override void ExitState()
    {
        // Implementation for exiting medium attack state
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
