using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayBackgroundSounds : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource backgroundSound;
    void Start()
    {
        backgroundSound = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", 0f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound()
    {
        backgroundSound.Play();
        //Debug.Log("played");
    }
    void StopSound()
    {
        backgroundSound.Stop();
    }
}
