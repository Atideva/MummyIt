using System;
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
    [SerializeField] VFX hitVfxPrefab;
    Sprite _defaultSprite;

    void Awake()
    {
        _defaultSprite = bulletSprite.sprite;
    }

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

    protected void PlayHitVfx(Vector3 pos)
        => Events.Instance.PlayVfx(hitVfxPrefab, pos);

    protected void ApplyAttackModifiers(Enemy target)
        => Events.Instance.ApplyAttackModifier(target, AttackModifiers);

    public abstract void Fire(Enemy newTarget);
    public void SetSprite(Sprite sprite) => bulletSprite.sprite = sprite;


    public void SetOverload(bool isOverload, float size = 1f, Sprite overloadSprite = null)
    {
        if (isOverload)
        {
            SetSprite(overloadSprite);
            transform.localScale = new Vector3(size, size, size);
        }
        else
        {
            SetSprite(_defaultSprite);
            transform.localScale = Vector3.one;
        }
    }

    public void SetDamage(float dmg, float plasma, float bonus)
    {
        damage = dmg;
        plasmaMult = plasma;
        bonusMult = bonus;
    }

    public void SetSpeed(float spd, float bonusSpeed)
    {
        speed = spd;
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