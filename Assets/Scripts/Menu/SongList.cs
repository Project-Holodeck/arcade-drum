using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SongList : MonoBehaviour
{
    public string songsFolder;

    private Dictionary<int, LevelData> levelDatas;
    private DirectoryInfo[] songDirectories;

    SceneCommunicator sceneCommunicator;
    MenuController menuController;


    // Start is called before the first frame update
    void Start()
    {

        songDirectories = new DirectoryInfo(Application.dataPath + "/" + songsFolder).GetDirectories();

        sceneCommunicator = SceneCommunicator.instance;
        menuController = MenuController.instance;

        levelDatas = new Dictionary<int, LevelData>();

        GenerateSongButtons();    
    }

    void GenerateSongButtons()
    {
        GameObject songButtonTemplate = transform.GetChild(0).gameObject;
        GameObject songButton;

        //Generates buttons for each song in Song Folder
        for (int i = 0; i < songDirectories.Length; i++)
        {
            int index = i;

            //Retrieve level information as string from json
            string levelDataText = File.ReadAllText(Application.dataPath + "/" + songsFolder + "/" + songDirectories[i].Name + "/" + songDirectories[i].GetFiles("*.json")[0].Name);

            //Format level information into LevelData
            LevelData levelInfo = JsonUtility.FromJson<LevelData>(levelDataText);
            levelDatas[i] = levelInfo;
            
            songButton = Instantiate(songButtonTemplate, transform);

            //Display song information in button
            songButton.transform.GetChild(0).GetComponent<Text>().text = levelInfo.songName;
            songButton.transform.GetChild(1).GetComponent<Text>().text = levelInfo.albumName;
            songButton.transform.GetChild(2).GetComponent<Text>().text = levelInfo.artistName;
            songButton.transform.GetChild(3).GetComponent<Text>().text = levelInfo.songLength.ToString();

            //Add onClick event listener to set leveldata for beatmap scene
            songButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                TransferLevelData(index);
            });
        }

        //Removes button template from list
        Destroy(songButtonTemplate);
    }

    void TransferLevelData(int index)
    {
        sceneCommunicator.SetBeatmapLevel(levelDatas[index]);
        menuController.LoadBeatmap();

    }
}
