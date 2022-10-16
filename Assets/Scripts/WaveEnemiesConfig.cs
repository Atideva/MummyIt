using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Spawn Line", menuName = "Configs/New Spawn Line")]
public class WaveEnemiesConfig : ScriptableObject
{
    [SerializeField] List<EnemyConfig> enemies = new();
    [SerializeField] AnimationCurve curve;
    [SerializeField] float chanceFactor = 4f;
    public IReadOnlyList<EnemyConfig> AmmoTypes => enemies;

    public  EnemyConfig GetRandom()
    {
        var id = 0;
        var chance = Random.Range(0f, 1);
        var sum = 0f;

        for (var i = 0; i < enemies.Count; i++)
        {
            sum += GetChance(i);
            if (chance > sum) continue;
            id = i;
            break;
        }

        return enemies[id];
    }

    public float GetChance(int id)
    {
        var point = enemies.Count > 1 ? (float) id / (enemies.Count - 1) : 0;
        var value = curve.Evaluate(point);

        var factor = 1 / chanceFactor;
        var factorValue = value + factor;
        var factorTotal = TotalChance + factor * enemies.Count;

        return factorTotal > 0 ? factorValue / factorTotal : value;
    }

    float TotalChance => enemies
        .Select((t, i) => i / (float) (enemies.Count - 1))
        .Sum(curve.Evaluate);
}