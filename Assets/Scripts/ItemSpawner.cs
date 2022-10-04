using System;
using System.Collections.Generic;
using Pools;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Setup")]
    public ItemSlot slotPrefab;
    public ItemPool pool;
    public ItemGetter getter;
    [Header("Settings")]
    public float cooldown = 1;
    public bool isPause;
    public float timer;

    [Header("DEBUG")]
    public List<ItemSlot> lineItems = new();
    public List<ItemSlot> hasSubscribe = new();

    public event Action<ItemSlot> OnCreate = delegate { };
    //  public event Action<ItemSlot> OnSlotClick = delegate { };
    bool _isMerchant;
    float _sellMult;
    float _cdMult;
         

    void Start()
    {
        _cdMult = 1;
        pool.SetPrefab(slotPrefab);
        isPause = false;
        timer = cooldown;
        Events.Instance.OnMerchant += OnMerchant;
        Events.Instance.OnItemSpawnRateAdd += OnSpawnRate;
    }

    void OnSpawnRate(float add)
    {
        _cdMult += add;
    }

    void OnMerchant(float sellMult)
    {
        _isMerchant = true;
        _sellMult = sellMult;
    }

    void FixedUpdate()
    {
        if (isPause) return;

        timer -= Time.fixedDeltaTime;
        if (timer > 0) return;
        timer = cooldown / _cdMult;
        Create();
    }

    void Create()
    {
        var slot = pool.Get();
        var item = getter.Get();

#if UNITY_EDITOR
        item.name = item.Name;
#endif

        slot.Reset();
        slot.Set(item);
        if (_isMerchant)
            slot.SetPrice(_sellMult);
        lineItems.Add(slot);
        OnCreate(slot);

        if (hasSubscribe.Contains(slot)) return;
        hasSubscribe.Add(slot);
        slot.OnUse += Remove;
        slot.OnSell += OnSell;
    }

    void OnSell(ItemSlot slot)
    {
        Events.Instance.AddGold(slot.Price);
        Remove(slot);
    }

    void Remove(ItemSlot slot)
    {
        if (lineItems.Contains(slot))
            lineItems.Remove(slot);
    }
}