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
    protected void SwitchState(PlayerBaseState newState)
    {
        //Exit current state
        ExitState();

        //New state becomes current state
        newState.EnterState();

        //switch current state of context
        _context.CurrentState = newState;
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        // Implementation for setting super state
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        // Implementation for setting sub state
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
