using System.Collections;
using System.Collections.Generic;
using Pools;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Bullet bulletPrefab;
    public BulletPool pool;

    public EnemySpawner enemiesSpawner;
    public TestMonsterAnim anim;
    public Transform firePos;

    void Start()
    {
        pool.Init(bulletPrefab);
    }

    public void Shoot(Enemy enemy) 
        => StartCoroutine(ShootRoutine(enemy));

    IEnumerator ShootRoutine(Enemy enemy)
    {
        anim.Attack();
        yield return new WaitForSeconds(anim.attackDur);
        var bullet = pool.Get();
        bullet.transform.position = firePos.position;
        bullet.Fire(enemy);
    }


    Enemy GetTarget(IReadOnlyList<Pattern> patterns)
    {
        foreach (var enemy in enemiesSpawner.currentEnemies)
        {
            if (enemiesSpawner.IsAttacked(enemy)) continue;
            if (patterns.Count != enemy.Config.patterns.Count) continue;

            var match = 0;
            for (var i = 0; i < patterns.Count; i++)
            {
                var p = patterns[i];
                var e = enemy.Config.patterns[i];
                if (p.start == e.start && p.end == e.end ||
                    p.start == e.end && p.end == e.start)
                {
                    match++;
                }
            }

            if (match == patterns.Count)
                return enemy;
        }

        return null;
    }
}