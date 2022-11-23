using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;


public class BeatmapEvent
{
    public float startTime;
    public float endTime;

}

[System.Serializable]
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

public enum Difficulty { 
    [EnumMember(Value = "EASY")]
    EASY, 

    [EnumMember(Value = "MEDIUM")]
    MEDIUM, 

    [EnumMember(Value = "HARD")]
    HARD };

/// <summary>
/// Manages the HitObjects,
/// </summary>
[System.Serializable]
public class Beatmap
{
    [Header("Beatmap Information")]
    public Difficulty difficulty;

    [Header("Style Information")]
    public RoadStyle roadStyle = RoadStyle.SUBWAY; // Which style to display on
    public int roadRegion = 0; // int from 0 upwards that indexes possible road styles. i.e. switching from grungy subway to clean for a solo section.

    [Header("Speed Information")]
    public float speed; // 1 / (pressTime - spawnTime), so higher speed means less time between spawn and hit

    [Header("Mapped HitObjects")]
    public List<HitObject> beatmapEvents;

    public Beatmap(Difficulty difficulty, float speed, List<HitObject> beatmapEvents){
        this.difficulty = difficulty;
        this.speed = speed;
        this.beatmapEvents = beatmapEvents;
    }
}
