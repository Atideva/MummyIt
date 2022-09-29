using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunShoot shoot;
    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI amountTxt;
    public GunConfig gun;
    public int magazine;
    public float shootTimer;
    public EnemySpawner enemiesSpawner;
    public float topCornerY = 4.5f;

    void Awake()
    {
        magazine = 0;
        RefreshText();
    }

    void FixedUpdate()
    {
        if (!enemiesSpawner) return;
        if (magazine == 0) return;

        var target = GetClosestEnemy(enemiesSpawner.currentEnemies);
        if (!target) return;

        shootTimer -= Time.fixedDeltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = 1 / gun.FireRate;
            Shoot(target);
            //  enemiesSpawner.EnemyAttacked(target);
        }
    }


    public void Set(GunConfig newGun)
    {
        gun = newGun;
        spriteRenderer.sprite = newGun.Sprite;
    }

    public void Shoot(Enemy target)
    {
        magazine--;
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
        var pos = spriteRenderer.transform.position;
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