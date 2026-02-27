using UnityEngine;

public class FighterHealthBar : MonoBehaviour
{
    public RectTransform healthBar;

    public float maxHealth = 100f;
    private float currentHealth;

    private float originalWidth;

    void Start()
    {
        currentHealth = maxHealth;
        originalWidth = healthBar.sizeDelta.x;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float healthPercent = currentHealth / maxHealth;
        healthBar.sizeDelta = new Vector2(originalWidth * healthPercent, healthBar.sizeDelta.y);
    }

    void Update()
    {
        // TEST DAMAGE
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(10f);
        }
    }
}
