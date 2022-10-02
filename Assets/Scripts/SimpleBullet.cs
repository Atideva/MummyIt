using UnityEngine;

public class SimpleBullet : Bullet
{
    public bool useLifeTime;
    public float lifeTime = 10f;
    float _timer;
    bool _isCollide;
    Transform _t;

    void Awake()
        => _t = transform;

    public override void Fire(Enemy newTarget)
    {
        _isCollide = false;
        _timer = lifeTime;
        
        var pos = (Vector2) _t.position;
        var targetPos = (Vector2) newTarget.transform.position;
        var dir = targetPos - pos;
        dir.Normalize();
        transform.up = dir;
    }

    protected override void OnEnemyCollide(Enemy enemy)
    {
        if (_isCollide) return;
        _isCollide = true;
        
        Debug.Log("Bullet collide enemy: " + enemy.name, enemy);
        enemy.Damage(Damage);
        ReturnToPool();
    }

    void Update()
    {
        _t.position += transform.up * (Speed * Time.deltaTime);

        if (!useLifeTime) return;
        _timer -= Time.deltaTime;
        if (_timer <= 0) ReturnToPool();;
    }
}