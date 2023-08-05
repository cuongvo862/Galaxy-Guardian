using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Create Wave Data")]
public class WaveData : ScriptableObject
{
    [Range(1, 10)] public int totalGroup;
    [Range(1, 10)] public int minTotalEnemies;
    [Range(1, 10)] public int maxTotalEnemies;
    [Range(1, 10)] public int speedMultiplier;
}
