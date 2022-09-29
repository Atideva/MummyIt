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
    public event Action<ItemSlot> OnSlotClick = delegate { };

    void Start()
    {
        pool.Init(slotPrefab);
        isPause = false;
        timer = cooldown;
    }

    void FixedUpdate()
    {
        if (isPause) return;

        timer -= Time.fixedDeltaTime;
        if (timer > 0) return;
        timer = cooldown;
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
        lineItems.Add(slot);
        OnCreate(slot);

        if (hasSubscribe.Contains(slot)) return;
        hasSubscribe.Add(slot);
        slot.OnUse += Remove;
        slot.OnClick += OnClick;
    }

    void OnClick(ItemSlot obj)
    {
        OnSlotClick(obj);
    }

    void Remove(ItemSlot slot)
    {
        if (lineItems.Contains(slot))
            lineItems.Remove(slot);
    }
}