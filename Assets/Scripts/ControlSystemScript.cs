// Play song, generate circles

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlSystemScript : MonoBehaviour
{
    //Y = 8.1, Z = 8, X = | 1.1, 3.4 |
    public float startY = 8.1f, startZ = 8f;
    public float[] lane = new float[4];
    public GameObject circle0, circle1, circle2, circle3;
    private PlayerController playerControllerScript;
    AudioSource song;

    // Start is called before the first frame update
    void Start()
    {
        song = GameObject.Find("Music Player").GetComponent<AudioSource>();
        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnCircles(Random.Range(0,4));
        if (Input.GetKeyDown(KeyCode.Return))
            song.Play();
    }

    void SpawnCircles(int laneNumber)
    {
        switch (laneNumber)
        {
            case 0:
                Instantiate(circle0, new Vector3(lane[laneNumber], startY, startZ), circle0.transform.rotation);
                break;
            case 1:
                Instantiate(circle1, new Vector3(lane[laneNumber], startY, startZ), circle1.transform.rotation);
                break;
            case 2:
                Instantiate(circle2, new Vector3(lane[laneNumber], startY, startZ), circle2.transform.rotation);
                break;
            case 3:
                Instantiate(circle3, new Vector3(lane[laneNumber], startY, startZ), circle3.transform.rotation);
                break;
            default:
                break;
        }
    }
}
