using System.Collections;
using System.Collections.Generic;
using Pools;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] EnemySpawner enemiesSpawner;
    [SerializeField] TestMonsterAnim anim;
    [SerializeField] Transform firePos;
    [Header("Bullet pool")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] BulletPool pool;
    [Header("Sprites")]
    [SerializeField] Sprite plasmaOverloadSprite;
    [SerializeField] Sprite baseBulletSprite;
    [Header("DEBUG")]
    [SerializeField] float plasmaDmgMult = 0;
    [SerializeField] float bonusDmgMult = 0;
    [SerializeField] float speedDmgMult = 0;
    bool _plasmaOverload;

    void Start()
    {
        pool.Init(bulletPrefab);
        Events.Instance.OnBulletSpeedAdd += OnBulletSpeedAdd;
        Events.Instance.OnBulletDamageAdd += OnBulletDamageAdd;
    }

    void OnBulletSpeedAdd(float mult)
        => speedDmgMult += mult;

    void OnBulletDamageAdd(float mult)
        => bonusDmgMult += mult;

    public void Shoot(Enemy enemy)
        => StartCoroutine(ShootRoutine(enemy));

    IEnumerator ShootRoutine(Enemy enemy)
    {
        anim.Attack();
        yield return new WaitForSeconds(anim.attackDur);
        var bullet = pool.Get();
        bullet.SetSprite(_plasmaOverload ? plasmaOverloadSprite : baseBulletSprite);
        bullet.SetDamageMult(plasmaDmgMult, bonusDmgMult);
        bullet.SetSpeedMult(speedDmgMult);
        bullet.transform.position = firePos.position;
        bullet.Fire(enemy);
    }

    public void PlasmaOverload(float mult)
    {
        _plasmaOverload = true;
        plasmaDmgMult = mult;
    }

    public void StopPlasmaOverload()
    {
        _plasmaOverload = false;
        plasmaDmgMult = 0;
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