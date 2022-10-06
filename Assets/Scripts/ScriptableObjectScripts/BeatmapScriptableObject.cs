using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapEvent
{
    public float startTime;
    public float endTime;

}

public class HitObject : BeatmapEvent
{
    public int lane; // 0 to 3
    public HitObject(float startTime, float endTime, int lane)
    {
        this.startTime = startTime;
        this.endTime = endTime;
        this.lane = lane;
    }
}

public class RoadRegionTransition : BeatmapEvent
{
    public int newRoadRegion;
    public RoadRegionTransition(float startTime, float endTime, int newRoadRegion)
    {
        this.startTime = startTime;
        this.endTime = endTime;
        this.newRoadRegion = newRoadRegion;
    }
}

public enum Difficulty { EASY, MEDIUM, HARD };

/// <summary>
/// Manages the HitObjects,
/// </summary>
[CreateAssetMenu(fileName = "Beatmap Configuration", menuName = "ScriptableObject/Beatmap Configuration", order = 1)]
public class BeatmapScriptableObject : ScriptableObject
{

    [Header("Beatmap Information")]
    public TrackScriptableObject track;
    public Difficulty difficulty;

    [Header("Style Information")]
    public RoadStyle roadStyle = RoadStyle.SUBWAY; // Which style to display on
    public int roadRegion = 0; // int from 0 upwards that indexes possible road styles. i.e. switching from grungy subway to clean for a solo section.

    [Header("Mapped HitObjects")]
    public List<HitObject> hitObjects;
}
