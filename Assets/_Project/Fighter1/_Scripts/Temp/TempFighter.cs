using UnityEngine;
using Brawler.Fighter;
using Brawler.Input;

public class TempFighter : FighterBase
{
    private SpriteRenderer _spriteRenderer;

    [Header("---Fighter Settings---")]
    [SerializeField] private string fighterName = "TempFighter";
    [Tooltip("Custom color tint for this fighter.")]
    [SerializeField] private Color fighterColor = Color.white;

    public override string FighterName => fighterName;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        base.Initialize(PlayerIndex, Input);
    }

    protected override void OnFighterInitialized()
    {
        // Apply color tint
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = fighterColor;
        }

        Debug.Log($"[{FighterName}] Initialized as Player {PlayerIndex + 1}");
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTakeDamage(float damage)
    {
        Debug.Log($"[{FighterName}] Took {damage} damage! Health: {Health.CurrentHealth}/{Health.MaxHealth}");

        // Flash red briefly
        if (_spriteRenderer != null)
        {
            StartCoroutine(DamageFlash());
        }
    }

    private System.Collections.IEnumerator DamageFlash()
    {
        Color originalColor = _spriteRenderer.color;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = originalColor;
    }

    protected override void OnKO()
    {
        Debug.Log($"[{FighterName}] KO'd!");
    }

    protected override void OnRespawn(Vector2 position)
    {
        Debug.Log($"[{FighterName}] Respawned at {position}");

        // Brief invincibility flash
        if (_spriteRenderer != null)
        {
            StartCoroutine(RespawnFlash());
        }
    }

    private System.Collections.IEnumerator RespawnFlash()
    {
        float duration = 2f;
        float flashRate = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(flashRate);
            elapsed += flashRate;
        }

        _spriteRenderer.enabled = true;
        EndRespawnInvincibility();
    }
}
