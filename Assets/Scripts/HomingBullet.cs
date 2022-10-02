using UnityEngine;

public class HomingBullet : Bullet
{
    [Header("DEBUG")]
    public Enemy target;

    public override void Fire(Enemy newTarget)
    {
        target = newTarget;
    }

    protected override void OnEnemyCollide(Enemy enemy)
    {
        Debug.Log("ENEMY COLIDED: "+enemy.name, enemy);
    }

    void Update()
    {
        if (!target) return;

        var pos = (Vector2) transform.position;
        var targetPos = (Vector2) target.transform.position;
        var dir = targetPos - pos;
        dir.Normalize();
        transform.up = dir;
        transform.position += transform.up * (Speed * Time.deltaTime);

        var dist = Vector2.Distance(pos, targetPos);
        if (dist < 0.5f)
        {
            target.Damage(Damage);
            ReturnToPool();
        }
    }
}