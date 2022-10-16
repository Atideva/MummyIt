using System;
using UnityEngine;

public class EnemySpawnWave : MonoBehaviour
{
    public LevelSpawnConfig _spawnConfig;
    int _current;
    int _spawned;
  public  bool IsCurrentWave => _current < _spawnConfig.waves.Count;
    SpawnWave CurrentWave => IsCurrentWave
        ? _spawnConfig.waves[_current]
        : null;
    public float Cooldown => IsCurrentWave
        ? 1 / CurrentWave.enemiesPerSec
        : 1;
    public event Action OnWaveComplete = delegate { };
    public event Action OnLastWaveComplete = delegate { };

    public void Init(EnemySpawner spawner, LevelSpawnConfig spawnConfig)
    {
        spawner.OnSpawn += OnEnemySpawn;
        _spawnConfig = spawnConfig;
        _current = 0;
        _spawned = 0;
    }

    public EnemyConfig Get()
        => IsCurrentWave ? CurrentWave.GetRandomEnemy() : null;

    void OnEnemySpawn(Enemy enemy)
    {
        _spawned++;

        if (_spawned >= CurrentWave.totalEnemies)
        {
            OnWaveComplete();
            if (_current + 1 < _spawnConfig.waves.Count)
            {
                _current++;
            }
            else
            {
                OnLastWaveComplete();
            }
        }
    }



    
}