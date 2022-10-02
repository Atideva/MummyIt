using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public PlayerPowerUps powerUps;
    public GunConfig startingGun;
    public int maxHp = 100;
    public HitPoints hitPoints;
    public PlayerHpUI hpBar;
    public PlayerPlateArmor plateArmor;
    public MeleeWeapon meleeWeapon;
    [Header("Setup")]
    public Gun gun;
    [Header("DEBUG")]
    public GunConfig currentGun;

    void Awake()
    {
        ChangeGun(startingGun);
        hitPoints.Init(maxHp);
        RefreshHpBar();
    }

    void Start()
    {
        Events.Instance.OnAmmoAdd += OnAddAmmo;
        Events.Instance.OnEnemyAttack += OnAttackedByEnemy;
        Events.Instance.OnPlayerMeleeWeapon += OnMeleeWeapon;
    }

    void OnMeleeWeapon(PlayerMeleeData wep)
    {
        if (!meleeWeapon.IsEnable)
            meleeWeapon.Enable();
        meleeWeapon.Refresh(wep);
    }

    void RefreshHpBar() => hpBar.RefreshBar(hitPoints.Percent);

    void OnAttackedByEnemy(Enemy enemy, float dmg)
    {
        if (plateArmor.IsAny)
            plateArmor.Damage(dmg);
        else
            hitPoints.Damage(dmg);

        RefreshHpBar();
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