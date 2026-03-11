using UnityEngine;
using System.Collections;

public class PlayerJHeavyAttackState : PlayerBaseState
{
    public PlayerJHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    InputHandler InputHandler => Context.InputHandler;
    
    public override void EnterState()
    {
        Context.CurrentSubSubState = SubSubStates.Air_HeavyAtk;
        
        Context.IsActionable = false;
        Context.StartCoroutine(WaitForFrames(10)); // Assuming 40 frames for the heavy
    }

    public override void UpdateState()
    {
        
        CheckSwitchState();
    }


    public override void CheckSwitchState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }
    
    public override void ExitState()
    {
        InputHandler.WasHeavyAttackPressed = false; // Reset the input flag
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
