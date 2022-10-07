using System.Collections.Generic;
using Pools;
using UnityEngine;

public class Enemy : PoolObject
{
    public float speed = 1;
    public EnemyConfig _enemy;
    public SpriteRenderer enemyIcon;
    public TestPatternView patternView;
    public HitPoints hp;
    public int baseHp;

    [Header("ABILITIES")]
    public List<EnemyAbility> abilities = new();

    public EnemyConfig Config => _enemy;
    public bool IsMeleeAttack => _isMeleeAttack;
    public bool IsMove => _isMove;
    public bool LastTakenDmgIsMelee { get; private set; }
    public bool IsFreeze { get; private set; }

    bool _isMeleeAttack;
    float _meleeTimer;
    bool _isMove;
    // ReSharper disable once InconsistentNaming
    float _bonusMOVEspd;
    // ReSharper disable once InconsistentNaming
    float _bonusATKspd;

    void Awake()
    {
        hp.OnDeath += OnDeath;
        foreach (var ability in abilities)
            ability.Init(this);
    }

    public void SetConfig(EnemyConfig enemy)
    {
        _enemy = enemy;
        speed = enemy.speed;
        enemyIcon.sprite = enemy.enemyIcon;
        //patternIcon.sprite = enemy.patternIcon;
        //     patternView.Set(enemy.patterns);
        hp.SetMaxHp(baseHp);
        foreach (var ability in abilities)
            ability.Reset();
        _bonusATKspd = 0;
        _bonusMOVEspd = 0;
    }

    public void Freeze(bool freeze)
        => IsFreeze = freeze;

    public void Move() 
        => _isMove = true;

    public void StopMove() 
        => _isMove = false;

    public void StartMeleeAttack()
    {
        _isMeleeAttack = true;
        _meleeTimer = 0;
    }

    public void StopMeleeAttack() 
        => _isMeleeAttack = false;

    void MeleeAttack() 
        => Events.Instance.EnemyAttack(this, _enemy.damage);


    void OnDeath()
    {
        Freeze(false);
        StopMove();
        StopMeleeAttack();
        Events.Instance.EnemyDeath(this);
        // gameObject.SetActive(false);
    }


    public void DamageByMelee(float dmg)
    {
        LastTakenDmgIsMelee = true;
        hp.Damage(dmg);
    }

    public void Damage(float dmg)
    {
        LastTakenDmgIsMelee = false;
        hp.Damage(dmg);
    }


    void Update()
    {
        if (_isMove && !IsFreeze)
        {
            transform.position += Vector3.down * (Time.deltaTime * speed * (1 + _bonusMOVEspd));
        }

        if (_isMeleeAttack)
        {
            _meleeTimer -= Time.deltaTime;
            if (_meleeTimer <= 0)
            {
                _meleeTimer = 1 / (_enemy.attackSpeed * (1 + _bonusATKspd));
                MeleeAttack();
            }
        }
    }


    public void AddBonusMoveSpeed(float mult) => _bonusMOVEspd += mult;
    public void RemoveBonusMoveSpeed(float mult) => _bonusMOVEspd -= mult;
    public void AddBonusAtkSpeed(float mult) => _bonusATKspd += mult;
    public void RemoveBonusAtkSpeed(float mult) => _bonusATKspd -= mult;
}