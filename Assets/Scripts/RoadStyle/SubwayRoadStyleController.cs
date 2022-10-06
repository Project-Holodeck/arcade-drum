using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayRoadStyleController : RoadStyleController
{
    // Public component references
    private Transform[] hitObjectSpawnTransforms;

    // Start is called before the first frame update
    void Start()
    {
        // Get lane spawns
        hitObjectSpawnTransforms = new Transform[4];
        Transform hitObjectSpawns = transform.Find("Hit Object Spawns");
        hitObjectSpawnTransforms[0] = hitObjectSpawns.Find("S0");
        hitObjectSpawnTransforms[1] = hitObjectSpawns.Find("S1");
        hitObjectSpawnTransforms[2] = hitObjectSpawns.Find("S2");
        hitObjectSpawnTransforms[3] = hitObjectSpawns.Find("S3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
