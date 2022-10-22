using System.Collections.Generic;
using System.Linq;
using AudioSystem;
using DG.Tweening;
using Items;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public DrawGemsSpawner drawGemSpawner;
    public ItemTransporter transporter;
    public AudioData ammoGetSound;
    public ItemSlotSpawner spawner;
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
    public bool AnyMatch() => GetSlot(drawer.drawPatterns, spawner.VisibleItems);
    
    void Awake()
    {
        enable = true;
        spawner.OnSlotClick += OnSlotClick;
        drawer.OnRelease += SearchDrawMatch;
        Events.Instance.OnAddAmmoPickup += OnAddAmmoPickup;
        transporter.OnMoveToLineEnd += OnSlotMoveEnd;
    }

    void OnSlotMoveEnd(ItemSlot slot)
    {
        slot.Empty();
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



   void SearchDrawMatch(List<Pattern> drawPattern)
    {
        var getSlot = GetSlot(drawPattern, spawner.VisibleItems);
        if (!getSlot)
        {
            var collectorSlot
                = GetSlot(drawPattern, collector.Items);
            if (collectorSlot)
                collector.UseItem(collectorSlot);

            drawGemSpawner.Release();
            return;
        }

        Pickup(getSlot);
        movingSlots.Add(getSlot);
        drawGemSpawner.Collect();

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


    public void Pickup(ItemSlot slot)
    {
        Debug.Log("Item pickup", slot);
        slot.EnableHighlight();
        
        var pos = slot.item is ItemAmmo
            ? ammoMoveTo.position
            : powerupsMoveTo.position;
        
        slot.container.DOScale(collectSize, moveTime);
        slot.container
            .DOMove(pos, moveTime)
            .OnComplete(()
                => UseSlot(slot));
        
        switch (slot.item)
        {
            case ItemAmmo:
                AudioManager.Instance.PlaySound(ammoGetSound);
                break;
            case ItemPowerUp powerUp:
                AudioManager.Instance.PlaySound(powerUp.Config.Sound);
                break;
        }
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
        slot.DisableHighlight();
       // slot.ReturnToPool();
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