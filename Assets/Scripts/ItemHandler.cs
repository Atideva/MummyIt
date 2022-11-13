using System;
using System.Collections.Generic;
using System.Linq;
using AudioSystem;
using DG.Tweening;
using Items;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public DrawGems drawGem;
    public DrawMatch drawMatch;
    public ItemTransporter transporter;
    public AudioData ammoGetSound;
    public AudioData candyGetSound;
    public ItemSlotSpawner spawner;
    public ItemCollector collector;
    public Transform powerupsMoveTo;
    public Transform ammoMoveTo;
    public float moveTime = 0.5f;
    public Drawer drawer;
    public int pickupAtOnce = 1;
    public LazyPickerHandler lazyPicker;
    public float collectSize = 0.4f;
    [Header("DEBUG")]
    //List<ItemSlot> _collectAtOnce = new();
    public List<ItemSlot> moving = new();
    public bool AnyAmmo => GetAmmoSlot();
    public bool enable;
    public void Disable() => enable = false;


    void Awake()
    {
        enable = true;
        spawner.OnSlotClick += OnSlotClick;
        //  drawer.OnRelease += SearchDrawMatch;
        drawMatch.OnMatchRelease += MatchReleaseDraw;
        transporter.OnMoveOutOfScreen += OnSlotMoveStart;
    }

    void MatchReleaseDraw(List<ItemSlot> matched)
    {
        foreach (var item in matched)
        {
            Pickup(item);
            moving.Add(item);
        }

        drawGem.Collect();
    }

    void Start()
    {
        Events.Instance.OnAddAmmoPickup += OnAddAmmoPickup;
    }

    void OnSlotMoveStart(ItemSlot slot)
    {
        slot.Empty();
    }

    void OnSlotClick(ItemSlot slot)
    {
        if (!enable) return;
        if (moving.Contains(slot)) return;

        if (collector.AnyEmptySlot && slot.item is not ItemAmmo)
        {
            Debug.Log("Slot collect", slot);
            moving.Add(slot);
            collector.Collect(slot);
            return;
        }

        if (lazyPicker.current > 0)
        {
            lazyPicker.SpendOneCharge();
            moving.Add(slot);
            Pickup(slot);
        }
    }

    void OnAddAmmoPickup(int amount)
    {
        pickupAtOnce += amount;
    }


    void SearchDrawMatch(List<Pattern> drawPattern)
    {
        var getSlot = GetItem(drawPattern, spawner.VisibleItems);
        if (!getSlot)
        {
            var collectorSlot
                = GetItem(drawPattern, collector.Items);
            if (collectorSlot)
                collector.UseItem(collectorSlot);

            drawGem.Release();
            return;
        }

        Pickup(getSlot);
        moving.Add(getSlot);
        drawGem.Collect();

        if (getSlot.item is not ItemAmmo || pickupAtOnce < 2)
            return;

        for (var i = 1; i < pickupAtOnce; i++)
        {
            var slot = GetItem(drawPattern, spawner.VisibleItems);
            if (!slot) continue;
            Pickup(slot);
            moving.Add(slot);
        }
    }


    public void CollectAmmo()
    {
        for (var i = 0; i < pickupAtOnce; i++)
        {
            var slot = GetAmmoSlot();
            if (!slot) continue;
            Pickup(slot);
            moving.Add(slot);
        }
    }


    public void Pickup(ItemSlot slot)
    {
        AudioManager.Instance.PlaySound(candyGetSound, drawGem.moveDur);
       
        Debug.Log("Item pickup", slot);
        slot.EnableHighlight();

        var pos = slot.item is ItemAmmo
            ? ammoMoveTo.position
            : powerupsMoveTo.position;

        slot.container
            .DOScale(collectSize, moveTime)
            .OnComplete(()
            => UseSlot(slot));
        
        // slot.container
        //     .DOLocalMove(slot.container.localPosition+Vector3.up*150, moveTime);
        
        /*
        slot.container
            .DOMove(pos, moveTime)
            .OnComplete(()
                => UseSlot(slot));
*/

        switch (slot.item)
        {
            case ItemAmmo:
                AudioManager.Instance.PlaySound(ammoGetSound);
                slot.container
                    .DOMove(pos, moveTime);
                break;
            case ItemPowerUp powerUp:
                AudioManager.Instance.PlaySound(powerUp.Config.PickupSound);
                break;
        }
    }

    public void FinishMove(ItemSlot slot)
    {
        if (moving.Contains(slot))
            moving.Remove(slot);
    }

    void UseSlot(ItemSlot slot)
    {
        FinishMove(slot);
        switch (slot.item)
        {
            case ItemPowerUp powerUp:
                AudioManager.Instance.PlaySound(powerUp.Config.UseSound);
                break;
        }

        slot.Use();
        slot.DisableHighlight();


        // slot.ReturnToPool();
    }


    ItemSlot GetItem(IReadOnlyList<Pattern> patterns, IEnumerable<ItemSlot> items)
    {
        foreach (var slot in items)
        {
            // if (_collectAtOnce.Contains(slot)) continue;
            if (moving.Contains(slot)) continue;
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
            if (moving.Contains(slot)) continue;
            if (slot.item is ItemAmmo)
                return slot;
        }

        return null;
    }
}