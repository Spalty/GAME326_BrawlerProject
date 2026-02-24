using System;
using UnityEngine;
using NaughtyAttributes;

public enum SoundType
{
    Attack,
    Hit,
    TEST,
}

/// <summary>
/// Limitations:
/// - No support for looping sounds (e.g., background music or continuous effects); do this on another script.
/// - Only supports one AudioSource and plays clips using PlayOneShot, which is suitable for short sound effects.
/// 
/// Could be improved by pooling SFX sources for better control over multiple simultaneous sounds
/// </summary>
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SFXManager : Singleton<SFXManager>
{
    private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;
    
    [Header("---SFXs---")]
    [SerializeField] private SoundList[] soundLists;

    [Header("---SFX Settings---")]
    public bool useRandomPitch;
    [ShowIf("useRandomPitch")]
    [SerializeField] private float minPitch = 0.8f;
    [ShowIf("useRandomPitch")]
    [SerializeField] private float maxPitch = 1.2f;

    [Header("---Debug---")]
    public bool useDebugButtons;
    [ShowIf("useDebugButtons")]
    [Button]
    public void PlayTestSound()
    {
        PlaySound(SoundType.TEST);
    }

    protected override void Awake()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif

        base.Awake();

        _audioSource = GetComponent<AudioSource>();
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundLists, names.Length);
        for (int i = 0; i < soundLists.Length; i++)
        {
            soundLists[i].name = names[i];
        }
    }
#endif

    public static void PlaySound(SoundType clip, float volume = 1)
    {
        AudioClip[] clips = Instance.soundLists[(int)clip].Clips;
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning($"SoundManager: No clips found for {clip}");
            return;
        }

        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        if (Instance.useRandomPitch)
        {
            Instance.AudioSource.pitch = UnityEngine.Random.Range(Instance.minPitch, Instance.maxPitch);
        }
        else
        {
            Instance.AudioSource.pitch = 1f;
        }

        Instance.AudioSource.PlayOneShot(randomClip, volume);
    }
}

[Serializable]
public struct SoundList
{
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] clips;

    public readonly AudioClip[] Clips => clips;
}
