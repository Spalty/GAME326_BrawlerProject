using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = false; 
    }
    InputHandler InputHandler => Context.InputHandler;  
    
    public override void EnterState()
    {
        Context.CurrentSubState = SubStates.Falling;
        //Context.AnimController.SetFallingBool(true);
    }

    public override void InitializeSubState()
    {
        if (InputHandler.WasJumpPressed && Context.AirDashCount < Context.FightData.MaxAirDashCount)
        {
            SetSubState(Factory.AirDash());
        }
        else if(InputHandler.WasLightAttackPressed)
        {
            SetSubState(Factory.JLightAttack());
        }
        else if(InputHandler.WasMediumAttackPressed)
        {
            SetSubState(Factory.JMediumAttack());
        }
        else if(InputHandler.WasHeavyAttackPressed)
        {
            SetSubState(Factory.JHeavyAttack());
        }
        else
        {
            SetSubState(Factory.FallingIdle());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if(InputHandler.WasDashPressed && Context.AirDashCount < Context.FightData.MaxAirDashCount)
        {
            SwitchState(Factory.AirDash());
        }
        else if(InputHandler.WasLightAttackPressed)
        {
            SwitchState(Factory.JLightAttack());
        }
        else if(InputHandler.WasMediumAttackPressed)
        {
            SwitchState(Factory.JMediumAttack());
        }
        else if(InputHandler.WasHeavyAttackPressed)
        {
            SwitchState(Factory.JHeavyAttack());
        }
    }

    public override void ExitState() 
    {
        //Context.AnimController.SetFallingBool(false);
    }
}
