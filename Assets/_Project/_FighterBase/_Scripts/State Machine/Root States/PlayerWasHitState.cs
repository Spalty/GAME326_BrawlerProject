using System.Collections;
using UnityEngine;

public class PlayerWasHitState : PlayerBaseState
{
    public PlayerWasHitState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    
    public override void EnterState()
    {
        // Implementation for entering was hit standing state
        Context.IsActionable = false; // Player cannot act while in hitstun
        Context.StartCoroutine(HandleHitstun(Context.Hitbox.HitstunFrames)); // Assuming 30 frames of hit
        
        Debug.Log("Entering Was Hit Standing State");
        Debug.Log("Player was hit hit for " + Context.Hitbox.Data.damage + " damage");
    }
    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        // Implementation for updating was hit standing state
        CheckSwitchState();
    }


    public override void CheckSwitchState()
    {
        if (Context.IsActionable && Context.IsGrounded())
        {
            SwitchState(Factory.Grounded());
        }
        if (Context.IsActionable && !Context.IsGrounded())
        {
            SwitchState(Factory.Airborne());
        }
    }

    public override void ExitState()
    {
        // Implementation for exiting was hit standing state
    }
    
    IEnumerator HandleHitstun(float frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return null;
        }
        Context.IsActionable = true;
        
    }
}
