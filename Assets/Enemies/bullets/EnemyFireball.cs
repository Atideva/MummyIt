using UnityEngine;

public class EnemyFireball : EnemyBullet
{
    void Update()
    {
        transform.position += Vector3.down * (speed * Time.deltaTime);
        if (transform.position.y <= damageTriggerPos)
        {
            Damage();
            ReturnToPool();
        }
    }

    void Damage()
    {
        Events.Instance.EnemyAttack(owner,damage);
    }
}