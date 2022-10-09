using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Pools;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Setup")]
    public ItemSlot slotPrefab;
    public ItemPool pool;
    public ItemGetter getter;
    public ItemTransporter transporter;
    [Header("Settings")]
    public float itemPerSec = 1;

    public bool isPause;
    public float timer;
    public int maxItemsCount = 10;
    [Header("DEBUG")]
    [SerializeField] List<ItemSlot> lineItems = new();
    [SerializeField] List<ItemSlot> hasSubscribe = new();

    public List<ItemSlot> VisibleItems  
        => lineItems.Where(item => transporter.IsVisible(item)).ToList();

    float Cooldown => 1 / itemPerSec;

    public IReadOnlyList<ItemSlot> LineItems => lineItems;

    public event Action<ItemSlot> OnCreate = delegate { };
    public event Action<ItemSlot> OnSlotClick = delegate { };
    bool _isMerchant;
    float _sellMult;
    float _cdMult;


    void Start()
    {
        _cdMult = 1;
        pool.SetPrefab(slotPrefab);
        isPause = false;
        timer = Cooldown;
        Events.Instance.OnMerchant += OnMerchant;
        Events.Instance.OnItemSpawnRateAdd += OnItemSpawnRate;
        Events.Instance.OnItemSpawnRequest += OnItemSpawnRequest;
    }

    void OnItemSpawnRequest(Item item, float posX)
    {
        var slot = Create(item);
        if (posX != -12345)
        {
            slot.slotRect.anchoredPosition = new Vector2(posX, 0);
        }
    }

    void OnItemSpawnRate(float add)
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
        if (lineItems.Count >= maxItemsCount) return;
        timer = Cooldown / _cdMult;
        var item = getter.Get();
        Create(item);
    }

    ItemSlot Create(Item item)
    {
        var slot = pool.Get();

#if UNITY_EDITOR
        item.name = item.Name;
#endif

        slot.Set(item);
        if (_isMerchant)
            slot.SetPrice(_sellMult);
        lineItems.Add(slot);

        OnCreate(slot);

        if (!hasSubscribe.Contains(slot))
        {
            hasSubscribe.Add(slot);
            // slot.OnUse += Remove;
            slot.OnDestroy += Remove;
            slot.OnSell += OnSell;
            slot.OnClick += OnClick;
        }

        return slot;
    }

    void OnClick(ItemSlot slot)
        => OnSlotClick(slot);

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