using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public event Action<Enemy> OnSpawn=delegate {  };
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
            ChangePosition();
            var enemy = Instantiate(prefab, spawnPos.position, Quaternion.identity);
            var config = GetEnemy();
            enemy.Init(config);
            enemy.gameObject.name = config.name;
            currentEnemies.Add(enemy);
            OnSpawn(enemy);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }


    EnemyConfig GetEnemy() => enemies[Random.Range(0, enemies.Count)];

    void ChangePosition()
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
}