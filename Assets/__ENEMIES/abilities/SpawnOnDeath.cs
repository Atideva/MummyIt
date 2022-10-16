using System.Collections.Generic;
using UnityEngine;


public class SpawnOnDeath : EnemyAbility
{
    public List<EnemyConfig> spawnEnemies = new();
    public List<Vector3> offsets = new();

    protected override void OnInit()
    {
        Owner.hp.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        for (int i = 0; i < spawnEnemies.Count; i++)
        {
            Events.Instance.SpawnEnemy(spawnEnemies[i], transform.position+offsets[i]);
        }
    }

    public override void Reset()
    {
    }
}