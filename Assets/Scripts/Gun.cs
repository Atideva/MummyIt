using System;
using System.Collections.Generic;
using AttackModificators;
using Powerups;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform container;
    public GunShoot shoot;
    // public SpriteRenderer gunSprite;
    //   public SpriteRenderer aimSprite;
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

    public GunView CurrentView { get; private set; }

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
        shoot.Init(this);
    }

    void Start()
    {
        Events.Instance.OnTakeAim += OnTakeAim;
    }

    public void Change(GunConfig newGun)
    {
        gun = newGun;

        if (CurrentView)
            Destroy(CurrentView.gameObject);
        CurrentView =
            Instantiate(newGun.GunPrefab, container);

        if (_takeAim)
            CurrentView.TakeAim();

        //  gunSprite.sprite = newGun.Sprite;
        shoot.ChangeGun(newGun);
    }

    void FixedUpdate()
    {
        shootCooldown -= Time.fixedDeltaTime;

        if (!enemiesSpawner) return;
        if (_magazine && _magazine.Ammo <= 0 && _overload.Disabled) return;
        if (!_autoShoot) return;

        ShootAtClosestTarget();
    }

    bool _takeAim;

    void OnTakeAim()
    {
        _takeAim = true;
        if (CurrentView)
            CurrentView.TakeAim();
    }


    public void ShootAtPos(Vector2 pos)
    {
        if (shootCooldown > 0) return;
        ResetShootCooldown();
        Shoot(pos);
    }

    public void ShootAtTarget(Enemy target)
    {
        if (shootCooldown > 0) return;
        ResetShootCooldown();
        Shoot(target);
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
    {
        var overloadMult = _overload ? _overload.AtkSpeed : 0f;
        shootCooldown = 1 / (gun.FireRate * (1 + overloadMult));
    }


    public void Disable()
    {
        if (CurrentView)
            CurrentView.gameObject.SetActive(false);
        enabled = false;
    }

    public void Enable()
    {
        if (CurrentView)
            CurrentView.gameObject.SetActive(true);
        enabled = true;
    }


    public void Upgrade()
    {
        lvl++;
    }

    public void Shoot(Enemy target) => shoot.Shoot(target);
    public void Shoot(Vector2 pos) => shoot.Shoot(pos);

    public void TakeAmmo()
    {
        if ((!_overload || _overload.Disabled)
            && _magazine) 
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
        var pos = CurrentView.transform.position;
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