using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoadStyle { SUBWAY, CANYON }; // Style of road enum

/// <summary>
/// Manages the road style (i.e. subway, canyon, etc..)
/// Contains several scene transitions that are toggleable in the beatmap.
/// Will likely only contain some general methods that are then inherited by other classes specific to the road style.
/// i.e. the subway road style exists as SubwayRoadStyleController and will manage all the events that come with being specifically a subway
/// </summary>
public class RoadStyleController : MonoBehaviour
{
    // Public variables
    public RoadStyle roadStyle = RoadStyle.SUBWAY;

    // Private component references
    public static RoadStyleController instance;

    private void Awake()
    {
        if (instance != null){
            Destroy(gameObject); // Can't have two roadstyle controllers active at once
        } else {
            instance = this;
        }
    }

    // Will spawn an individual hit object, implemented in each inherited class cause it's specific to the road style
    public virtual void HandleBeatmapEvent(BeatmapEvent be){

    }
}
