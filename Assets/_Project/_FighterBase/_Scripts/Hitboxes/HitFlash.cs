using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private int _playerIndex;
    public int PlayerIndex { get { return _playerIndex; } set { _playerIndex = value; } }

    [Header("---Hit Flash Settings---")]
    [SerializeField] private Color flashColor = Color.red;

    private void OnEnable()
    {
        FighterGameEvents.OnPlayerHit += HandleHitFlash;
    }

    private void OnDisable()
    {
       FighterGameEvents.OnPlayerHit -= HandleHitFlash;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void HandleHitFlash(PlayerHitEvent playerHitEvent)
    {
        if (playerHitEvent.PlayerIndex == _playerIndex)
        {
            StartCoroutine(Flash(0.1f));
        }
    }

    private IEnumerator Flash(float duration)
    {
        Color originalColor = _spriteRenderer.color;
        _spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.color = originalColor;
    }
}
