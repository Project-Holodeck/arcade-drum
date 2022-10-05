using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
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
