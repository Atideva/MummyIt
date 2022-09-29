using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public ItemSpawner spawner;
    public Transform moveTo;
    public float moveTime = 0.5f;
    public PatternDrawer drawer;
    public int pickupAtOnce = 1;

    void Awake()
    {
        //     creator.OnSlotClick += CollectSlot;
        drawer.OnRelease += MatchSearch;
    }

    List<ItemSlot> collected = new();

    void MatchSearch(List<Pattern> drawPattern)
    {
        collected = new List<ItemSlot>();
        for (int i = 0; i < pickupAtOnce; i++)
        {
            var slot = GetSlot(drawPattern);
            if (slot)
            {
                Collect(slot);
                collected.Add(slot);
                //           StartCoroutine(Shoot(slot));
                //    enemiesSpawner.EnemyAttacked(slot);
            }
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
        slot.Use();
    }


    ItemSlot GetSlot(IReadOnlyList<Pattern> patterns)
    {
        foreach (var slot in spawner.lineItems)
        {
            //if (enemiesSpawner.IsAttacked(slot)) continue;
            if (collected.Contains(slot)) continue;
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
}