using UnityEngine;

public class FireBall : EnemyAbility
{
    public float cooldown = 2;
    public float damage = 5;
    public float speed = 10;
    public EnemyBullet bulletPrefab;
    float _timer;
    
    protected override void OnInit()
    {
    }

    public override void Reset()
    {
    }

    void Shoot()
    {
        var bullet = EnemyBulletStorage.Instance.Get(bulletPrefab);
        bullet.damage = damage;
        bullet.speed = speed;
        bullet.transform.position = transform.position;
        bullet.owner = Owner;
    }



    void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
        if (_timer < 0)
        {
            _timer = cooldown;
            Shoot();
        }
    }
}