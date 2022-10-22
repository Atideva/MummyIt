using System;
using System.Collections.Generic;
using System.Linq;
using Pools;
using UnityEngine;

public class ItemSlotSpawner : MonoBehaviour
{
    [Header("Setup")]
    public int totalSlots;
    public ItemTransporter transporter;

    [Header("Pool")]
    public ItemSlot slotPrefab;
    public ItemSlotPool slotPool;

    [Header("DEBUG")]
    [SerializeField] List<ItemSlot> subscribe = new();
    [SerializeField] List<ItemSlot> slots = new();

    public ItemSlot GetFreeSlot()
    {
        return slots.FirstOrDefault(item 
            => transporter.OutRightSideScreen(item) &&
               item.IsEmpty);
    }

    public IReadOnlyList<ItemSlot> Slots => slots;

    public List<ItemSlot> VisibleItems
        => slots.Where(item
            => transporter.VisibleAtScreen(item)).ToList();

    public event Action<ItemSlot> OnCreate = delegate { };
    public event Action<ItemSlot> OnSlotClick = delegate { };

    void Awake()
    {
        slotPool.SetPrefab(slotPrefab);
        for (var i = 0; i < totalSlots; i++)
        {
            var slot = slotPool.Get();
            slot.Empty();
            slot.DisableHighlight();
            
            slots.Add(slot);
            if (!subscribe.Contains(slot))
            {
                subscribe.Add(slot);
                  slot.OnUse += OnUse;
                // slot.OnDestroy += Remove;
                // slot.OnSell += OnSell;
                slot.OnClick += OnClick;
            }

            // OnCreate(slot);
        }

        transporter.SetBaseSlotsPos(slots);
        transporter.OnMoveToLineEnd += OnSlotMove;
    }

    void OnUse(ItemSlot slot)
    {
         slot.Empty();
    }

    void OnSlotMove(ItemSlot slot)
    {
        slots.Remove(slot);
        slots.Add(slot);
    }

    void OnClick(ItemSlot slot)
        => OnSlotClick(slot);
}