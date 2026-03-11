using UnityEngine;
using System.Collections;

public class PlayerJMediumAttackState : PlayerBaseState
{
    public PlayerJMediumAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }
    InputHandler InputHandler => Context.InputHandler;
    
    public override void EnterState()
    {
        
        Context.CurrentSubSubState = SubSubStates.Air_MediumAtk;


        Context.IsActionable = false;
        Context.StartCoroutine(WaitForFrames(10)); // Assuming 10 frames for the medium
    }

    public override void UpdateState()
    {
        
        CheckSwitchState();
    }


    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
    }

    public override void InitializeSubState() { }

    public override void ExitState()
    {
        InputHandler.WasMediumAttackPressed = false; // Reset the input flag
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