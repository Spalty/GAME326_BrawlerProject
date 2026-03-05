using UnityEngine;
using UnityEngine.SceneManagement;

public class RematchButton : MonoBehaviour
{
    public void RestartGame()
    {
        TestGameManager.Instance.ResetMatch();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
