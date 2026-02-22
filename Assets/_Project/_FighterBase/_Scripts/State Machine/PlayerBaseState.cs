public abstract class PlayerBaseState
{
    protected PlayerStateMachine _context;
    protected PlayerStateFactory _factory;
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _context = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();

    void UpdateStates(){}
    void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();
    }

    void SetSuperState(PlayerBaseState superState)
    {
        // Implementation for setting super state
    }

    void SetSubState(PlayerBaseState subState)
    {
        // Implementation for setting sub state
    }
}
