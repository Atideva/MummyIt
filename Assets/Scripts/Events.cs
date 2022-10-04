using System;
using Powerups;
using UnityEngine;

public class Events : MonoBehaviour
{
    #region Singleton

    //-------------------------------------------------------------
    public static Events Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // transform.SetParent(null);
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-------------------------------------------------------------

    #endregion

    public event Action<Enemy> OnEnemyDeath = delegate { };
    public void EnemyDeath(Enemy enemy) => OnEnemyDeath(enemy);

    public event Action<AmmoConfig> OnAmmoAdd = delegate { };
    public void AddAmmo(AmmoConfig ammo) => OnAmmoAdd(ammo);

    public event Action<PowerUpConfig> OnUsePowerUp = delegate { };
    public void UsePowerUp(PowerUpConfig powerUp) => OnUsePowerUp(powerUp);

    public event Action<PlasmaOverloadData> OnUsePlasmaOverload = delegate { };
    public void UsePlasmaOverload(PlasmaOverloadData data) => OnUsePlasmaOverload(data);

    public event Action<Enemy, float> OnEnemyAttack = delegate { };
    public void EnemyAttack(Enemy enemy, float damage) => OnEnemyAttack(enemy, damage);

    public event Action OnLevelUp = delegate { };
    public void LevelUp() => OnLevelUp();

    public event Action<int> OnAddAmmoPickup = delegate { };
    public void AddAmmoPickup(int amount) => OnAddAmmoPickup(amount);

    public event Action<float> OnAmmoMagnet = delegate { };
    public void AmmoMagnet(float cooldown) => OnAmmoMagnet(cooldown);

    public event Action<float> OnBulletSpeedAdd = delegate { };
    public void AddBulletSpeed(float mult) => OnBulletSpeedAdd(mult);

    public event Action<float> OnBulletDamageAdd = delegate { };
    public void AddBulletDamage(float mult) => OnBulletDamageAdd(mult);

    public event Action OnTakeAim = delegate { };
    public void TakeAim() => OnTakeAim();

    public event Action<float> OnPlateArmorAdd = delegate { };
    public void AddPlateArmor(float onePlateDurability) => OnPlateArmorAdd(onePlateDurability);

    public event Action OnPlateArmorRestore = delegate { };
    public void RestorePlateArmor() => OnPlateArmorRestore();

    public event Action<MeleeWeaponConfig> OnPlayerMeleeWeapon = delegate { };
    public void PlayerMeleeWeapon(MeleeWeaponConfig config) => OnPlayerMeleeWeapon(config);

    public event Action<float> OnMerchant = delegate { };
    public void Merchant(float sellMult) => OnMerchant(sellMult);

    public event Action<int> OnAddGold = delegate { };
    public void AddGold(int amount) => OnAddGold(amount);

    public event Action<float> OnItemSpawnRateAdd = delegate { };
    public void AddItemSpawnRate(float addMult) => OnItemSpawnRateAdd(addMult);

    public event Action<float> OnPowerupSpawnRateAdd = delegate { };
    public void AddPowerupSpawnRate(float addMult) => OnPowerupSpawnRateAdd(addMult);
}