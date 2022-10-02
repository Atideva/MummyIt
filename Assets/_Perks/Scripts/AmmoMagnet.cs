using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMagnet : Perk
{
    public int bonusItemPerLvl;
   
    void Refresh()
    {
        Events.Instance.AddAmmoPickup(bonusItemPerLvl);
    }
}
