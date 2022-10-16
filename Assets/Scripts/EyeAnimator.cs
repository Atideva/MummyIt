using System.Collections.Generic;
using AudioSystem;
using DG.Tweening;
using UnityEngine;

public class EyeAnimator : EnemyAnimator
{
    public List<DOTweenAnimation> moveAnims = new();
    public List<DOTweenAnimation> attackAnims = new();

    public override void Move()
    {
        foreach (var anim in moveAnims)
        {
            anim.DORestart();
            anim.DOPlay();
        }

        foreach (var anim in attackAnims)
        {
            anim.DOPause();
        }
    }

    public override void Death()
    {
        AudioManager.Instance.PlaySound(Enemy.Config.deathSound);
        Events.Instance.PlayVfx(DeathVfx, transform.position);
    }

    public override void Attack()
    {
        AudioManager.Instance.PlaySound(Enemy.Config.attackSound, attackVfxDelay);
        Events.Instance.PlayVfx(AttackVfx, transform.position, 0.2f);

        foreach (var anim in moveAnims)
        {
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
}