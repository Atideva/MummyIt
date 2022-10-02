using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponTrigger : MonoBehaviour
{
    public BoxCollider2D triggerCollider;
    [Header("DEBUG")]
    public List<Enemy> invaders = new();
    float _timer;
    MeleeWeapon _melee;
    public bool Searching { get; private set; }
    
    public void Init(MeleeWeapon melee)
        => _melee = melee;
    
    void Awake()
        => triggerCollider.enabled = false;

    public void SearchForTargets()
    {
        Searching = true;
        invaders = new List<Enemy>();
        triggerCollider.enabled = true;
        Invoke(nameof(Response), 0.05f);
    }

    void Response()
    {
        Searching = false;
        if (invaders.Count > 0)
            _melee.TargetsAcquire();
        else
            _melee.NoTargets();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (!enemy) return;
        if (invaders.Contains(enemy)) return;
        invaders.Add(enemy);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (!enemy) return;
        if (invaders.Contains(enemy)) return;
        invaders.Add(enemy);
    }
}