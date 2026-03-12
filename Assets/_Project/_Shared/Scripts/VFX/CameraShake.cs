using System.Collections;
using UnityEngine;
using NaughtyAttributes;

public class CameraShake : MonoBehaviour
{
    private Coroutine _cameraShakeRoutine;
    private Vector2 _initialPosition;

    [Header("---Settings---")]
    [Expandable]
    [SerializeField] private VFXData vfxData;

    [Header("---Debug---")]
    public bool useButton;
    [ShowIf("useButton")]
    [Button]
    public void TestShakeCamera()
    {
        _initialPosition = transform.position;
        _cameraShakeRoutine = StartCoroutine(ShakeFor(vfxData.CameraShakeDuration, vfxData.AmplitudeVariance));
    }

    private void OnEnable()
    {
        FighterGameEvents.OnStrongHit += ShakeCamera;
    }

    private void OnDisable()
    {

        FighterGameEvents.OnStrongHit -= ShakeCamera;
    }

    private void ShakeCamera(StrongHitEvent strongHitEvent)
    {
        _initialPosition = transform.position;
        float randomVariance = Random.Range(-vfxData.AmplitudeVariance, vfxData.AmplitudeVariance);
        _cameraShakeRoutine = StartCoroutine(ShakeFor(vfxData.CameraShakeDuration, vfxData.CameraShakeAmplitude + randomVariance));
    }

    private IEnumerator ShakeFor(float duration, float amplitude)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;

            transform.localPosition = Random.insideUnitCircle * amplitude;
            yield return null;
        }

        transform.localPosition = _initialPosition;
        StopCoroutine(_cameraShakeRoutine);
        _cameraShakeRoutine = null;
    }
}
