using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Configuration", menuName = "ScriptableObject/Level Configuration", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public Dictionary<Difficulty, BeatmapScriptableObject> beatmaps;
}
