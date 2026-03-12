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
        HandleHitStun();
        //Apply Knockback

        //Animations & Effects
        Context.AnimController.SetHitBool(true);
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
    
    private void HandleHitStun()
    {
        if (Context.HitStunCoroutine != null)
        {
            Context.StopCoroutine(Context.HitStunCoroutine);
            Context.HitStunCoroutine = null;
        }

        Context.HitStunCoroutine = Context.StartCoroutine(HitStun(Context.Hurtbox.HitstunFrames));
    }

    private IEnumerator HitStun(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        Context.IsActionable = true;
    }
}
