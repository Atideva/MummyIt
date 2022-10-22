using System.Collections.Generic;
using Items;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Setup")]
    public ItemGetter getter;
    public ItemSlotSpawner slotSpawner;

    [Header("Settings")]
    public float itemPerSec = 1;
    public bool isPause;
    public float timer;
    public float Cooldown => 1 / itemPerSec;

    readonly Queue<Item> _queue=new();
    bool _isMerchant;
    float _sellMult;
    float _cdMult;

    void Start()
    {
        _cdMult = 1;
        isPause = false;
        timer = Cooldown;
        Events.Instance.OnMerchant += OnMerchant;
        Events.Instance.OnIncreaseItemRate += OnIncreaseItemRate;
        Events.Instance.OnItemSpawnRequest += OnItemSpawnRequest;
    }

    void FixedUpdate()
    {
        if (isPause) return;

        if (_queue.Count > 0)
        {
            TryPutQueueItem();
        }

        timer -= Time.fixedDeltaTime;
        if (timer > 0) return;
        timer = Cooldown / _cdMult;

        var newItem = getter.Get();
        TryPutItem(newItem);
    }


    void OnItemSpawnRequest(Item item, float posX)
    {
        var freeSlot = slotSpawner.GetFreeSlot();
        var slot = TryPutItem(item);
        if (slot && posX != -12345)
        {
            slot.slotRect.anchoredPosition = new Vector2(posX, 0);
        }
    }

    void OnIncreaseItemRate(float add)
    {
        _cdMult += add;
    }

    void OnMerchant(float sellMult)
    {
        _isMerchant = true;
        _sellMult = sellMult;
    }

    void TryPutQueueItem()
    {
        var freeSlot = slotSpawner.GetFreeSlot();
        if (!freeSlot) return;
        var item = _queue.Dequeue();
        PutItem(item, freeSlot);
    }

    ItemSlot TryPutItem(Item item)
    {
        var freeSlot = slotSpawner.GetFreeSlot();
        if (freeSlot)
        {
            PutItem(item, freeSlot);
        }
        else
        {
            _queue.Enqueue(item);
        }

        return freeSlot;
    }

    void PutItem(Item item, ItemSlot slot)
    {
#if UNITY_EDITOR
        item.name = item.Name;
#endif
        slot.Set(item);
        if (_isMerchant)
            slot.SetPrice(_sellMult);

        // if (!hasSubscribe.Contains(slot))
        // {
        //     hasSubscribe.Add(slot);
        //     // slot.OnUse += Remove;
        //     slot.OnDestroy += Remove;
        //     slot.OnSell += OnSell;
        //     slot.OnClick += OnClick;
        // }
    }


    // void OnSell(ItemSlot slot)
    // {
    //     Events.Instance.AddGold(slot.Price);
    //     Remove(slot);
    // }

    // void Remove(ItemSlot slot)
    // {
    //     if (lineItems.Contains(slot))
    //         lineItems.Remove(slot);
    // }
}