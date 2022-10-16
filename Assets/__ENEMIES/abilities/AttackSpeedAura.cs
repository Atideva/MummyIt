using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedAura : EnemyAbility
{
    public float bonusMult;
    [Header("DEBUG")]
    public List<Enemy> inAuraRange = new();

    protected override void OnInit()
    {
    }

    public override void Reset()
    {
        inAuraRange = new List<Enemy>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (!enemy) return;
        if (inAuraRange.Contains(enemy)) return;
        inAuraRange.Add(enemy);
        enemy.AddBonusAtkSpeed(bonusMult);
    }

    public void OnTriggerExit(Collider col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (!enemy) return;
        if (!inAuraRange.Contains(enemy)) return;
        inAuraRange.Remove(enemy);
        enemy.RemoveBonusAtkSpeed(bonusMult);
    }
}