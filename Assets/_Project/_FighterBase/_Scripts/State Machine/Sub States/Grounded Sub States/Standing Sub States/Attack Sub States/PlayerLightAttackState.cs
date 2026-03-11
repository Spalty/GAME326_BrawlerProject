using UnityEngine;

public class PlayerLightAttackState : PlayerBaseState
{   
    public PlayerLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    private InputHandler InputHandler => Context.InputHandler;

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_LightAtk;

        //Logic
        PlayerRB.linearVelocity = Vector2.zero; // Stop player movement during attack
        Context.IsActionable = false;

        //Animation
        Context.AnimController.TriggerAttack(Context.AnimController.LightAtkHash);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.IsActionable)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void ExitState()
    {
        InputHandler.WasLightAttackPressed = false; // Reset the input flag
    }
}
