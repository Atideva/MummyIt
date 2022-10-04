using Items;
using Pools;
using UnityEngine;

public class ItemGetter : MonoBehaviour
{
    [Header("Setup")]
    public Player player;
    [Header("PowerUp")]
    public ItemPowerUp powerUpPrefab;
    public ItemPowerUpPool powerUpPool;
    [Header("Ammo")]
    public ItemAmmo ammoPrefab;
    public ItemAmmoPool ammoPool;
    [Header("Colors")]
    public Color ammoColor;
    public Color powerUpColor;
    public Color gunColor;
    [Header("Powerup chance")]
    public float powerupChance = 0.3f;
    public float powerupChanceMax = 0.5f;
   
    void Start()
    {
        ammoPool.SetPrefab(ammoPrefab, 10);
        powerUpPool.SetPrefab(powerUpPrefab, 5);
        Events.Instance.OnPowerupSpawnRateAdd += OnPowerupSpawnRate;
    }

    void OnPowerupSpawnRate(float add)
    {
        powerupChance *= add;
        if (powerupChance > powerupChanceMax) 
            powerupChance = powerupChanceMax;
    }

    public Item Get()
    {
        var r = Random.Range(0f, 1f);
        if (r <= powerupChance)
            return GetPowerUp();

        return GetAmmo();
    }

    Item GetAmmo()
    {
        var item = ammoPool.Get();
        var id = 0;
        var chance = Random.Range(0f, 1);
        var sum = 0f;

        for (var i = 0; i < player.currentGun.Ammo.Count; i++)
        {
            sum += player.currentGun.GetAmmoChance(i);
            if (chance > sum) continue;
            id = i;
            break;
        }

        var ammo = player.currentGun.Ammo[id];
        item.Set(ammo);
        item.SetColor(ammoColor);
        return item;
    }

    Item GetPowerUp()
    {
        var item = powerUpPool.Get();
        var id = 0;
        var chance = Random.Range(0f, 1);
        var sum = 0f;

        for (var i = 0; i < player.powerUps.availablePowerUps.Count; i++)
        {
            sum += player.powerUps.GetAmmoChance(i);
            if (chance > sum) continue;
            id = i;
            break;
        }

        var ammo = player.powerUps.availablePowerUps[id];
        item.Set(ammo);
        item.SetColor(powerUpColor);
        return item;
    }
    
}