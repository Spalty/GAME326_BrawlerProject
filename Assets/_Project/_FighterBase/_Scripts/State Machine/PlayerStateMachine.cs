using UnityEngine;
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

    private int playerIndex;
    public int PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }

    [Header("---Ground Check---")]
    [SerializeField] private Transform groundCheck;
    [Space(10)]
    [SerializeField] private LayerMask groundLayer;
    //private bool _isgrounded;
    
    private const float groundCheckRadius = 0.2f;
    private int _airDashCount;
    [SerializeField] private int _jumpCount;
    
    private bool _isActionable = true;
    private bool _isWalkingBack;
    private bool _isBlocking;

    public Transform Opponent { get; set; }

    [Header("---Fighter Data---")]
    [Expandable][SerializeField] private FighterData fighterData;

    [Header("---Hit / Hurt Boxes---")]
    [SerializeField] private Hitbox hitBox;
    [SerializeField] private Hurtbox hurtBox;
    private bool _wasHit;
    private Coroutine _hitStunCoroutine;
    private Coroutine _hitStopCoroutine;
    private Coroutine _blockStunCoroutine;

    [Header("---Hit Flash---")]
    [SerializeField] private HitFlash playerSprite;

    #region ---Getter/Setters---
    public Rigidbody2D PlayerRB { get { return _playerRB; } set { _playerRB = value; } }
    public InputHandler InputHandler { get { return _inputHandler; } set { _inputHandler = value; } }
    public FighterAnimController AnimController { get { return _animController; } set { _animController = value; } }

    //Hitbox / Hurtbox
    public Hitbox Hitbox { get { return hitBox; } set { hitBox = value; } }
    public Hurtbox Hurtbox { get { return hurtBox; } set { hurtBox = value; } }
    public bool WasHit {get { return _wasHit; }  set  {_wasHit = value;} }
    public Coroutine HitStunCoroutine { get { return _hitStunCoroutine; } set { _hitStunCoroutine = value; } }
    public Coroutine HitStopCoroutine { get { return _hitStopCoroutine; } set { _hitStopCoroutine = value; } }
    public Coroutine BlockStunCoroutine { get { return _blockStunCoroutine; } set { _blockStunCoroutine = value; } }

    //Movement
    //public bool IsGrounded { get { return _isgrounded; } set { _isgrounded = value; } }
    public bool IsBlocking { get { return _isBlocking; } set { _isBlocking = value; } }
    public bool IsActionable { get { return _isActionable; } set { _isActionable = value; } }
    public bool IsWalkingBack { get { return _isWalkingBack; } set {_isWalkingBack = value;} }
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
    public int AirDashCount { get { return _airDashCount; } set { _airDashCount = value; } }

    //Fighter Data
    public FighterData FightData => fighterData;

    //AttackData
    public HitboxData LightAtk => fighterData.LightAtk;
    public HitboxData MediumAtk => fighterData.MediumAtk;
    public HitboxData HeavyAtk => fighterData.HeavyAtk;
    public HitboxData JLightAtk => fighterData.JLightAtk;
    public HitboxData JMediumAtk => fighterData.JMediumAtk;
    public HitboxData JHeavyAtk => fighterData.JHeavyAtk;
    public HitboxData CRLightAtk => fighterData.CRLightAtk;
    public HitboxData CRMediumAtk => fighterData.CRMediumAtk;
    public HitboxData CRHeavyAtk => fighterData.CRHeavyAtk;
    #endregion

    private void OnEnable()
    {
        FighterGameEvents.OnPlayerHit += WasPlayerHit; // Subscribe to the event
    }
    private void OnDisable()
    {
        FighterGameEvents.OnPlayerHit -= WasPlayerHit; // Unsubscribe from the event
    }
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

    public void InitializePlayerHitbox()
    {
        if (hitBox != null) hitBox.PlayerIndex = PlayerIndex;
        
        if (hurtBox != null)
        {
            hurtBox.PlayerIndex = PlayerIndex;
            hurtBox.HurtBoxOwner = this;
        }

        if (playerSprite != null) playerSprite.PlayerIndex = PlayerIndex;
    }

    void Update()
    {
        HandleSpriteFlipping();

        _currentState.UpdateAllStates();
        IsGrounded();
        ResetJumpCounter();
    }

   private void ResetJumpCounter()
    {
        if(JumpCount == 0)
        {
            InputHandler.WasJumpPressed = false;
        }
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


    //This event only occurs when the player is hit and taking damage
    private void WasPlayerHit(PlayerHitEvent playerHitEvent)
    {
        if (playerHitEvent.PlayerIndex != playerIndex) return;

        _wasHit = true;
    }
}

public enum RootStates
{
    Grounded,
    Airborne,
    Jump,
    WasHit,
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