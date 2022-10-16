 
public class Reincarnate : EnemyAbility
{
    public int resurrects = 1;
    int _remain;
    public float delay = 1f;

    protected override void OnInit()
    {
        Owner.hp.OnDeath += OnDeath;
    }

    public override void Reset()
    {
        _remain = resurrects;
    }

    void OnDeath()
    {
        if (resurrects > 0)
            Invoke(nameof(Resurrect), delay);
    }

    void Resurrect()
    {
        resurrects--;
        Owner.hp.HealAll();
        Owner.Move();
        EnemySpawner.Instance.AddToList(Owner);
    }
    
}