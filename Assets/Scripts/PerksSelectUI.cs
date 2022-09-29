using System;
using System.Collections.Generic;
using UnityEngine;

public class PerksSelectUI : MonoBehaviour
{
    public List<ChooseSlot> slots = new();
    public CanvasGroup group;
    public Canvas canvas;
    public event Action<PerkConfig> OnPerkSelected = delegate { };
    public event Action<AbilityConfig> OnAbilitySelected = delegate { };

    void Awake()
    {
        Hide();
        foreach (var s in slots)
            s.OnClick += OnSlotClick;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

    void OnSlotClick(ChooseSlot slot)
    {
        if (slot.perk) OnPerkSelected(slot.perk);
        if (slot.ability) OnAbilitySelected(slot.ability);
        Hide();
    }

    public void Show(List<PerkConfig> perks)
    {
        canvas.enabled = true;
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < perks.Count)
                slots[i].Set(perks[i]);
        }
    }
}