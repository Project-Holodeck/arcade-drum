using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class LevelData
{
    [Header("Level Information")]
    public string songName;
    public string albumName;
    public string artistName;
    public int songLength; // In seconds, will convert to minutes:seconds for display

    [Header("Beatmap")]
    public Dictionary<Difficulty, Beatmap> beatmaps;

    public LevelData(string songName, string albumName, string artistName, int songLength, Dictionary<Difficulty, Beatmap> beatmaps){
        this.songName = songName;
        this.albumName = albumName;
        this.artistName = artistName;
        this.songLength = songLength;
        this.beatmaps = beatmaps;
    }
}
