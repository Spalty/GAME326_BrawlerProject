using UnityEngine;

public class PlayerBackWalkState : PlayerBaseState
{
    public PlayerBackWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering back walk state
        Debug.Log("Entering Back Walk State");
    }

    public override void UpdateState()
    {
        // Implementation for updating back walk state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting back walk state
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
