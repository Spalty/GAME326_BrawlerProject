using UnityEngine;
using Brawler.Combat;
using NaughtyAttributes;

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

    [Header("---Ground Check---")]
    [SerializeField] private Transform groundCheck;
    [Space(10)]
    [SerializeField] private LayerMask groundLayer;
    private const float groundCheckRadius = 0.2f;
    private int _airDashCount;
    private int _jumpCount;

    private bool _isActionable = true;
    private bool _touchingBlockBox;

    public Transform Opponent { get; set; }

    [Header("---Fighter Data---")]
    [Expandable][SerializeField] private FighterData fighterData;
     
    #region ---Getter/Setters---
    public Rigidbody2D PlayerRB { get { return _playerRB; } set { _playerRB = value; } }
    public InputHandler InputHandler { get { return _inputHandler; } set { _inputHandler = value; } }
    public FighterAnimController AnimController { get { return _animController; } set { _animController = value; } }

    //public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool TouchingBlockBox { get { return _touchingBlockBox; } set { _touchingBlockBox = value; } }
    public bool IsActionable { get { return _isActionable; } set { _isActionable = value; } }
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
    public int AirDashCount { get { return _airDashCount; } set { _airDashCount = value; } }

    //Fighter Data
    public FighterData FightData => fighterData;

    //AttackData
    public AttackData LightAtk => fighterData.LightAtk;
    public AttackData MediumAtk => fighterData.MediumAtk;
    public AttackData HeavyAtk => fighterData.HeavyAtk;
    public AttackData JLightAtk => fighterData.JLightAtk;
    public AttackData JMediumAtk => fighterData.JMediumAtk;
    public AttackData JHeavyAtk => fighterData.JHeavyAtk;
    public AttackData CRLightAtk => fighterData.CRLightAtk;
    public AttackData CRMedium => fighterData.CRMediumAtk;
    public AttackData CRHeavyAtk => fighterData.CRHeavyAtk;
    #endregion
    
    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<InputHandler>();
        _animController = GetComponent<FighterAnimController>();
    }

    private void Start()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void Update()
    {
        HandleSpriteFlipping();

        _currentState.UpdateAllStates();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }

    /// <summary>
    /// Ensures the player is always facing their opponent by by flipping the transform's local scale X
    /// </summary>
    private void HandleSpriteFlipping()
    {
        Vector2 directionToOpponent = Opponent.position - transform.position;
        float flippedScaleX = directionToOpponent.x < 0 ? -1 : 1;
        transform.localScale = new(Mathf.Abs(transform.localScale.x) * flippedScaleX, transform.localScale.y, transform.localScale.z);
    }
}

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
    Falling,
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

    //Falling SubSubStates
    FallingIdle,
    AirDash,
    Air_LightAtk,
    Air_MediumAtk,
    Air_HeavyAtk,
    Air_Block,
    Air_Hit,
}