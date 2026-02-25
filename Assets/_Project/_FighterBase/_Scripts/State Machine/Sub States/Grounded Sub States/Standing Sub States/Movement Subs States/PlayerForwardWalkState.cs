using UnityEngine;

public class PlayerForwardWalkState : PlayerBaseState
{
    public PlayerForwardWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering forward walk state
        Debug.Log("Entering Forward Walk State");
    }

    public override void UpdateState()
    {
        // Implementation for updating forward walk state
        HandleWalkingForward();
        
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting forward walk state
    }


    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
    }
    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (Context.InputHandler.moveDirection == 0)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.InputHandler.verticalInput < 0)
        {
            SwitchState(Factory.Crouch());
        }
        else if (Context.InputHandler.IsLightAttackPressed)
        {
            SwitchState(Factory.LightAttack());
        }
        else if (Context.InputHandler.IsMediumAttackPressed)
        {
            SwitchState(Factory.MediumAttack());
        }
        else if (Context.InputHandler.IsHeavyAttackPressed)
        {
            SwitchState(Factory.HeavyAttack());
        }
        else if (Context.InputHandler.WasDashPressed)
        {
            SwitchState(Factory.ForwardDash());
        }
    }

    private void HandleWalkingForward()
    {
        Context.PlayerRB.linearVelocity = new Vector2(Context.InputHandler.moveDirection * Context.WalkSpeed, Context.PlayerRB.linearVelocity.y);
    }
}
