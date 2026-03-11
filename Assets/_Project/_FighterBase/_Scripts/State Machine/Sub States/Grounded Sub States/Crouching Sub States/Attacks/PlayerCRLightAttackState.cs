using UnityEngine;

public class PlayerCRLightAttackState : PlayerBaseState
{
    public PlayerCRLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Debug
        Context.CurrentSubSubState = SubSubStates.Crouch_LightAtk;

        //Logic

        //Animation

    }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void CheckSwitchState() { }

    public override void InitializeSubState() { }
}
