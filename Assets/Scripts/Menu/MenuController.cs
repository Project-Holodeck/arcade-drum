using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu..?
/// </summary>
public class MenuController : MonoBehaviour
{

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void playGame()
    {
        SceneManager.LoadScene("MusicScene");
    }
}
