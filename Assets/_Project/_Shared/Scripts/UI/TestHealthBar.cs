using UnityEngine;
using UnityEngine.UI;

public class TestHealthBar : MonoBehaviour
{
    [Header("HealthBar Settings")]
    [SerializeField] private Image fill;
    [SerializeField] private Image background;

    private void OnEnable()
    {
        TestHealthManager.OnHealthUpdated += UpdateHealthBar;
    }

    private void OnDisable()
    {
        
    }

    private void UpdateHealthBar(int index)
    {
        Debug.Log("Hit Player");


    }
}
