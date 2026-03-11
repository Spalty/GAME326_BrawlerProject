using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("---Player Index---")]
    [SerializeField] private int playerIndex; //This can be handled by a manager
    
    [Header("---HealthBar References---")]
    [SerializeField] private Image fill;

    private void OnEnable()
    {
        FighterGameEvents.OnPlayerHit += UpdateHealthBar;
    }

    private void OnDisable()
    {
        FighterGameEvents.OnPlayerHit -= UpdateHealthBar;
    }

    private void UpdateHealthBar(PlayerHitEvent playerHitEvent)
    {
        if (playerHitEvent.PlayerIndex == playerIndex)
        {
            float healthPercent = playerHitEvent.DamagePercent;
            fill.fillAmount = healthPercent;
        }
    }
}
