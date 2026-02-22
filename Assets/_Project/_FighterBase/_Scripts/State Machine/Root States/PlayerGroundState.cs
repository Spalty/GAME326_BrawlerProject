using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        // Implementation for entering ground state
        Debug.Log("Entering Ground State");
    }

    public override void UpdateState()
    {
        // Implementation for updating ground state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting ground state
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
