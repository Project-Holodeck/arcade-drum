using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Configuration", menuName = "ScriptableObject/Level Configuration", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    [Header("Level Information")]
    public string songName;
    public string albumName;
    public string artistName;
    public int songLength; // In seconds, will convert to minutes:seconds for display

    [Header("Beatmap")]
    public Dictionary<Difficulty, BeatmapScriptableObject> beatmaps;
}
