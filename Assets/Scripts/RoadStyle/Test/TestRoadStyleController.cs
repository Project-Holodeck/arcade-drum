using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Derived class of RoadStyleController for Test RoadStyle.
/// </summary>
public class TestRoadStyleController : RoadStyleController
{
    [Header("Prefabs")]
    public GameObject circlePrefab, headPrefab, bodyPrefab, buttPrefab;
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

        setSpeed(0f);
        Setup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void HandleBeatmapEvent(BeatmapEvent be, out HitObjectVisual hv, int segmentCount, int lastSegment)
    {
        System.Type beType = be.GetType();
        if (beType == typeof(HitObject))
        {
            HitObject h = (HitObject)be;

            GameObject ratPart;

            //Spawns short rat
            if (h.startTime == h.endTime)
                ratPart = circlePrefab;

            //Spawns long rat
            else
            {
                if (segmentCount == 0)
                    ratPart = headPrefab;
                else if (segmentCount != lastSegment)
                    ratPart = bodyPrefab;
                else
                    ratPart = buttPrefab;


            }

            GenerateRatBody(h, out hv, segmentCount, ratPart);

        }
        else if (beType == typeof(RoadRegionTransition))
        {
            RoadRegionTransition rrt = (RoadRegionTransition)be;
            hv = null;
        } else {
            hv = null;
        }
    }

    void GenerateRatBody(HitObject h, out HitObjectVisual hv, int segmentCount, GameObject ratPart)
    {

        Vector3 pos = Vector3.forward * 0;
        if(ratPart == buttPrefab)
            pos = Vector3.forward * (bodyPrefab.GetComponent<BoxCollider>().size.z * (segmentCount - 1) + headPrefab.GetComponent<BoxCollider>().size.z /2 + buttPrefab.GetComponent<BoxCollider>().size.z /2);
        else if(ratPart == bodyPrefab)
            pos =  Vector3.forward * (bodyPrefab.GetComponent<BoxCollider>().size.z * (segmentCount - 1) + bodyPrefab.GetComponent<BoxCollider>().size.z /2 + headPrefab.GetComponent<BoxCollider>().size.z /2 );
            
        Circle circle = Instantiate(ratPart, hitObjectSpawnTransforms[h.lane].position + pos, Quaternion.identity).GetComponent<Circle>();
        circle.Setup(distance * trackSpeed);
        hv = circle;
    }
}
