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
    public float shootTimer;

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
        if (!enemiesSpawner) return;
        if (magazine == 0 && !_plasmaOverload) return;

        if (_plasmaOverload)
        {
            if (_plasmaOverloadTimer > 0)
                _plasmaOverloadTimer -= Time.fixedDeltaTime;
            else
                StopPlasmaOverload();
        }

        var target = GetClosestEnemy(enemiesSpawner.currentEnemies);
        if (!target) return;

        shootTimer -= Time.fixedDeltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = 1 / (gun.FireRate * (1 + _plasmaAtkSpdBonus));
            Shoot(target);
            //  enemiesSpawner.EnemyAttacked(target);
        }
    }


    public void Set(GunConfig newGun)
    {
        gun = newGun;
        gunSprite.sprite = newGun.Sprite;
    }

    public void Shoot(Enemy target)
    {
        if (!_plasmaOverload) magazine--;
        shoot.Shoot(target);
        RefreshText();
    }

    public void AddAmmo(int amount)
    {
        magazine += amount;
        RefreshText();
    }


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

    void RefreshText() => amountTxt.text = magazine.ToString();
}