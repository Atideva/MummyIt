using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pools;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    #region Singleton

    //-------------------------------------------------------------
    public static EnemySpawner Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // transform.SetParent(null);
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-------------------------------------------------------------

    #endregion

    public List<EnemyConfig> enemies = new();
    public Enemy prefab;
    public float spawnCooldown;
    public Transform spawnPos;
    public List<Enemy> currentEnemies = new();
    public List<Enemy> attackedEnemies = new();
    public event Action<Enemy> OnSpawn = delegate { };
    readonly Dictionary<Enemy, EnemyPool> _pools = new();



    void Start()
    {
        StartCoroutine(Spawn());
        Events.Instance.OnEnemyDeath += OnEnemyDeath;
    }

    public Enemy TryFindEnemy(Transform t)
        => currentEnemies.FirstOrDefault(e => e.transform == t);

    public bool IsAttacked(Enemy en) => attackedEnemies.Contains(en);

    public void EnemyAttacked(Enemy en)
    {
        if (!attackedEnemies.Contains(en))
            attackedEnemies.Add(en);
    }

    void OnEnemyDeath(Enemy en)
    {
        if (currentEnemies.Contains(en))
            currentEnemies.Remove(en);

        if (attackedEnemies.Contains(en))
            attackedEnemies.Remove(en);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            MoveSpawnPosition();
            var config = GetEnemyConfig();
            var enemy = Pool(config.prefab).Get();
            enemy.transform.position = spawnPos.position;
            enemy.transform.rotation = Quaternion.identity;
#if UNITY_EDITOR
            enemy.gameObject.name = config.name;
#endif
            enemy.Init(config);
            currentEnemies.Add(enemy);
            OnSpawn(enemy);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }


    EnemyConfig GetEnemyConfig() => enemies[Random.Range(0, enemies.Count)];

    void MoveSpawnPosition()
    {
        var s = 2f;
        var x = Random.Range(-s, s);
        spawnPos.position = new Vector3(x, spawnPos.position.y, 0);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (currentEnemies.Contains(enemy))
        {
            currentEnemies.Remove(enemy);
        }
    }
    
    EnemyPool Pool(Enemy enemy)
    {
        if (_pools.ContainsKey(enemy)) return _pools[enemy];

        var container = new GameObject {name = enemy.name};
        container.transform.SetParent(transform);

        var pool = container.AddComponent<EnemyPool>();
        pool.SetPrefab(enemy);

        _pools.Add(enemy, pool);
        return _pools[enemy];
    }
}