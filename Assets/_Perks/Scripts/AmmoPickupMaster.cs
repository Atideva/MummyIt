using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupMaster : Perk
{
    public int bonusItemPerLvl;
  
    public override void Activate()
    {
        Events.Instance.AddAmmoPickup(bonusItemPerLvl);
    }
}
