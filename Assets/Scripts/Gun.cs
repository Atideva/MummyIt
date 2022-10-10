using System.Collections.Generic;
using AttackModificators;
using Powerups;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunShoot shoot;
    public SpriteRenderer gunSprite;
    public SpriteRenderer aimSprite;
    public EnemySpawner enemiesSpawner;
    public float topCornerY = 4.5f;
    [Header("DEBUG")]
    public GunConfig gun;
    public float shootCooldown;
    bool _autoShoot;
    AmmoMagazine _magazine;
    public int lvl;
    public AttackModificatorConfig attackModificator;
    PlasmaOverloading _overload;

    public bool CanUpgrade => Enabled && gun && lvl < gun.MaxUpgradeLevel;
    public bool CanChange => Enabled && gun && lvl < 2;
    public bool ChangeDisabled => !CanChange;
    public bool IsMaxed => Enabled && gun && lvl >= gun.MaxUpgradeLevel;
    public bool Enabled { get; private set; }
    public bool Empty => gun == null;

    public void Init(AmmoMagazine magazine, PlasmaOverloading overload)
    {
        _overload = overload;
        overload.OnOverloadEnd += OverloadEnd;
        overload.OnOverloadStart += OverloadStart;
        Enabled = true;
        lvl = 1;
        _magazine = magazine;
    }

    void OverloadEnd()
        => shoot.StopPlasmaOverload();

    void OverloadStart(PlasmaOverloadData data)
        => shoot.PlasmaOverload(data.DamageBonus);

    void Awake()
    {
        aimSprite.enabled = false;
    }

    void Start()
    {
        Events.Instance.OnTakeAim += OnTakeAim;
    }

    void OnTakeAim()
    {
        aimSprite.enabled = true;
    }


    void FixedUpdate()
    {
        shootCooldown -= Time.fixedDeltaTime;

        if (!enemiesSpawner) return;
        if (_magazine.Ammo <= 0 && _overload.Disabled) return;
        if (!_autoShoot) return;

        ShootAtClosestTarget();
    }

    public void ShootAtPos(Vector2 pos)
    {
        if (shootCooldown > 0) return;
        ResetShootCooldown();
        Shoot(pos);
    }

    public void ShootAtClosestTarget()
    {
        if (shootCooldown > 0) return;
        var target = GetClosestEnemy(enemiesSpawner.currentEnemies);
        if (!target) return;
        ResetShootCooldown();
        Shoot(target);
        //enemiesSpawner.EnemyAttacked(target);
    }

    void ResetShootCooldown()
        => shootCooldown = 1 / (gun.FireRate * (1 + _overload.AtkSpeed));


    public void Disable()
    {
        gunSprite.enabled = false;
        enabled = false;
    }

    public void Enable()
    {
        gunSprite.enabled = true;
        enabled = true;
    }

    public void Change(GunConfig newGun)
    {
        gun = newGun;
        gunSprite.sprite = newGun.Sprite;
        shoot.ChangeGun(newGun);
    }

    public void Upgrade()
    {
        lvl++;
    }

    public void Shoot(Enemy target)
    {
        shoot.Shoot(target);
        RefreshMagazine();
    }

    public void Shoot(Vector2 pos)
    {
        shoot.Shoot(pos);
        RefreshMagazine();
    }

    void RefreshMagazine()
    {
        if (_overload.Disabled)
            _magazine.TakeAmmo();
    }


    public void EnableAutoShoot()
        => _autoShoot = true;

    public void DisableAutoShoot()
        => _autoShoot = false;

    Enemy GetClosestEnemy(List<Enemy> enemies)
    {
        Enemy closest = null;
        var minDist = Mathf.Infinity;
        var pos = gunSprite.transform.position;
        foreach (var enemy in enemies)
        {
            if (enemy.transform.position.y > topCornerY) continue;
            var dir = enemy.transform.position - pos;
            var dist = dir.sqrMagnitude;
            if (dist >= minDist) continue;
            minDist = dist;
            closest = enemy;
        }

        return closest;
    }
}