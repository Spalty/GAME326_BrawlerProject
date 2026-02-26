using Brawler.Combat;
using Brawler.Fighter;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //THE GAME IS HARD CAPPED TO 60 FPS, SO THIS IS A WAY TO MAKE SURE THE ATTACK LASTS FOR THE SAME AMOUNT OF TIME REGARDLESS OF FRAME RATE
    #region StateMachine Properties
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    #endregion

    private Rigidbody2D _playerRB;
    private InputManager _inputManager;

    private bool _isGrounded = true;
    private bool _isActionable = true;
    private bool _touchingBlockBox;
    
    private float _walkSpeed = 5f;
    
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
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody2D PlayerRB { get { return _playerRB; } set { _playerRB = value; } }
    public InputManager InputHandler { get { return _inputManager; } set { _inputManager = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool TouchingBlockBox { get { return _touchingBlockBox; } set { _touchingBlockBox = value; } }
    public float WalkSpeed { get { return _walkSpeed; } set { _walkSpeed = value; } }
    public bool IsActionable { get { return _isActionable; } set { _isActionable = value; } }

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
        _inputManager = GetComponent<InputManager>();
        
        _states = new PlayerStateFactory(this);
        _currentState = _states.Ground();
        _currentState.EnterState();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _currentState.UpdateAllStates();
    }

}
