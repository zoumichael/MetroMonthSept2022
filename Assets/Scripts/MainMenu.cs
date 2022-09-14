using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }


    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
