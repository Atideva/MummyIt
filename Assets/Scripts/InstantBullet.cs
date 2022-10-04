using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantBullet : Bullet
{
    public override void Fire(Enemy newTarget)
    {
        transform.position = newTarget.transform.position;
        newTarget.Damage(Damage);
        ReturnToPool();
    }

    protected override void OnEnemyCollide(Enemy enemy)
    {
   
    }
}
