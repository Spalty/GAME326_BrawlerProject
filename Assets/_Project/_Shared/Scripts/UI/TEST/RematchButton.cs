using UnityEngine;
using UnityEngine.SceneManagement;

public class RematchButton : MonoBehaviour
{
    public void RestartGame()
    {
        FighterGM.Instance.ResetMatch();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
