public abstract class PlayerBaseState
{
    private PlayerStateMachine _context;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;
    private bool _isRootState = false;

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

    void UpdateAllStates()
    {
        UpdateState();

        if (_currentSubState != null)
        {
            _currentSubState.UpdateAllStates();
        }
    }
    protected void SwitchState(PlayerBaseState newState)
    {
        //Exit current state
        ExitState();

        //New state becomes current state
        newState.EnterState();
        if (_isRootState)
        {  
            _context.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
        
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

    #region ---Getters---
    protected PlayerStateMachine Context { get { return _context; } }
    protected PlayerStateFactory Factory { get { return _factory; } }
    protected bool IsRootState { set { _isRootState = value; } }

    #endregion
}
