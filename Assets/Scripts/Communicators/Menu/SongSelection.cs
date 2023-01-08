using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;

public class SongSelection : MonoBehaviour
{

    public string songsFolder;

    private Dictionary<int, LevelData> levelDatas;
    private Dictionary<int, Sprite> spriteDatas;
    private DirectoryInfo[] songDirectories;
    private LevelController levelController;

    private int currentIndex = 0;

    public Image currentImage, leftImage, rightImage;

    CameraPosition camera;


    // Start is called before the first frame update
    void Start()
    {
        songDirectories = new DirectoryInfo(Application.dataPath + "/" + songsFolder).GetDirectories();
        camera = CameraPosition.instance;

        levelDatas = new Dictionary<int, LevelData>();
        spriteDatas = new Dictionary<int, Sprite>();

        levelController = LevelController.instance;

        GenerateSongList();
    }

    void Update() {
        int leftIndex = currentIndex-1;
        if(leftIndex < 0)
            leftIndex = levelDatas.Count-1;
        
        int rightIndex = currentIndex+1;
        if(rightIndex >= levelDatas.Count)
            rightIndex = 0;
        
        currentImage.sprite = spriteDatas[currentIndex];
        leftImage.sprite = spriteDatas[leftIndex];
        rightImage.sprite = spriteDatas[rightIndex];
    }

    void GenerateSongList() {
        for(int i = 0; i < songDirectories.Length; i++) {

            //Retrieve level information as string from json and load image as sprite
            string levelDataText = File.ReadAllText(Application.dataPath + "/" + songsFolder + "/" + songDirectories[i].Name + "/" + songDirectories[i].GetFiles("*.json")[0].Name);
            string imageFile = Application.dataPath + "/" + songsFolder + "/" + songDirectories[i].Name + "/" + songDirectories[i].GetFiles("*.png")[0].Name;
            Sprite songCover = LoadSprite(imageFile);

            //Format level information into LevelData
            LevelData levelInfo = JsonConvert.DeserializeObject<LevelData>(levelDataText);
            levelDatas[i] = levelInfo;
            spriteDatas[i] = songCover;
        }
    }


    //Shifts current index down
    public void LeftButton() {
        currentIndex-=1;
        if(currentIndex < 0)
            currentIndex = levelDatas.Count-1;
    }

    //Shifts current index up
    public void RightButton() {
        currentIndex+=1;
        if(currentIndex >= levelDatas.Count)
            currentIndex = 0;
    }

    //Loads a sprite from an image file path
    public Sprite LoadSprite(string filePath, float pixelsPerUnit = 100.0f) {

        Texture2D spriteTexture = LoadTexture(filePath);
        return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), pixelsPerUnit);

    }

    //Loads a texture2D from an image file path
    public Texture2D LoadTexture(string filePath) {
        Texture2D texture;
        byte[] fileData;

        if(File.Exists(filePath)) {
            fileData = File.ReadAllBytes(filePath);
            texture = new Texture2D(500,500);
            if(texture.LoadImage(fileData))
                return texture;
    
        }

        return null;
    }

    public void StartMap() {
        string beatmapSongDirectory = "Songs/" + songDirectories[currentIndex].Name;
        levelController.audioSource.clip = Resources.Load<AudioClip>(beatmapSongDirectory);
        
        levelController.SetLevel(levelDatas[currentIndex]);
        levelController.PrepareLevel();
        levelController.ProcessBeatmap();
        levelController.StartMap();
        levelController.playMusic();
        levelController.startTimer();
        
        camera.setBeatmapPosition();
        Debug.Log("Donezo");
    }
}
