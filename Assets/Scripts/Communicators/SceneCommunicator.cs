using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneCommunicator : MonoBehaviour
{
    public LevelData sceneLevel;

    public static SceneCommunicator instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    void OnLevelWasLoaded()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Beatmap Level")
            GameObject.Find("LevelController").GetComponent<LevelController>().SetLevel(instance.sceneLevel);
    }

    public void SetBeatmapLevel(LevelData levelData)
    {
        sceneLevel = levelData;
    }
}
