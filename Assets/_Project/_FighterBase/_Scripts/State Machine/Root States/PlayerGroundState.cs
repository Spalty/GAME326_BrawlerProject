using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        
    }
    public override void EnterState()
    {
        InitializeSubState();
        
        Debug.Log("Entering Ground State");
    }

    public override void UpdateState()
    {
        
        CheckSwitchState();
    }

    public override void ExitState() { }
    

    public override void CheckSwitchState()
    {
        if (!Context.IsGrounded)
        {
            SwitchState(Factory.Airborne());
        }
    }

    public override void InitializeSubState()
    {

        if (Context.InputHandler.verticalInput < 0)
        {
            SetSubState(Factory.Crouch());
        }
        else
        {
            SetSubState(Factory.Standing());
        }
    }
}
