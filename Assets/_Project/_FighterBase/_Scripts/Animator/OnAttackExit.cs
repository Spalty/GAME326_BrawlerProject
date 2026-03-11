using UnityEngine;

public class OnAttackExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerStateMachine playerSM = animator.gameObject.GetComponentInParent<PlayerStateMachine>();
        playerSM.IsActionable = true;
    }
}
