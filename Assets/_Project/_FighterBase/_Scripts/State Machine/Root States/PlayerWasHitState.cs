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
        HandleHitStop();
        HandleHitStun();
        //Apply Knockback

        //Animations & Effects
        Context.AnimController.SetHitBool(true);

        Quaternion randomRotationX = Quaternion.Euler(Random.Range(0, 360), 90, 0);
        Context.ParticlePool.SpawnFromPool(ParticleTypes.Hit, Context.transform.position, randomRotationX);
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
        Context.AnimController.SetHitBool(false);
    }
    
    private void HandleHitStun()
    {
        if (Context.HitStunCoroutine != null)
        {
            Context.StopCoroutine(Context.HitStunCoroutine);
            Context.HitStunCoroutine = null;
        }

        Context.HitStunCoroutine = Context.StartCoroutine(HitStun(Context.Hurtbox.OnHitData.hitstunDuration));
    }

    private IEnumerator HitStun(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        Context.IsActionable = true;
    }

    private void HandleHitStop()
    {
        if (Context.HitStunCoroutine != null)
        {
            Context.StopCoroutine(Context.HitStunCoroutine);
            Context.HitStunCoroutine = null;
        }    

        Context.HitStopCoroutine = Context.StartCoroutine(HitStop(Context.Hurtbox.OnHitData.hitstopDuration));
    }

    private IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
