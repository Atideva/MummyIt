using System;
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

    public Transform immunePos;
    public List<EnemyConfig> enemies = new();
    public Enemy prefab;
    public float enemiesPerSec = 2;
      float SpawnCooldown=> 1/enemiesPerSec;
    public Transform spawnPos;
    public List<Enemy> currentEnemies = new();
    public List<Enemy> attackedEnemies = new();
    public event Action<Enemy> OnSpawn = delegate { };
    readonly Dictionary<Enemy, EnemyPool> _pools = new();

    public void StopSpawn() => _spawn = false;
    
    void Start()
    {
        _spawn = true;
        _timer = SpawnCooldown;
        Events.Instance.OnEnemyDeath += OnEnemyDeath;
        Events.Instance.OnEnemySpawnRequest += OnEnemySpawnRequest;
    }

    void OnEnemySpawnRequest(EnemyConfig enemy, Vector2 pos)
    {
        Spawn(enemy,pos);
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

    float _timer;
    bool _spawn;
    void FixedUpdate()
    {
        
        _timer -= Time.fixedDeltaTime;
        if (_timer > 0) return;
        if (!_spawn) return;
        
        _timer = SpawnCooldown;
        
        MoveSpawnPosition();
        var enemyConfig = GetEnemyConfig();
        Spawn(enemyConfig, spawnPos.position);
    }
 

    void Spawn(EnemyConfig config, Vector2 pos)
    {
        var enemy = Pool(config.prefab).Get();
        enemy.transform.position = pos;
        enemy.transform.rotation = Quaternion.identity;
#if UNITY_EDITOR
        enemy.gameObject.name = config.name;
#endif
        enemy.SetConfig(config);
        enemy.SetImmune(immunePos.position.y);
        currentEnemies.Add(enemy);
        OnSpawn(enemy);
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