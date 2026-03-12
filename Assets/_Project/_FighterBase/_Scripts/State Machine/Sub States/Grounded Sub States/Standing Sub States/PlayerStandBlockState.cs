using System.Collections;
using UnityEngine;

public class PlayerStandBlockState : PlayerBaseState
{
    public PlayerStandBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_Block;

        //Logic
        Context.IsActionable = false;
        Context.PlayerRB.linearVelocity = Vector2.zero;
        HandleBlockStun();
        //Apply knockback
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (Context.IsActionable == true)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void ExitState()
    {
        Context.IsBlocking = false;
    }

    private void HandleBlockStun()
    {
        if (Context.BlockStunCoroutine != null)
        {
            Context.StopCoroutine(Context.BlockStunCoroutine);
            Context.BlockStunCoroutine = null;
        }

        Context.BlockStunCoroutine = Context.StartCoroutine(BlockStun(Context.Hurtbox.OnHitData.blockstunDuration));
    }

    private IEnumerator BlockStun(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        Context.IsActionable = true;
    }
}
