using System.Collections.Generic;
using Perks;
using UnityEngine;

public class OwnedPerks : MonoBehaviour
{
    public OwnedPerkUI slotPrefab;
    public RectTransform container;
    public List<PerkConfig> perks = new();
    public List<OwnedPerkUI> slots = new();

    public void PerkSelect(PerkConfig perk, int lvl)
    {
        OwnedPerkUI slot;
        if (perks.Contains(perk))
        {
            slot = slots[perks.IndexOf(perk)];
        }
        else
        {
            slot = Instantiate(slotPrefab, container);
            slot.Set(perk);
            slots.Add(slot);
            perks.Add(perk);
#if UNITY_EDITOR
            slot.gameObject.name = "OwnedPerk: " + perk.name;
#endif
        }

        slot.SetLevel(lvl);
    }
}