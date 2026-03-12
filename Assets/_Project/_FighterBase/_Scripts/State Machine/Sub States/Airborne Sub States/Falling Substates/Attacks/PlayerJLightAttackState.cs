public class PlayerJLightAttackState : PlayerBaseState
{
    public PlayerJLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    InputHandler InputHandler => Context.InputHandler;

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Air_LightAtk;
        
        //Logic
        Context.Hitbox.Data = Context.LightAtk; 
        Context.IsActionable = false;

        //Animation
        Context.AnimController.TriggerAttack(Context.AnimController.JLightAtkHash);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.IsActionable)
        {
            SwitchState(Factory.FallingIdle());
        }
    }

    public override void InitializeSubState() { }
    
    public override void ExitState()
    {
        InputHandler.WasLightAttackPressed = false; // Reset the input flag
    }
}
