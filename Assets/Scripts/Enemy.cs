using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1;
    public EnemyConfig _enemy;
    //   public SpriteRenderer patternIcon;
    public SpriteRenderer enemyIcon;
    public TestPatternView patternView;
    public HitPoints hp;
    public int baseHp;
    public EnemyConfig Config => _enemy;

    public bool IsMeleeAttack => _isMeleeAttack;

    public bool IsMove => _isMove;

    bool _isMeleeAttack;
    float _meleeTimer;
    bool _isMove;

    public void Move()
    {
        _isMove = true;
    }

    public void StopMove()
    {
        _isMove = false;
    }

    public void StartMeleeAttack()
    {
        _isMeleeAttack = true;
        _meleeTimer = 0;
    }

    public void StopMeleeAttack()
    {
        _isMeleeAttack = false;
    }


    void MeleeAttack()
    {
        Events.Instance.EnemyAttack(this, _enemy.damage);
    }


    void Awake()
    {
        hp.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        StopMove();
        StopMeleeAttack();
        Events.Instance.EnemyDeath(this);
        gameObject.SetActive(false);
    }

    public void Init(EnemyConfig enemy)
    {
        _enemy = enemy;
        speed = enemy.speed;
        enemyIcon.sprite = enemy.enemyIcon;
        //   patternIcon.sprite = enemy.patternIcon;
        patternView.Set(enemy.patterns);
        hp.Init(baseHp);
    }

    public void Damage(int amount)
    {
        hp.Damage(amount);
    }

    void Update()
    {
        if (_isMove)
            transform.position += Vector3.down * (Time.deltaTime * speed);

        if (_isMeleeAttack)
        {
            _meleeTimer -= Time.deltaTime;
            if (_meleeTimer <= 0)
            {
                _meleeTimer = 1 / _enemy.attackSpeed;
                MeleeAttack();
            }
        }
    }
}