using UnityEngine;
using System.Collections;

public class PlayerHeavyAttackState : PlayerBaseState
{
    public PlayerHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory){}
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    
    public override void EnterState()
    {
        // Implementation for entering heavy attack state
        Debug.Log("Entering Heavy Attack State");
        PlayerRB.linearVelocity = Vector2.zero; // Stop player movement during attack
        Context.IsActionable = false;
        Context.StartCoroutine(WaitForFrames(20)); // Assuming 20 frames for the attack

        //Animation
        Context.AnimController.TriggerAttack(Context.AnimController.HeavyAtkHash);
    }

    public override void UpdateState()
    {
        // Implementation for updating heavy attack state
        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Implementation for exiting heavy attack state
    }

    public override void CheckSwitchState()
    {
        // Implementation for checking state switches
        if (Context.IsActionable)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        // Implementation for initializing sub states
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
