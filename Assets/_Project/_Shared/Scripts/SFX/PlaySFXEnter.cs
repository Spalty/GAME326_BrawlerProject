using UnityEngine;

public class PlaySFXEnter : StateMachineBehaviour
{
    [Header("---SFX Settings---")]
    [SerializeField] private SoundType sfxType;
    [Range(0, 1)][SerializeField] private float volume = 1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SFXManager.PlaySound(sfxType, volume);
    }
}
