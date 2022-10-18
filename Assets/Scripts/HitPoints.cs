using System;
 
using UnityEngine;

public class HitPoints : MonoBehaviour
{
     [Header("DEBUG")]
  public float hp;
    public float maxHp;
    public event Action OnDeath = delegate { };
    public float Percent => hp / maxHp;

    public void SetMaxHp(float maximumHp)
    {
        maxHp = maximumHp;
        hp = maxHp;
    }

    public void Heal(float amount)
    {
        hp += amount;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void HealAll() => hp = maxHp;

    public void Damage(float dmg)
    {
        if (hp <= 0) return;

        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            OnDeath();
        }
    }
}