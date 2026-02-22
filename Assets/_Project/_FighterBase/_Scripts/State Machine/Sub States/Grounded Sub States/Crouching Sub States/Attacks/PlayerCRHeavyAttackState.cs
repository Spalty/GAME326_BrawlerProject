using UnityEngine;

public class PlayerCRHeavyAttackState : PlayerBaseState
{
    public PlayerCRHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering crouch heavy attack state
        Debug.Log("Entering Crouch Heavy Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating crouch heavy attack state
    }

    public override void ExitState()
    {
        // Implementation for exiting crouch heavy attack state
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
