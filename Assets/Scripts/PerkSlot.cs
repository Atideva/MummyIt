using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkSlot : MonoBehaviour
{
    public Image icon;
    public Button button;
    public PerkConfig perk;
    // public  AbilityConfig ability;
    public TextMeshProUGUI description;
    public event Action<PerkSlot> OnClick = delegate { };

    void Awake()
        => button.onClick.AddListener(Click);

    void Click()
        => OnClick(this);

    // public void Set(AbilityConfig slotAbility)
    // {
    //     ability= slotAbility;
    //     icon.sprite = slotAbility.Icon;
    //     perk = null;
    // }

    public void Set(PerkConfig perkConfig)
    {
        perk = perkConfig;
        icon.sprite = perkConfig.Icon;
        description.text = perkConfig.Description;
        //  ability = null;
    }
}