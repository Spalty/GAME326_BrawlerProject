using UnityEngine;

public class PlayerStandBlockState : PlayerBaseState
{
    public PlayerStandBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering stand block state
        Debug.Log("Entering Stand Block State");
    }

    public override void UpdateState()
    {
        // Implementation for updating stand block state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting stand block state
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
