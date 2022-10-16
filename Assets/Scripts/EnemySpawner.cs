using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public bool ENABLED;
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

    public LevelSpawnConfig levelSpawn;
    public EnemySpawnPositions spawnPositions;
    public EnemySpawnWave wave;
    public EnemySpawnPool pool;
    public Transform immunePos;
    public Transform spawnPos;
    public List<Enemy> currentEnemies = new();
    public List<Enemy> attackedEnemies = new();
    public event Action<Enemy> OnSpawn = delegate { };


    public void StopSpawn() => _spawn = false;

    void Start()
    {
        if (!ENABLED)
        {
            Debug.LogError("ENEMY SPAWNER DISABLED!");
        }
        
        wave.Init(this, levelSpawn);
        _spawn = true;
        _timer = wave.Cooldown;
        Events.Instance.OnEnemyDeath += OnEnemyDeath;
        Events.Instance.OnEnemySpawnRequest += OnEnemySpawnRequest;
    }

    void OnEnemySpawnRequest(EnemyConfig enemy, Vector2 pos)
    {
        Spawn(enemy, pos);
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
        if (!ENABLED) return;
        
        _timer -= Time.fixedDeltaTime;
        if (_timer > 0) return;
        if (!_spawn) return;

        _timer = wave.Cooldown;
        var enemyConfig = wave.Get();
        if (!enemyConfig) return;
        var pos = spawnPositions.GetRandom();
        Spawn(enemyConfig, pos);
    }

    void Spawn(EnemyConfig config, Vector2 pos)
    {
        var enemy = pool.Get(config);
        enemy.transform.position = pos;
        enemy.transform.rotation = Quaternion.identity;
#if UNITY_EDITOR
        enemy.gameObject.name = config.name;
#endif
        enemy.SetConfig(config);
        enemy.SetImmune(immunePos.position.y);
        AddToList(enemy);
        OnSpawn(enemy);
    }

    public void AddToList(Enemy enemy)
        => currentEnemies.Add(enemy);
}