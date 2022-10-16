[System.Serializable]
public class SpawnWave
{
    public int totalEnemies;
    public float enemiesPerSec;
    public WaveEnemiesConfig enemies;
    public EnemyConfig GetRandomEnemy() => enemies.GetRandom();
}