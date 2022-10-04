using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Items;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public ItemSpawner spawner;
    public Transform moveTo;
    public float moveTime = 0.5f;
    public PatternDrawer drawer;
    public int pickupAtOnce = 1;
    [Header("DEBUG")]
    //   List<ItemSlot> _collectAtOnce = new();
    public List<ItemSlot> movingSlots = new();

    void Awake()
    {
        // creator.OnSlotClick += CollectSlot;
        drawer.OnRelease += MatchSearch;
        Events.Instance.OnAddAmmoPickup += OnAddAmmoPickup;
    }

    void OnAddAmmoPickup(int amount)
    {
        pickupAtOnce += amount;
    }


    void MatchSearch(List<Pattern> drawPattern)
    {
        //  _collectAtOnce = new List<ItemSlot>();
        for (var i = 0; i < pickupAtOnce; i++)
        {
            var slot = GetSlot(drawPattern);
            if (!slot) continue;
            Collect(slot);
            movingSlots.Add(slot);
            //  _collectAtOnce.Add(slot);
            //  StartCoroutine(Shoot(slot));
            //  enemiesSpawner.EnemyAttacked(slot);
        }
    }

    public bool AnyAmmo => GetAmmoSlot();

    public void CollectAmmo()
    {
        for (var i = 0; i < pickupAtOnce; i++)
        {
            var slot = GetAmmoSlot();
            if (!slot) continue;
            Collect(slot);
            movingSlots.Add(slot);
        }
    }

    void Collect(ItemSlot slot)
    {
        Debug.Log("Slot collect", slot);
        slot.container
            .DOMove(moveTo.position, moveTime)
            .OnComplete(()
                => UseSlot(slot));
    }

    void UseSlot(ItemSlot slot)
    {
        movingSlots.Remove(slot);
        slot.Use();
    }


    ItemSlot GetSlot(IReadOnlyList<Pattern> patterns)
    {
        foreach (var slot in spawner.lineItems)
        {
            // if (_collectAtOnce.Contains(slot)) continue;
            if (movingSlots.Contains(slot)) continue;
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
        foreach (var slot in spawner.lineItems)
        {
            if (movingSlots.Contains(slot)) continue;
            if (slot.item is ItemAmmo)
                return slot;
        }

        return null;
    }
}