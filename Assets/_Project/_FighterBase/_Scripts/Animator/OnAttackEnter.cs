using UnityEngine;

public class OnAttackEnter : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerStateMachine playerSM = animator.gameObject.GetComponentInParent<PlayerStateMachine>();
        playerSM.IsActionable = false;
    }
}
