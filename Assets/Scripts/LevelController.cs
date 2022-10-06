using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls most other components in the level.
/// Also controls the runtime of the song.
/// </summary>
public class LevelController : MonoBehaviour
{
    [Header("Level Data")]
    public LevelScriptableObject level; // Level data, contains beatmaps, difficulty settings, and styles

    [Header("Track Data")]
    [SerializeField]
    private float trackTime;

    // Private component references
    AudioSource audioSource;
    RoadStyleController roadStyleController;
    public static LevelController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Can't have two roadstyle controllers active at once
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

    }

    // Update is called once per frame
    void Update()
    {
        trackTime += Time.deltaTime;

        // Input detection
        bool tempSpawnRandomNote = Input.GetKeyDown(KeyCode.Space);
        bool tempStartSong = Input.GetKeyDown(KeyCode.Return);
        
        if (tempSpawnRandomNote){
            HitObject randomHitObject = new HitObject(trackTime, trackTime + 0.01f, Random.Range(0, 4));
            roadStyleController.HandleBeatmapEvent(randomHitObject);
        }

        if (tempStartSong){
            audioSource.Play();
        }
    }
}
