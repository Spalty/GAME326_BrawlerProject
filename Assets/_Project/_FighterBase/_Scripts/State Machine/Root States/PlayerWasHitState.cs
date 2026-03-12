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

        //Logic
        Context.IsActionable = false; // Player cannot act while in hitstun
        Context.StartCoroutine(HandleHitstun(Context.Hurtbox.HitstunFrames));

        //Animations & Effects
        //Play hit animation, spawn hit effects here
    }
    public override void InitializeSubState() { }

    public override void UpdateState()
    {
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
        Context.WasHit = false;
    }
    
    private IEnumerator HandleHitstun(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        Context.IsActionable = true;
    }
}
