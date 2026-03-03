using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Brawler.Core;

/// <summary>
/// This is a code smell
/// P1HealthManager.cs and P2HealthManager.cs violates DRY principle
/// DRY = Dont Repeat Yourself
/// 
/// Both these scripts call the exact same methods and functions
/// This can be refactored into one single script
/// </summary>
public class P1HealthManager : MonoBehaviour
{
    //Should used serialized fields instead of public variables
    public Image healthBar;
    public float healthAmount = 100f;

    //Instead of Update this can be an event
    void Update()
    {
        //This needs to return early or player can still GetInput even when game is over
        if (healthAmount <= 0)
        {
            GameOver();
        }

        //The input is hard coded
        //This means everytime we want to change the input, we have to edit the script
        //This violates SOLID principles
        if (Input.GetKeyUp(KeyCode.A))
        {
            TakeDamage(10);
        }
    }

    //Unsure if loading a scene for GameOver is the right method
    //How would we seamlessly transtion between rounds?
    void GameOver()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("GameOver");
    }

    //Should use private properties unless this method is intended to be called from other scripts
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }
}
