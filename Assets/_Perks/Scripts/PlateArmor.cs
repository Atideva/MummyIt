using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateArmor : Perk
{
    public float onePlateDurability;
    
    

    public override void Activate()
    {
        Events.Instance.AddPlateArmor(onePlateDurability);
    }
}
