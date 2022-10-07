using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMagnet : Perk
{
    public float cooldown;
 
    public override void Activate()
    {
        Events.Instance.AmmoMagnet(cooldown);
    }
}
