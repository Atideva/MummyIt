using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponCollider : MonoBehaviour
{
    public BoxCollider2D col2D;
    [Header("DEBUG")]
    public float damage;
    public List<Enemy> enemies = new();

    void Awake()
        => col2D.enabled = false;

    public void Enable(float dmg,float duration)
    {
        damage = dmg;
        enemies = new List<Enemy>();
        col2D.enabled = true;
        Invoke(nameof(Disable),duration);
    }

    public void Disable()
        => col2D.enabled = false;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (!enemy) return;
        if (enemies.Contains(enemy)) return;
        enemy.DamageByMelee(damage);
        enemies.Add(enemy);
    }
}