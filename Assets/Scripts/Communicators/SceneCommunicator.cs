using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneCommunicator : MonoBehaviour
{
    public LevelData sceneLevel;

    void Awake()
    {
        GameObject[] sceneCommunicators = GameObject.FindGameObjectsWithTag("SceneCommunicator");

        if(sceneCommunicators.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Beatmap Level")
            GameObject.Find("LevelController").GetComponent<LevelController>().SetLevel(sceneLevel);
    }

    public void SetSceneLevel(LevelData level)
    {
        this.sceneLevel = level;
    }
}
