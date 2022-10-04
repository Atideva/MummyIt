using System.Collections.Generic;
using AudioSystem;
using UnityEngine;

public class MeleeWeaponCollider : MonoBehaviour
{
    public BoxCollider2D col2D;
    [Header("DEBUG")]
    public MeleeWeaponConfig weapon;
    public List<Enemy> enemies = new();
    AudioData _hitSound;

    void Awake()
        => col2D.enabled = false;

    public void Enable(MeleeWeaponConfig wep, float duration)
    {
        weapon = wep;
        enemies = new List<Enemy>();
        col2D.enabled = true;
        Invoke(nameof(Disable), duration);
    }

    public void Disable()
        => col2D.enabled = false;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (!enemy) return;
        if (enemies.Contains(enemy)) return;
        enemy.DamageByMelee(weapon.Damage);
        AudioManager.Instance.PlaySound(weapon.HitSound);
        enemies.Add(enemy);
    }
}