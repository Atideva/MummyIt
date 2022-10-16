using System.Collections.Generic;
using Pools;
using UnityEngine;

public class EnemySpawnPool : MonoBehaviour
{
    readonly Dictionary<Enemy, EnemyPool> _pools = new();

    public Enemy Get(EnemyConfig config)
        => Pool(config.prefab).Get();

    EnemyPool Pool(Enemy enemy)
    {
        if (_pools.ContainsKey(enemy)) return _pools[enemy];

        var container = new GameObject {name = "Pool: " +enemy.name};
        container.transform.SetParent(transform);

        var pool = container.AddComponent<EnemyPool>();
        pool.SetPrefab(enemy);

        _pools.Add(enemy, pool);
        return _pools[enemy];
    }
}
