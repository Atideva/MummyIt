using System.Collections.Generic;
using EPOOutline;
using Powerups;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunShoot shoot;
    public SpriteRenderer gunSprite;
    public SpriteRenderer aimSprite;
    public Outlinable plasmaOverloadOutline;
    public TextMeshProUGUI amountTxt;
    public EnemySpawner enemiesSpawner;
    public float topCornerY = 4.5f;
    [Header("DEBUG")]
    public GunConfig gun;
    public int magazine;
    public float shootCooldown;

    bool _autoShoot;
    bool _plasmaOverload;
    float _plasmaOverloadTimer;
    float _plasmaAtkSpdBonus;

    void Awake()
    {
        aimSprite.enabled = false;
        plasmaOverloadOutline.enabled = false;
        magazine = 0;
        RefreshText();
    }

    void Start()
    {
        Events.Instance.OnUsePlasmaOverload += OnPlasmaOverload;
        Events.Instance.OnTakeAim += OnTakeAim;
    }

    void OnTakeAim()
    {
        aimSprite.enabled = true;
    }


    void OnPlasmaOverload(PlasmaOverloadData data)
    {
        _plasmaOverload = true;
        _plasmaOverloadTimer += data.Duration;
        _plasmaAtkSpdBonus = data.AtkSpdBonus;
        shoot.PlasmaOverload(data.DamageBonus);
        plasmaOverloadOutline.enabled = true;
    }

    void StopPlasmaOverload()
    {
        _plasmaOverload = false;
        _plasmaOverloadTimer = 0;
        _plasmaAtkSpdBonus = 0;
        shoot.StopPlasmaOverload();
        plasmaOverloadOutline.enabled = false;
    }


    void FixedUpdate()
    {
        shootCooldown -= Time.fixedDeltaTime;
        if (!enemiesSpawner) return;
        if (magazine <= 0 && !_plasmaOverload) return;

        if (_plasmaOverload)
        {
            if (_plasmaOverloadTimer > 0)
                _plasmaOverloadTimer -= Time.fixedDeltaTime;
            else
                StopPlasmaOverload();
        }

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
        => shootCooldown = 1 / (gun.FireRate * (1 + _plasmaAtkSpdBonus));

    public void ChangeGun(GunConfig newGun)
    {
        gun = newGun;
        gunSprite.sprite = newGun.Sprite;
        shoot.ChangeGun(newGun);
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
        if (!_plasmaOverload) magazine--;
        RefreshText();
    }

    public void AddAmmo(int amount)
    {
        magazine += amount;
        RefreshText();
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

    void RefreshText() 
        => amountTxt.text = magazine.ToString();
}