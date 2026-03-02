using UnityEngine;
using System.Collections;

public class PlayerMediumAttackState : PlayerBaseState
{
    public PlayerMediumAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory){}
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    public override void EnterState()
    {
        Debug.Log("Entering Medium Attack State");
        PlayerRB.linearVelocity = Vector2.zero; // Stop player movement during attack
        Context.IsActionable = false;
        Context.StartCoroutine(WaitForFrames(20)); // Assuming 20 frames for the attack

        //Animation
        Context.AnimController.TriggerAttack(Context.AnimController.MediumAtkHash);
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
    IEnumerator WaitForFrames(int frameCount)//Timer for how many frames the attack should last
    {

        for (int i = 0; i < frameCount; i++)
        {
            yield return null;
        }
        Context.IsActionable = true;
    }
}
