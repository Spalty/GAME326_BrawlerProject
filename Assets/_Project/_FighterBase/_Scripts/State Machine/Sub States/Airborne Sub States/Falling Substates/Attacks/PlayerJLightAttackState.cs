using UnityEngine;
using System.Collections;

public class PlayerJLightAttackState : PlayerBaseState
{
    public PlayerJLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    InputHandler InputHandler => Context.InputHandler;
    public override void EnterState()
    {
        Context.CurrentSubSubState = SubSubStates.Air_LightAtk;
        
        Context.IsActionable = false;
        Context.StartCoroutine(WaitForFrames(20));

        //Animation
        //PLAYER JUMP LIGHT ATTACK ANIMATION HERE
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }


    public override void CheckSwitchState()
    {
        
        if (Context.IsActionable)
        {
            SwitchState(Factory.FallingIdle());
        }
    }

    public override void InitializeSubState() { }
    
    public override void ExitState()
    {
        InputHandler.WasLightAttackPressed = false; // Reset the input flag
    }
    
    IEnumerator WaitForFrames(int frameCount)//Timer for how many frames the attack should last
    {

        for (int i = 0; i < frameCount; i++)
        {
            yield return null;
        }
        Context.IsActionable = true;
    }
}
