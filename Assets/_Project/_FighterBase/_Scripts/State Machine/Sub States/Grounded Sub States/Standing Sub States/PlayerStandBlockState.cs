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
        HandleKnockback();
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

    private void HandleKnockback()
    {
        Vector2 direction = Context.Hurtbox.OnHitData.knockbackAngle;
        Vector2 directionToOpponent = (Context.Opponent.position - Context.transform.position).normalized;

        Vector2 knockBackDirection = new(direction.x * -Mathf.Sign(directionToOpponent.x), direction.y);

        Context.PlayerRB.linearVelocity = knockBackDirection * Context.Hurtbox.OnHitData.baseKnockback;
    }
}
