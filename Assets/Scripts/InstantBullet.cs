public class InstantBullet : Bullet
{
    public override void Fire(Enemy newTarget)
    {
        transform.position = newTarget.transform.position;
        newTarget.Damage(Damage);
        if (AttackModifiers.Count > 0)
            Events.Instance.ApplyAttackModifier(newTarget, AttackModifiers);
        ReturnToPool();
    }

    protected override void OnEnemyCollide(Enemy enemy)
    {
    }
}