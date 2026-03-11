using UnityEngine;

public class PlayerMediumAttackState : PlayerBaseState
{
    public PlayerMediumAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory){}
   
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    
    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_MediumAtk;
        
        //Logic
        PlayerRB.linearVelocity = Vector2.zero; // Stop player movement during attack
        Context.IsActionable = false;

        //Animation
        Context.AnimController.TriggerAttack(Context.AnimController.MediumAtkHash);
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
        Context.InputHandler.WasMediumAttackPressed = false; // Reset the input flag
    }
}
