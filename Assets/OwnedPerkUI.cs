 
using Perks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OwnedPerkUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI lvlTxt;
    [Header("DEBUG")]
    public PerkConfig perk;
    public int lvl;
    public void Set(PerkConfig setPerk)
    {
        perk = setPerk;
        icon.sprite = setPerk.Icon;
    }

    //  public TextMeshProUGUI perkName;
    public void SetIcon(Sprite iconSprite)
    {
        icon.sprite = iconSprite;
    }

    public void SetLevel(int level)
    {
        lvl = level;
        lvlTxt.text = level.ToString();
    }
}