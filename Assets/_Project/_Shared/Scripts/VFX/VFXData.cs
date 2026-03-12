using UnityEngine;

[CreateAssetMenu(fileName = "HitboxData", menuName = "Game Data/VFXData")]
public class VFXData : ScriptableObject
{
    [Header("---Camera---")]
    [SerializeField] private float cameraShakeDuration = 0.4f;
    [Space(10)]
    [SerializeField] private float cameraShakeAmplitude = 0.13f;
    [SerializeField] private float amplitudeVariance = 0.05f;

    [Header("---Shake Conditions---")]
    [SerializeField] private float minDamageForShake = 10f;

    public float CameraShakeDuration => cameraShakeDuration;
    public float CameraShakeAmplitude => cameraShakeAmplitude;
    public float AmplitudeVariance => amplitudeVariance;
    public float MinDamageForShake => minDamageForShake;
}
