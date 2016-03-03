using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

	public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void StartButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
