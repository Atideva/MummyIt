using AudioSystem;
using UnityEngine;

public class BasicEnemyAnimator : EnemyAnimator
{
    public Animator animator;
 
    static readonly int WalkAnim = Animator.StringToHash("walk");
    static readonly int Atk = Animator.StringToHash("attack");
    static readonly int Damage = Animator.StringToHash("damage");

    void Awake()
    {
        if (!animator)
        {
            Debug.LogError("Animator has not assigned", this);
        }
    }

    public override void Move()
    {
        if (!animator) return;
        Walk();
    }

    public override void Death()
    {
        AudioManager.Instance.PlaySound(Enemy.Config.deathSound);
        Events.Instance.PlayVfx(DeathVfx, transform.position);
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySound(Enemy.Config.attackSound, AttackVfxDelay);
        Events.Instance.PlayVfx(AttackVfx, transform.position, AttackVfxDelay);

        if (!animator) return;
        StopWalk();
        animator.SetTrigger(Atk);
    }

    public override void Idle()
    {
        if (!animator) return;
        StopWalk();
    }

    public override void TakeDamage()
    {
        if (!animator) return;
        StopWalk();
        animator.SetTrigger(Damage);
        Invoke(nameof(Walk), 0.2f);
    }

    void Walk()
    {
        if (IsMove)
            animator.SetBool(WalkAnim, true);
    }

    void StopWalk()
    {
        animator.SetBool(WalkAnim, false);
    }
}