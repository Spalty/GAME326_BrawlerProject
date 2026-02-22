using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
   public override void EnterState()
    {
        // Implementation for entering airborne state
        Debug.Log("Entering Airborne State");
    }

    public override void UpdateState()
    {
        // Implementation for updating airborne state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting airborne state
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
