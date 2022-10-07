using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    [Header("Level Information")]
    public string songName;
    public string albumName;
    public string artistName;
    public int songLength; // In seconds, will convert to minutes:seconds for display

    [Header("Beatmap")]
    public Dictionary<Difficulty, Beatmap> beatmaps;
}
