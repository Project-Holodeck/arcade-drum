using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu..?
/// </summary>
public class MenuController : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MusicScene");
    }
}
