using Pools;
using UnityEngine;

public abstract class Bullet : PoolObject
{
    public int damage = 10;
    public float speed = 10;

    public abstract void Fire(Enemy newTarget);

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Tags.ENEMY))
        {
            var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
            if (enemy)
                OnEnemyCollide(enemy);
        }
    }

    protected abstract void OnEnemyCollide(Enemy enemy);
}