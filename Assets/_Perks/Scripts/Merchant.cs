using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : Perk
{
    public float sellMult;
    public float incPerLvlMult;

    public void Refresh()
    {
        Events.Instance.Merchant(sellMult);
    }
}
