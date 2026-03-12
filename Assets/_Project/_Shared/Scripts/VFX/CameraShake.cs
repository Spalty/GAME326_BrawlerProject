using UnityEngine;
using NaughtyAttributes;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Coroutine _cameraShakeRoutine;
    private Vector2 _initialPosition;

    [Header("---Settings---")]
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float amplitude = 0.5f;

    [Header("---Debug---")]
    public bool useButton;
    [ShowIf("useButton")]
    [Button] public void TestShakeCamera()
    {
        _initialPosition = transform.position;
        _cameraShakeRoutine = StartCoroutine(ShakeFor(duration, amplitude));
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
        _cameraShakeRoutine = StartCoroutine(ShakeFor(duration, amplitude));
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
