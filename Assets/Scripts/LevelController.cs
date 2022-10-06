using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Level Data")]
    public LevelScriptableObject level;

    // private component references
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get private component references
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
