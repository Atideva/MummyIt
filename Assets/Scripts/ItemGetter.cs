using Items;
using Pools;
using Powerups;
using UnityEngine;

public class ItemGetter : MonoBehaviour
{
    [Header("Setup")]
    public Player player;
    [Header("Slots backgrounds")]
    public Sprite ammoSlotSprite;
    public Sprite ammoSlotSprite2;
    public Sprite boosterSprite;
    public Sprite boosterSprite2;
    public Sprite skillSprite;
    public Sprite skillSprite2;
    [Header("PowerUp")]
    public ItemPowerUp powerUpPrefab;
    public ItemPowerUpPool powerUpPool;
    [Header("Ammo")]
    public ItemAmmo ammoPrefab;
    public ItemAmmoPool ammoPool;
    // [Header("Colors")]
    // public Color ammoColor;
    // public Color powerUpColor;
    // public Color gunColor;
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

    public Item GetAmmoItem(AmmoConfig ammo)
    {
        var item = ammoPool.Get();
        item.Set(ammo);
        item.SetTypeSprite(ammoSlotSprite,ammoSlotSprite2);
      //  item.SetColor(ammoColor);
        return item;
    }
    
    public Item GetPowerupItem(PowerUpConfig powerup)
    {
        var item = powerUpPool.Get();
        item.Set(powerup);
        var spr = powerup.Type == PowerupType.Booster ? boosterSprite : skillSprite;
        var spr2 = powerup.Type == PowerupType.Booster ? boosterSprite2 : skillSprite2;
        item.SetTypeSprite(spr,spr2);
   //     item.SetColor(ammoColor);
        return item;
    }
    Item GetAmmo()
    {
        var item = ammoPool.Get();
        var id = 0;
        var chance = Random.Range(0f, 1);
        var sum = 0f;

        for (var i = 0; i < player.ammoMagazine.AmmoTypes.Count; i++)
        {
            sum += player.ammoMagazine.GetAmmoChance(i);
            if (chance > sum) continue;
            id = i;
            break;
        }

        var ammo = player.ammoMagazine.AmmoTypes[id];
        item.Set(ammo);
        item.SetTypeSprite(ammoSlotSprite,ammoSlotSprite2);
     //   item.SetColor(ammoColor);
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

        var powerup = player.powerUps.availablePowerUps[id];
        item.Set(powerup);
        var spr = powerup.Type == PowerupType.Booster ? boosterSprite : skillSprite;
        var spr2 = powerup.Type == PowerupType.Booster ? boosterSprite2 : skillSprite2;
        item.SetTypeSprite(spr,spr2);
      //  item.SetColor(powerUpColor);
        return item;
    }
    
}