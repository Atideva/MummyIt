using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateArmor : Perk
{
    public float onePlateDurability;
    
    void Refresh()
    {
        Events.Instance.AddPlateArmor(onePlateDurability);
 
    }
}
