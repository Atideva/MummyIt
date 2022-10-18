using System.Collections.Generic;
using AudioSystem;
using DG.Tweening;
using UnityEngine;

public class DoAnimator : EnemyAnimator
{
    public List<DOTweenAnimation> moveAnims = new();
    public List<DOTweenAnimation> attackAnims = new();
    [Header("DEBUG")]
    public bool moveAnim;
    public bool attackAnim;

    public override void Move()
    {
        foreach (var anim in attackAnims)
        {
            anim.DORestart();
            anim.DOPause();
        }
        
        foreach (var anim in moveAnims)
        {
            anim.DORestart();
            anim.DOPlay();
        }

     
    }

    public override void Death()
    {
        if (Enemy.Config)
            AudioManager.Instance.PlaySound(Enemy.Config.deathSound);
        Events.Instance.PlayVfx(DeathVfx, transform.position);
    }

    public override void Attack()
    {
        if (Enemy.Config)
            AudioManager.Instance.PlaySound(Enemy.Config.attackSound, AttackVfxDelay);
        Events.Instance.PlayVfx(AttackVfx, transform.position, 0.2f);

        foreach (var anim in moveAnims)
        {
            anim.DORestart();
            anim.DOPause();
        }

        foreach (var anim in attackAnims)
        {
            anim.DORestart();
            anim.DOPlay();
        }
    }

    public override void Idle()
    {
    }

    public override void TakeDamage()
    {
    }

#if UNITY_EDITOR
    void Update()
    {
        if (moveAnim)
        {
            moveAnim = false;
            Move();
        }

        if (attackAnim)
        {
            attackAnim = false;
            Attack();
        }
    }
#endif
}