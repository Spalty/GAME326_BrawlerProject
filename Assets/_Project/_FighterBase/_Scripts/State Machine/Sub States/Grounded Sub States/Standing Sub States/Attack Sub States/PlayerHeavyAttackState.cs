using UnityEngine;

public class PlayerHeavyAttackState : PlayerBaseState
{
    public PlayerHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory){}
    
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    
    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_HeavyAtk;

        //Logic
        Context.Hitbox.Data = Context.HeavyAtk; 
        PlayerRB.linearVelocity = Vector2.zero; // Stop player movement during attack
        Context.IsActionable = false;

        //Animation
        Context.AnimController.TriggerAttack(Context.AnimController.HeavyAtkHash);
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
        Context.InputHandler.WasHeavyAttackPressed = false; // Reset the input flag
    }
}
