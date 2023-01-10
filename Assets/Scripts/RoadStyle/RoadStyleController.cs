using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoadStyle { TEST, SUBWAY, CANYON }; // Style of road enum

/// <summary>
/// Manages the road style (i.e. subway, canyon, etc..)
/// Contains several scene transitions that are toggleable in the beatmap.
/// Will likely only contain some general methods that are then inherited by other classes specific to the road style.
/// i.e. the subway road style exists as SubwayRoadStyleController and will manage all the events that come with being specifically a subway
/// </summary>
public class RoadStyleController : MonoBehaviour
{
    [Header("Inherited Prefabs")]
    public TrackMove trackPrefab;

    // Public variables
    public RoadStyle roadStyle = RoadStyle.TEST;

    // Private component references
    public static RoadStyleController instance;
    protected Beatmap beatmap;

    public float distance;
    public float timeOffset;

    protected float trackSpeed;

    private void Awake()
    {
        if (instance != null){
            Destroy(gameObject); // Can't have two roadstyle controllers active at once
        } else {
            instance = this;
        }
    }

    public void setSpeed(float speed) { 
        this.trackSpeed = speed; 
        timeOffset = 1f / trackSpeed;
    }

    public float getSpeed() { return trackSpeed; }

    public void Setup()
    {
       

        // Draw the epic track
        Vector3 startPos = transform.position + transform.forward * trackPrefab.GetComponent<BoxCollider>().size.z * 29;
        TrackMove first = null;
        TrackMove lol = null;
        TrackMove last = null;
        for (int i = -4; i < 30; i++)
        {
            TrackMove tm = Instantiate(trackPrefab, transform.position + transform.forward * i * trackPrefab.GetComponent<BoxCollider>().size.z, trackPrefab.transform.rotation);
            if (i == -4)
            {
                first = tm;
                lol = tm;
            }
            else
            {
                //Debug.Log(string.Format("{0} sjsus usus", distance * beatmap.speed));
                tm.Setup(startPos, lol, transform.position.z);
                lol = tm;
            }
        }
        first.Setup(startPos, lol, transform.position.z);
    }

    // Will spawn an individual hit object, implemented in each inherited class cause it's specific to the road style
    public virtual void HandleBeatmapEvent(BeatmapEvent be, out HitObjectVisual hv, int segmentCount, int lastSegment)
    {
        hv = null;
    }
}
