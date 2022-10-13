public class InstantBullet : Bullet
{
    public override void Fire(Enemy newTarget)
    {
        transform.position = newTarget.transform.position;
        newTarget.Damage(Damage);
        ApplyAttackModifiers(newTarget);
        PlayHitVfx(newTarget.ChestPos);
        ReturnToPool();
    }

    protected override void OnEnemyCollide(Enemy enemy)
    {
    }
}