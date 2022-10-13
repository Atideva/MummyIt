using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedAura : EnemyAbility
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
        enemy.AddBonusMoveSpeed(bonusMult);
    }

    public void OnTriggerExit(Collider col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (!enemy) return;
        if (!inAuraRange.Contains(enemy)) return;
        inAuraRange.Remove(enemy);
        enemy.RemoveBonusMoveSpeed(bonusMult);
    }
}