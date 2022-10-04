using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMagnet : Perk
{
    public float cooldown;
   
    void Refresh()
    {
        Events.Instance.AmmoMagnet(cooldown);
    }
}
