using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayRatSounds : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource backgroundSound;
    void Start()
    {
        backgroundSound = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", 0f, 4f);
        InvokeRepeating("StopSound", 1.75f, 4f);
    }

    void PlaySound()
    {
        backgroundSound.Play();
    }
    void StopSound()
    {
        backgroundSound.Stop();
    }
}
