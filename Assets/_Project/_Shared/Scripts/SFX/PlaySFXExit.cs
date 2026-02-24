using UnityEngine;

public class PlaySFXExit : StateMachineBehaviour
{
    [Header("---SFX Settings---")]
    [SerializeField] private SoundType sfxType;
    [Range(0, 1)][SerializeField] private float volume = 1;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SFXManager.PlaySound(sfxType, volume);
    }
}
