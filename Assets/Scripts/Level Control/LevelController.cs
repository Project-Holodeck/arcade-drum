using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



/// <summary>
/// Controls most other components in the level.
/// Also controls the runtime of the song, hit detection, scores, and combos.
/// </summary>
public class LevelController : MonoBehaviour
{
    [Header("Level Data")]
    public LevelData level; // Level data, contains beatmaps, difficulty settings, and styles
    private Beatmap beatmap;
    private Dictionary<int, List<HitObject>> hitObjectsByLane;

    [Header("Track Data")]
    [SerializeField]
    private float trackTime;

    [Header("Current Attempt Data")]
    private int scoreInt = 0;
    public int comboCount = 0;

    // Private component references
    private TextMeshProUGUI scoreCountText, comboText, comboCountText;
    AudioSource audioSource;
    RoadStyleController roadStyleController;
    PlayerInputController playerInputController;
    public static LevelController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Can't have two level controllers active at once
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get private component references
        audioSource = GetComponent<AudioSource>();
        roadStyleController = RoadStyleController.instance;
        playerInputController = PlayerInputController.instance;
        scoreCountText = GameObject.Find("ScoreCountText").GetComponent<TextMeshProUGUI>();
        comboText = GameObject.Find("ComboText").GetComponent<TextMeshProUGUI>();
        comboCountText = GameObject.Find("ComboCountText").GetComponent<TextMeshProUGUI>();

        ProcessBeatmap();
    }

    // This method call should come at scene start from a DontDestroyOnLoad class. That class will act as a communicator between scenes and probably
    // shouldn't process the data. Also it receives the level data from a save load class that is separate. TODO
    void SetLevel(LevelData level){
        this.level = level;
        beatmap = level.beatmaps[Difficulty.EASY]; 
    }

    void ProcessBeatmap(){
        hitObjectsByLane = new Dictionary<int, List<HitObject>>();
        for (int i = 0; i < 3; i++)
        {
            hitObjectsByLane[i] = new List<HitObject>(); ;
        }

        foreach (BeatmapEvent be in beatmap.beatmapEvents)
        {
            if (be.GetType() == typeof(HitObject)){
                HitObject h = (HitObject)be;
                hitObjectsByLane[h.lane].Add(h);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        trackTime += Time.deltaTime;

        // Input detection

        // Janky input detection
        bool tempSpawnRandomNote = Input.GetKeyDown(KeyCode.Space);
        bool tempStartSong = Input.GetKeyDown(KeyCode.Return);

        // Less janky input detection
        playerInputController.UpdateInputs(); // General class, derived class will connect w/ Arduino
        
        if (tempSpawnRandomNote){
            HitObject randomHitObject = new HitObject(trackTime, trackTime + 0.01f, Random.Range(0, 4));
            roadStyleController.HandleBeatmapEvent(randomHitObject);
        }

        if (tempStartSong){
            audioSource.Play();
        }

        // For each lane, check if pressing and if it hit
        for (int i = 0; i < 4; i++)
        {
            if (playerInputController.lanePressedArray[i]){
                bool hit = false;
                foreach (HitObject h in hitObjectsByLane[i])
                {
                    float difference = trackTime - h.startTime;
                    if (Mathf.Abs(difference) < 0.5f){
                        hit = true;
                        //scoreInt += (int)((1 / distance) * 50000.0f * (1 + comboCount / 10f)); //edited formula: combocount actually matters in terms of scoring
                        // pls edit this to work with not distance, but TIME. TY! TODO
                        //to edit: 500000 is random, we should probably test a fair value
                        scoreInt += (int)((1 / difference) * 500000.0f * (1 + comboCount / 10f));
                        scoreCountText.text = scoreInt.ToString();
                        comboCount++;
                        //sDestroy(circle);
                    }
                }
                if (!hit){
                    comboCount = 0;
                }
            }
        }

        // Check combos
        if (comboCount > 0)
        {
            comboText.maxVisibleCharacters = 5;
            comboCountText.maxVisibleCharacters = 10;
            comboCountText.text = comboCount.ToString() + 'x';
        }
        if (comboCount == 0)
        {
            comboText.maxVisibleCharacters = 0;
            comboCountText.maxVisibleCharacters = 0;
        }
    }
}
