using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongOne : MonoBehaviour
{

    private LevelData level;
    public TextAsset levelFile;
    // Start is called before the first frame update
    void Start()
    {


        this.Button.onClick.AddListener(GameObject.Find("Scene Communicator").GetComponent<SceneCommunicator>().setSceneLevel(this.level));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void retrieveLevelData()
    {
        
    }


}
