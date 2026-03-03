using Brawler.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class P1HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // gameOverUI.SetActive(false);
    }

    void GameOver()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("GameOver");
    }

    

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {

            GameOver();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    
}
