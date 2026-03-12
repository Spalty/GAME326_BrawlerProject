using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private int _playerIndex;
    public int PlayerIndex { get { return _playerIndex; } set { _playerIndex = value; } }

    [Header("---Hit Flash Settings---")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private Color blockColor = Color.blue;

    private void OnEnable()
    {
        FighterGameEvents.OnPlayerHit += HandleHitFlash;
        FighterGameEvents.OnPlayerBlock += HandleBlockFlash;
    }

    private void OnDisable()
    {
       FighterGameEvents.OnPlayerHit -= HandleHitFlash;
       FighterGameEvents.OnPlayerBlock -= HandleBlockFlash;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void HandleHitFlash(PlayerHitEvent playerHitEvent)
    {
        if (playerHitEvent.PlayerIndex == _playerIndex)
        {
            StartCoroutine(Flash(0.1f, hitColor));
        }
    }

    private void HandleBlockFlash(PlayerBlockEvent playerBlockEvent)
    {
        if (playerBlockEvent.PlayerIndex == _playerIndex)
        {
            StartCoroutine(Flash(0.1f, blockColor));
        }
    }

    private IEnumerator Flash(float duration, Color color)
    {
        Color originalColor = Color.white;
        _spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        _spriteRenderer.color = originalColor;
    }
}
