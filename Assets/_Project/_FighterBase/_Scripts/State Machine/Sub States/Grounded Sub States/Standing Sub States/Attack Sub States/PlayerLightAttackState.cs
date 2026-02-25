using System.Collections;
using UnityEngine;

public class PlayerLightAttackState : PlayerBaseState
{   
    IEnumerator WaitForFrames(int frameCount)//Timer for how many frames the attack should last
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return Context.IsActionable = true;
        }
    
    }
    public PlayerLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }
    public override void EnterState()
    {
        
        Debug.Log("Entering Light Attack State");
        
        //PLAY LIGHT ATTACK ANIMATION
        Context.IsActionable = false;
        Context.StartCoroutine(WaitForFrames(20)); // Assuming 20 frames for the attack
        
    }

    public override void UpdateState()
    {
        
        CheckSwitchState();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchState()
    {
        
        if (Context.IsActionable)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
