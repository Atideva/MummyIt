using System;
using System.Collections.Generic;
using Perks;
using UnityEngine;

public class PerksSelectUI : MonoBehaviour
{
    public List<PerkSlot> slots = new();
    public CanvasGroup group;
    public Canvas canvas;

    public event Action<PerkConfig> OnPerkSelect = delegate { };

    public event Action<GunConfig> OnGunSelect = delegate { };
    //  public event Action<AbilityConfig> OnAbilitySelected = delegate { };

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

    void OnSlotClick(PerkSlot slot)
    {
        if (slot.perk) OnPerkSelect(slot.perk);
        if (slot.gun) OnGunSelect(slot.gun);
        //   if (slot.ability) OnAbilitySelected(slot.ability);
        Hide();
    }

    public void Show(GunConfig gun, List<PerkConfig> perks, List<int> lvl)
    {
        canvas.enabled = true;
        var from = 0;
        if (gun)
        {
            slots[0].Set(gun, lvl[0]);
            from++;
        }

        for (int i = from; i < slots.Count; i++)
        {
            if (i < perks.Count)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Set(perks[i], lvl[i]);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}