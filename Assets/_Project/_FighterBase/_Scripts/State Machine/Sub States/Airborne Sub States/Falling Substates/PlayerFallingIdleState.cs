using UnityEngine;

public class PlayerFallingIdleState : PlayerBaseState
{
    public PlayerFallingIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = false; 
    }  
    
    InputHandler InputHandler => Context.InputHandler;
    public override void EnterState()
    {
        //Devbug
        Context.CurrentSubSubState = SubSubStates.FallingIdle;

        //Animations & Effects
        Context.AnimController.SetJumpingBool(true);
        //Context.AnimController.SetFallingIdleBool(true);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if(InputHandler.WasJumpPressed && Context.AirDashCount < Context.FightData.MaxAirDashCount)
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
        //Context.AnimController.SetFallingIdleBool(false);
    }
}
