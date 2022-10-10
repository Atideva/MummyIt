using Powerups;
using UnityEngine;

public class TestSpawnItems : MonoBehaviour
{
    [Header("ENABLE THIS TEST")]
    // ReSharper disable once InconsistentNaming
    public bool ENABLE;
    [Header("Settings")]
    public bool disableSpawner;
    public int powerupCount;
    public ItemSpawner spawner;
    public ItemGetter itemGetter;
    public PowerUpConfig powerupItem;


    void Start()
    {
        if (!ENABLE) return;

        Invoke(nameof(DoFastShit),0.1f);
        Invoke(nameof(DoShit),1f);
    }

    void DoFastShit()
    {
        if (disableSpawner)
        {
            spawner.isPause = true;
        }
    }
    void DoShit()
    {
        for (int i = 0; i < powerupCount; i++)
        {
            var item = itemGetter.GetPowerupItem(powerupItem);
            Events.Instance.SpawnItem(item);
        } 
    }
}