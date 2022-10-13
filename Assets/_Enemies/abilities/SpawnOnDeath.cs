public class SpawnOnDeath : EnemyAbility
{
    public EnemyConfig spawnEnemy;
    public int count;

    protected override void OnInit()
    {
        Owner.hp.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        for (int i = 0; i < count; i++)
        {
            Events.Instance.SpawnEnemy(spawnEnemy, transform.position);
        }
    }

    public override void Reset()
    {
    }
}