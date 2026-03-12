using UnityEngine;

public class PlayerStandBlockState : PlayerBaseState
{
    public PlayerStandBlockState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Stand_Block;

        //Logic
        Context.PlayerRB.linearVelocity = Vector2.zero;

        //Animation
        //
    }
    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }


    public override void CheckSwitchState()
    {
        if (!Context.TouchingBlockBox || !Context.IsWalkingBack)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void ExitState() { }

}
