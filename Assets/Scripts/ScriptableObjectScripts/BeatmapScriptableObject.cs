using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HitObject
{
    public float startTime;
    public float endTime;
}

public class BeatmapEvent
{
    public float startTime;
    public float endTime;
}

public class RoadRegionTransition : BeatmapEvent
{
    public int newRoadRegion;
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
