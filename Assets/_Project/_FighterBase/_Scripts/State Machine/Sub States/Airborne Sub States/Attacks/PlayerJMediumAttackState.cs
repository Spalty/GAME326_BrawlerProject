using UnityEngine;

public class PlayerJMediumAttackState : PlayerBaseState
{
    public PlayerJMediumAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        // Implementation for entering jump medium attack state
        Debug.Log("Entering Jump Medium Attack State");
    }

    public override void UpdateState()
    {
        // Implementation for updating jump medium attack state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting jump medium attack state
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
