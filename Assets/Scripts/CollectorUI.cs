using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorUI : MonoBehaviour
{
    public List<ItemSlot> slots = new();
 
    void Awake()
    {
        foreach (var slot in slots)
        {
            slot.Empty();
        }
    }
}
