using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //State Machine Variables
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private Rigidbody2D _playerRB;

    private InputHandler _inputHandler;

    private bool _isGrounded = true;
    private bool _isCrouching;
    private bool _isMoving;
    private bool _touchingBlockBox;
    
    private float _walkSpeed = 5f;
    
    #region ---Getter/Setters---
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody2D PlayerRB { get { return _playerRB; } set { _playerRB = value; } }
    public InputHandler InputHandler { get { return _inputHandler; } set { _inputHandler = value; } }
    public bool IsCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
    public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool TouchingBlockBox { get { return _touchingBlockBox; } set { _touchingBlockBox = value; } }
    public float WalkSpeed { get { return _walkSpeed; } set { _walkSpeed = value; } }
    #endregion
    
    void Awake()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Ground();
        _currentState.EnterState();
    }

    void Start()
    {
        _currentState.EnterState();
    }

    void Update()
    {
        _currentState.UpdateAllStates();
    }

}
