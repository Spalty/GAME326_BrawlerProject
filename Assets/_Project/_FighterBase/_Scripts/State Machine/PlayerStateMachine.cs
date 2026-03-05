using UnityEngine;
using Brawler.Combat;
using NaughtyAttributes;
using UnityEngine.XR;

public enum RootStates
{
    Grounded,
    Airborne,
    Jump,
}

public enum SubStates
{
    //Grounded Sub States
    Standing,
    Crouching,

    //Airborne SubStates
    Air_LightAtk,
    Air_MediumAtk,
    Air_HeavyAtk,
    Air_Block,
    Air_Hit,
}

public enum SubSubStates
{
    None,

    //Standing SubSub States
    Stand_Idle,
    Stand_ForwardWalk,
    Stand_ForwardDash,
    Stand_BackWalk,
    Stand_BackDash,

    Stand_LightAtk,
    Stand_MediumAtk,
    Stand_HeavyAtk,
    Stand_Block,
    Stand_Hit,

    //Crouching SubSubStates
    Crouch_LightAtk,
    Crouch_MediumAtk,
    Crouch_HeavyAtk,
    Crouch_Block,
    Crouch_Hit,
}

public class PlayerStateMachine : MonoBehaviour
{
    //THE GAME IS HARD CAPPED TO 60 FPS,
    //THIS MAKES SURE THE ATTACK LASTS FOR THE SAME AMOUNT OF TIME REGARDLESS OF FRAME RATE

    #region StateMachine Properties
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    //Debug Properties
    private RootStates _currentRootState;
    private SubStates _currentSubState;
    private SubSubStates _currentSubSubState;
    public RootStates CurrentRootState { get { return _currentRootState; } set { _currentRootState = value; } }
    public SubStates CurrentSubState { get { return _currentSubState; } set { _currentSubState = value; } }
    public SubSubStates CurrentSubSubState { get { return _currentSubSubState; } set { _currentSubSubState = value; } }
    #endregion

    private Rigidbody2D _playerRB;
    private InputHandler _inputHandler;
    private FighterAnimController _animController;
    private int _jumpCount;

    
    private bool _isActionable = true;
    private bool _touchingBlockBox;
    [Header("---Ground Check---")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;

    [Header("---Fighter Data---")]
    [Expandable][SerializeField] private FighterData fighterData;
     
    [Header("---Attack Data---")]
    [Expandable][SerializeField] private AttackData lightAtk;
    [Expandable][SerializeField] private AttackData mediumAtk;
    [Expandable][SerializeField] private AttackData heavyAtk;
    [Space(10)]
    [Expandable][SerializeField] private AttackData jLightAtk;
    [Expandable][SerializeField] private AttackData jMediumAtk;
    [Expandable][SerializeField] private AttackData jHeavyAtk;
    [Space(10)]
    [Expandable][SerializeField] private AttackData crLightAtk;
    [Expandable][SerializeField] private AttackData crMediumAtk;
    [Expandable][SerializeField] private AttackData crHeavyAtk;

    #region ---Getter/Setters---
    public Rigidbody2D PlayerRB { get { return _playerRB; } set { _playerRB = value; } }
    public InputHandler InputHandler { get { return _inputHandler; } set { _inputHandler = value; } }
    public FighterAnimController AnimController { get { return _animController; } set { _animController = value; } }

    //public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool TouchingBlockBox { get { return _touchingBlockBox; } set { _touchingBlockBox = value; } }
    public bool IsActionable { get { return _isActionable; } set { _isActionable = value; } }
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }

    //Fighter Data
    public FighterData FightData => fighterData;

    //AttackData
    public AttackData LightAtk => lightAtk;
    public AttackData MediumAtk => mediumAtk;
    public AttackData HeavyAtk => heavyAtk;
    public AttackData JLightAtk => jLightAtk;
    public AttackData JMediumAtk => jMediumAtk;
    public AttackData JHeavyAtk => jHeavyAtk;
    public AttackData CRLightAtk => crLightAtk;
    public AttackData CRMedium => crMediumAtk;
    public AttackData CRHeavyAtk => crHeavyAtk;
    #endregion
    
    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<InputHandler>();
        _animController = GetComponent<FighterAnimController>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void Update()
    {
        HandleGroundCheck();
        _currentState.UpdateAllStates();
    }

    public void HandleGroundCheck()
    {
       if (IsGrounded())
        {
            _animController.SetGroundedBool(true);
        }
        else
        {
            _animController.SetGroundedBool(false);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }
}
