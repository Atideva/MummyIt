using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level spawn", menuName = "Configs/New Level Spawn")]
public class LevelSpawnConfig : ScriptableObject
{
    public List<SpawnWave> waves = new();
}