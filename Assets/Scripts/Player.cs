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
    public AmmoMagazine ammoMagazine;
    [Header("Setup")]
    public Gun firstGun;
    public Gun secondGun;
    public GunSlotsUI gunSlotsUI;
    [Header("DEBUG")]
    public GunConfig firstGunConfig;
    public GunConfig secondGunConfig;
    

    void Awake()
    {
        firstGun.Enable();
        secondGun.Disable();

        firstGun.Init(ammoMagazine);
        ChangeFirst(startingGun);

        hitPoints.SetMaxHp(maxHp);
        RefreshHpBar();
    }

    void Start()
    {
        Events.Instance.OnAmmoAdd += OnAddAmmo;
        Events.Instance.OnEnemyAttack += OnAttackedByEnemy;
        Events.Instance.OnPlayerMeleeWeapon += OnMeleeWeapon;
        Events.Instance.OnHealPlayer += OnHealPlayer;
        Events.Instance.OnGunPickup += OnGunPickup;
        Events.Instance.OnAllowSecondGun += AllowSecondGun;
    }

    void AllowSecondGun()
    {
        secondGun.Init(ammoMagazine);
        ChangeSecond(startingGun);
    }

    void ChangeFirst(GunConfig newGun)
    {
        firstGunConfig = newGun;
        firstGun.Change(newGun);
        gunSlotsUI.FirstSlotRefresh(firstGun);
    }

    void ChangeSecond(GunConfig newGun)
    {
        secondGunConfig = newGun;
        secondGun.Change(newGun);
        gunSlotsUI.SecondSlotRefresh(secondGun);
    }

    void UpgradeFirst()
    {
        firstGun.Upgrade();
        gunSlotsUI.FirstSlotRefresh(firstGun);
    }

    void UpgradeSecond()
    {
        secondGun.Upgrade();
        gunSlotsUI.SecondSlotRefresh(secondGun);
    }

    void OnGunPickup(GunConfig gun)
    {
        if (secondGun.Enabled && secondGun.Empty)
        {
            ChangeSecond(gun);
            return;
        }

        if (firstGun.CanUpgrade && firstGun.gun == gun)
        {
            UpgradeFirst();
            return;
        }

        if (secondGun.CanUpgrade && secondGun.gun == gun)
        {
            UpgradeSecond();
            return;
        }

        if (firstGun.CanChange)
        {
            ChangeFirst(gun);
            return;
        }

        if (secondGun.CanChange)
        {
            ChangeSecond(gun);
            return;
        }
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
        => ammoMagazine.AddAmmo(ammo.Amount);
}