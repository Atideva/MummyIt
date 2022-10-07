using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public PlayerPowerUps powerUps;
    public PlayerGunSwitcher gunSwitcherUI;
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
        hitPoints.SetMaxHp(maxHp);
        RefreshHpBar();
    }

    void Start()
    {
        Events.Instance.OnAmmoAdd += OnAddAmmo;
        Events.Instance.OnEnemyAttack += OnAttackedByEnemy;
        Events.Instance.OnPlayerMeleeWeapon += OnMeleeWeapon;
        Events.Instance.OnHealPlayer += OnHealPlayer;
        
    }

    void OnHealPlayer(float hpRestore)
    {
         hitPoints.Heal(hpRestore);
         RefreshHpBar();
    }

    void OnMeleeWeapon(MeleeWeaponConfig wep)
    {
        if (!meleeWeapon.IsEnable)
            meleeWeapon.Enable();
        meleeWeapon.Refresh(wep);
    }

    void RefreshHpBar()
        => hpBar.RefreshBar(hitPoints.Percent);

    void OnAttackedByEnemy(Enemy enemy, float dmg)
    {
        if (plateArmor.IsAny)
            plateArmor.Damage(dmg);
        else
            hitPoints.Damage(dmg);

        RefreshHpBar();
    }


    void OnAddAmmo(AmmoConfig ammo)
        => gun.AddAmmo(ammo.Amount);

    public void ChangeGun(GunConfig newGun)
    {
        currentGun = newGun;
        gun.ChangeGun(newGun);
    }
}