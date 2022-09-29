using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSlot : MonoBehaviour
{
    public Image icon;
    public Button button;
    public  PerkConfig perk;
    public  AbilityConfig ability;

    public void Set(AbilityConfig slotAbility)
    {
        ability= slotAbility;
        icon.sprite = slotAbility.Icon;
        perk = null;
    }

    public void Set(PerkConfig slotPerk)
    {
        perk = slotPerk;
        icon.sprite = slotPerk.Icon;
        ability = null;
    }

    void Awake()
    {
        button.onClick.AddListener(Click);
    }

    public event Action<ChooseSlot> OnClick = delegate { };

    void Click()
    {
        OnClick(this);
    }
}