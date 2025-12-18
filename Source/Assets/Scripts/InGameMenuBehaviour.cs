using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuBehaviour : MonoBehaviour
{
    public void GoBackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
