using UnityEngine;

public class PlayerHeavyAttackState : PlayerBaseState
{
    public PlayerHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering heavy attack state
        Debug.Log("Entering Heavy Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating heavy attack state
    }

    public override void ExitState()
    {
        // Implementation for exiting heavy attack state
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
