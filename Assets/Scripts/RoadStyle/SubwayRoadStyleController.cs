using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayRoadStyleController : RoadStyleController
{
    [Header("Prefabs")]
    public GameObject ratPrefab;
    
    // Private component references
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

    public override void HandleBeatmapEvent(BeatmapEvent be){
        System.Type beType = be.GetType();
        if (beType == typeof(HitObject)){
            HitObject h = (HitObject)be;
            // Spawn a rat
            Instantiate(ratPrefab, hitObjectSpawnTransforms[h.lane].position, Quaternion.identity);
        } else if (beType == typeof(RoadRegionTransition)){
            RoadRegionTransition rrt = (RoadRegionTransition)be;
        }
        
    }
}
