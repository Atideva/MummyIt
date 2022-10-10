using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Items;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public ItemSpawner spawner;
    public ItemCollector collector;
    public Transform powerupsMoveTo;
    public Transform ammoMoveTo;
    public float moveTime = 0.5f;
    public PatternDrawer drawer;
    public int pickupAtOnce = 1;
    public LazyPickerHandler lazyPicker;
    public float collectSize = 0.4f;
    [Header("DEBUG")]
    //List<ItemSlot> _collectAtOnce = new();
    public List<ItemSlot> movingSlots = new();
    public bool AnyAmmo => GetAmmoSlot();
    public bool enable;
    public void Disable() => enable = false;
    void Awake()
    {
        enable = true;
        spawner.OnSlotClick += OnSlotClick;
        drawer.OnRelease += SearchMatchItem;
        Events.Instance.OnAddAmmoPickup += OnAddAmmoPickup;
    }

    void OnSlotClick(ItemSlot slot)
    {
        if (!enable) return;
        if (movingSlots.Contains(slot)) return;

        if (collector.AnyEmptySlot && slot.item is not ItemAmmo)
        {
            Debug.Log("Slot collect", slot);
            movingSlots.Add(slot);
            collector.Collect(slot);
            return;
        }

        if (lazyPicker.current > 0)
        {
            lazyPicker.SpendOneCharge();
            movingSlots.Add(slot);
            Pickup(slot);
        }
    }

    void OnAddAmmoPickup(int amount)
    {
        pickupAtOnce += amount;
    }


    void SearchMatchItem(List<Pattern> drawPattern)
    {
        var getSlot = GetSlot(drawPattern, spawner.VisibleItems);
        if (!getSlot)
        {
            var collectorSlot = GetSlot(drawPattern, collector.Items);
            if (collectorSlot)
                collector.UseItem(collectorSlot);

            return;
        }

        Pickup(getSlot);
        movingSlots.Add(getSlot);

        if (getSlot.item is not ItemAmmo || pickupAtOnce < 2)
            return;

        for (var i = 1; i < pickupAtOnce; i++)
        {
            var slot = GetSlot(drawPattern, spawner.VisibleItems);
            if (!slot) continue;
            Pickup(slot);
            movingSlots.Add(slot);
        }
    }


    public void CollectAmmo()
    {
        for (var i = 0; i < pickupAtOnce; i++)
        {
            var slot = GetAmmoSlot();
            if (!slot) continue;
            Pickup(slot);
            movingSlots.Add(slot);
        }
    }


    public  void Pickup(ItemSlot slot)
    {
        var pos = slot.item is ItemAmmo
            ? ammoMoveTo.position
            : powerupsMoveTo.position;


        slot.container.DOScale(collectSize, moveTime);
        
        Debug.Log("Slot collect", slot);
        slot.container
            .DOMove(pos, moveTime)
            .OnComplete(()
                => UseSlot(slot));
    }

    public void FinishMove(ItemSlot slot)
    {
        if (movingSlots.Contains(slot))
            movingSlots.Remove(slot);
    }

    void UseSlot(ItemSlot slot)
    {
        FinishMove(slot);
        slot.Use();
        slot.ReturnToPool();
    }


    ItemSlot GetSlot(IReadOnlyList<Pattern> patterns, IEnumerable<ItemSlot> items)
    {
        foreach (var slot in items)
        {
            // if (_collectAtOnce.Contains(slot)) continue;
            if (movingSlots.Contains(slot)) continue;
            if (slot.IsEmpty) continue;
            if (patterns.Count != slot.item.Patterns.Count) continue;

            var match = patterns.Count
            (draw =>
                slot.item.Patterns.Any
                (item =>
                    draw.start == item.start && draw.end == item.end ||
                    draw.start == item.end && draw.end == item.start));

            if (match == patterns.Count)
                return slot;
        }

        return null;
    }

    ItemSlot GetAmmoSlot()
    {
        foreach (var slot in spawner.VisibleItems)
        {
            if (movingSlots.Contains(slot)) continue;
            if (slot.item is ItemAmmo)
                return slot;
        }

        return null;
    }
}