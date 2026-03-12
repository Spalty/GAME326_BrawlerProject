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
        //Debug
        Context.CurrentRootState = RootStates.WasHit;
        Debug.Log($"Player {Context} was hit for {Context.Hitbox.HitstunFrames} ", Context.gameObject);

        //Logic
        Context.IsActionable = false; // Player cannot act while in hitstun
        Context.StartCoroutine(HandleHitstun(Context.Hitbox.HitstunFrames));

        //Animations & Effects 
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
        Context.WasHit = false;
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
