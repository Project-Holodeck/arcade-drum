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
    // Public variables
    public RoadStyle roadStyle = RoadStyle.TEST;

    // Private component references
    public static RoadStyleController instance;
    protected Beatmap beatmap;

    public float distance;
    public float timeOffset;

    private void Awake()
    {
        if (instance != null){
            Destroy(gameObject); // Can't have two roadstyle controllers active at once
        } else {
            instance = this;
        }
    }

    public void Setup(Beatmap beatmap)
    {
        this.beatmap = beatmap;
        timeOffset = 1f / beatmap.speed;
    }

    // Will spawn an individual hit object, implemented in each inherited class cause it's specific to the road style
    public virtual void HandleBeatmapEvent(BeatmapEvent be, out HitObjectVisual hv){
        hv = null;
    }
}
