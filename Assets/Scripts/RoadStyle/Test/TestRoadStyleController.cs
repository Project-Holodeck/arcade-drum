using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Derived class of RoadStyleController for Test RoadStyle.
/// </summary>
public class TestRoadStyleController : RoadStyleController
{
    [Header("Prefabs")]
    public GameObject circlePrefab;
    // Private component references
    private Transform[] hitObjectSpawnTransforms;
    private LevelController levelController;

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

        levelController = LevelController.instance;
        distance = hitObjectSpawnTransforms[0].position.z; // the physical distance the circle moves in this road
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void HandleBeatmapEvent(BeatmapEvent be, out HitObjectVisual hv)
    {
        System.Type beType = be.GetType();
        if (beType == typeof(HitObject))
        {
            HitObject h = (HitObject)be;
            // Spawn a rat
            Circle circle = Instantiate(circlePrefab, hitObjectSpawnTransforms[h.lane].position, Quaternion.identity).GetComponent<Circle>();
            //Debug.Log(string.Format("{0} sussy", distance * beatmap.speed));
            circle.Setup(distance * beatmap.speed);
            hv = circle;
        }
        else if (beType == typeof(RoadRegionTransition))
        {
            RoadRegionTransition rrt = (RoadRegionTransition)be;
            hv = null;
        } else {
            hv = null;
        }
    }
}
