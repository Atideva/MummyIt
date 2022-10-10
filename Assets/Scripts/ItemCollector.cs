using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public ItemHandler handler;
    public CollectorUI ui;
    public float moveTime = 0.5f;
    public bool AnyItem => ui.slots.Any(s => s.HasItem);
    public bool AnyEmptySlot => ui.slots.Any(s => s.Enabled && s.IsEmpty);
    ItemSlot EmptySlot => ui.slots.FirstOrDefault(s
        => s.Enabled && s.IsEmpty && NotMoving(s));
    bool NotMoving(ItemSlot s) => !moving.Contains(s);
    [Header("DEBUG")]
    public List<ItemSlot> moving = new();
    public IReadOnlyList<ItemSlot> Items => ui.slots;
    public void Collect(ItemSlot slot)
    {
        var s = EmptySlot;
        if (s)
        {
            moving.Add(s);
            slot.container.DOScale(handler.collectSize, moveTime);
            slot.container
                .DOMove(s.transform.position, moveTime)
                .OnComplete(()
                    => FinishMove(slot, s));
        }
        else
        {
            handler.FinishMove(slot);
            slot.ReturnToPool();
        }
    }

    public void UseItem(ItemSlot slot)
    {
        moving.Add(slot);
        slot.container
            .DOMove(handler.powerupsMoveTo.position, moveTime)
            .OnComplete(()
                => UseSlot(slot));
    }

    void UseSlot(ItemSlot slot)
    {
        if (moving.Contains(slot))
            moving.Remove(slot);
        slot.Use();
        slot.Empty();
    }

    void FinishMove(ItemSlot fromSlot, ItemSlot putToSlot)
    {
        putToSlot.Set(fromSlot.item);
        moving.Remove(putToSlot);

        handler.FinishMove(fromSlot);
        fromSlot.ReturnToPool();
    }
}