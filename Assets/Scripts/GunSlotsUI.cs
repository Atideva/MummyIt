using System;
using System.Collections.Generic;
using UnityEngine;

public class GunSlotsUI : MonoBehaviour
{
    public List<GunSlot> slots = new();
    GunSlot _currentSlot;
    public event Action<GunSlot> OnSlotChange = delegate { };

    void Start()
    {
        foreach (var slot in slots)
        {
            slot.OnClick += OnSlotClick;
        }
    }

    void OnSlotClick(GunSlot slot)
    {
        if (_currentSlot && _currentSlot != slot)
        {
            _currentSlot = slot;
            OnSlotChange(slot);
        }
    }
}