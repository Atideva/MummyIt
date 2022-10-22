using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using Pools;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] EnemySpawner enemiesSpawner;
    [SerializeField] TestMonsterAnim anim;
        // [SerializeField] Transform firePos;
    [Header("Sprites")]
    [SerializeField] Sprite plasmaOverloadSprite;
    [SerializeField] Sprite baseBulletSprite;
    [Header("DEBUG")]
    [SerializeField] BulletPool currentPool;
    [SerializeField] GunConfig currentGun;
    [SerializeField] float plasmaDmgMult = 0;
    [SerializeField] float bonusDmgMult = 0;
    [SerializeField] float speedDmgMult = 0;
    bool _plasmaOverload;
    readonly Dictionary<Bullet, BulletPool> _pools = new();
    Gun _gun;

    void Start()
    {
        Events.Instance.OnBulletSpeedAdd += OnBulletSpeedAdd;
        Events.Instance.OnBulletDamageAdd += OnBulletDamageAdd;
    }

    public void ChangeGun(GunConfig gun)
    {
        currentGun = gun;
        UpdatePool(gun.BulletPrefab);
    }

    void UpdatePool(Bullet bullet)
    {
        if (!_pools.ContainsKey(bullet))
        {
            var container = new GameObject
            {
                name = "Pool: " +bullet.name
            };
            container.transform.SetParent(transform);
            var pool = container.AddComponent<BulletPool>();
            pool.SetPrefab(bullet);
            _pools.Add(bullet, pool);
        }

        currentPool = _pools[bullet];
    }


    void OnBulletSpeedAdd(float mult)
        => speedDmgMult += mult;

    void OnBulletDamageAdd(float mult)
        => bonusDmgMult += mult;

    public void Shoot(Enemy enemy)
        => StartCoroutine(ShootRoutine(Vector2.up, enemy));

    public void Shoot(Vector2 pos)
        => StartCoroutine(ShootRoutine(pos));

    public void Init(Gun gun)
    {
        _gun = gun;
    }
    IEnumerator ShootRoutine(Vector2 shootPos, Enemy enemy = null)
    {
        anim.Attack();
        yield return new WaitForSeconds(anim.attackDur);
        ShootSound();
        MuzzleVfx();

        if (currentGun.MultiShot)
        {
            var bullets = currentGun.MultiShot ? currentGun.BulletsAmountPerShot : 1;
            var spread = Random.Range(currentGun.AngleSpread.minValue, currentGun.AngleSpread.maxValue);

            var pos = (Vector2) _gun.CurrentView.OriginPos;
            var targetPos = enemy ? (Vector2) enemy.transform.position : shootPos;
            var dir = targetPos - pos;
            _gun.CurrentView.transform.up = dir;
          //  firePos.up = dir;

            var angle =  _gun.CurrentView.Rotation.eulerAngles.z;
            dir.Normalize();

            if (bullets > 1) angle -= spread / 2;
            var step = bullets > 1 ? spread / (bullets - 1) : 0;

            for (var i = 0; i < bullets; i++)
            {
                var random = step * 0.5f;
                var randomAngle = Random.Range(angle - random, angle + random);

                //BULLET_POOL.CreateBullet(damage, bulletSpeed, firePoint.position, Quaternion.Euler(0f, 0f, randomAngle));

                var bullet = currentPool.Get();
                bullet.SetDamage(currentGun.Damage, plasmaDmgMult, bonusDmgMult);
                bullet.SetSpeed(currentGun.BulletSpeed, speedDmgMult);
               // bullet.SetSprite(_plasmaOverload ? plasmaOverloadSprite : baseBulletSprite);
                bullet.transform.position =  _gun.CurrentView.FirePos;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);
                bullet.Fire(enemy);

                _gun.TakeAmmo();
                
                angle += step;
                if (currentGun.DelayBetweenShots > 0)
                    yield return new WaitForSeconds(currentGun.DelayBetweenShots);
            }
        }
        else
        {
            var bullet = currentPool.Get();
        //    bullet.SetSprite(_plasmaOverload ? plasmaOverloadSprite : baseBulletSprite);
            bullet.SetDamage(currentGun.Damage, plasmaDmgMult, bonusDmgMult);
            bullet.SetSpeed(currentGun.BulletSpeed, speedDmgMult);

            var pos = (Vector2)  _gun.CurrentView.OriginPos;
            var targetPos = (Vector2) enemy.transform.position;

            var dir = (targetPos - pos).normalized;
            _gun.CurrentView.transform.up = dir;
            
            var dir2 = (targetPos - pos).normalized;
            bullet.transform.up = dir2;
            bullet.transform.position = _gun.CurrentView.FirePos;
            bullet.Fire(enemy);
            
            _gun.TakeAmmo();
        }
    }

    void MultiShot()
    {
        var spread = Random.Range(currentGun.AngleSpread.minValue, currentGun.AngleSpread.maxValue);
        var angle =  _gun.CurrentView.Rotation.eulerAngles.z;
        angle -= spread / 2;
        var bullets = currentGun.BulletsAmountPerShot;
        var step = spread / bullets;

        for (var i = 0; i < bullets; i++)
        {
            var random = step * 0.5f;
            var randomAngle =
                Random.Range(angle - random,
                    angle + random); //Рандомный разброс при выстреле , просто для визуальной разнообразия

            //   BULLET_POOL.CreateBullet(damage, bulletSpeed, firePoint.position, Quaternion.Euler(0f, 0f, randomAngle));

            angle += step;
        }
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


    void MuzzleVfx()
    {
        if (currentGun.MuzzleVfxPrefab)
            Events.Instance.PlayVfx(currentGun.MuzzleVfxPrefab,  _gun.CurrentView.FirePos,  _gun.CurrentView.Rotation);
    }

    void ShootSound() => AudioManager.Instance.PlaySound(currentGun.ShootSound);

    // Enemy GetTarget(IReadOnlyList<Pattern> patterns)
    // {
    //     foreach (var enemy in enemiesSpawner.currentEnemies)
    //     {
    //         if (enemiesSpawner.IsAttacked(enemy)) continue;
    //         if (patterns.Count != enemy.Config.patterns.Count) continue;
    //
    //         var match = 0;
    //         for (var i = 0; i < patterns.Count; i++)
    //         {
    //             var p = patterns[i];
    //             var e = enemy.Config.patterns[i];
    //             if (p.start == e.start && p.end == e.end ||
    //                 p.start == e.end && p.end == e.start)
    //             {
    //                 match++;
    //             }
    //         }
    //
    //         if (match == patterns.Count)
    //             return enemy;
    //     }
    //
    //     return null;
    // }
}