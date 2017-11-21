using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string inGame = "PACMan";

    public void startGame()
    {
        SceneManager.LoadScene(inGame);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
