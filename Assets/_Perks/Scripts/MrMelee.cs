using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMelee : Perk
{
    public PlayerMeleeData baseStats;
    public PlayerMeleeData incPerLvl;

    void Refresh()
    {
        Events.Instance.PlayerMeleeWeapon(baseStats);
    }
}

[System.Serializable]
public class PlayerMeleeData
{
    public float damage;
    public float cooldown;
}