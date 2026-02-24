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
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting forward walk state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (!Context.isMoving || Context.MoveDirection == 0)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.isCrouching)
        {
            SwitchState(Factory.Crouch());
        }
        else if (Context.isLightAttackPressed)
        {
            SwitchState(Factory.LightAttack());
        }
        else if (Context.isMediumAttackPressed)
        {
            SwitchState(Factory.MediumAttack());
        }
        else if (Context.isHeavyAttackPressed)
        {
            SwitchState(Factory.HeavyAttack());
        }
        else if (Context.WasDashPressed)
        {
            SwitchState(Factory.ForwardDash());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
    }

    private void HandleWalkingForward()
    {
        Context.PlayerRB.linearVelocity = new Vector2(Context.MoveDirection * Context.WalkSpeed, Context.PlayerRB.linearVelocity.y);
    }
}
