using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    public override void EnterState()
    {
        InitializeSubState();
        // Implementation for entering idle state
        Debug.Log("Entering Idle State");
    }

    public override void UpdateState()
    {
        // Implementation for updating idle state
        
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting idle state
    }

    public override void CheckSwitchState()
    {
        
        if (Context.InputHandler.moveDirection > 0)
        {
            SwitchState(Factory.ForwardWalk());
        }
        else if (Context.InputHandler.moveDirection < 0)
        {
            SwitchState(Factory.BackWalk());
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
    }

    public override void InitializeSubState()
    {
        
    }
}
