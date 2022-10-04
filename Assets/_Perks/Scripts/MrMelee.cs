using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;

public class MrMelee : Perk
{
 
    public List<MeleeWeaponConfig> weapons = new();
 
    
    //public PlayerMeleeData incPerLvl;

    void Refresh()
    {
        Events.Instance.PlayerMeleeWeapon(weapons[0]);
    }
}
 