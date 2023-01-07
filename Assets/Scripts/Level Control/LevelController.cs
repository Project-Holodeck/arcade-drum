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
    private RectTransform barPos;
    // Private component references
    private TextMeshProUGUI scoreCountText, comboText, comboCountText, accuracyText;
    AudioSource audioSource;
    private AudioSource[] soundEffects;

    RoadStyleController roadStyleController;
    PlayerInputController playerInputController;

    public static LevelController instance;

    private bool[] longPressedLaneEnable; //Enables long-press hit detection for each lane
    private float[] longPressedLaneEnd; //Stores end-time of a long-hit
    private Queue<Queue<KeyValuePair<float, HitObjectVisual>>>[] longHitObjectsToHitByLane; //Stores hit object visuals to be deleted at certain times upon hit detection

    private int songDuration;
    
    public GameObject HUD;

    private bool start;

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
<<<<<<< HEAD
        scoreCountText = GameObject.Find("ScoreCountText").GetComponent<TextMeshProUGUI>();
        comboText = GameObject.Find("ComboText").GetComponent<TextMeshProUGUI>();
        comboCountText = GameObject.Find("ComboCountText").GetComponent<TextMeshProUGUI>();
        accuracyText = GameObject.Find("AccuracyText").GetComponent<TextMeshProUGUI>();
        barPos = GameObject.Find("BarCover").GetComponent<RectTransform>();
        // Temp fix
        /*
        Beatmap testBeatmap = new Beatmap(Difficulty.EASY, 0.5f, new List<HitObject>() { new HitObject(2f, 5f, 0) });
        LevelData testLevel = new LevelData("Test", "Test", "Test", 10, new Dictionary<Difficulty, Beatmap> { { Difficulty.EASY, testBeatmap } });

        SetLevel(testLevel);*/
        PrepareLevel();
        ProcessBeatmap();
        Invoke("playMusic", 2.9f);
        scoreCountText.text = "0000000000";
=======
        
        // Temp fix 
        Beatmap testBeatmap = new Beatmap(Difficulty.EASY, 0.5f, new List<HitObject>() { new HitObject(2f, 5f, 0) });
        LevelData testLevel = new LevelData("Test", "Test", "Test", 10, new Dictionary<Difficulty, Beatmap> { { Difficulty.EASY, testBeatmap } });

        //SetLevel(testLevel);
        //PrepareLevel();
        //ProcessBeatmap();
        start = false;

        
>>>>>>> e39f1d2efbbe112aea8bdf89e1ae64fb46735a1a
    }
    void playMusic()
    {
        audioSource.Play();
    }
    // This method call should come at scene start from a DontDestroyOnLoad class. That class will act as a communicator between scenes and probably
    // shouldn't process the data. Also it receives the level data from a save load class that is separate. TODO
    public void SetLevel(LevelData level) {
        this.level = level;
    }

    public void PrepareLevel() {
        beatmap = level.beatmaps[Difficulty.EASY];
        roadStyleController.setSpeed(beatmap.speed);
        roadStyleController.Setup();
        songDuration = level.songLength;
        Debug.Log(songDuration);
    }

    void InitializeHitObjectLanes(){
        hitObjectsToSpawnByLane = new Dictionary<int, List<HitObject>>();
        hitObjectsToHitByLane = new Dictionary<int, List<HitObjectVisualPairing>>();
        longHitObjectsToHitByLane = new Queue<Queue<KeyValuePair<float, HitObjectVisual>>>[4];

        longPressedLaneEnable = new bool[4];
        longPressedLaneEnd = new float[4];

        for (int i = 0; i < 4; i++)
        {
            hitObjectsToSpawnByLane[i] = new List<HitObject>();
            hitObjectsToHitByLane[i] = new List<HitObjectVisualPairing>();
            longHitObjectsToHitByLane[i] = new Queue<Queue<KeyValuePair<float, HitObjectVisual>>>();
        }
    }

    public void ProcessBeatmap(){
        InitializeHitObjectLanes();
        foreach (BeatmapEvent be in beatmap.beatmapEvents)
        {
            if (be.GetType() == typeof(HitObject)){
                HitObject h = (HitObject)be;
                hitObjectsToSpawnByLane[h.lane].Add(h);
            }
        }
    }

    public void StartMap() {
        
        HUD.SetActive(true);

        scoreCountText = GameObject.Find("ScoreCountText").GetComponent<TextMeshProUGUI>();
        comboText = GameObject.Find("ComboText").GetComponent<TextMeshProUGUI>();
        comboCountText = GameObject.Find("ComboCountText").GetComponent<TextMeshProUGUI>();
        accuracyText = GameObject.Find("AccuracyText").GetComponent<TextMeshProUGUI>();
        barPos = GameObject.Find("BarCover").GetComponent<RectTransform>();

        scoreCountText.text = "0000000000";

        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!start)
            return;
            
        trackTime += Time.deltaTime;
        //bar 
        var pos = barPos.localPosition;
        barPos.localPosition = new Vector3(-618f + trackTime / songDuration * 641f, pos.y, pos.z);
        
        // Input detection
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
        for (int i = 0; i < 4; i++) {
            foreach (HitObject h in hitObjectsToSpawnByLane[i]) {
                if (h.startTime - roadStyleController.timeOffset <= trackTime){
                    if(h.startTime == h.endTime)
                        GenerateShortHitRat(i, h);
                    else if(h.startTime != h.endTime)
                        GenerateLongHitRat(i, h);
                    
                    hitObjectsToSpawnByLane[i].Remove(h);
                    //Debug.Log("T");
                    break;
                }
            }
        }

        // Loop over all to hit
        for (int i = 0; i < 4; i++)
        {
            bool hit = false;
            bool missed = false;
            bool hitShort = playerInputController.laneShortPressedArray[i];
            bool hitLong = playerInputController.laneLongPressedArray[i];

            float tolerance = 0.3f; //TODO global tolerance


            if (longPressedLaneEnable[i])
                HitDetectionLong(hitLong, tolerance, out hit, out missed, i, longHitObjectsToHitByLane[i].Peek());
            else {

                if (hitObjectsToHitByLane[i].Count == 0) continue;

                HitObjectVisualPairing pairing = hitObjectsToHitByLane[i][0];
                HitObject h = pairing.h;
                float differenceStart = trackTime - h.startTime;

                if (h.startTime == h.endTime)
                    HitDetectionShort(hitShort, differenceStart, tolerance, pairing, out hit, out missed, i);
                else
                {
                    HitDetectionShort(hitShort, differenceStart, tolerance, pairing, out longPressedLaneEnable[i], out missed, i); //When head arrives

                    if(longPressedLaneEnable[i]) 
                        longPressedLaneEnd[i] = h.endTime;
                    else if(missed)
                        longHitObjectsToHitByLane[i].Dequeue();
                    
                }
            }
                


            if (missed)
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

    void HitDetectionShort(bool hitShort, float difference, float tolerance, HitObjectVisualPairing pairing, out bool hit, out bool missed, int i)
    {

        if (hitShort && Mathf.Abs(difference) < tolerance) // TODO Global tolerance
        {
            
            //scoreInt += (int)((1 / distance) * 50000.0f * (1 + comboCount / 10f)); //edited formula: combocount actually matters in terms of scoring
            // pls edit this to work with not distance, but TIME. TY! TODO
            //to edit: 500000 is random, we should probably test a fair value
            IncreaseScoreAccuracy(tolerance, difference, difference);

            pairing.hv.Hit();
            hitObjectsToHitByLane[i].Remove(pairing);
            //Debug.Log("Hit!");

            //accuracy text

            //hit sound effects
            //hitSound.Play();

            hit = true;
            missed = false;
        }
        else if (difference > tolerance)
        {
            hitObjectsToHitByLane[i].Remove(pairing);
            
            accuracy.Add(0f);
            //Debug.Log("Too late!");

            //miss sound effects
            //lateSound.Play();
            hit = false;
            missed = true;
        }
        else
        {
            hit = false;
            missed = false;
        }
            
    }

    void HitDetectionLong(bool hitLong, float tolerance, out bool hit, out bool missed, int i, Queue<KeyValuePair<float, HitObjectVisual>> queue)
    {
        float differenceEnd = trackTime - longPressedLaneEnd[i];
        hit = false;

        if (Mathf.Abs(differenceEnd) < tolerance) //When tail arrives 
        {
            hit = true;
            longPressedLaneEnable[i] = hitLong;
            if(queue.Peek().Key <= trackTime && !hitLong) {
                queue.Peek().Value.Hit();
                queue.Dequeue();

                
                IncreaseScoreAccuracy(tolerance, differenceEnd, differenceEnd);
            }
                
            

        }
        else if (trackTime < longPressedLaneEnd[i] - tolerance) //During body
        {
            hit = hitLong ? true : false;

            if(queue.Peek().Key <= trackTime && hitLong) {
                queue.Peek().Value.Hit();
                queue.Dequeue();

                IncreaseScoreAccuracy(tolerance, tolerance, 0);
                   
            }
            
        }
        else if (differenceEnd > tolerance) //After tail leaves
        {
            longPressedLaneEnable[i] = false;
            hit = false;
            accuracy.Add(0f);
        } 

        if (!hit)
            longPressedLaneEnable[i] = false;

        if(!longPressedLaneEnable[i])
            longHitObjectsToHitByLane[i].Dequeue();

        missed = !hit;
    }

    void GenerateShortHitRat(int lane, HitObject h) {
        HitObjectVisual hv;
        roadStyleController.HandleBeatmapEvent(h, out hv, 0, 0);
        hitObjectsToHitByLane[lane].Add(new HitObjectVisualPairing(h, hv));
    }

    void GenerateLongHitRat(int lane, HitObject h) {
        float speed = 10f,
            headSize = ((TestRoadStyleController)roadStyleController).headPrefab.GetComponent<BoxCollider>().size.z,
            bodySize = ((TestRoadStyleController)roadStyleController).bodyPrefab.GetComponent<BoxCollider>().size.z,
            tailSize = ((TestRoadStyleController)roadStyleController).buttPrefab.GetComponent<BoxCollider>().size.z,
            time = h.endTime - h.startTime - headSize / speed - tailSize / speed,
            bodyTime = bodySize / speed;

        int segmentCount = (int)(2 + time / bodyTime + 0.5);

        Queue<KeyValuePair<float, HitObjectVisual>> newLongRat = new Queue<KeyValuePair<float, HitObjectVisual>>();

        for(int j = 0; j < segmentCount; j++)
        {
            HitObjectVisual hv;
            roadStyleController.HandleBeatmapEvent(h, out hv, j, segmentCount-1);
            float startTime = 0;
            

            if(j == 0) 
                hitObjectsToHitByLane[lane].Add(new HitObjectVisualPairing(h, hv));
            else {

                if(j == segmentCount - 1) 
                    startTime = h.startTime + headSize/speed * 0.5f + bodyTime * (j - 1) + tailSize/speed * 0.5f;
                else if (j != 0) 
                    startTime = h.startTime + headSize/speed * 0.5f + bodyTime * (j - 0.5f);

                newLongRat.Enqueue(new KeyValuePair<float, HitObjectVisual>(startTime,hv));
            }
        }

        longHitObjectsToHitByLane[lane].Enqueue(newLongRat);
    }

    void IncreaseScoreAccuracy(float tolerance, float difference, float accuracyDifference) {
        scoreInt += (int)((1 / Mathf.Abs(difference)) * 5000.0f * (1 + comboCount / 10f));
        scoreCountText.text = scoreInt.ToString();
        comboCount++;

        accuracy.Add((tolerance - Mathf.Abs(accuracyDifference)) / tolerance * 100f);
        int averageAcc = (int)accuracy.Sum() / accuracy.Count();
        accuracyText.text = averageAcc.ToString() + '%';
    }
}
