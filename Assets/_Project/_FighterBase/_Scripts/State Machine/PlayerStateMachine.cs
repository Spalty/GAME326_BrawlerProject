using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //State Machine Variables
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    
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

    #endregion
}
