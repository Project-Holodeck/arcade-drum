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

    public void DrawVisuals(){

    }
}
