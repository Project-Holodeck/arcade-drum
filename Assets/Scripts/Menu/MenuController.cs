using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu..?
/// </summary>
public class MenuController : MonoBehaviour
{

    public static MenuController instance;

    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadBeatmap()
    {
        SceneManager.LoadScene("Beatmap Level");
    }

}
