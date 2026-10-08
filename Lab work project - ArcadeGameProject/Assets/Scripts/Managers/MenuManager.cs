using UnityEngine;

public class MenuManager : MonoBehaviour
{ 
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TwoPlayerMode()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("2Player");
    }
}
