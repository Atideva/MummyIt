using Items;
using Pools;
using UnityEngine;

public class ItemGetter : MonoBehaviour
{
    [Header("Setup")]
    public Player player;
    [Header("Pool")]
    public ItemAmmo ammoPrefab;
    public ItemAmmoPool ammoPool;
    [Header("Colors")]
    public Color ammoColor;
    public Color powerupColor;
    public Color gunColor;
    void Start()
    {
        ammoPool.Init(ammoPrefab);
    }

    public Item Get()
    {
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
}