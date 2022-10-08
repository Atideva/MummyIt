using System;
using Perks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkSlot : MonoBehaviour
{
    public Button button;
    public Image icon;
    public GameObject levelUp;
    public TextMeshProUGUI curLvl;
    public TextMeshProUGUI nextLvl;
    public TextMeshProUGUI description;
    [Header("DEBUG")]
    public PerkConfig perk;
    public GunConfig gun;
    // public  AbilityConfig ability;

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

    public void Set(PerkConfig perkConfig, int lvl)
    {
        perk = perkConfig;
        gun = null;
        
        icon.sprite = perkConfig.Icon;
        description.text = perkConfig.Description;
     
        levelUp.SetActive(lvl > 1);
        curLvl.text = lvl.ToString();
        nextLvl.text = (lvl + 1).ToString();


        //  ability = null;
    }

    public void Set(GunConfig gunConfig,int lvl)
    {
        gun = gunConfig;
        perk = null;
        
        icon.sprite = gunConfig.Icon;
        description.text = gunConfig.Description;
       
        levelUp.SetActive(false);
    }
}