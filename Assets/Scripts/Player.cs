using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GunConfig startingGun;
    public int maxHp = 100;
    public HitPoints hitPoints;
    public PlayerHpUI hpBar;
    [Header("Setup")]
    public Gun gun;

    [Header("DEBUG")]
    public GunConfig currentGun;

    void Awake()
    {
        ChangeGun(startingGun);
        hitPoints.Init(maxHp);
        RefreshHpBar();
        Events.Instance.OnEnemyAttack += OnAttackedByEnemy;
    }

    void RefreshHpBar() => hpBar.RefreshBar(hitPoints.Percent);

    void OnAttackedByEnemy(Enemy enemy, float dmg)
    {
        hitPoints.Damage(dmg);
        RefreshHpBar();
    }

    void Start()
    {
        Events.Instance.OnAmmoAdd += OnAddAmmo;
    }

    void OnAddAmmo(AmmoConfig ammo)
    {
        gun.AddAmmo(ammo.Amount);
    }

    public void ChangeGun(GunConfig newGun)
    {
        currentGun = newGun;
        gun.Set(newGun);
    }
}