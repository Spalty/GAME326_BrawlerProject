using UnityEngine;

public class PlayerJHeavyAttackState : PlayerBaseState
{
    public PlayerJHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        // Implementation for entering jump heavy attack state
        Debug.Log("Entering Jump Heavy Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating jump heavy attack state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting jump heavy attack state
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
