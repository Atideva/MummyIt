using AttackModificators;
using UnityEngine;

public abstract class EnemyAnimator : MonoBehaviour
{
    protected VFX DeathVfx;
    protected VFX AttackVfx;
    public float attackVfxDelay;
    public void SetDeathVFX(VFX vfx) => DeathVfx = vfx;

    public void SetAttackVFX(VFX vfx, float delay)
    {
        attackVfxDelay = delay;
        AttackVfx = vfx;
    }


    protected bool IsMove => Enemy.IsMove;

    protected Enemy Enemy { get; private set; }

    public void Init(Enemy enemy) => Enemy = enemy;

    public abstract void Move();
    public abstract void Death();
    public abstract void Attack();
    public abstract void Idle();
    public abstract void TakeDamage();
}