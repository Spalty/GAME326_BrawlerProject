using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //State Machine Variables
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    public Rigidbody2D PlayerRB;

    private bool _isGrounded;
    private bool _isCrouching;
    private bool _isMoving;
    private bool _wasDashPressed;
    private bool _touchingBlockBox;

    private bool _isLightAttackPressed;
    private bool _isMediumAttackPressed;
    private bool _isHeavyAttackPressed;
    
    private float _moveDirection;
    private float _walkSpeed = 5f;
    
    void Awake()
    {
        _states = new PlayerStateFactory(this);//Initialize the state factory and pass in a reference to this state machine so that the states can access it
        _currentState = _states.Ground();//Set the initial state to Ground, which will then set its default sub state to Idle in its EnterState() method
        _currentState.EnterState();
    }

    void Start()
    {
        _currentState.EnterState();
    }

    void Update()
    {
        _currentState.UpdateState();
    }

    #region ---Getter/Setters---
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    //public Rigidbody2D PlayerRB { get { return _playerRB; } set { _playerRB = value; } }
    public bool isCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
    public bool isMoving { get { return _isMoving; } set { _isMoving = value; } }
    public float MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
    public bool isLightAttackPressed { get { return _isLightAttackPressed; } set { _isLightAttackPressed = value; } }
    public bool isMediumAttackPressed { get { return _isMediumAttackPressed; } set { _isMediumAttackPressed = value; } }
    public bool isHeavyAttackPressed { get { return _isHeavyAttackPressed; } set { _isHeavyAttackPressed = value; } }
    public bool isGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool WasDashPressed { get { return _wasDashPressed; } set { _wasDashPressed = value; } }
    public bool TouchingBlockBox { get { return _touchingBlockBox; } set { _touchingBlockBox = value; } }
    public float WalkSpeed { get { return _walkSpeed; } set { _walkSpeed = value; } }

    #endregion
}
