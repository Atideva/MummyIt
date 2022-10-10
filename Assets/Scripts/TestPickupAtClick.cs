using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickupAtClick : MonoBehaviour
{
    [Header("ENABLE THIS TEST")]
    // ReSharper disable once InconsistentNaming
    public bool ENABLE;
    [Header("Settings")]
    public ItemSpawner spawner;
    public ItemHandler handler;

    void Awake()
    {
        if (ENABLE)
        {
            spawner.OnSlotClick += OnSlotClick;
            Invoke(nameof(DoFastShit), 0.1f);
        }
    }

    void DoFastShit()
    {
        handler.Disable();
    }

    void OnSlotClick(ItemSlot slot)
    {
        handler.movingSlots.Add(slot);
        handler.Pickup(slot);
    }
}