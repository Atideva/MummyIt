using System.Collections.Generic;
using AttackModificators;
using Pools;
using UnityEngine;

public abstract class Bullet : PoolObject
{
    [SerializeField] float damage = 10;
    [SerializeField] SpriteRenderer bulletSprite;
    [SerializeField] float speed = 10;
    [SerializeField] List<AttackModificatorConfig> attackModifiers = new();

    public void SetModifier(List<AttackModificatorConfig> mods)
    {
        foreach (var mod in mods)
            attackModifiers.Add(mod);
    }

    public float plasmaMult = 0;
    public float bonusMult = 0;
    public float speedMult = 0;
    protected float Damage => damage * (1 + plasmaMult + bonusMult);
    protected float Speed => speed * (1 + speedMult);

    protected IReadOnlyList<AttackModificatorConfig> AttackModifiers => attackModifiers;

    public abstract void Fire(Enemy newTarget);
    public void SetSprite(Sprite sprite) => bulletSprite.sprite = sprite;

    public void SetDamageMult(float plasma, float bonus)
    {
        plasmaMult = plasma;
        bonusMult = bonus;
    }

    public void SetSpeedMult(float bonusSpeed)
    {
        speedMult = bonusSpeed;
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(Tags.ENEMY)) return;
        var enemy = EnemySpawner.Instance.TryFindEnemy(col.transform);
        if (enemy)
            OnEnemyCollide(enemy);
    }

    protected abstract void OnEnemyCollide(Enemy enemy);
}