using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public void OpenTestLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void CloseGame()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }
}
