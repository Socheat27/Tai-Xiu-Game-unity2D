using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    private void Start()
    {
        ScreenOrientation landscapeRight = ScreenOrientation.LandscapeRight;
        Screen.orientation = landscapeRight;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
