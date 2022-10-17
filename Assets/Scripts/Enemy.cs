using System.Collections.Generic;
using Pools;
using UnityEngine;

public class Enemy : PoolObject
{
    [Header("Setup")]
    public EnemyAnimator animator;
    public Transform chest;
    public HitPoints hp;
    public Grayscale grayScale;
    [Header("ABILITIES")]
    public List<EnemyAbility> abilities = new();
    [Header("DEBUG")]
    public EnemyConfig config;
    float _speed;
    // ReSharper disable once InconsistentNaming
    float _bonusMOVEspd;
    // ReSharper disable once InconsistentNaming
    float _bonusATKspd;
    float _immunePosY;
    bool _isMeleeAttack;
    float _meleeTimer;
    bool _isMove;

    //    public TestPatternView patternView;
    public EnemyConfig Config => config;
    public Vector3 ChestPos => chest.position;
    public bool IsMeleeAttack => _isMeleeAttack;
    public bool IsMove => _isMove;
    public bool LastTakenDmgIsMelee { get; private set; }
    public bool IsFreeze { get; private set; }
    public bool Immune { get; private set; }


    void Awake()
    {
        animator.Init(this);
        hp.OnDeath += OnDeath;
        foreach (var ability in abilities)
            ability.Init(this);
    }

    public void SetImmune(float y)
    {
        Immune = true;
        _immunePosY = y;
        grayScale.Enable();
    }

    public void SetConfig(EnemyConfig enemy)
    {
        config = enemy;
        if (enemy.deathVfx) animator.SetDeathVFX(enemy.deathVfx);
        if (enemy.attackVfx) animator.SetAttackVFX(enemy.attackVfx, enemy.attackVfxDelay);
        _speed = enemy.moveSpeed;
        //patternIcon.sprite = enemy.patternIcon;
        //     patternView.Set(enemy.patterns);
        hp.SetMaxHp(enemy.hitpoints);
        foreach (var ability in abilities)
            ability.Reset();
        _bonusATKspd = 0;
        _bonusMOVEspd = 0;
    }

    public void Freeze(bool freeze)
    {
        if (Immune) return;

        IsFreeze = freeze;
    }

    public void Move()
    {
        _isMove = true;
        animator.Move();
    }

    public void StopMove()
    {
        _isMove = false;
        animator.Idle();
    }

    public void StartMeleeAttack()
    {
        _isMeleeAttack = true;
        _meleeTimer = 0;
    }

    public void StopMeleeAttack()
        => _isMeleeAttack = false;

    void MeleeAttack()
    {
        animator.Attack();
        Invoke(nameof(DoDamageToPlayer), config.attackVfxDelay);
    }

    void DoDamageToPlayer()
        => Events.Instance.EnemyAttack(this, config.damage);

    void OnDeath()
    {
        animator.Death();
        Freeze(false);
        StopMove();
        StopMeleeAttack();
        Events.Instance.EnemyDeath(this);
        // gameObject.SetActive(false);
    }


    public void DamageByMelee(float dmg)
    {
        if (Immune) return;

        LastTakenDmgIsMelee = true;
        hp.Damage(dmg);
    }

    public void Damage(float dmg)
    {
        if (Immune) return;

        LastTakenDmgIsMelee = false;
        hp.Damage(dmg);
    }


    void Update()
    {
        if (Immune && transform.position.y <= _immunePosY)
        {
            Immune = false;
            grayScale.Disable();
        }

        if (_isMove && !IsFreeze)
        {
            transform.position += Vector3.down * (Time.deltaTime * _speed * (1 + _bonusMOVEspd));
        }

        if (_isMeleeAttack)
        {
            _meleeTimer -= Time.deltaTime;
            if (_meleeTimer <= 0)
            {
                _meleeTimer = 1 / (config.attackSpeed * (1 + _bonusATKspd));
                MeleeAttack();
            }
        }
    }

    public void AddBonusMoveSpeed(float mult) => _bonusMOVEspd += mult;
    public void RemoveBonusMoveSpeed(float mult) => _bonusMOVEspd -= mult;
    public void AddBonusAtkSpeed(float mult) => _bonusATKspd += mult;
    public void RemoveBonusAtkSpeed(float mult) => _bonusATKspd -= mult;
}