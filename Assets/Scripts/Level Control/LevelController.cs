using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

using Newtonsoft.Json;

public struct HitObjectVisualPairing{
    public HitObject h;
    public HitObjectVisual hv;

    public HitObjectVisualPairing(HitObject h, HitObjectVisual hv){
        this.h = h;
        this.hv = hv;
    }
}


/// <summary>
/// Controls most other components in the level.
/// Also controls the runtime of the song, hit detection, scores, and combos.
/// </summary>
public class LevelController : MonoBehaviour
{
    [Header("Level Data")]
    public LevelData level; // Level data, contains beatmaps, difficulty settings, and styles
    [HideInInspector]
    public Beatmap beatmap;
    private Dictionary<int, List<HitObject>> hitObjectsToSpawnByLane;
    private Dictionary<int, List<HitObjectVisualPairing>> hitObjectsToHitByLane;


    [Header("Track Data")]
    [SerializeField]
    private float trackTime;

    [Header("Current Attempt Data")]
    private int scoreInt = 0;
    public int comboCount = 0;
    private List<float> accuracy = new List<float>();
    // Private component references
    private TextMeshProUGUI scoreCountText, comboText, comboCountText, accuracyText;
    AudioSource audioSource;
    private AudioSource[] soundEffects;
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
        soundEffects = GameObject.Find("Test Sounds").GetComponentsInChildren<AudioSource>();
        roadStyleController = RoadStyleController.instance;
        playerInputController = PlayerInputController.instance;
        scoreCountText = GameObject.Find("ScoreCountText").GetComponent<TextMeshProUGUI>();
        comboText = GameObject.Find("ComboText").GetComponent<TextMeshProUGUI>();
        comboCountText = GameObject.Find("ComboCountText").GetComponent<TextMeshProUGUI>();
        accuracyText = GameObject.Find("AccuracyText").GetComponent<TextMeshProUGUI>();
        // Temp fix 
        Beatmap testBeatmap = new Beatmap(Difficulty.EASY, 0.5f, new List<HitObject>() { new HitObject(2f, 2f, 0) });
        LevelData testLevel = new LevelData("Test", "Test", "Test", 10, new Dictionary<Difficulty, Beatmap> { { Difficulty.EASY, testBeatmap } });


        //InitializeHitObjectLanes();
        PrepareLevel();
        ProcessBeatmap();



    }

    // This method call should come at scene start from a DontDestroyOnLoad class. That class will act as a communicator between scenes and probably
    // shouldn't process the data. Also it receives the level data from a save load class that is separate. TODO
    public void SetLevel(LevelData level) {
        this.level = level;
    }

    public void PrepareLevel() {
        beatmap = level.beatmaps[Difficulty.EASY];
        roadStyleController.Setup(beatmap);
    }

    void InitializeHitObjectLanes(){
        hitObjectsToSpawnByLane = new Dictionary<int, List<HitObject>>();
        hitObjectsToHitByLane = new Dictionary<int, List<HitObjectVisualPairing>>();
        for (int i = 0; i < 4; i++)
        {
            hitObjectsToSpawnByLane[i] = new List<HitObject>();
            hitObjectsToHitByLane[i] = new List<HitObjectVisualPairing>();
        }
    }

    void ProcessBeatmap(){
        InitializeHitObjectLanes();
        foreach (BeatmapEvent be in beatmap.beatmapEvents)
        {
            if (be.GetType() == typeof(HitObject)){
                HitObject h = (HitObject)be;
                hitObjectsToSpawnByLane[h.lane].Add(h);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        trackTime += Time.deltaTime;

        // Input detection
        //Debug.Log("hi");
        // Janky input detection
        bool tempSpawnRandomNote = Input.GetKeyDown(KeyCode.Space);
        bool tempStartSong = Input.GetKeyDown(KeyCode.Return);

        // Less janky input detection
        playerInputController.UpdateInputs(); // General class, derived class will connect w/ Arduino

        // To delete lol, is just for testing
        if (tempSpawnRandomNote){
            int lane = Random.Range(0,  4);
            HitObject randomHitObject = new HitObject(trackTime + roadStyleController.timeOffset, trackTime + roadStyleController.timeOffset, lane);
            //roadStyleController.HandleBeatmapEvent(randomHitObject);
            hitObjectsToSpawnByLane[lane].Add(randomHitObject);
        }

        if (tempStartSong){
            audioSource.Play();
        }

        // For all lanes, visualize hitobjects
        for (int i = 0; i < 4; i++)
        {
            foreach (HitObject h in hitObjectsToSpawnByLane[i])
            {
                if (h.startTime - roadStyleController.timeOffset <= trackTime){
                    HitObjectVisual hv;
                    roadStyleController.HandleBeatmapEvent(h, out hv);
                    hitObjectsToSpawnByLane[i].Remove(h);
                    hitObjectsToHitByLane[i].Add(new HitObjectVisualPairing(h, hv));
                    break;
                }
            }
        }

        // Loop over all to hit
        for (int i = 0; i < 4; i++)
        {
            bool hit = false;
            bool missed = false;
            bool hitting = playerInputController.lanePressedArray[i];

            if (hitObjectsToHitByLane[i].Count == 0) continue;
            HitObjectVisualPairing pairing = hitObjectsToHitByLane[i][0];
            HitObject h = pairing.h;
            float difference = trackTime - h.startTime;
            float tolerance = 0.3f; //TODO global tolerance
            // HIT DETECTION
            if (hitting && Mathf.Abs(difference) < tolerance) // TODO Global tolerance
            {
                hit = true;
                //scoreInt += (int)((1 / distance) * 50000.0f * (1 + comboCount / 10f)); //edited formula: combocount actually matters in terms of scoring
                // pls edit this to work with not distance, but TIME. TY! TODO
                //to edit: 500000 is random, we should probably test a fair value
                scoreInt += (int)((1 / Mathf.Abs(difference)) * 500000.0f * (1 + comboCount / 10f));
                scoreCountText.text = scoreInt.ToString();
                comboCount++;

                pairing.hv.Hit();
                hitObjectsToHitByLane[i].Remove(pairing);
                //Debug.Log("Hit!");

                //accuracy text
                accuracy.Add((tolerance - Mathf.Abs(difference)) / tolerance * 100f);
                int averageAcc = (int)accuracy.Sum() / accuracy.Count();
                accuracyText.text = averageAcc.ToString() + '%';

                //hit sound effects
                //hitSound.Play();
                
            } else if (difference > tolerance){
                hitObjectsToHitByLane[i].Remove(pairing);
                missed = true;
                accuracy.Add(0f);
                //Debug.Log("Too late!");

                //miss sound effects
                //lateSound.Play();
            }

            if ((!hit && hitting) || missed)
            {
                // The player missed, do whatever
                comboCount = 0;
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
        
        //accuracy text

    }
}
